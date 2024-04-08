using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class Status
    {
        public Status()
        {
            Description ??= string.Empty;
        }

        [Key]
        public int StatusId { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<User> Users { get; set; } = null!;
    }
}
