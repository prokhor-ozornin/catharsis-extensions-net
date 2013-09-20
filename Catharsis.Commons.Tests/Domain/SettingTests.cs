using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Setting"/>.</para>
  /// </summary>
  public sealed class SettingTests : EntityUnitTests<Setting>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Setting.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Setting { Name = null });
      Assert.Throws<ArgumentException>(() => new Setting { Name = string.Empty });

      Assert.True(new Setting { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Setting.Type"/> property.</para>
    /// </summary>
    [Fact]
    public void Type_Property()
    {
      Assert.True(new Setting { Type = 1 }.Type == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Setting.Value"/> property.</para>
    /// </summary>
    [Fact]
    public void Value_Property()
    {
      Assert.True(new Setting { Value = "value" }.Value == "value");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Setting()"/>
    ///   <seealso cref="Setting(IDictionary{string, object})"/>
    ///   <seealso cref="Setting(string, string, string, int)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var setting = new Setting();
      Assert.True(setting.Id == null);
      Assert.True(setting.Name == null);
      Assert.True(setting.Type == 0);
      Assert.True(setting.Value == null);

      Assert.Throws<ArgumentNullException>(() => new City(null));
      setting = new Setting(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Name", "name")
        .AddNext("Type", 1)
        .AddNext("Value", "value"));
      Assert.True(setting.Id == "id");
      Assert.True(setting.Name == "name");
      Assert.True(setting.Type == 1);
      Assert.True(setting.Value == "value");

      Assert.Throws<ArgumentNullException>(() => new Setting(null, "name", "value"));
      Assert.Throws<ArgumentNullException>(() => new Setting("id", null, "value"));
      Assert.Throws<ArgumentException>(() => new Setting(string.Empty, "name", "value"));
      Assert.Throws<ArgumentException>(() => new Setting("id", string.Empty, "value"));
      setting = new Setting("id", "name", "value", 1);
      Assert.True(setting.Id == "id");
      Assert.True(setting.Name == "name");
      Assert.True(setting.Type == 1);
      Assert.True(setting.Value == "value");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Setting.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Setting { Value = "value" }.ToString() == "value");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Setting"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Name", new[] { "Name", "Name_2" })
        .AddNext("Type", new[] { (object) 1, 2 }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Setting.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Setting.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Setting.Xml(null));

      var xml = new XElement("Setting",
        new XElement("Id", "id"),
        new XElement("Name", "name"),
        new XElement("Type", 1));
      var setting = Setting.Xml(xml);
      Assert.True(setting.Id == "id");
      Assert.True(setting.Name == "name");
      Assert.True(setting.Type == 1);
      Assert.True(setting.Value == null);
      Assert.True(new Setting("id", "name", null, 1).Xml().ToString() == xml.ToString());
      Assert.True(Setting.Xml(setting.Xml()).Equals(setting));

      xml = new XElement("Setting",
        new XElement("Id", "id"),
        new XElement("Name", "name"),
        new XElement("Type", 1),
        new XElement("Value", "value"));
      setting = Setting.Xml(xml);
      Assert.True(setting.Id == "id");
      Assert.True(setting.Name == "name");
      Assert.True(setting.Type == 1);
      Assert.True(setting.Value == "value");
      Assert.True(new Setting("id", "name", "value", 1).Xml().ToString() == xml.ToString());
      Assert.True(Setting.Xml(setting.Xml()).Equals(setting));
    }
  }
}