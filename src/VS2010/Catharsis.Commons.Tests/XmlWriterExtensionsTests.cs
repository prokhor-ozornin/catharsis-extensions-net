using System;
using System.IO;
using System.Text;
using System.Xml;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XmlWriterExtensions"/>.</para>
  /// </summary>
  public sealed class XmlWriterExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XmlWriterExtensions.Write{WRITER}(WRITER, Action{WRITER})"/> method.</para>
    /// </summary>
    [Fact]
    public void Write_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlWriterExtensions.Write<XmlWriter>(null, writer => {}));
      Assert.Throws<ArgumentNullException>(() => XmlWriterExtensions.Write(XmlWriter.Create(Path.GetTempFileName()), null));

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      var stringWriter = new StringWriter();
      var xmlWriter = stringWriter.XmlWriter(true, Encoding.Unicode);
      Assert.True(ReferenceEquals(xmlWriter.Write(writer =>
      {
        writer.WriteStartDocument();
        writer.WriteElementString("article", "text");
        writer.WriteEndDocument();
      }), xmlWriter));
      Assert.Throws<InvalidOperationException>(() => xmlWriter.WriteRaw(string.Empty));
      Assert.Equal(Xml, stringWriter.ToString());
      Assert.Throws<ObjectDisposedException>(() => stringWriter.WriteLine());
    }
  }
}