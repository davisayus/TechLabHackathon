using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ParameterTypeDto
    {
        public int ParameterTypeId { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }

        public ParameterTypeDto()
        {
            Description ??= string.Empty;
            DataType ??= string.Empty;
        }

        public ParameterTypeDto(int parameterTypeId, string description, string dataType)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            if (string.IsNullOrEmpty(dataType))
                throw new ArgumentException($"'{nameof(dataType)}' cannot be null or empty.", nameof(dataType));

            ParameterTypeId = parameterTypeId;
            Description = description;
            DataType = dataType;
        }
    }

}
