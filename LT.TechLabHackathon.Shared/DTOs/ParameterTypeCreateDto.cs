using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ParameterTypeCreateDto
    {
        public string Description { get; set; }
        public string DataType { get; set; }

        public ParameterTypeCreateDto()
        {
            Description ??= string.Empty;
            DataType ??= string.Empty;
        }

        public ParameterTypeCreateDto(string description, string dataType)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            if (string.IsNullOrEmpty(dataType))
                throw new ArgumentException($"'{nameof(dataType)}' cannot be null or empty.", nameof(dataType));

            Description = description;
            DataType = dataType;
        }
    }
}
