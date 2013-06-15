using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  public interface ITaggable
  {
    ICollection<string> Tags { get; }
  }
}