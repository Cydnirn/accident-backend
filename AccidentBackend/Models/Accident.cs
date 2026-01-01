using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class Accident
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(100)]
        public string? AccidentNumber { get; set; }
        [Required]
        [ForeignKey("Site")]
        public int SiteId { get; set; }
        [Required]
        public DateTime OccurredAt { get; set; }
        public DateTime ReportedAt { get; set; } = DateTime.Now;
        [ForeignKey("ReportedByWorker")]
        public int? ReportedByWorkerId { get; set; }
        [ForeignKey("Shift")]
        public int? ShiftId { get; set; }
        [ForeignKey("HazardType")]
        public int? HazardTypeId { get; set; }
        [ForeignKey("Cause")]
        public int? CauseId { get; set; }
        public short? SeverityLevel { get; set; }
        public bool IsFatal { get; set; } = false;
        public string? Description { get; set; }
        public string? RootCauseAnalysis { get; set; }
        [MaxLength(50)]
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        // Navigation properties
        public Site Site { get; set; } = null!;
        public Worker? ReportedByWorker { get; set; }
        public Shift? Shift { get; set; }
        public HazardType? HazardType { get; set; }
        public AccidentCause? Cause { get; set; }
        public ICollection<AccidentParticipant> Participants { get; set; } = new List<AccidentParticipant>();
        public ICollection<AccidentEquipment> AccidentEquipments { get; set; } = new List<AccidentEquipment>();
        public ICollection<Witness> Witnesses { get; set; } = new List<Witness>();
        public ICollection<ActionTaken> ActionsTaken { get; set; } = new List<ActionTaken>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
