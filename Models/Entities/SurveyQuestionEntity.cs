using Survey.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entities
{
	[Table("SurveyQuestions")]
	public class SurveyQuestionEntity : AuditableEntity
	{
		public SurveyQuestionEntity()
		{
			SurveyQuestionAnswerOptions = new List<SurveyQuestionAnswerOptionEntity>();
			SurveyQuestionAnswers = new List<SurveyQuestionAnswerEntity>();
		}

        [Key]
		public int Id { get; set; }

        [Required]
		[Column(TypeName = "nvarchar(400)")]
		public string Detail { get; set; }

		public bool IsActive { get; set; }

		public int SurveyId { get; set; }
		public virtual SurveyEntity Survey { get; set; }

		public int QuestionTypeId { get; set; }
		public virtual QuestionTypeEntity QuestionType { get; set; }

		public virtual IList<SurveyQuestionAnswerOptionEntity> SurveyQuestionAnswerOptions { get; set; }
		public virtual IList<SurveyQuestionAnswerEntity> SurveyQuestionAnswers { get; set; }
	}
}
