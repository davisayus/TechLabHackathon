using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.Core;

namespace LT.TechLabHackathon.UI.Pages
{
    public partial class IDEChallenge
    {

        private CodeEditorCore editorCore;

        private IEnumerable<ProgrammingLanguageDto> _programmingLanguages;
        private IEnumerable<ChallengeLevelDto> _challengeLevels;
        private IEnumerable<ChallengeDto> _challenges;

        private ChallengeDto _selectedChallenge;
        private ProgrammingLanguageDto _selectedProgrammingLanguage;

        private string _messageResultExecution = string.Empty;

        public IDEChallenge()
        {
            _programmingLanguages = [];
            _challengeLevels = [];
            _challenges = [];
            _selectedChallenge = new ChallengeDto();
            _selectedProgrammingLanguage = new ProgrammingLanguageDto();
        }

        protected override async Task OnInitializedAsync()
        {

            var responseServicePL = await _programingLanguegeRepository.GetAllAsync();
            if (responseServicePL != null && responseServicePL.Content != null)
                _programmingLanguages = responseServicePL.Content;

            var responseServiceCL = await _challengeLevelRepository.GetAllAsync();
            if (responseServiceCL != null && responseServiceCL.Content != null)
                _challengeLevels = responseServiceCL.Content;

            await base.OnInitializedAsync();
        }



        private async Task Filters_DropDown_ChangeValue((int programmingLanguageId, int levelId, int challengeId) filters)
        {
            if (filters.challengeId == 0)
            {
                if (filters.programmingLanguageId != 0 && filters.levelId != 0)
                {
                    var responseService = await _challengeRepository.GetAllAsync();
                    if (responseService != null && responseService.Content != null)
                        _challenges = responseService.Content;
                }
            }
            else
            {
                _selectedChallenge = _challenges.Where(c => c.ChallengeId == filters.challengeId).FirstOrDefault(new ChallengeDto());
                _selectedProgrammingLanguage = _programmingLanguages.Where(p => p.ProgrammingLanguageId == filters.programmingLanguageId).FirstOrDefault(new ProgrammingLanguageDto());
            }
        }

        private void OnExecution(string message)
        {
            _messageResultExecution = message;
        }
    }
}
