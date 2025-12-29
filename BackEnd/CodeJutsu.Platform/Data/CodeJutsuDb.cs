using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CodeJutsu.Platform.Models;
using Microsoft.AspNetCore.Identity;


namespace CodeJutsu.Platform.Data
{
    public sealed class CodeJutsuDb: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public CodeJutsuDb(DbContextOptions<CodeJutsuDb> options): base(options) { }

        public DbSet<Problem> Problems => Set<Problem>();
        public DbSet<Submission> Submissions => Set<Submission>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
