namespace BogusStore.Domain.Exceptions;

/// <summary>
/// <see cref="Exception"/> to throw when the <see cref="Entity"/> was not found.
/// </summary>
public class EntityNotFoundException : ApplicationException
{
    /// <param name="entityName">Name / type of the <see cref="Entity"/>.</param>
    /// <param name="id">The identifier of the duplicate key.</param>
    public EntityNotFoundException(string entityName, object id) : base($"'{entityName}' with 'Id':'{id}' was not found.")
    {
    }
}

