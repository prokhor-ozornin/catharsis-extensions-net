using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Category")]
  public class Article : Item, IImageable
  {
    public string Annotation { get; set; }
    public ArticlesCategory Category { get; set; }
    public Image Image { get; set; }

    public Article()
    {
    }

    public Article(IDictionary<string, object> properties) : base(properties)
    {
    }

    public Article(string id, string language, string name, ArticlesCategory category, string annotation = null, string text = null, string authorId = null, Image image = null) : base(id, language, name, text, authorId)
    {
      this.Category = category;
      this.Annotation = annotation;
      this.Image = image;
    }
  }
}