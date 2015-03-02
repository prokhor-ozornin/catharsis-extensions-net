using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="object"/>.</para>
  /// </summary>
  /// <seealso cref="object"/>
  public static class BinaryFormatterExtensions
  {
    /// <summary>
    ///   <para>Serializes an object, or graph of connected objects, to the given stream.</para>
    /// </summary>
    /// <param name="self">The object at the root of the graph to serialize.</param>
    /// <param name="destination">The stream to which the graph is to be serialized.</param>
    /// <param name="close"><c>true</c> to auto-close target <paramref name="destination"/> stream after serialization, <c>false</c> to leave it intact.</param>
    /// <returns>Back reference to the current serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="destination"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BinaryFormatter"/>
    public static object Binary(this object self, Stream destination, bool close = false)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(destination);

      try
      {
        new BinaryFormatter().Serialize(destination, self);
      }
      finally
      {
        if (close)
        {
          destination.Close();
        }
      }

      return self;
    }

    /// <summary>
    ///   <para>Serializes an object, or graph of connected objects, and returns serialization data as an array of bytes.</para>
    /// </summary>
    /// <param name="self">The object at the root of the graph to serialize.</param>
    /// <returns>Binary data of serialized object graph.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BinaryFormatter"/>
    public static byte[] Binary(this object self)
    {
      Assertion.NotNull(self);

      using (var stream = new MemoryStream())
      {
        self.Binary(stream);
        return stream.ToArray();
      }
    }
  }
}