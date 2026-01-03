using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AccidentBackend.Models
{
    public class Site
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? SiteCode { get; set; }
        [MaxLength(50)]
        public double? Latitude { get; set; }
        [MaxLength(50)]
        public double? Longitude { get; set; }
        [MaxLength(100)]
        public string? Type { get; set; }
        [MaxLength(50)]
        public string? ContactNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // Navigation properties
        public ICollection<Worker> Workers { get; set; } = new List<Worker>();
        public ICollection<SafetyEquipment> SafetyEquipments { get; set; } = new List<SafetyEquipment>();
        public ICollection<Accident> Accidents { get; set; } = new List<Accident>();
    }
}
