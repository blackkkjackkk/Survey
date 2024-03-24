using Survey.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entities
{
    [Table("UserSurveyHistories")]
	public class UserSurveyHistoryEntity : AuditableEntity
	{
        public UserSurveyHistoryEntity()
        {
			UserSurveyQuestionAnswerHistories = new List<UserSurveyQuestionAnswerHistoryEntity>();
		}

        [Key]
		public int Id { get; set; }

		public bool IsActive { get; set; }
		public bool IsPublished { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public int SurveyId { get; set; }
		public virtual SurveyEntity Survey { get; set; }

		public virtual IList<UserSurveyQuestionAnswerHistoryEntity> UserSurveyQuestionAnswerHistories { get; set; }
	}
}
