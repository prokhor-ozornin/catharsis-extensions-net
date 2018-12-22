**Catharsis.Commons** is a library of extensions for many common .NET types. It can significantly speedup your daily .NET development tasks, relieving you of writing boilerplate code time and again. Do more by writing less code; write less code using a popular fluent-kind interface approach; safely perform many tasks in a single line of code with joined methods calls.

This library is extensively unit-tested, greatly reducing the possibility of bugs and quirks.

**Purpose** : Provide extension methods for common .NET types and common domain classes to simplify and speedup typical .NET business applications development process.

**Target** : .NET Framework 4.5.2/4.6.2/4.7.2, .NET Core 2.2, .NET Standard 2.0

**NuGet package** : https://www.nuget.org/packages/Catharsis.Commons

***

**Support**

This project needs your support for further developments ! Please consider donating.

- _Yandex.Money_ : 41001577953208

[![Image](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=APHM8MU9N76V8 "Donate")

***

**Structure**

The list of extended structures, classes and interfaces includes:

* _System.Array_
* _System.Reflection.Assembly_
* _System.Boolean_
* _System.DateTime_
* _System.Delegate_
* _System.Enum_
* _System.Reflection.FieldInfo_
* _System.IO.FileInfo_
* _System.Collections.Generic.ICollection<T>_
* _System.Collections.Generic.IEnumerable<T>_
* _System.Linq.IQueryable<T>_
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

***

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

***

**Full list of extension methods**


**System.Array**

`byte[] MD5(this byte[])`

`byte[] SHA1(this byte[])`

`byte[] SHA256(this byte[])`

`byte[] SHA512(this byte[])`

`string Base64(this byte[])`

`byte[] Bytes(this char[], [System.Text.Encoding])`

`string Hex(this byte[])`

`T[] Join<T>(this T[], T[])`

`string String(this char[])`

`string String(this byte[], [System.Text.Encoding])`

**System.Reflection.Assembly**

`string Resource(this System.Reflection.Assembly, string, [System.Text.Encoding])`


**System.Boolean**

`bool And(this bool, bool)`

`bool Not(this bool)`

`bool Or(this bool, bool)`

`bool Xor(this bool, bool)`


**System.DateTime**

`DateTime DownTo(this System.DateTime, System.DateTime, System.Action)`

`DateTime EndOfDay(this System.DateTime)`

`DateTime EndOfMonth(this System.DateTime)`

`DateTime EndOfYear(this System.DateTime)`

`bool Friday(this System.DateTime)`

`string ISO8601(this System.DateTime)`

`bool IsSameDate(this System.DateTime, System.DateTime)`

`bool IsSameTime(this System.DateTime, System.DateTime)`

`bool Monday(this System.DateTime)`

`DateTime NextDay(this System.DateTime)`

`DateTime NextMonth(this System.DateTime)`

`DateTime NextYear(this System.DateTime)`

`DateTime PreviousDay(this System.DateTime)`

`DateTime PreviousMonth(this System.DateTime)`

`DateTime PreviousYear(this System.DateTime)`

`string RFC1121(this System.DateTime)`

`bool Saturday(this System.DateTime)`

`DateTime StartOfDay(this System.DateTime)`

`DateTime StartOfMonth(this System.DateTime)`

`DateTime StartOfYear(this System.DateTime)`

`bool Sunday(this System.DateTime)`

`bool Thursday(this System.DateTime)`

`bool Tuesday(this System.DateTime)`

`DateTime UpTo(this System.DateTime, System.DateTime, System.Action)`

`bool Wednesday(this System.DateTime)`


**System.Delegate**

`Delegate And(this System.Delegate, System.Delegate)`

`Delegate Not(this System.Delegate, System.Delegate)`


**System.Enum**

`string Description(this System.Enum)`

`IEnumerable<string> Descriptions<T>()`


**System.Reflection.FieldInfo**

`bool IsProtected(this System.Reflection.FieldInfo)`


**System.IO.FileInfo**

`FileInfo Append(this System.IO.FileInfo, byte[])`

`FileInfo Append(this System.IO.FileInfo, string, [System.Text.Encoding])`

`FileInfo Append(this System.IO.FileInfo, System.IO.Stream)`

`byte[] Bytes(this System.IO.FileInfo)`

`FileInfo Clear(this System.IO.FileInfo)`

`IList<string> Lines(this System.IO.FileInfo, [System.Text.Encoding])`

`string Text(this System.IO.FileInfo, [System.Text.Encoding])`


**System.Collections.Generic.ICollection<T>**

`ICollection<T> Add<T>(this System.Collections.Generic.ICollection<T>, 
System.Collections.Generic.IEnumerable<T>)`

`ICollection<T> Remove<T>(this System.Collections.Generic.ICollection<T>, 
System.Collections.Generic.IEnumerable<T>)`


**System.Collections.Generic.IEnumerable<T>**

`IEnumerable<T> Each<T>(this System.Collections.Generic.IEnumerable<T>, System.Action<T>)`

`string Join<T>(this System.Collections.Generic.IEnumerable<T>, string)`

`IEnumerable<T> Paginate<T>(this System.Collections.Generic.IEnumerable<T>, [int], [int])`

`T Random<T>(this System.Collections.Generic.IEnumerable<T>)`

`string ToListString<T>(this System.Collections.Generic.IEnumerable<T>)`

`ISet<T> ToSet<T>(this System.Collections.Generic.IEnumerable<T>)`


**System.Linq.IQueryable<T>**

`IQueryable<T> Paginate<T>(this System.Linq.IQueryable<T>, [int], [int])`

`T Random<T>(this System.Linq.IQueryable<T>)`


**System.Reflection.MemberInfo**

`string Description(this System.Reflection.MemberInfo)`

`object Attribute(this System.Reflection.MemberInfo, System.Type)`

`T Attribute<T>(this System.Reflection.MemberInfo)`

`IEnumerable<object> Attributes(this System.Reflection.MemberInfo, System.Type)`

`IEnumerable<T> Attributes<T>(this System.Reflection.MemberInfo)`

`bool IsConstructor(this System.Reflection.MemberInfo)`

`bool IsEvent(this System.Reflection.MemberInfo)`

`bool IsField(this System.Reflection.MemberInfo)`

`bool IsMethod(this System.Reflection.MemberInfo)`

`bool IsProperty(this System.Reflection.MemberInfo)`

`Type Type(this System.Reflection.MemberInfo)`


**System.Reflection.MethodInfo**

`Delegate Delegate(this System.Reflection.MethodInfo, System.Type)`

`Delegate Delegate<T>(this System.Reflection.MethodInfo)`


**Numeric Types**

`short Abs(this short)`

`int Abs(this int)`

`long Abs(this long)`

`float Abs(this float)`

`double Abs(this double)`

`decimal Abs(this decimal)`

`double Ceil(this double)`

`TimeSpan Days(this byte)`

`TimeSpan Days(this short)`

`TimeSpan Days(this int)`

`void DownTo(this byte, byte, System.Action)`

`void DownTo(this short, short, System.Action)`

`void DownTo(this int, int, System.Action)`

`void DownTo(this long, long, System.Action)`

`bool Even(this byte)`

`bool Even(this short)`

`bool Even(this int)`

`bool Even(this long)`

`double Floor(this double)`

`TimeSpan Hours(this byte)`

`TimeSpan Hours(this short)`

`TimeSpan Hours(this int)`

`TimeSpan Milliseconds(this byte)`

`TimeSpan Milliseconds(this short)`

`TimeSpan Milliseconds(this int)`

`TimeSpan Minutes(this byte)`

`TimeSpan Minutes(this short)`

`TimeSpan Minutes(this int)`

`double Power(this double, double)`

`double Round(this double)`

`decimal Round(this decimal)`

`TimeSpan Seconds(this byte)`

`TimeSpan Seconds(this short)`

`TimeSpan Seconds(this int)`

`double Sqrt(this double)`

`void Times(this byte, System.Action)`

`void Times(this short, System.Action)`

`void Times(this int, System.Action)`

`void Times(this long, System.Action)`

`void UpTo(this byte, byte, System.Action)`

`void UpTo(this short, short, System.Action)`

`void UpTo(this int, int, System.Action)`

`void UpTo(this long, long, System.Action)`


**System.Object**

`object Binary(this object, System.IO.Stream, [bool])`

`byte[] Binary(this object)`

`T As<T>(this object)`

`OUTPUT Do<SUBJECT, OUTPUT>(this SUBJECT, System.Func<SUBJECT,OUTPUT>)`

`T Do<T>(this T, System.Action<T>)`

`string Dump(this object)`

`bool Equality<T>(this T, T, params string[])`

`bool Equality<T>(this T, T, params System.Linq.Expressions.Expression<System.Func<T,object>>[])`

`object Field(this object, string)`

`int GetHashCode<T>(this T, System.Collections.Generic.IEnumerable<string>)`

`int GetHashCode<T>(this T, params System.Linq.Expressions.Expression<System.Func<T,object>>[])`

`bool Is<T>(this object)`

`bool IsFalse(this object)`

`bool IsNumeric(this object)`

`bool IsTrue(this object)`

`MEMBER Member<T, MEMBER>(this T, System.Linq.Expressions.Expression<System.Func<T,MEMBER>>)`

`object Method(this object, string, params object[])`

`T Properties<T>(this T, System.Collections.Generic.IDictionary<string,object>)`

`T Properties<T>(this T, object)`

`IDictionary<string, object> PropertiesMap(this object)`

`object Property(this object, string)`

`T Property<T>(this T, string, object)`

`T To<T>(this object)`

`string ToString(this object, System.Collections.Generic.IEnumerable<string>)`

`string ToString<T>(this T, params System.Linq.Expressions.Expression<System.Func<T,object>>[])`

`string ToStringInvariant(this object)`

`string ToXml<T>(this T, params System.Type[])`

`T ToXml<T>(this T, System.IO.Stream, [System.Text.Encoding], params System.Type[])`

`T ToXml<T>(this T, System.IO.TextWriter, params System.Type[])`

`T ToXml<T>(this T, System.Xml.XmlWriter, params System.Type[])`


**System.Reflection.PropertyInfo**

`bool PropertyInfoExtensions.IsPublic(this System.Reflection.PropertyInfo)`


**System.Random**

`byte[] Bytes(this System.Random, int)`


**System.IO.Stream**

`DeflateStream Deflate(this System.IO.Stream, System.IO.Compression.CompressionMode)`

`byte[] Deflate(this System.IO.Stream)`

`STREAM Deflate<STREAM>(this STREAM, byte[])`

`GZipStream GZip(this System.IO.Stream, System.IO.Compression.CompressionMode)`

`byte[] GZip(this System.IO.Stream)`

`STREAM GZip<STREAM>(this STREAM, byte[])`

`BufferedStream Buffered(this System.IO.Stream, [int?])`

`XDocument AsXDocument(this System.IO.Stream, [bool])`

`BinaryReader BinaryReader(this System.IO.Stream, [System.Text.Encoding])`

`BinaryWriter BinaryWriter(this System.IO.Stream, [System.Text.Encoding])`

`byte[] Bytes(this System.IO.Stream, [bool])`

`T Rewind<T>(this T)`

`string Text(this System.IO.Stream, [bool], [System.Text.Encoding])`

`TextReader TextReader(this System.IO.Stream, [System.Text.Encoding])`

`TextWriter TextWriter(this System.IO.Stream, [System.Text.Encoding])`

`T Write<T>(this T, byte[])`

`T Write<T>(this T, System.IO.Stream)`

`T Write<T>(this T, string, [System.Text.Encoding])`

`XmlReader XmlReader(this System.IO.Stream, [bool])`

`XmlWriter XmlWriter(this System.IO.Stream, [bool], [System.Text.Encoding])`

`T AsXml<T>(this System.IO.Stream, [bool], params System.Type[])`

`XmlDocument AsXmlDocument(this System.IO.Stream, [bool])`


**System.String**

`SecureString Secure(this string)`

`string Capitalize(this string, [System.Globalization.CultureInfo])`

`byte[] Base64(this string)`

`byte[] Bytes(this string, [System.Text.Encoding], [bool])`

`int CompareTo(this string, string, System.StringComparison)`

`int CompareTo(this string, string, System.Globalization.CompareOptions, [System.Globalization.CultureInfo])`

`string Drop(this string, int)`

`byte[] Hex(this string)`

`bool IsBoolean(this string)`

`bool IsDateTime(this string)`

`bool IsDouble(this string)`

`bool IsEmpty(this string)`

`bool IsGuid(this string)`

`bool IsInteger(this string)`

`bool IsIpAddress(this string)`

`bool IsMatch(this string, string, [System.Text.RegularExpressions.RegexOptions?])`

`bool IsUri(this string)`

`string[] Lines(this string)`

`MatchCollection Matches(this string, string, [System.Text.RegularExpressions.RegexOptions?])`

`string Multiply(this string, int)`

`string Prepend(this string, string)`

`string Replace(this string, System.Collections.Generic.IDictionary<string,string>)`

`string Replace(this string, System.Collections.Generic.IEnumerable<string>, 
System.Collections.Generic.IEnumerable<string>)`

`string SwapCase(this string, [System.Globalization.CultureInfo])`

`bool ToBoolean(this string)`

`bool ToBoolean(this string, out bool)`

`byte ToByte(this string)`

`bool ToByte(this string, out byte)`

`DateTime ToDateTime(this string)`

`bool ToDateTime(this string, out System.DateTime)`

`decimal ToDecimal(this string)`

`bool ToDecimal(this string, out decimal)`

`double ToDouble(this string)`

`bool ToDouble(this string, out double)`

`T ToEnum<T>(this string)`

`Guid ToGuid(this string)`

`bool ToGuid(this string, out System.Guid)`

`short ToInt16(this string)`

`bool ToInt16(this string, out short)`

`int ToInt32(this string)`

`bool ToInt32(this string, out int)`

`long ToInt64(this string)`

`bool ToInt64(this string, out long)`

`IPAddress ToIpAddress(this string)`

`bool ToIpAddress(this string, out System.Net.IPAddress)`

`string Tokenize(this string, System.Collections.Generic.IDictionary<string,object>, [string])`

`Regex ToRegex(this string)`

`Single ToSingle(this string)`

`bool ToSingle(this string, out float)`

`Uri ToUri(this string)`

`bool ToUri(this string, out System.Uri)`

`bool Whitespace(this string)`

`string HtmlDecode(this string)`

`string HtmlEncode(this string)`

`string UrlDecode(this string)`

`string UrlEncode(this string)`

`T AsXml<T>(this string, params System.Type[])`


**System.Security.Cryptography.SymmetricAlgorithm**

`byte[] Decrypt(this System.Security.Cryptography.SymmetricAlgorithm, byte[])`

`byte[] Decrypt(this System.Security.Cryptography.SymmetricAlgorithm, System.IO.Stream, [bool])`

`byte[] Encrypt(this System.Security.Cryptography.SymmetricAlgorithm, byte[])`

`byte[] Encrypt(this System.Security.Cryptography.SymmetricAlgorithm, System.IO.Stream, [bool])`


**System.IO.TextReader**

`string[] Lines(this System.IO.TextReader, [bool])`

`string Text(this System.IO.TextReader, [bool])`

`XDocument XDocument(this System.IO.TextReader, [bool])`

`XmlReader XmlReader(this System.IO.TextReader, [bool])`

`T AsXml<T>(this System.IO.TextReader, [bool], params System.Type[])`


**System.IO.TextWriter**

`XmlWriter TextWriterExtensions.XmlWriter(this System.IO.TextWriter, [bool], [System.Text.Encoding])`


**System.TimeSpan**

`DateTime BeforeNow(this System.TimeSpan)`

`DateTime BeforeNowUtc(this System.TimeSpan)`

`DateTime FromNow(this System.TimeSpan)`

`DateTime FromNowUtc(this System.TimeSpan)`


**System.Type**

`EventInfo AnyEvent(this System.Type, string)`

`FieldInfo AnyField(this System.Type, string)`

`MethodInfo AnyMethod(this System.Type, string)`

`PropertyInfo AnyProperty(this System.Type, string)`

`ConstructorInfo DefaultConstructor(this System.Type)`

`bool HasField(this System.Type, string)`

`bool HasMethod(this System.Type, string)`

`bool HasProperty(this System.Type, string)`

`bool Implements(this System.Type, System.Type)`

`bool Implements<T>(this System.Type)`

`IEnumerable<Type> Inherits(this System.Type)`

`bool IsAnonymous(this System.Type)`

`bool IsAssignableTo<T>(this System.Type)`

`object NewInstance(this System.Type, params object[])`

`object NewInstance(this System.Type, System.Collections.Generic.IDictionary<string,object>)`

`object NewInstance(this System.Type, object)`

`PropertyInfo[] Properties(this System.Type)`


**System.Uri**

`byte[] Bytes(this System.Uri)`

`byte[] Bytes(this System.Uri, [System.Collections.Generic.IDictionary<string,object>])`

`byte[] Bytes(this System.Uri, [System.Collections.Generic.IDictionary<string,string>])`

`byte[] Bytes(this System.Uri, [object])`

`IPHostEntry Host(this System.Uri)`

`Uri Query(this System.Uri, System.Collections.Generic.IDictionary<string,object>)`

`Uri Query(this System.Uri, System.Collections.Generic.IDictionary<string,string>)`

`Uri Query(this System.Uri, object)`

`Stream Stream(this System.Uri)`

`Stream Stream(this System.Uri, [System.Collections.Generic.IDictionary<string,object>])`

`Stream Stream(this System.Uri, [System.Collections.Generic.IDictionary<string,string>])`

`Stream Stream(this System.Uri, [object])`

`string Text(this System.Uri)`

`string Text(this System.Uri, [System.Collections.Generic.IDictionary<string,object>])`

`string Text(this System.Uri, [System.Collections.Generic.IDictionary<string,string>])`

`string Text(this System.Uri, [object])`


**System.Xml.Linq.XDocument**

`IDictionary<string, object> Dictionary(this System.Xml.Linq.XDocument)`


**System.Xml.Linq.XElement**

`IDictionary<string, object> Dictionary(this System.Xml.Linq.XElement)`


**System.Xml.XmlDocument**

`IDictionary<string, object> Dictionary(this System.Xml.XmlDocument)`

`string String(this System.Xml.XmlDocument)`

`XmlDocument XmlDocument(this System.IO.TextReader, [bool])`


**System.Xml.XmlReader**

`IDictionary<string, object> Dictionary(this System.Xml.XmlReader, [bool])`

`RESULT Read<READER, RESULT>(this READER, System.Func<READER,RESULT>)`

`object Deserialize(this System.Xml.XmlReader, System.Type, 
[System.Collections.Generic.IEnumerable<System.Type>], [bool])`

`T Deserialize<T>(this System.Xml.XmlReader, [System.Collections.Generic.IEnumerable<System.Type>], [bool])`


**System.Xml.XmlWriter**

`WRITER Write<WRITER>(this WRITER, System.Action<WRITER>)`