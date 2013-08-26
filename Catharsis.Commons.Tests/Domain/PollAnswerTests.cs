using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="PollAnswer"/>.</para>
  /// </summary>
  public sealed class PollAnswerTests : EntityUnitTests<PollAnswer>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="PollAnswer.AuthorId"/> property.</para>
    /// </summary>
    [Fact]
    public void AuthorId_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new PollAnswer { AuthorId = null });
      Assert.Throws<ArgumentException>(() => new PollAnswer { AuthorId = string.Empty });
      Assert.True(new PollAnswer { AuthorId = "authorId" }.AuthorId == "authorId");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PollAnswer.DateCreated"/> property.</para>
    /// </summary>
    [Fact]
    public void DateCreated_Property()
    {
      Assert.True(new PollAnswer { DateCreated = DateTime.MinValue }.DateCreated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PollAnswer.LastUpdated"/> property.</para>
    /// </summary>
    [Fact]
    public void LastUpdated_Property()
    {
      Assert.True(new PollAnswer { DateCreated = DateTime.MaxValue }.DateCreated == DateTime.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PollAnswer.Options"/> property.</para>
    /// </summary>
    [Fact]
    public void Options_Property()
    {
      var option = new PollOption();
      var answer = new PollAnswer();
      Assert.True(answer.Options.Count == 0);
      answer.Options.Add(option);
      Assert.True(answer.Options.Count == 1);
      Assert.True(ReferenceEquals(answer.Options.Single(), option));
      answer.Options.Add(option);
      Assert.True(answer.Options.Count == 1);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="PollAnswer()"/>
    ///   <seealso cref="PollAnswer(IDictionary{string, object})"/>
    ///   <seealso cref="PollAnswer(string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var answer = new PollAnswer();
      Assert.True(answer.Id == null);
      Assert.True(answer.AuthorId == null);
      Assert.True(answer.DateCreated <= DateTime.UtcNow);
      Assert.True(answer.LastUpdated <= DateTime.UtcNow);
      Assert.True(answer.Options.Count == 0);

      Assert.Throws<ArgumentNullException>(() => new PollAnswer(null));
      answer = new PollAnswer(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Poll", new Poll()));
      Assert.True(answer.Id == "id");
      Assert.True(answer.AuthorId == "authorId");
      Assert.True(answer.DateCreated <= DateTime.UtcNow);
      Assert.True(answer.LastUpdated <= DateTime.UtcNow);
      Assert.True(answer.Options.Count == 0);

      Assert.Throws<ArgumentNullException>(() => new PollAnswer(null, "authorId"));
      Assert.Throws<ArgumentNullException>(() => new PollAnswer("id", null));
      Assert.Throws<ArgumentException>(() => new PollAnswer(string.Empty, "authorId"));
      Assert.Throws<ArgumentException>(() => new PollAnswer("id", string.Empty));
      answer = new PollAnswer("id", "authorId");
      Assert.True(answer.Id == "id");
      Assert.True(answer.AuthorId == "authorId");
      Assert.True(answer.DateCreated <= DateTime.UtcNow);
      Assert.True(answer.LastUpdated <= DateTime.UtcNow);
      Assert.True(answer.Options.Count == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="PollAnswer"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("AuthorId", new[] { "AuthorId", "AuthorId_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PollAnswer.CompareTo(PollAnswer)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new PollAnswer { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new PollAnswer { DateCreated = new DateTime(2000, 1, 1) }) == 0);
      Assert.True(new PollAnswer { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new PollAnswer { DateCreated = new DateTime(2000, 1, 2) }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="PollAnswer.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="PollAnswer.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => PollAnswer.Xml(null));

      var xml = new XElement("PollAnswer",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()));
      var answer = PollAnswer.Xml(xml);
      Assert.True(answer.Id == "id");
      Assert.True(answer.AuthorId == "authorId");
      Assert.True(answer.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(answer.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(new PollAnswer("id", "authorId") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(PollAnswer.Xml(answer.Xml()).Equals(answer));
    }
  }
}