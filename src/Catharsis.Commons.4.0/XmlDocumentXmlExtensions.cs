using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XmlDocument"/>.</para>
  /// </summary>
  /// <seealso cref="XmlDocument"/>
  public static class XmlDocumentXmlExtensions
  {
    /// <summary>
    ///   <para>Deserializes XML contents of stream into <see cref="XmlDocument"/> object.</para>
    /// </summary>
    /// <param name="self">Stream of XML data for deserialization.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> after deserialization process or leave it intact.</param>
    /// <returns>Deserialized XML contents of source <paramref name="self"/> as instance of <see cref="XmlDocument"/> class.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlDocument"/>
    public static XmlDocument AsXmlDocument(this Stream self, bool close = false)
    {
      Assertion.NotNull(self);

      var document = new XmlDocument();
      try
      {
        document.Load(self);
      }
      finally
      {
        if (close)
        {
          self.Close();
        }
      }
      return document;
    }

    /// <summary>
    ///   <para>Translates specified <see cref="XmlDocument"/> into a dictionary.</para>
    ///   <para>Attributes in XML document are translated to string keys and values, nodes become dictionaries with string keys themselves.</para>
    /// </summary>
    /// <param name="self"><see cref="XmlDocument"/>, whose structure is to be converted to <see cref="IDictionary{string, object}"/> instance.</param>
    /// <returns>Dictionary that follows the structure of <see cref="XmlDocument"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> Dictionary(this XmlDocument self)
    {
      Assertion.NotNull(self);

      var result = new Dictionary<string, object>();
      DictionaryXml(self.DocumentElement, result);
      return result;
    }

    /// <summary>
    ///   <para>Represents given <see cref="XmlDocument"/> instance as a string of XML data in <see cref="Encoding.UTF8"/> encoding.</para>
    /// </summary>
    /// <param name="self"><see cref="XmlDocument"/> to be converted to a string.</param>
    /// <returns>XML content, representing XML <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string String(this XmlDocument self)
    {
      Assertion.NotNull(self);

      using (var writer = new StringWriter())
      {
        writer.XmlWriter(true, Encoding.UTF8).Write(self.Save).Close();
        return writer.ToString();
      }
    }

    /// <summary>
    ///   <para>Deserializes XML contents of <see cref="TextReader"/> into <see cref="XmlDocument"/> object.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> which is used as a source for XML data.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> after deserialization process or leave it intact.</param>
    /// <returns>Deserialized XML contents of source <paramref name="self"/> as instance of <see cref="XmlDocument"/> class.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlDocument"/>
    public static XmlDocument XmlDocument(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      var document = new XmlDocument();
      try
      {
        document.Load(self);
      }
      finally
      {
        if (close)
        {
          self.Close();
        }
      }
      return document;
    }

    private static void DictionaryXml(XmlElement xml, IDictionary<string, object> dictionary)
    {
      if (xml.FirstChild == null)
      {
        dictionary[xml.LocalName] = null;
      }
      else if (xml.FirstChild.NodeType == XmlNodeType.CDATA || xml.FirstChild.NodeType == XmlNodeType.Text)
      {
        dictionary[xml.LocalName] = xml.FirstChild.Value;
      }
      else
      {
        var childDictionary = new Dictionary<string, object>();
        dictionary[xml.LocalName] = childDictionary;
        foreach (var childAttribute in xml.Attributes)
        {
          childDictionary[childAttribute.To<XmlAttribute>().LocalName] = childAttribute.To<XmlAttribute>().Value;
        }
        foreach (var element in xml.ChildNodes.Cast<XmlNode>().Where(node => node.NodeType == XmlNodeType.Element))
        {
          DictionaryXml(element.To<XmlElement>(), childDictionary);
        }
      }
    }
  }
}