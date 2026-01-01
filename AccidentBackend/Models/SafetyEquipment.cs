using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class SafetyEquipment
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? TagNumber { get; set; }
        [MaxLength(200)]
        public string? Name { get; set; }
        [MaxLength(100)]
        public string? EquipmentType { get; set; }
        [ForeignKey("Site")]
        public int? SiteId { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        [MaxLength(50)]
        public string? Status { get; set; }
        // Navigation properties
        public Site? Site { get; set; }
        public ICollection<AccidentEquipment> AccidentEquipments { get; set; } = new List<AccidentEquipment>();
    }
}
