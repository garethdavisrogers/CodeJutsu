using CodeJutsu.Platform.Models;

namespace CodeJutsu.Platform.Data
{
    public interface ISubmissionRepo
    {
        Task<Submission?> GetAsync(Guid userId, Guid problemId, CancellationToken ct = default);
        Task UpsertAsync(Submission submission, CancellationToken ct = default);
        Task<List<Submission>> ListForUserAsync(Guid userId, int take = 50, CancellationToken ct = default);
        Task SaveAsync(CancellationToken ct = default);
    }

    public sealed class SubmissionRepo : ISubmissionRepo
    {
        private readonly AppDb _db;
        public SubmissionRepo(AppDb db) => _db = db;

        public Task<Submission?> GetAsync(Guid userId, Guid problemId, CancellationToken ct = default) =>
            _db.Submissions.FirstOrDefaultAsync(s => s.UserId == userId && s.ProblemId == problemId, ct);

        public async Task UpsertAsync(Submission submission, CancellationToken ct = default)
        {
            var existing = await GetAsync(submission.UserId, submission.ProblemId, ct);
            if (existing is null)
            {
                await _db.Submissions.AddAsync(submission, ct);
                return;
            }

            // update in-place (composite key unchanged)
            existing.Code = submission.Code;
            existing.Language = submission.Language;
            existing.Status = submission.Status;
            existing.AttemptNumber = submission.AttemptNumber;
            existing.RuntimeMs = submission.RuntimeMs;
            existing.MemoryKb = submission.MemoryKb;
            existing.CreatedAt = DateTimeOffset.UtcNow;
        }

        public Task<List<Submission>> ListForUserAsync(Guid userId, int take = 50, CancellationToken ct = default) =>
            _db.Submissions.AsNoTracking()
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .Take(take)
                .ToListAsync(ct);

        public Task SaveAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }

}
