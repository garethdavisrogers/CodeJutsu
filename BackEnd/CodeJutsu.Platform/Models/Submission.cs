namespace CodeJutsu.Platform.Models
{
    public sealed class Submission
    {
        public Guid UserId { get; set; }
        public Guid ProblemId { get; set; }

        public string Language { get; set; } = "csharp";
        public string Code { get; set; } = null!;
        public string Status { get; set; } = "fail"; // pass/fail/error/timeout
        public int AttemptNumber { get; set; }
        public int? RuntimeMs { get; set; }
        public int? MemoryKb { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public User User { get; set; } = null!;
        public Problem Problem { get; set; } = null!;
    }

}
