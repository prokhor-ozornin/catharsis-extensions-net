using System;

namespace Catharsis.Commons.Extensions
{
  [Serializable]
  [EqualsAndHashCode("PublicProperty")]
  public class TestObject : IDisposable
  {
    private object ReadOnlyProperty
    {
      get
      {
        return null;
      }
    }
    public static object PublicStaticProperty { get; set; }
    protected static object ProtectedStaticProperty { get; set; }
    private static object PrivateStaticProperty { get; set; }
    public object PublicProperty { get; set; }
    protected object ProtectedProperty { get; set; }
    private object PrivateProperty { get; set; }

    public static object PublicStaticField;
    protected static object ProtectedStaticField;
    private static object PrivateStaticField;
    public object PublicField;
    protected object ProtectedField;
    private object PrivateField;

    public delegate void PublicDelegate();
    protected delegate void ProtectedDelegate();
    private delegate void PrivateDelegate();
    
    public event PublicDelegate PublicEvent;
    protected event ProtectedDelegate ProtectedEvent;
    private event PrivateDelegate PrivateEvent;

    public TestObject()
    {
    }

    public TestObject(object publicProperty)
    {
      this.PublicProperty = publicProperty;
    }

    public static void PublicStaticMethod()
    {
    }

    protected static void ProtectedStaticMethod()
    {
    }

    private static void PrivateStaticMethod()
    {
    }

    public void PublicMethod()
    {
    }

    protected void ProtectedMethod()
    {
    }

    private void PrivateMethod()
    {
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}