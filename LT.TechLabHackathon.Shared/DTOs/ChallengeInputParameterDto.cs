using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeInputParameterDto
    {
        public ChallengeInputParameterDto()
        {
            InputValue ??= string.Empty;
        }

        public ChallengeInputParameterDto(int inputParameterId, int validationId, int sequence, string inputValue)
        {
            if (string.IsNullOrEmpty(inputValue))
                throw new ArgumentException($"'{nameof(inputValue)}' cannot be null or empty.", nameof(inputValue)); // Validación de InputValue

            InputParameterId = inputParameterId;
            ValidationId = validationId;
            Sequence = sequence;
            InputValue = inputValue;
        }

        public int InputParameterId { get; set; }
        public int ValidationId { get; set; }
        public int Sequence { get; set; }
        public string InputValue { get; set; }
    }
}
