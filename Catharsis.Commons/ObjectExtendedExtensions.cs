using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="object"/>.</para>
  ///   <seealso cref="object"/>
  /// </summary>
  public static class ObjectExtendedExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="destination"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="destination"/> is a <c>null</c> reference.</exception>
    public static object Binary(this object subject, Stream destination, bool close = false)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(destination);

      try
      {
        new BinaryFormatter().Serialize(destination, subject);
      }
      finally
      {
        if (close)
        {
          destination.Close();
        }
      }

      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static byte[] Binary(this object subject)
    {
      Assertion.NotNull(subject);

      return new MemoryStream().With(stream =>
      {
        subject.Binary(stream);
        return stream;
      }).ToArray();
    }
  }
}