using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("ManagerWorker")]
        public int? ManagerWorkerId { get; set; }
        // Navigation properties
        public Worker? ManagerWorker { get; set; }
        public ICollection<Worker> Workers { get; set; } = new List<Worker>();
    }
}
