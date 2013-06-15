using System;
using System.Collections.Generic;
using System.IO;
using Catharsis.Commons.Domain;
using Catharsis.Commons.Extensions;

namespace Example
{
  class Program
  {
    static void Main(string[] args)
    {
      // Will return "second,third"
      new HashSet<string>().AddNext("first").AddNext("second").RemoveNext("first").AddNext("third").Join(",");

      // Output word "Test" to console ten times
      1.UpTo(10, () => Console.Out.WriteLine("Test"));

      // HEX-encoded 1001 random bytes, and then replace "A" to "B" and "C" to "D" in encoded string
      new Random().Bytes(1001).EncodeHex().Replace(new[] { "A", "B" }, new[] { "C", "D" });

      // Creates instances of Article class by calling its default no-args constructor
      typeof(Article).NewInstance();

      // Creates instance of Article class by calling constructor with two arguments, and then serialize it to XML format
      typeof(Article).NewInstance("first", 10).Xml();

      // Won't add null element
      try
      {
        new List<string>().AsNonNullable().AddNext("first").AddNext("second").AddAll(new[] { "third", "fourth" }).Add(null);
      }
      catch (Exception e)
      {
      }

      // Returns text contents of file and checks whether it contains alphanumerical characters
      new FileInfo("myfile").Text().Match("[A-Za-z0-9]");

      // Decode GZIP'ped file, and calculates SHA512 hash from decoded data
      new FileInfo("myfile.gz").OpenRead().GZip().EncodeSHA512();

      // MD5 encoded data of file
      new FileInfo("myfile").Bytes().EncodeMD5();

      // Returns BASE-64 encoded HTML content of downloaded web page
      new Uri("http://yandex.ru").Stream().Bytes().EncodeBase64();

      // Load web page and return its contents
      new Uri("http://yandex.ru").Text();

      // Create new dictionary, populate it and serialize to binary format
      new Dictionary<string, object>().AddNext("article", new Article()).AddNext("blog", new Blog()).AddNext("text", new Text()).Binary();
    }
  }
}