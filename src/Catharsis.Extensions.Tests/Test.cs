using System.Security;
using System.Security.Cryptography;
using AutoFixture;
using FluentAssertions;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para></para>
/// </summary>
public class Test : IDisposable
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected IFixture Fixture { get; } = new Fixture();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Random Random { get; } = new();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected byte[] Bytes { get; } = new Random().ToByte(short.MaxValue).AsArray();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected object[] Objects { get; } = new Random().ToObject(short.MaxValue).AsArray();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected string Shell => "cmd.exe";
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected IAsyncEnumerable<object> EmptyAsyncEnumerable { get; } = Enumerable.Empty<object>().ToAsyncEnumerable();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected SecureString EmptySecureString { get; } = new();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected SecureString RandomSecureString { get; } = new Random().ToSecureString(short.MaxValue, ['a'..'z', 'A'..'Z']);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected SymmetricAlgorithm SymmetricAlgorithm { get; } = Aes.Create();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected HashAlgorithm HashAlgorithm { get; } = SHA512.Create();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected TextReader EmptyTextReader { get; } = string.Empty.ToStringReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream EmptyStream { get; } = new MemoryStream();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected MemoryStream Stream { get; } = new Random().ToMemoryStreamAsync(short.MaxValue).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream ReadOnlyStream { get; } = new Random().ToMemoryStreamAsync(short.MaxValue).Await().AsReadOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream ReadOnlyForwardStream { get; } = new Random().ToMemoryStreamAsync(short.MaxValue).Await().AsReadOnlyForward();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream WriteOnlyStream { get; } = new MemoryStream().AsWriteOnly();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream WriteOnlyForwardStream { get; } = new MemoryStream().AsWriteOnlyForward();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected FileInfo FakeFile { get; } = new Random().ToFilePath().ToFile();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected FileInfo EmptyFile { get; } = new Random().ToFile();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected FileInfo NonEmptyFile { get; } = new Random().ToTextFileAsync(short.MaxValue).Await();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected DirectoryInfo Directory { get; } = new Random().ToDirectory();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected DirectoryInfo FakeDirectory { get; } = new Random().DirectoryPath().ToDirectory();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Test()
  {
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
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

  /// <summary>
  ///   <para></para>
  /// </summary>
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

  /// <summary>
  ///   <para></para>
  /// </summary>
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

  /// <summary>
  ///   <para></para>
  /// </summary>
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