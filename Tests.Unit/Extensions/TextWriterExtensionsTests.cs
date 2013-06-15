using System;
using System.IO;
using System.Text;
using Xunit;

namespace Catharsis.Commons.Extensions
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
        Assert.True(writer.ToString() == string.Empty);
      });

      var subject = Guid.NewGuid();
      new StringWriter().With(writer =>
      {
        Assert.True(ReferenceEquals(writer.WriteObject(subject), writer));
        Assert.True(writer.ToString() == subject.ToString());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextWriterExtensions.XmlWriter(TextWriter, Encoding, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlWriter_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextWriterExtensions.XmlWriter(null));

      const string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      
      var textWriter = new StringWriter();
      textWriter.XmlWriter().Write(writer =>
      {
        Assert.False(writer.Settings.CloseOutput);
        Assert.True(writer.Settings.Encoding.ToString().Equals(Encoding.Unicode.ToString()));
        Assert.False(writer.Settings.Indent);
        writer.WriteElementString("article", "text");
      });
      Assert.True(textWriter.ToString() == xml);
      textWriter.Write(string.Empty);
      textWriter.Close();

      textWriter = new StringWriter();
      textWriter.XmlWriter(null, true).Write(writer =>
      {
        Assert.True(writer.Settings.CloseOutput);
        Assert.True(writer.Settings.Encoding.ToString().Equals(Encoding.Unicode.ToString()));
        Assert.False(writer.Settings.Indent);
      });
      Assert.Throws<ObjectDisposedException>(() => textWriter.Write(string.Empty));
    }
  }
}