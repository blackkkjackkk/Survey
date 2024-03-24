using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Survey.Models.Common;
using Survey.Models.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Survey.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<SurveyEntity> Surveys { get; set; }
        public DbSet<SurveyQuestionEntity> SurveyQuestions { get; set; }

        public DbSet<SurveyQuestionAnswerOptionEntity> SurveyQuestionAnswerOptions { get; set; }
        public DbSet<SurveyQuestionAnswerEntity> SurveyQuestionAnswers { get; set; }

        public DbSet<QuestionTypeEntity> QuestionTypes { get; set; }
        public DbSet<UserSurveyHistoryEntity> UserSurveyHistories { get; set; }

        public DbSet<UserSurveyQuestionAnswerHistoryEntity> UserSurveyQuestionAnswersHistoryEntities { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Identity

            #region Table Name Change

            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            #endregion

            #endregion 

        }
    }
}
