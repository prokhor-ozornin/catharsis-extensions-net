using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XElement"/>.</para>
  ///   <seealso cref="XElement"/>
  /// </summary>
  public static class XElementExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> Dictionary(this XElement xml)
    {
      Assertion.NotNull(xml);

      var result = new Dictionary<string, object>();
      DictionaryXml(xml, result);
      return result;
    }

    private static void DictionaryXml(XElement xml, IDictionary<string, object> dictionary)
    {
      if (xml.FirstNode == null)
      {
        dictionary[xml.Name.LocalName] = null;
      }
      else if (xml.FirstNode.NodeType == XmlNodeType.CDATA)
      {
        dictionary[xml.Name.LocalName] = xml.FirstNode.To<XCData>().Value;
      }
      else if (xml.FirstNode.NodeType == XmlNodeType.Text)
      {
        dictionary[xml.Name.LocalName] = xml.FirstNode.To<XText>().Value;
      }
      else
      {
        var childDictionary = new Dictionary<string, object>();
        dictionary[xml.Name.LocalName] = childDictionary;
        foreach (var childAttribute in xml.Attributes())
        {
          childDictionary[childAttribute.Name.LocalName] = childAttribute.Value;
        }
        foreach (var element in xml.Elements())
        {
          DictionaryXml(element, childDictionary);
        }
      }
    }
  }
}