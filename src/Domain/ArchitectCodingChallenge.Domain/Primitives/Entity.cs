using Newtonsoft.Json;

namespace ArchitectCodingChallenge.Domain.Primitives;

/// <summary>
/// Represents the base Entity class used for inheritance.
/// </summary>
public abstract class Entity : IEquatable<Entity> {
    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="Entity"/>, defining its <see cref="Guid"/>.
    /// </summary>
    /// <param name="guid">Global unique identifier of the entity. When <c>null</c>, will be assigned automatically.</param>
    protected Entity(Guid? guid) {
        Guid = guid ?? Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the global unique identifier of the Entity.
    /// </summary>
    [JsonIgnore]
    public virtual Guid Guid { get; private init; }

    /// <summary>
    /// Gets or sets the Entity creation date and time UTC.
    /// </summary>
    [JsonIgnore]
    public virtual DateTimeOffset CreatedAt { get; protected init; }
    #endregion

    #region IEquatable
    /// <inheritdoc/>
    public virtual bool Equals(Entity? other) {
        return Equals(other);
    }

    #endregion

    #region Overrides
    /// <inheritdoc/>
    public override bool Equals(object? obj) {
        if ((obj is null) || (obj.GetType() != GetType()) || (obj is not Entity entity)) {
            return false;
        }

        return entity.Guid == Guid;
    }

    /// <inheritdoc/>
    public override int GetHashCode() {
        return Guid.GetHashCode() * 11;
    }
    #endregion

    #region Operator Overrides
    /// <summary>
    /// Equality operator between two <see cref="Entity"/> objects.
    /// </summary>
    /// <param name="left">Object <see cref="Entity"/> left of operator.</param>
    /// <param name="right">Object <see cref="Entity"/> right of operator.</param>
    /// <returns>Returns <c>true</c> when both objects are equal; <c>false</c> otherwise.</returns>
    public static bool operator ==(Entity? left, Entity? right) => left is not null && right is not null && left.Equals(right);

    /// <summary>
    /// Inequality operator between two <see cref="Entity"/> objects.
    /// </summary>
    /// <param name="left">Object <see cref="Entity"/> left of operator.</param>
    /// <param name="right">Object <see cref="Entity"/> right of operator.</param>
    /// <returns>Returns <c>true</c> when both objects are not equal; <c>false</c> otherwise.</returns>
    public static bool operator !=(Entity? left, Entity? right) => !(left == right);
    #endregion
}