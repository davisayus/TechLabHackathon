using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeInputSetupParameterDto
    {
        public ChallengeInputSetupParameterDto(int challengeInputSetupParameterId, int challengeId, int sequence, string parameterName, int dataTypeId)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentException($"'{nameof(parameterName)}' cannot be null or empty.", nameof(parameterName));

            ChallengeInputSetupParameterId = challengeInputSetupParameterId;
            ChallengeId = challengeId;
            Sequence = sequence;
            ParameterName = parameterName;
            DataTypeId = dataTypeId;
        }

        public ChallengeInputSetupParameterDto()
        {
            ParameterName ??= string.Empty;
        }

        public int ChallengeInputSetupParameterId { get; set; }
        public int ChallengeId { get; set; }
        public int Sequence { get; set; }
        public string ParameterName { get; set; }
        public int DataTypeId { get; set; }

        public GeneralDataTypeDto DataType { get; set; } = null!;
    }
}
