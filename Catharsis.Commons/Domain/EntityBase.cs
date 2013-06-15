using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  public abstract class EntityBase : IEntity
  {
    public string Id { get; set; }

    public override bool Equals(object other)
    {
      return this.Equality(other, (string[]) null);
    }

    public override int GetHashCode()
    {
      return this.GetHashCode((string[]) null);
    }
  }
}