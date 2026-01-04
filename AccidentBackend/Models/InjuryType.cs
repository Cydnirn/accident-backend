using System.ComponentModel.DataAnnotations;

namespace AccidentBackend.Models
{
    public class InjuryType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(250)]
        public string? Description { get; set; }
        // Navigation properties
        public ICollection<AccidentParticipant> AccidentParticipants { get; set; } = new List<AccidentParticipant>();
    }
}