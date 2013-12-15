﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="TextReader"/>.</para>
  ///   <seealso cref="TextReader"/>
  /// </summary>
  public static class TextReaderExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <param name="close"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static IList<string> Lines(this TextReader reader, bool close = false)
    {
      Assertion.NotNull(reader);

      var lines = new List<string>();
      
      try
      {
        var line = reader.ReadLine();
        while (line != null)
        {
          lines.Add(line);
          line = reader.ReadLine();
        }
      }
      finally
      {
        if (close)
        {
          reader.Close();
        }
      }

      return lines;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <param name="close"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static string Text(this TextReader reader, bool close = false)
    {
      Assertion.NotNull(reader);

      var result = string.Empty;
      try
      {
        result = reader.ReadToEnd();
      }
      finally
      {
        if (close)
        {
          reader.Close();
        }
      }
      return result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="reader"></param>
    /// <param name="close"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static T Xml<T>(this TextReader reader, bool close = false, params Type[] types)
    {
      Assertion.NotNull(reader);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      try
      {
        return serializer.Deserialize(reader).To<T>();
      }
      finally
      {
        if (close)
        {
          reader.Close();
        }
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static XDocument XDocument(this TextReader reader, bool close = false)
    {
      Assertion.NotNull(reader);

      return System.Xml.XmlReader.Create(reader, new XmlReaderSettings { CloseInput = close }).Read(System.Xml.Linq.XDocument.Load);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static XmlReader XmlReader(this TextReader reader, bool close = false)
    {
      Assertion.NotNull(reader);

      return System.Xml.XmlReader.Create(reader, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true });
    }
  }
}