using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeCreateDto
    {
        public ChallengeCreateDto(int challengeId, string description, string title, int levelId, string methodName, int inputParameters, int resultDataTypeId)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            if (string.IsNullOrEmpty(title))
                throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));

            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentException($"'{nameof(methodName)}' cannot be null or empty.", nameof(methodName));

            Description = description;
            Title = title;
            LevelId = levelId;
            MethodName = methodName;
            InputParameters = inputParameters;
            ResultDataTypeId = resultDataTypeId;
        }

        public ChallengeCreateDto()
        {
            Description ??= string.Empty;
            Title ??= string.Empty;
            MethodName ??= string.Empty;
        }

        public string Description { get; set; }
        public string Title { get; set; }
        public int LevelId { get; set; }

        public string MethodName { get; set; }
        public int InputParameters { get; set; }
        public int ResultDataTypeId { get; set; }

        public virtual IEnumerable<ChallengeExampleCreateDto> Examples { get; set; } = null!;
        public virtual IEnumerable<ChallengeConstraintCreateDto> Constraints { get; set; } = null!;
        public virtual IEnumerable<ChallengeLanguageSignatureCreateDto> LanguageSignatures { get; set; } = null!;
        public virtual IEnumerable<ChallengeValidationCreateDto> Validations { get; set; } = null!;
    }
}
