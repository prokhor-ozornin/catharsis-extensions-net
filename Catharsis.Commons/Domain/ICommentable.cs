using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  public interface ICommentable
  {
    ICollection<Comment> Comments { get; }
  }
}