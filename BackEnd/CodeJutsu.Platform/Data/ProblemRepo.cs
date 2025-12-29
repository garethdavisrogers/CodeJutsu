using CodeJutsu.Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeJutsu.Platform.Data
{
    public interface IProblemRepo
    {
        Task<Problem?> GetAsync(Guid id, CancellationToken ct = default);
        Task<List<Problem>> ListAsync(int take = 50, CancellationToken ct = default);
        Task AddAsync(Problem problem, CancellationToken ct = default);
        Task SaveAsync(CancellationToken ct = default);
    }

    public sealed class ProblemRepo : IProblemRepo
    {
        private readonly CodeJutsuDb _db;
        public ProblemRepo(CodeJutsuDb db) => _db = db;

        public Task<Problem?> GetAsync(Guid id, CancellationToken ct = default) =>
            _db.Problems.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id.ToString(), ct);

        public Task<List<Problem>> ListAsync(int take = 50, CancellationToken ct = default) =>
            _db.Problems.AsNoTracking()
                .OrderByDescending(p => p.CreatedAt)
                .Take(take)
                .ToListAsync(ct);

        public Task AddAsync(Problem problem, CancellationToken ct = default) =>
            _db.Problems.AddAsync(problem, ct).AsTask();

        public Task SaveAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }

}
