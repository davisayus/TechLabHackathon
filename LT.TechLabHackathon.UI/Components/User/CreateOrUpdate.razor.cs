using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Radzen;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.UI.Components.User
{
    public partial class CreateOrUpdate
    {

        [Parameter] public bool IsCreate { get; set; }

        private UserCreateDto _userCreate;
        private UserDto _currentUser;

        private string _password = string.Empty;
        private string _repeatPassword = string.Empty;

        private string _fileName = string.Empty;
        private long? _fileSize;
        
        public CreateOrUpdate()
        {
            _userCreate = new UserCreateDto();
            _currentUser ??= new UserDto();
        }

        protected override void OnParametersSet()
        {

            if (!IsCreate)
            {
                _currentUser = _collectionsShared.UserInfo;
                _userCreate.Name = _currentUser.Name;
                _userCreate.Email = _currentUser.Email;
                _userCreate.AuthDoublefactor = _currentUser.AuthDoublefactor;
                _userCreate.PhoneNumber = _currentUser.PhoneNumber;
                _userCreate.Picture = _currentUser.Picture;
            }

            base.OnParametersSet();
        }

        private async Task Submit(UserCreateDto userCreate)
        {
            if (IsCreate)
            {
                var response = await _userRepository.Create(userCreate);
                if (response is null || response.HasError)
                    _notification.Notify(NotificationSeverity.Error, "User Register", response?.Message, 3000);
                else
                {
                    var setPasswordResponse = await _authRepository.SetPassword(new LoginRequestDto(userCreate.Email, _password));
                    if (setPasswordResponse is null || setPasswordResponse.HasError)
                        _notification.Notify(NotificationSeverity.Error, "Set Password", response?.Message, 3000);

                    var loginResponse = await _authRepository.Login(new LoginRequestDto(userCreate.Email, _password));
                    if (loginResponse is null || loginResponse.HasError || loginResponse.Content is null)
                        _notification.Notify(NotificationSeverity.Error, "Login", response?.Message, 3000);
                    else
                        await _loginService.Login(loginResponse.Content!.User, loginResponse.Content.Token!);
                    //Goto home 
                    _navigationManager.NavigateTo("/");
                }

            }
            else
            {
                var response = await _userRepository.Update(userCreate, _currentUser.UserId);
                if (response is null || response.HasError)
                    _notification.Notify(NotificationSeverity.Error, "User Register", response?.Message, 3000);
                else
                {
                    _notification.Notify(NotificationSeverity.Success, "Update User", "User updated successfully", 3000);
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        private void Cancel()
        {
            _notification.Notify(NotificationSeverity.Warning, "Cancel Register", "The user has cancelled the process", 3000);
            _navigationManager.NavigateTo("/");
        }

        private void OnSelectFile(string value)
        {

        }

        private void OnErrorSelectFile(UploadErrorEventArgs args)
        {
            _notification.Notify(NotificationSeverity.Warning, "Error file selected", args.Message, 3000);
        }
    }
}
