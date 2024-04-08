using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeDto
    {
        public ChallengeDto(int challengeId, string description, string title, int levelId, string methodName, int inputParameters, int resultDataTypeId)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            if (string.IsNullOrEmpty(title))
                throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentException($"'{nameof(methodName)}' cannot be null or empty.", nameof(methodName));

            Description = description;
            Title = title;
            ChallengeId = challengeId;
            LevelId = levelId;
            MethodName = methodName;
            InputParameters = inputParameters;
            ResultDataTypeId = resultDataTypeId;
        }

        public ChallengeDto()
        {
            Description ??= string.Empty;
            Title ??= string.Empty;
            MethodName ??= string.Empty;
        }

        public int ChallengeId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int LevelId { get; set; }
        public string MethodName { get; set; }
        public int InputParameters { get; set; }
        public int ResultDataTypeId { get; set; }

        public IEnumerable<ChallengeExampleDto> Examples { get; set; } = null!;
        public IEnumerable<ChallengeConstraintDto> Constraints { get; set; } = null!;
        public IEnumerable<ChallengeLanguageSignatureDto> LanguageSignatures { get; set; } = null!;
        public IEnumerable<ChallengeValidationDto> Validations { get; set; } = null!;
        public IEnumerable<ChallengeInputSetupParameterDto> InputSetupParameters { get; set; } = null!;

        public GeneralDataTypeDto DataType { get; set; } = null!;
    }
}
