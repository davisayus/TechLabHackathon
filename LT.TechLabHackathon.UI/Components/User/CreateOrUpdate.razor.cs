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

        private bool isProcessing = false;
        private string taskProcessing = string.Empty;

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
            isProcessing = true;

            if (IsCreate)
            {
                taskProcessing = $"Registering user ... {DateTime.Now.ToLongTimeString()}";
                Console.WriteLine(taskProcessing);

                var response = await _authRepository.Register(userCreate);
                if (response is null || response.HasError)
                {
                    taskProcessing = response!.Message;
                    _notification.Notify(NotificationSeverity.Error, "User Register", response?.Message, 3000);
                }
                else
                {
                    await _loginService.Login(response.Content!.User, response.Content.Token!);
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

            isProcessing = false;
            Console.WriteLine(taskProcessing);
            StateHasChanged();
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
