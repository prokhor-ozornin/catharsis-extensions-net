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
See **Examples** page for more usage detail, as well as API documentation in distribution packages (work in progress).

**Part II**
Set of common domain classes, representing generic business domain entities, which can be used in many typical .NET applications. These classes are located in the _Catharsis.Commons.Domain_ namespace. The list of domain classes includes :
* _Announcement_, _AnnouncementsCategory_
* _Art_, _ArtsAlbum_
* _Article, _ArticlesCategory_
* _Audio_, _AudiosCategory_
* _Blog_, _BlogEntry_
* _City_
* _Comment_
* _Country_
* _Download_, _DownloadsCategory_
* _Dream_, _DreamsCategory_
* _Faq_
* _File_
* _Idea_, _IdeasCategory_
* _Image_, _ImagesCategory_
* _Location_
* _Person_
* _Playcast_, _PlaycastsCategory_
* _Poll_, _PollAnswer_, _PollOption_
* _Profile_
* _Quote_
* _Song_, _SongsAlbum_
* _Subscription_
* _Text_, _TextsCategory_
* _User_
* _Video_, _VideosCategory_
* _WebLink_, _WebLinksCategory_

As well several domain interfaces with extension methods are provided, and many of domain classes implement one or more of them. You can make your domain classes implement any of provided interface to quickly add additional functionality. The list of domain interfaces includes :
* _IAccessable_
* _IAuthorable_
* _ICommentable_
* _IDescriptable_
* _IEmailable_
* _IEntity_
* _IImageable_
* _IInetAddressable_
* _INameable_
* _IPersonalizable_
* _ISizeable_
* _IStatusable_
* _ITaggable_
* _ITextable_
* _ITimeable_
* _ITypeable_
* _IUrlAddressable_

In addition, extension methods for _Enumerable<T>_ generic interfaces for theses classes are provided for easy quering, searching and sorting.

As a bonus, _Catharsis.Commons.Assertion_ class is provided, which fills the gap between Java/Grails world with inlined language assertions and .NET CLR, being extensively used in the library itself as well.

**The story of name**
You may wonder, why the name - "Catharsis" ? The true answer, however, has been lost in times. Some folks say that it's a word that represents a spiritual inspiration, moving a man forward, while others argue that it has something to do with cats. Who really knows ...