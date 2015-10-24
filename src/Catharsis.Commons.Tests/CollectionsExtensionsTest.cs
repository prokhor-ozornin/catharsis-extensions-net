using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class CollectionsExtensionsTest
  {
    [Fact]
    public void add()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Add(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].Add(null));

      ICollection<string> first = new HashSet<string> {"first"};
      ICollection<string> second = new List<string> {"second"};

      first.Add(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("first", first.ElementAt(0));
      Assert.Equal("second", first.ElementAt(1));
    }

    [Fact]
    public void base64()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Base64(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().Base64());
      Assert.Equal(System.Convert.ToBase64String(bytes), bytes.Base64());
    }

    [Fact]
    public void bytes()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Bytes(null));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text.ToCharArray().Bytes(Encoding.Unicode).Length * 2, text.ToCharArray().Bytes(Encoding.UTF32).Length);
      Assert.Equal(text, text.ToCharArray().Bytes(Encoding.Unicode).String(Encoding.Unicode));
    }

    [Fact]
    public void each()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Each<object>(null, it => {}));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().Each(null));

      var strings = new [] { "first", "second", "third" };
      var list = new List<string>();
      Assert.True(ReferenceEquals(strings.Each(list.Add), strings));
      Assert.Equal(3, list.Count);
      Assert.Equal("first", list[0]);
      Assert.Equal("second", list[1]);
      Assert.Equal("third", list[2]);
    }

    [Fact]
    public void hex()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Hex(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().Hex());
      Assert.Equal(bytes.Length * 2, bytes.Hex().Length);
      Assert.True(bytes.Hex().IsMatch("[0-9A-Z]"));
    }

    [Fact]
    public void join_arrays()
    {

      Assert.Equal(0, Enumerable.Empty<object>().ToArray().Join(Enumerable.Empty<object>().ToArray()).Length);
      Assert.True(new[] { "first" }.Join(Enumerable.Empty<object>().ToArray()).SequenceEqual(new[] { "first" }));
      Assert.True(Enumerable.Empty<object>().ToArray().Join(new[] { "second" }).SequenceEqual(new[] { "second" }));
      Assert.True(new[] { "first", "second" }.Join(new[] { "third" }).SequenceEqual(new[] { "first", "second", "third" }));
    }

    [Fact]
    public void join_enumerable()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Join(null, Enumerable.Empty<object>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().ToArray().Join(null));
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Join<object>(null, "separator"));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().Join(null));

      Assert.Equal(string.Empty, Enumerable.Empty<object>().Join("separator"));
      Assert.Equal("1", new[] { 1 }.Join(","));
      Assert.Equal("123", new[] { 1, 2, 3 }.Join(string.Empty));
      Assert.Equal("1,2,3", new [] { 1, 2, 3 }.Join(","));
    }

    [Fact]
    public void paginate()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Paginate<object>(null));

      Assert.False(Enumerable.Empty<object>().Paginate().Any());

      var sequence = new[] { "first", "second", "third" };
      Assert.Equal("first", sequence.Paginate(-1, 1).Single());
      Assert.Equal("first", sequence.Paginate(0, 1).Single());
      Assert.Equal("first", sequence.Paginate(1, 1).Single());
      Assert.True(sequence.Paginate(1, 2).SequenceEqual(new[] { "first", "second" }));
      Assert.True(sequence.Paginate(1, -1).SequenceEqual(sequence));
      Assert.True(sequence.Paginate(1, 0).SequenceEqual(sequence));
      Assert.Equal("second", sequence.Paginate(2, 1).Single());
      Assert.Equal("third", sequence.Paginate(2, 2).Single());
    }

    [Fact]
    public void random()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Random<object>(null));

      Assert.Null(Enumerable.Empty<object>().Random());

      var element = new object();
      Assert.True(ReferenceEquals(new[] { element }.Random(), element));

      var elements = new[] { "first", "second" };
      Assert.True(elements.Contains(elements.Random()));
    }

    [Fact]
    public void remove()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Remove(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].Remove(null));

      var first = new HashSet<string> {"1", "2", "3"};
      var second = new List<string> {"2", "4"};
      first.Remove(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("1", first.ElementAt(0));
      Assert.Equal("3", first.ElementAt(1));
    }

    [Fact]
    public void to_list_string()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.ToListString<object>(null));

      Assert.Equal("[]", Enumerable.Empty<object>().ToListString());
      Assert.Equal("[1]", new[] { 1 }.ToListString());
      Assert.Equal("[1, 2, 3]", new [] { 1, 2, 3 }.ToListString());
    }

    [Fact]
    public void to_set()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.ToSet<object>(null));

      Assert.False(Enumerable.Empty<object>().ToSet().Any());
      var set = new [] { 1, 1, 2, 3, 4, 5, 5 }.ToSet();
      Assert.Equal(5, set.Count);
      for (var i = 1; i <= 5; i++)
      {
        Assert.Equal(i, set.ElementAt(i - 1));
      }
    }

    [Fact]
    public void to_string()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.String(null));
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.String(null, Encoding.Default));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text, text.ToCharArray().String());
      Assert.Equal(text, Encoding.Default.GetBytes(text).String());
      Assert.Equal(text, Encoding.Unicode.GetBytes(text).String(Encoding.Unicode));
    }
  }
}