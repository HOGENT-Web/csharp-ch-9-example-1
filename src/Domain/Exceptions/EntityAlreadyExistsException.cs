namespace BogusStore.Domain.Exceptions;

/// <summary>
/// <see cref="Exception"/> to throw when the <see cref="Entity"/> already exists.
/// </summary>
public class EntityAlreadyExistsException : ApplicationException
{
    /// <param name="entityName">Name / type of the <see cref="Entity"/>.</param>
    /// <param name="parameterName">Name of the property that is invalid.</param>
    /// <param name="parameterValue">The value that was marked as a duplicate.</param>
    public EntityAlreadyExistsException(string entityName, string parameterName, string? parameterValue) : base($"'{entityName}' with '{parameterName}':'{parameterValue}' already exists.")
    {
    }
}
