namespace Domain;

/// <summary>
/// Base interface for entities with a unique identifier.
/// </summary>
public interface IBaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    Guid Id { get; set; }
}