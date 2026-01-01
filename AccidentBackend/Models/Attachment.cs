using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccidentBackend.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Accident")]
        public long AccidentId { get; set; }
        [MaxLength(255)]
        public string? FileName { get; set; }
        [MaxLength(1000)]
        public string? FilePath { get; set; }
        [MaxLength(100)]
        public string? ContentType { get; set; }
        [ForeignKey("UploadedByWorker")]
        public int? UploadedByWorkerId { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        // Navigation properties
        public Accident Accident { get; set; } = null!;
        public Worker? UploadedByWorker { get; set; }
    }
}
