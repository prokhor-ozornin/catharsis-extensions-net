namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for async tasks and multitasking types.</para>
/// </summary>
/// <seealso cref="Task"/>
/// <seealso cref="ValueTask"/>
public static class TaskExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  public static ValueTask Await(this ValueTask task, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    task.AsTask().Await(timeout, cancellation);
    return task;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static T Await<T>(this ValueTask<T> task, TimeSpan? timeout = null, CancellationToken cancellation = default) => task.AsTask().Await(timeout, cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="result"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static ValueTask<T> Await<T>(this ValueTask<T> task, out T result, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    result = task.Await(timeout, cancellation);
    return task;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  public static Task Await(this Task task, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (timeout != null)
    {
      task.Wait((int) timeout.Value.TotalMilliseconds, cancellation);
    }
    else
    {
      task.Wait(cancellation);
    }

    return task;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static T Await<T>(this Task<T> task, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (timeout != null)
    {
      task.Wait((int) timeout.Value.TotalMilliseconds, cancellation);
    }
    else
    {
      task.Wait(cancellation);
    }

    return task.Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="result"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static Task<T> Await<T>(this Task<T> task, out T result, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    result = task.Await(timeout, cancellation);
    return task;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async ValueTask Execute(this ValueTask task, Action<ValueTask>? success = null, Action<ValueTask>? failure = null, Action<ValueTask>? cancellation = null)
  {
    await task;

    if (task.IsCompletedSuccessfully && success != null)
    {
      success(task);
    }
    else if (task.IsFaulted && failure != null)
    {
      failure(task);
    }
    else if (task.IsCanceled && cancellation != null)
    {
      cancellation(task);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static ValueTask Execute(this ValueTask task, Action? success = null, Action? failure = null, Action? cancellation = null) => task.Execute(_ => success?.Invoke(), _ => failure?.Invoke(), _ => cancellation?.Invoke());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task Execute(this Task task, Action<Task>? success = null, Action<Task>? failure = null, Action<Task>? cancellation = null)
  {
    await task;

    if (task.IsCompletedSuccessfully && success != null)
    {
      success(task);
    }
    else if (task.IsFaulted && failure != null)
    {
      failure(task);
    }
    else if (task.IsCanceled && cancellation != null)
    {
      cancellation(task);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static Task Execute(this Task task, Action? success = null, Action? failure = null, Action? cancellation = null) => task.Execute(_ => success?.Invoke(), _ => failure?.Invoke(), _ => cancellation?.Invoke());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static Task<T> Execute<T>(this Task<T> task, Action? success = null, Action? failure = null, Action? cancellation = null) => task.Execute(_ => success?.Invoke(), _ => failure?.Invoke(), _ => cancellation?.Invoke());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> Execute<T>(this Task<T> task, Action<Task<T>>? success = null, Action<Task<T>>? failure = null, Action<Task<T>>? cancellation = null)
  {
    await task;

    if (task.IsCompletedSuccessfully && success != null)
    {
      success(task);
    }
    else if (task.IsFaulted && failure != null)
    {
      failure(task);
    }
    else if (task.IsCanceled && cancellation != null)
    {
      cancellation(task);
    }

    return task.Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="action"></param>
  /// <param name="cancellation"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  public static Task ToTask(this Action action, CancellationToken cancellation = default, TaskCreationOptions options = TaskCreationOptions.None) => new(action, cancellation, options);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="action"></param>
  /// <param name="state"></param>
  /// <param name="cancellation"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  public static Task ToTask(this Action<object?> action, object? state, CancellationToken cancellation = default, TaskCreationOptions options = TaskCreationOptions.None) => new(action, state, cancellation, options);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="function"></param>
  /// <param name="cancellation"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  public static Task<T> ToTask<T>(this Func<T> function, CancellationToken cancellation = default, TaskCreationOptions options = TaskCreationOptions.None) => new(function, cancellation, options);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="function"></param>
  /// <param name="state"></param>
  /// <param name="cancellation"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  public static Task<T> ToTask<T>(this Func<object?, T> function, object? state, CancellationToken cancellation = default, TaskCreationOptions options = TaskCreationOptions.None) => new(function, state, cancellation, options);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <returns></returns>
  public static ValueTask ToValueTask(this Task task) => new(task);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <returns></returns>
  public static ValueTask<T> ToValueTask<T>(this Task<T> task) => new(task);
}