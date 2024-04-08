using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public sealed class ProgrammingLanguageCreateDto
    {

        public ProgrammingLanguageCreateDto(string description, string title, int version, IEnumerable<string> reservedWords)
        {
            Description = string.IsNullOrEmpty(description) ? throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description)) : description;
            Title = string.IsNullOrEmpty(title) ? throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title)): title;
            ReservedWords = reservedWords is null? throw new ArgumentNullException(nameof(reservedWords)): reservedWords;
            Version = version;
        }

        public ProgrammingLanguageCreateDto()
        {
            Description ??= string.Empty;
            Title ??= string.Empty;
            ReservedWords ??= [];
        }

        public string Description { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }
        public IEnumerable<string> ReservedWords { get; set; }


    }
}
