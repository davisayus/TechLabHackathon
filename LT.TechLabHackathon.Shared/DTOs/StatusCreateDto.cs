using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class StatusCreateDto
    {
        public StatusCreateDto(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            Description = description;
        }

        public StatusCreateDto()
        {
            Description ??= string.Empty;
        }

        public string Description { get; set; }
    }
}
