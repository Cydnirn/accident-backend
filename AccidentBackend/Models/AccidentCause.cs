using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccidentBackend.Models
{
    public class AccidentCause
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50) UNIQUE")]
        public string? Code { get; set; }
        [MaxLength(150)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        // Navigation properties
        public ICollection<Accident> Accidents { get; set; } = new List<Accident>();
    }
}
