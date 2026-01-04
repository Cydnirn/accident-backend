using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class AccidentParticipant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Accident")]
        public long AccidentId { get; set; }
        [ForeignKey("Worker")]
        public int? WorkerId { get; set; }
        [MaxLength(100)]
        public string? Role { get; set; }
        public bool Injured { get; set; } = false;
        [ForeignKey("InjuryType")]
        public int? InjuryTypeId { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        // Navigation properties
        public Accident Accident { get; set; } = null!;
        public Worker? Worker { get; set; }
        public InjuryType? InjuryType { get; set; }
    }
}
