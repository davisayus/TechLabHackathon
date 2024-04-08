using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ChallengeLanguageSignature
    {
        public ChallengeLanguageSignature()
        {
            Signature ??= string.Empty;    
        }

        [Key]
        public int LanguageSignatureId { get; set; }
        public int ChallengeId { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Signature { get; set; }

        public virtual Challenge Challenge { get; set; } = null!;
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; } = null!;
    }
}
