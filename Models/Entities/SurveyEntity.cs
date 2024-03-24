using Survey.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entities
{
    [Table("Surveys")]
    public class SurveyEntity : AuditableEntity
    {
        public SurveyEntity()
        {
            SurveyQuestions = new List<SurveyQuestionEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public virtual IList<SurveyQuestionEntity> SurveyQuestions { get; set; }
    }
}
