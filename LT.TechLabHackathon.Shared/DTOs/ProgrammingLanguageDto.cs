using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public sealed class ProgrammingLanguageDto
    {
        public ProgrammingLanguageDto(int programmingLanguageId, string description, string title, int version, IEnumerable<ProgrammingLanguageReservedWordDto> reservedWords)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            if (string.IsNullOrEmpty(title))
                throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));

            Description = description;
            Title = title;
            ProgrammingLanguageId = programmingLanguageId;
            Version = version;
            ReservedWords = reservedWords;
        }

        public ProgrammingLanguageDto()
        {
            Description ??= string.Empty; 
            Title ??= string.Empty;
            ReservedWords ??= [];
            DataTypes ??= [];
        }

        public int ProgrammingLanguageId { get; set; } 
        public string Description { get; set; } 
        public string Title { get; set; } 
        public int Version { get; set; }
        public IEnumerable<ProgrammingLanguageReservedWordDto> ReservedWords { get; set; } = null!;
        public IEnumerable<ProgrammingLanguageDataTypeDto> DataTypes { get; set; } = null!;
    }
}
