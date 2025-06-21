using System.Security;
using System.Security.Cryptography;
using AutoFixture;
using FluentAssertions;

namespace Catharsis.Extensions.Tests;

public class Test : IDisposable
{
  protected IFixture Fixture { get; } = new Fixture();
  protected Random Random { get; } = new();
  protected byte[] Bytes { get; } = new Random().Byte(short.MaxValue).AsArray();
  protected object[] Objects { get; } = new Random().Object(short.MaxValue).AsArray();
  protected string Shell => "cmd.exe";
  protected IAsyncEnumerable<object> EmptyAsyncEnumerable { get; } = Enumerable.Empty<object>().ToAsyncEnumerable();
  protected SecureString EmptySecureString { get; } = new();
  protected SecureString RandomSecureString { get; } = new Random().SecureString(short.MaxValue, ['a'..'z', 'A'..'Z']);
  protected SymmetricAlgorithm SymmetricAlgorithm { get; } = Aes.Create();
  protected HashAlgorithm HashAlgorithm { get; } = SHA512.Create();
  protected TextReader EmptyTextReader { get; } = string.Empty.ToStringReader();
  protected Stream EmptyStream { get; } = new MemoryStream();
  protected MemoryStream Stream { get; } = new Random().MemoryStreamAsync(short.MaxValue).Await();
  protected Stream ReadOnlyStream { get; } = new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnly();
  protected Stream ReadOnlyForwardStream { get; } = new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnlyForward();
  protected Stream WriteOnlyStream { get; } = new MemoryStream().AsWriteOnly();
  protected Stream WriteOnlyForwardStream { get; } = new MemoryStream().AsWriteOnlyForward();
  protected FileInfo FakeFile { get; } = new Random().FilePath().ToFile();
  protected FileInfo EmptyFile { get; } = new Random().File();
  protected FileInfo NonEmptyFile { get; } = new Random().TextFileAsync(short.MaxValue).Await();
  protected DirectoryInfo Directory { get; } = new Random().Directory();
  protected DirectoryInfo FakeDirectory { get; } = new Random().DirectoryPath().ToDirectory();

  protected Test()
  {
  }

  public virtual void Dispose()
  {
    EmptySecureString.Dispose();
    EmptyTextReader.Dispose();
    RandomSecureString.Dispose();
    EmptyStream.Dispose();
    Stream.Dispose();
    ReadOnlyStream.Dispose();
    ReadOnlyForwardStream.Dispose();
    WriteOnlyStream.Dispose();
    WriteOnlyForwardStream.Dispose();
    EmptyFile.Delete();
    NonEmptyFile.Delete();
    Directory.Delete();
    SymmetricAlgorithm.Dispose();
  }

  protected void TestCompareTo<TClass, TProperty>(string property, TProperty lower, TProperty greater, Func<TClass> constructor = null)
  {
    constructor ??= () => typeof(TClass).Instance<TClass>();

    var first = constructor().To<IComparable<TClass>>();
    var second = constructor().To<TClass>();

    first.SetPropertyValue(property, lower);
    second.SetPropertyValue(property, lower);

    first.CompareTo(second).Should().Be(0);
    second.SetPropertyValue(property, greater);
    first.CompareTo(second).Should().BeLessThan(0);
  }

  protected void TestEquality<TClass, TProperty>(string property, TProperty oldValue, TProperty newValue, Func<TClass> constructor = null)
  {
    constructor ??= () => typeof(TClass).Instance<TClass>();
    var entity = constructor();

    entity.Equals(new object()).Should().BeFalse();
    entity.Equals(null).Should().BeFalse();
    entity.Equals(entity).Should().BeTrue();
    //entity.Equals(constructor()).Should().BeTrue();

    constructor().SetPropertyValue(property, oldValue).Equals(constructor().SetPropertyValue(property, oldValue)).Should().BeTrue();
    constructor().SetPropertyValue(property, oldValue).Equals(constructor().SetPropertyValue(property, newValue)).Should().BeFalse();
  }

  protected void TestHashCode<TClass, TProperty>(string property, TProperty oldValue, TProperty newValue, Func<TClass> constructor = null)
  {
    constructor ??= () => typeof(TClass).Instance<TClass>();
    var entity = constructor();

    entity.GetHashCode().Should().Be(entity.GetHashCode());
    //entity.GetHashCode().Should().Be(constructor().GetHashCode());

    constructor().SetPropertyValue(property, oldValue).GetHashCode().Should().Be(constructor().SetPropertyValue(property, oldValue).GetHashCode());
    constructor().SetPropertyValue(property, oldValue).GetHashCode().Should().NotBe(constructor().SetPropertyValue(property, newValue).GetHashCode());
  }
}