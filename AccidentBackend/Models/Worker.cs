using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string? EmployeeNumber { get; set; }
        [MaxLength(120)]
        public string? FirstName { get; set; }
        [MaxLength(120)]
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? HireDate { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        [ForeignKey("CurrentSite")]
        public int? CurrentSiteId { get; set; }
        [MaxLength(50)]
        public string? Phone { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        public bool IsContractor { get; set; } = false;
        // Navigation properties
        public Department? Department { get; set; }
        public Site? CurrentSite { get; set; }
        public ICollection<Accident> ReportedAccidents { get; set; } = new List<Accident>();
        public ICollection<AccidentParticipant> AccidentParticipations { get; set; } = new List<AccidentParticipant>();
        public ICollection<Witness> WitnessStatements { get; set; } = new List<Witness>();
        public ICollection<ActionTaken> ActionsTaken { get; set; } = new List<ActionTaken>();
        public ICollection<Attachment> UploadedAttachments { get; set; } = new List<Attachment>();
    }
}
