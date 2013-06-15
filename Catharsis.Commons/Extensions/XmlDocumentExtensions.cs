using System.IO;
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
    public static string String(this XmlDocument document)
    {
      Assertion.NotNull(document);

      return new StringWriter().With(writer =>
      {
        writer.XmlWriter(Encoding.UTF8, true).Write(document.Save).Close();
        return writer;
      }).ToString();
    }
  }
}