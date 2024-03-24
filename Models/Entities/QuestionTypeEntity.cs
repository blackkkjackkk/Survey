using Survey.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entities
{
    public class QuestionTypeEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
