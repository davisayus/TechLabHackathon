using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class StatusDto
    {
        public StatusDto(int statusId, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            StatusId = statusId;
            Description = description;
        }

        public StatusDto()
        {
            Description ??= string.Empty;
        }

        public int StatusId { get; set; }
        public string Description { get; set; }
    }
}
