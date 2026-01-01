using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class Witness
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Accident")]
        public long AccidentId { get; set; }
        [ForeignKey("Worker")]
        public int? WorkerId { get; set; }
        [MaxLength(200)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Contact { get; set; }
        public string? Statement { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.Now;
        // Navigation properties
        public Accident Accident { get; set; } = null!;
        public Worker? Worker { get; set; }
    }
}
