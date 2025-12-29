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

            b.Entity<Problem>(e =>
            {
                e.ToTable("problems");
                e.HasKey(x => x.Id);

                e.Property(x => x.Title).HasMaxLength(200).IsRequired();
                e.Property(x => x.Prompt).IsRequired();
                e.Property(x => x.Constraints);
                e.Property(x => x.Difficulty).IsRequired();
                e.Property(x => x.CreatedAt).IsRequired();
            });

            b.Entity<Submission>(e =>
            {
                e.ToTable("submissions");

                // composite PK (UserId, ProblemId) per your plan
                e.HasKey(x => new { x.UserId, x.ProblemId });

                e.Property(x => x.Language).HasMaxLength(32).IsRequired();
                e.Property(x => x.Code).IsRequired();
                e.Property(x => x.Status).HasMaxLength(16).IsRequired();
                e.Property(x => x.AttemptNumber).IsRequired();
                e.Property(x => x.CreatedAt).IsRequired();

                e.HasOne(x => x.User)
                 .WithMany(u => u.Submissions)
                 .HasForeignKey(x => x.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Problem)
                 .WithMany(p => p.Submissions)
                 .HasForeignKey(x => x.ProblemId)
                 .OnDelete(DeleteBehavior.Cascade);

                // Useful index for “latest submissions for a user”
                e.HasIndex(x => new { x.UserId, x.CreatedAt });
            });
        }
    }
}
