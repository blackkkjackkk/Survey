using Survey.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entities
{
	[Table("SurveyQuestionAnswerOptions")]
	public class SurveyQuestionAnswerOptionEntity : AuditableEntity
	{
        [Key]
		public int Id { get; set; }
		
        [Required]
		[Column(TypeName = "nvarchar(200)")]
		public string Detail { get; set; }

        [Required]
		public int Order { get; set; }

		public bool IsActive { get; set; }

		public int SurveyQuestionId { get; set; }
		public virtual SurveyQuestionEntity SurveyQuestion { get; set; }
	}
}
