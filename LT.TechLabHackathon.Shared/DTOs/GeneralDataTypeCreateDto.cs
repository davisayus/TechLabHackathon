using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class GeneralDataTypeCreateDto
    {
        public GeneralDataTypeCreateDto(string name, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(description));

            Name = name;
            Description = description;
        }

        public GeneralDataTypeCreateDto()
        {
            Name ??= string.Empty;
            Description ??= string.Empty;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
