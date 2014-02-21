**Catharsis.Commons** is a library of extensions for many common .NET types. It can significantly speedup your daily .NET development tasks, relieving you of writing boilerplate code time and again. Do more by writing less code; write less code using a popular fluent-kind interface approach; safely perform many tasks in a single line of code with joined methods calls.

This library is extensively unit-tested, greatly reducing the possibility of bugs and quirks.

**Purpose** : Provide extension methods for common .NET types and common domain classes to simplify and speedup typical .NET business applications development process.

**Target** : .NET Framework 3.5/3.5 Client Profile/4.0/Silverlight 5

**NuGet package** : https://www.nuget.org/packages/Catharsis.Commons

### Structure

The list of extended structures, classes and interfaces includes:
* _System.Array_
* _System.Boolean_
* _System.DateTime_
* _System.Delegate_
* _System.Reflection.FieldInfo_
* _System.IO.FileInfo_
* _System.Collections.Generic.ICollection<T>_
* _System.Collections.Generic.IDictionary<TKey, TValue>_
* _System.Collections.Generic.IEnumerable<T>_
* _System.Collections.Generic.IList<T>_
* _System.Reflection.MemberInfo_
* _System.Reflection.MethodInfo_
* Numeric Types ( _System.Byte_, _System.Int16_, _System.Int32_, _System.Long_, System.Decimal, _System.Single_, _System.Double_, etc.)
* _System.Object_
* _System.Reflection.PropertyInfo_
* _System.Random_
* _System.IO.Stream_
* _System.String_
* _System.Security.Cryptography.SymmetricAlgorithm_
* _System.IO.TextReader_
* _System.IO.TextWriter_
* _System.Type_
* _System.Uri_
* _System.Xml.Linq.XContainer_
* _System.Xml.Linq.XDocument_
* _System.Xml.Linq.XElement_
* _System.Xml.XmlDocument_
* _System.Xml.XmlReader_
* _System.Xml.XmlWriter_

More extension methods can be added to the library on request.
See **Examples** for more usage detail, as well as API documentation in distribution packages (work in progress).

In addition, extension methods for _Enumerable<T>_ generic interfaces for theses classes are provided for easy quering, searching and sorting.

As a bonus, _Catharsis.Commons.Assertion_ class is provided, which fills the gap between Java/Grails world with inlined language assertions and .NET CLR, being extensively used in the library itself as well.

**The story of name**
You may wonder, why the name - "Catharsis" ? The true answer, however, has been lost in times. Some folks say that it's a word that represents a spiritual inspiration, moving a man forward, while others argue that it has something to do with cats. Who really knows ...


**Examples**

    // Will return "second,third"
    new HashSet<string>().AddNext("first").AddNext("second").RemoveNext("first").AddNext("third").Join(",");

    // Output word "Test" to console ten times
    1.UpTo(10, () => Console.Out.WriteLine("Test"));

    // HEX-encoded 1001 random bytes, and then replace "A" to "C" and "B" to "D" in encoded string
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

    // Returns text contents of file
    new FileInfo("myfile").Text();

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