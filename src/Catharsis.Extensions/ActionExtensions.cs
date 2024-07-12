namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for <see cref="Action"/> classes.</para>
/// </summary>
/// <seealso cref="Action{T}"/>
/// <seealso cref="Predicate{T}"/>
public static class ActionExtensions
{
  /// <summary>
  ///   <para>Executes a specified action continuously while a certain condition remains true.</para>
  /// </summary>
  /// <param name="action">Action to execute.</param>
  /// <param name="condition">Condition of execution.</param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="action"/> or <paramref name="condition"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Execute{T}(Action{T}, Predicate{T}, T)"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="action"/> or <paramref name="condition"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Execute(Action, Func{bool})"/>
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

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="action"></param>
  /// <param name="options"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTask(Action{object}, object, TaskCreationOptions, CancellationToken)"/>
  public static Task ToTask(this Action action, TaskCreationOptions options = TaskCreationOptions.None, CancellationToken cancellation = default) => action is not null ? new Task(action, cancellation, options) : throw new ArgumentNullException(nameof(action));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="action"></param>
  /// <param name="state"></param>
  /// <param name="options"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTask(Action, TaskCreationOptions, CancellationToken)"/>
  public static Task ToTask(this Action<object> action, object state, TaskCreationOptions options = TaskCreationOptions.None, CancellationToken cancellation = default) => action is not null ? new Task(action, state, cancellation, options) : throw new ArgumentNullException(nameof(action));
}