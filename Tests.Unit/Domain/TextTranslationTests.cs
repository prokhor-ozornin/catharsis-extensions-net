using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TextTranslation"/>.</para>
  /// </summary>
  public sealed class TextTranslationTests : EntityUnitTests<TextTranslation>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="TextTranslation.Language"/> property.</para>
    /// </summary>
    [Fact]
    public void Language_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new TextTranslation { Language = null });
      Assert.Throws<ArgumentException>(() => new TextTranslation { Language = string.Empty });
      Assert.True(new TextTranslation { Language = "language" }.Language == "language");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextTranslation.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new TextTranslation { Name = null });
      Assert.Throws<ArgumentException>(() => new TextTranslation { Name = string.Empty });
      Assert.True(new TextTranslation { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextTranslation.Text"/> property.</para>
    /// </summary>
    [Fact]
    public void Text_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new TextTranslation { Text = null });
      Assert.Throws<ArgumentException>(() => new TextTranslation { Text = string.Empty });
      Assert.True(new TextTranslation { Text = "text" }.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextTranslation.Translator"/> property.</para>
    /// </summary>
    [Fact]
    public void Translator_Property()
    {
      Assert.True(new TextTranslation { Translator = "translator" }.Translator == "translator");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="TextTranslation()"/>
    ///   <seealso cref="TextTranslation(IDictionary{string, object})"/>
    ///   <seealso cref="TextTranslation(string, string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var translation = new TextTranslation();
      Assert.True(translation.Id == null);
      Assert.True(translation.Language == null);
      Assert.True(translation.Name == null);
      Assert.True(translation.Text == null);
      Assert.True(translation.Translator == null);

      Assert.Throws<ArgumentNullException>(() => new TextTranslation(null));
      translation = new TextTranslation(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Translator", "translator"));
      Assert.True(translation.Id == "id");
      Assert.True(translation.Language == "language");
      Assert.True(translation.Name == "name");
      Assert.True(translation.Text == "text");
      Assert.True(translation.Translator == "translator");

      Assert.Throws<ArgumentNullException>(() => new TextTranslation(null, "language", "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new TextTranslation("id", null, "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new TextTranslation("id", "language", null, "text"));
      Assert.Throws<ArgumentNullException>(() => new TextTranslation("id", "language", "name", null));
      Assert.Throws<ArgumentException>(() => new TextTranslation(string.Empty, "language", "name", "text"));
      Assert.Throws<ArgumentException>(() => new TextTranslation("id", string.Empty, "name", "text"));
      Assert.Throws<ArgumentException>(() => new TextTranslation("id", "language", string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new TextTranslation("id", "language", "name", string.Empty));

      translation = new TextTranslation("id", "language", "name", "text", "translator");
      Assert.True(translation.Id == "id");
      Assert.True(translation.Language == "language");
      Assert.True(translation.Name == "name");
      Assert.True(translation.Text == "text");
      Assert.True(translation.Translator == "translator");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextTranslation.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new TextTranslation { Name = "Name" }.ToString() == "Name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="TextTranslation"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Language", new[] { "Language", "Language_2" })
        .AddNext("Name", new[] { "Name", "Name_2" })
        .AddNext("Translator", new[] { "Translator", "Translator_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="TextTranslation.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="TextTranslation.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => TextTranslation.Xml(null));

      var xml = new XElement("TextTranslation",
        new XElement("Id", "id"),
        new XElement("Language", "language"),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var translation = TextTranslation.Xml(xml);
      Assert.True(translation.Id == "id");
      Assert.True(translation.Language == "language");
      Assert.True(translation.Name == "name");
      Assert.True(translation.Text == "text");
      Assert.True(translation.Translator == null);
      Assert.True(new TextTranslation("id", "language", "name", "text").Xml().ToString() == xml.ToString());
      Assert.True(TextTranslation.Xml(translation.Xml()).Equals(translation));

      xml = new XElement("TextTranslation",
        new XElement("Id", "id"),
        new XElement("Language", "language"),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Translator", "translator"));
      translation = TextTranslation.Xml(xml);
      Assert.True(translation.Id == "id");
      Assert.True(translation.Language == "language");
      Assert.True(translation.Name == "name");
      Assert.True(translation.Text == "text");
      Assert.True(translation.Translator == "translator");
      Assert.True(new TextTranslation("id", "language", "name", "text", "translator").Xml().ToString() == xml.ToString());
      Assert.True(TextTranslation.Xml(translation.Xml()).Equals(translation));
    }
  }
}