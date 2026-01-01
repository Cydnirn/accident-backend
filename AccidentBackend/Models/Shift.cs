using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AccidentBackend.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Description { get; set; }
        // Navigation properties
        public ICollection<Accident> Accidents { get; set; } = new List<Accident>();
    }
}
