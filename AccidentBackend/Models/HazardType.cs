using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AccidentBackend.Models
{
    public class HazardType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Code { get; set; }
        [MaxLength(150)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        // Navigation properties
        public ICollection<Accident> Accidents { get; set; } = new List<Accident>();
    }
}
