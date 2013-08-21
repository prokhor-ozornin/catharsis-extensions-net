using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XmlDocument"/>.</para>
  ///   <seealso cref="XmlDocument"/>
  /// </summary>
  public static class XmlDocumentExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> Dictionary(this XmlDocument document)
    {
      Assertion.NotNull(document);

      var result = new Dictionary<string, object>();
      DictionaryXml(document.DocumentElement, result);
      return result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is a <c>null</c> reference.</exception>
    public static string String(this XmlDocument document)
    {
      Assertion.NotNull(document);

      return new StringWriter().With(writer =>
      {
        writer.XmlWriter(Encoding.UTF8, true).Write(document.Save).Close();
        return writer;
      }).ToString();
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