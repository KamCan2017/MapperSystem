namespace DIRS21.Core.Result;

/// <summary>
/// The operation result class provides informations about a task or process
/// </summary>
/// <param name="Output">The output model</param>
/// <param name="ResultType">The result type</param>
/// <param name="Resume">The optional resume</param>
public record OperationResult(object? Output, ResultType ResultType, string? Resume);
