using CodeJutsu.Platform.Models;

namespace CodeJutsu.Platform.Data
{
    public interface ISubmissionRepo {
        Task<Submission?> GetAsync();
        Task<List<Submission>> ListAsync();
        Task AddAsync();
        Task SaveAsync();
        Task DeleteAsync();

    }
    public sealed class SubmissionRepo: ISubmissionRepo
    {
        private readonly CodeJutsuDb _db;
        public SubmissionRepo(CodeJutsuDb db) => _db = db;

        public Task<Submission?> GetAsync() { }

        public Task<List<Submission>> ListAsync() { }

        public Task AddAsync() { }

        public Task SaveAsync() { }

        public Task DeleteAsync() { }
    }
}
