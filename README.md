**Catharsis.Commons** is a library of extensions for many common .NET types. It can significantly speedup your daily .NET development tasks, relieving you of writing boilerplate code time and again. Do more by writing less code; write less code using a popular fluent-kind interface approach; safely perform many tasks in a single line of code with joined methods calls.

This library is extensively unit-tested, greatly reducing the possibility of bugs and quirks.

**Purpose** : Provide extension methods for common .NET types and common domain classes to simplify and speedup typical .NET business applications development process.

### Structure
This library consists of two main parts :

**Part I**
Extension methods for popular CLR runtime classes. They are located in _Catharsis.Commons.Extensions_ namespace.

The list of extended structures, classes and interfaces includes:
* _System.Array_
* _System.Boolean_
* _System.DateTime_
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
* _System.Xml.XmlDocument_
* _System.Xml.XmlReader_
* _System.Xml.XmlWriter_

More extension methods can be added to the library on request.
See **Examples** page for more usage detail, as well as API documentation in distribution packages (work in progress).

**Part II**
Set of common domain classes, representing generic business domain entities, which can be used in many typical .NET applications. These classes are located in the _Catharsis.Commons.Domain_ namespace. The list of domain classes includes :
* _Catharsis.Commons.Domain.Article_
* _Catharsis.Commons.Domain.ArticlesCategory_
* _Catharsis.Commons.Domain.Audio_
* _Catharsis.Commons.Domain.AudiosCategory_
* _Catharsis.Commons.Domain.Blog_
* _Catharsis.Commons.Domain.BlogEntry_
* _Catharsis.Commons.Domain.Comment_
* _Catharsis.Commons.Domain.Faq_
* _Catharsis.Commons.Domain.File_
* _Catharsis.Commons.Domain.Image_
* _Catharsis.Commons.Domain.ImagesCategory_
* _Catharsis.Commons.Domain.Location_
* _Catharsis.Commons.Domain.Person_
* _Catharsis.Commons.Domain.Playcast_
* _Catharsis.Commons.Domain.PlaycastsCategory_
* _Catharsis.Commons.Domain.Profile_
* _Catharsis.Commons.Domain.Quote_
* _Catharsis.Commons.Domain.Subscription_
* _Catharsis.Commons.Domain.Text_
* _Catharsis.Commons.Domain.TextsCategory_
* _Catharsis.Commons.Domain.User_
* _Catharsis.Commons.Domain.Video_
* _Catharsis.Commons.Domain.VideosCategory_
* _Catharsis.Commons.Domain.WebLink_
* _Catharsis.Commons.Domain.WebLinksCategory_

As well several domain interfaces with extension methods are provided, and many of domain classes implement one or more of them. You can make your domain classes implement any of provided interface to quickly add additional functionality. The list of domain interfaces includes :
* _Catharsis.Commons.Domain.IAccessable_
* _Catharsis.Commons.Domain.IAuthorable_
* _Catharsis.Commons.Domain.ICommentable_
* _Catharsis.Commons.Domain.IDescriptable_
* _Catharsis.Commons.Domain.IEmailable_
* _Catharsis.Commons.Domain.IEntity_
* _Catharsis.Commons.Domain.IImageable_
* _Catharsis.Commons.Domain.IInetAddressable_
* _Catharsis.Commons.Domain.INameable_
* _Catharsis.Commons.Domain.IPersonalizable_
* _Catharsis.Commons.Domain.ISizeable_
* _Catharsis.Commons.Domain.IStatusable_
* _Catharsis.Commons.Domain.ITaggable_
* _Catharsis.Commons.Domain.ITextable_
* _Catharsis.Commons.Domain.ITimeable_
* _Catharsis.Commons.Domain.ITypeable_
* _Catharsis.Commons.Domain.IUrlAddressable_

In addition, extension methods for _Enumerable<T>_ generic interfaces for theses classes are provided for easy quering, searching and sorting.

As a bonus, _Catharsis.Commons.Assertion_ class is provided, which fills the gap between Java/Grails world with inlined language assertions and .NET CLR, being extensively used in the library itself as well.

**The story of name**
You may wonder, why the name - "Catharsis" ? The true answer, however, has been lost in times. Some folks say that it's a word that represents a spiritual inspiration, moving a man forward, while others argue that it has something to do with cats. Who really knows ...