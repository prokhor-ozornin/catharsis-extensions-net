using System;

namespace Catharsis.Commons.Domain
{
  public interface ITimeable
  {
    DateTime DateCreated { get; set; }
    DateTime LastUpdated { get; set; }
  }
}