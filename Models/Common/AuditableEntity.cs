using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Common
{
    public class AuditableEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
