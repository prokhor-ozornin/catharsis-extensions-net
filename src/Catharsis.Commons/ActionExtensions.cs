namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for actions and predicates.</para>
/// </summary>
/// <seealso cref="Action{T}"/>
/// <seealso cref="Predicate{T}"/>
public static class ActionExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="action"></param>
  /// <param name="condition"></param>
  /// <returns></returns>
  public static Action Execute(this Action action, Func<bool> condition)
  {
    if (action is null) throw new ArgumentNullException(nameof(action));
    if (condition is null) throw new ArgumentNullException(nameof(condition));

    while (condition())
    {
      action();
    }

    return action;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="action"></param>
  /// <param name="condition"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static Action<T> Execute<T>(this Action<T> action, Predicate<T> condition, T instance)
  {
    if (action is null) throw new ArgumentNullException(nameof(action));
    if (condition is null) throw new ArgumentNullException(nameof(condition));

    while (condition(instance))
    {
      action(instance);
    }

    return action;
  }
}