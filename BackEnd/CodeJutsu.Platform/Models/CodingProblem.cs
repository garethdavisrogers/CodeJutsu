using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeJutsu.Platform.Models;

public sealed class CodingProblem
{
    // Use string IDs for stable JSON (e.g. "two-sum"). You can still map to Guid internally if you want.
    public string Id { get; init; } = default!;
    public int SchemaVersion { get; init; } = 1;

    public string Title { get; init; } = default!;
    public string Prompt { get; init; } = default!;
    public string Difficulty { get; init; } = default!;  // later: enum
    public List<string> Tags { get; init; } = [];

    public List<string> Constraints { get; init; } = [];

    // Key idea: language-neutral map
    // "csharp", later "java", "python", etc.
    public Dictionary<string, LanguageSpec> Languages { get; init; } = new();

    public List<Hint> Hints { get; init; } = [];
    public List<Approach> Approaches { get; init; } = [];

    public TestSuite Tests { get; init; } = new();
}

public sealed class LanguageSpec
{
    public string FunctionSignature { get; init; } = default!;
    public string StarterCode { get; init; } = default!;
    public string? ReferenceSolution { get; init; } // optional
}

public sealed class Hint
{
    public int Tier { get; init; }
    public string Text { get; init; } = default!;
}

public sealed class Approach
{
    public string Name { get; init; } = default!;
    public string Time { get; init; } = default!;
    public string Space { get; init; } = default!;
}

public sealed class TestSuite
{
    public List<TestCase> Cases { get; init; } = [];
}

public sealed class TestCase
{
    // Keep tests language-agnostic in v1:
    // Use JsonElement so you can store arbitrary shapes and interpret per problem type later.
    public Dictionary<string, JsonElement> Input { get; init; } = new();
    public JsonElement Output { get; init; }
}
