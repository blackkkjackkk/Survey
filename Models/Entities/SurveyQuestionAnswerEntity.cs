using Survey.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entities
{
    [Table("SurveyQuestionAnswers")]
    public class SurveyQuestionAnswerEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Answer { get; set; }

        public bool IsActive { get; set; }

        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestionEntity SurveyQuestion { get; set; }

        public int? SurveyQuestionAnswerOptionId { get; set; }
        public virtual SurveyQuestionAnswerOptionEntity SurveyQuestionAnswerOption { get; set; }
    }
}
