using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace LT.TechLabHackathon.UI.Components.Ide
{
    public partial class FiltersChallenge
    {
        [Parameter] public IEnumerable<ProgrammingLanguageDto> ProgrammingLanguages { get; set; }
        [Parameter] public IEnumerable<ChallengeLevelDto> Levels { get; set; }
        [Parameter] public IEnumerable<ChallengeDto> Challenges { get; set; }

        [Parameter] public EventCallback<(int ProgrammingLanguageId, int LevelId, int ChallengeId)> DropDownEvent_ChangeValue { get; set; }

        int _programmingLanguageId = 0;
        int _levelId = 0;
        int _challengeId = 0;

        private async Task DropDown_ChangeValue()
        {
            await DropDownEvent_ChangeValue.InvokeAsync((_programmingLanguageId, _levelId, _challengeId));
        }

    }
}
