using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Survey.Models.Entities;
using Survey.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Data
{
    public static class ApplicationDbContextSeed
    {
        private static readonly string AdminUserName = "admin@admin.com";
        private static readonly string AdminRoleName = "Administrator";
        private static readonly string StandartUserName = "test@test.com";
        private static readonly string StandartUserRoleName = "StandartUserRole";

        /// <summary>
        /// Add Administrator Role and CMSUser Role and some claims for them
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task SeedRoleAndClaimAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedAdminRolesAsync(roleManager);

            await SeedStandartUserRolesAsync(roleManager);
        }

        /// <summary>
        /// Add Admin User and CMS User
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task SeedDefaultUsersAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedAdminUserAndRoleAsync(userManager, roleManager);
            await SeedStandartUserAndRoleAsync(userManager, roleManager);
        }

        /// <summary>
        /// Add some default question types
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task SeedQuestionTypeDataAsync(ApplicationDbContext context)
        {
            var adminUser = context.Users.FirstOrDefault(u => u.UserName == AdminUserName);

            if (adminUser != null)
            {
                await using var transaction = await context.Database.BeginTransactionAsync();

                var currentQuestionTypes = context.QuestionTypes.ToList();

                var insertCount = 0;

                var singleSelect = currentQuestionTypes.FirstOrDefault(l => l.Id == (int)QuestionType.SingleSelect);
                if (singleSelect == null)
                {
                    context.QuestionTypes.Add(new QuestionTypeEntity
                    {
                        Id = 1,
                        Name = "Tek cevap",
                        Description = "Tek bir cevap verilebilen sorular",
                        CreatedBy = adminUser.Id,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    });
                    insertCount++;
                }

                var multiSelect = currentQuestionTypes.FirstOrDefault(l => l.Id == (int)QuestionType.MultiSelect);
                if (multiSelect == null)
                {
                    context.QuestionTypes.Add(new QuestionTypeEntity
                    {
                        Id = 1,
                        Name = "Çoklu cevap",
                        Description = "Birden fazla cevap verilebilen sorular",
                        CreatedBy = adminUser.Id,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    });
                    insertCount++;
                }

                var text = currentQuestionTypes.FirstOrDefault(l => l.Id == (int)QuestionType.Text);
                if (text == null)
                {
                    context.QuestionTypes.Add(new QuestionTypeEntity
                    {
                        Id = 1,
                        Name = "Text cevap",
                        Description = "Text olarak cevap verilebilen sorular",
                        CreatedBy = adminUser.Id,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    });
                    insertCount++;
                }

                if (insertCount > 0)
                {
                    var x = await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [AygazCMS].[dbo].[Language] ON;");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [AygazCMS].[dbo].[Language] OFF;");
                }

                await transaction.CommitAsync();
            }
        }

        /// <summary>
        /// Add Admin User and role
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        private static async Task SeedAdminUserAndRoleAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(AdminRoleName);

            if (adminRole != null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = AdminUserName,
                    Email = AdminUserName,
                    EmailConfirmed = true
                };

                if (userManager.Users.All(u => u.UserName != adminUser.UserName))
                {
                    await userManager.CreateAsync(adminUser, "Admin1234!");
                    await userManager.AddToRolesAsync(adminUser, new[] { AdminRoleName });
                }
            }
        }

        /// <summary>
        /// Add Standart User and role
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        private static async Task SeedStandartUserAndRoleAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var standartUserRole = await roleManager.FindByNameAsync(StandartUserRoleName);

            if (standartUserRole != null)
            {
                var contentEditor = new IdentityUser
                {
                    UserName = StandartUserName,
                    Email = StandartUserName,
                    EmailConfirmed = true
                };

                if (userManager.Users.All(u => u.UserName != contentEditor.UserName))
                {
                    await userManager.CreateAsync(contentEditor, "Test1234!");
                    await userManager.AddToRolesAsync(contentEditor, new[] { StandartUserRoleName });
                }
            }
        }

        /// <summary>
        /// Add Admin role
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        private static async Task SeedAdminRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = await roleManager.FindByNameAsync(AdminRoleName);

            if (administratorRole == null)
            {
                administratorRole = new IdentityRole(AdminRoleName);

                await roleManager.CreateAsync(administratorRole);
            }
        }

        /// <summary>
        /// Add Standart User role
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        private static async Task SeedStandartUserRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var standartUserRole = await roleManager.FindByNameAsync(StandartUserRoleName);
            if (standartUserRole == null)
            {
                standartUserRole = new IdentityRole(StandartUserRoleName);

                await roleManager.CreateAsync(standartUserRole);
            }
        }
    }
}