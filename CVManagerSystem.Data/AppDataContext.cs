using CVManagerSystem.Data.DataContext.DbIdentity;
using CVManagerSystem.Data.DataContext.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVManagerSystem.Data
{
    public class AppDataContext: IdentityDbContext<ApplicationUser>
    {

        public AppDataContext()
        {

        }

        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }

        public DbSet<CV> CV { get; set; } = default!;
        public DbSet<PersonalInformation> PersonalInformation { get; set; } = default!;
        public DbSet<ExperienceInformation> ExperienceInformation { get; set; } = default!;
    }
}
