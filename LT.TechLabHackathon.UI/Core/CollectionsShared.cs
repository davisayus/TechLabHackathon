using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.DTOs;
using System.Runtime.Serialization;

namespace LT.TechLabHackathon.UI.Core
{
    public class CollectionsShared
    {

        public enum EnCollectionShared
        {
            [EnumMember(Value = "LVL")] Levels,
            [EnumMember(Value = "Token")] Token,
        }

        private readonly string KeyStorage = "TechLab.Hackathon";

        public CollectionsShared()
        {
            UserInfo ??= new UserDto();
            CurrentToken ??= string.Empty;
            ChallengeLevels ??= [];
            SecretKey ??= string.Empty;
        }

        public IEnumerable<ChallengeLevelDto> ChallengeLevels { get; set; }
        public string CurrentToken { get; set; }
        public UserDto UserInfo { get; set; }
        public string SecretKey { get; set; }

        public async Task InitializedAsync(ILocalStorageService localStorage)
        {
            try
            {
                ChallengeLevels ??= await localStorage.GetItemAsync<IEnumerable<ChallengeLevelDto>>($"{KeyStorage}.LVL") ?? [];
                CurrentToken = await localStorage.GetItemAsync<string>($"{KeyStorage}.Token") ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error assigning the collections shared: {ex.Message}");
            }
        }

        public async Task UpdateSharedValuesAsync(ILocalStorageService localStorage)
        {
            try
            {
                await localStorage.SetItemAsync<IEnumerable<ChallengeLevelDto>>($"{KeyStorage}.LVL", ChallengeLevels);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving the collections shared in localstorage: {ex.Message}");
            }
        }

        public async Task UpdateTokenAsync(ILocalStorageService localStorage)
        {
            try
            {
                await localStorage.SetItemAsync<string>($"{KeyStorage}.Token", CurrentToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving the collections shared in localstorage: {ex.Message}");
            }
        }

        public async Task ResetCollectionShared(ILocalStorageService localStorage, EnCollectionShared collection)
        {
            await localStorage.RemoveItemAsync($"{KeyStorage}.{collection}");
        }

    }
}
