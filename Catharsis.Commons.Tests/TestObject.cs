using System;
using System.ComponentModel;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  [EqualsAndHashCode("PublicProperty")]
  public class TestObject : IDisposable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    [Description]
    [DisplayName]
    private object ReadOnlyProperty
    {
      get { return null; }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    [Description("PublicStaticProperty Description")]
    [DisplayName("PublicStaticProperty")]
    public static object PublicStaticProperty { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    protected static object ProtectedStaticProperty { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    private static object PrivateStaticProperty { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    [Description("PublicProperty")]
    public object PublicProperty { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    [DisplayName("ProtectedProperty")]
    protected object ProtectedProperty { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    private object PrivateProperty { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    [Description]
    public static object PublicStaticField;
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    protected static object ProtectedStaticField;
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    private static object PrivateStaticField;
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    [Description]
    public object PublicField;
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    protected object ProtectedField;
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    private object PrivateField;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public delegate void PublicDelegate();
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    protected delegate void ProtectedDelegate();
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    private delegate void PrivateDelegate();
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public event PublicDelegate PublicEvent;
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    protected event ProtectedDelegate ProtectedEvent;
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    private event PrivateDelegate PrivateEvent;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public TestObject()
    {
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="publicProperty"></param>
    public TestObject(object publicProperty)
    {
      this.PublicProperty = publicProperty;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public static void PublicStaticMethod()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    protected static void ProtectedStaticMethod()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    private static void PrivateStaticMethod()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    [Description]
    public void PublicMethod()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    protected void ProtectedMethod()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    private void PrivateMethod()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}