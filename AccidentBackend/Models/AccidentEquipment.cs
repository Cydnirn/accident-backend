using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class AccidentEquipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Accident")]
        public long AccidentId { get; set; }
        [ForeignKey("Equipment")]
        public int? EquipmentId { get; set; }
        [MaxLength(200)]
        public string? ConditionAtTime { get; set; }
        public bool? WasOperational { get; set; }
        // Navigation properties
        public Accident Accident { get; set; } = null!;
        public SafetyEquipment? Equipment { get; set; }
    }
}
