using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class ActionTaken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Accident")]
        public long AccidentId { get; set; }
        public DateTime? ActionTime { get; set; }
        [ForeignKey("PerformedByWorker")]
        public int? PerformedByWorkerId { get; set; }
        [MaxLength(150)]
        public string? ActionType { get; set; }
        public string? Notes { get; set; }
        // Navigation properties
        public Accident Accident { get; set; } = null!;
        public Worker? PerformedByWorker { get; set; }
    }
}
