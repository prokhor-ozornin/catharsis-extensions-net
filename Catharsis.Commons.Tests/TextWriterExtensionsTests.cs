using System;
using System.IO;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TextWriterExtensions"/>.</para>
  /// </summary>
  public sealed class TextWriterExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="TextWriterExtensions.WriteObject{WRITER}(WRITER, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void WriteObject_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextWriterExtensions.WriteObject<TextWriter>(null, new object()));

      new StringWriter().With(writer =>
      {
        Assert.True(ReferenceEquals(writer.WriteObject(null), writer));
        Assert.Equal(string.Empty, writer.ToString());
      });

      var subject = Guid.NewGuid();
      new StringWriter().With(writer =>
      {
        Assert.True(ReferenceEquals(writer.WriteObject(subject), writer));
        Assert.Equal(subject.ToString(), writer.ToString());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextWriterExtensions.XmlWriter"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlWriter_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextWriterExtensions.XmlWriter(null));

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      
      var textWriter = new StringWriter();
      textWriter.XmlWriter().Write(writer =>
      {
        Assert.False(writer.Settings.CloseOutput);
        Assert.Equal(Encoding.Unicode.ToString(), writer.Settings.Encoding.ToString());
        Assert.False(writer.Settings.Indent);
        writer.WriteElementString("article", "text");
      });
      Assert.Equal(Xml, textWriter.ToString());
      textWriter.Write(string.Empty);
      textWriter.Close();

      textWriter = new StringWriter();
      textWriter.XmlWriter(true).Write(writer =>
      {
        Assert.True(writer.Settings.CloseOutput);
        Assert.Equal(Encoding.Unicode.ToString(), writer.Settings.Encoding.ToString());
        Assert.False(writer.Settings.Indent);
      });
      Assert.Throws<ObjectDisposedException>(() => textWriter.Write(string.Empty));
    }
  }
}