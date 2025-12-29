using CodeJutsu.Platform.Models;

namespace CodeJutsu.Platform.Data
{
    public interface IProblemRepo {
        Task<Problem?> GetAsync();
        Task<List<Problem>> ListAsync();
        Task AddAsync();
        Task SaveAsync();
        Task DeleteAsync();
    }
    public sealed class ProblemRepo: IProblemRepo
    {
        private readonly CodeJutsuDb _db;
        public ProblemRepo(CodeJutsuDb db) => _db = db;

        public Task<Problem?> GetAsync() { }

        public Task<List<Problem>> ListAsync() { }

        public Task AddAsync() { }

        public Task SaveAsync() { }

        public Task DeleteAsync() { }
    }
}
