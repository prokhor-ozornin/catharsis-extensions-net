using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XElement"/>.</para>
  /// </summary>
  /// <seealso cref="XElement"/>
  public static class XElementExtensions
  {
    /// <summary>
    ///   <para>Translates specified <see cref="XElement"/> into a dictionary.</para>
    ///   <para>Attributes in XML document are translated to string keys and values, nodes become dictionaries with string keys themselves.</para>
    /// </summary>
    /// <param name="self"><see cref="XElement"/>, whose structure is to be converted to <see cref="IDictionary{string, object}"/> instance.</param>
    /// <returns>Dictionary that follows the structure of <see cref="XElement"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XDocumentExtensions.Dictionary(XDocument)"/>
    public static IDictionary<string, object> Dictionary(this XElement self)
    {
      Assertion.NotNull(self);

      var result = new Dictionary<string, object>();
      DictionaryXml(self, result);
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
        dictionary[xml.Name.LocalName] = ((XCData) xml.FirstNode).Value;
      }
      else if (xml.FirstNode.NodeType == XmlNodeType.Text)
      {
        dictionary[xml.Name.LocalName] = ((XText) xml.FirstNode).Value;
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