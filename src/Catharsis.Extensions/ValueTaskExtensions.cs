namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for async tasks and multitasking types.</para>
/// </summary>
/// <seealso cref="ValueTask"/>
public static class ValueTaskExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  public static ValueTask Await(this ValueTask task, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (task.IsCompleted)
    {
      return task;
    }

    cancellation.ThrowIfCancellationRequested();
    
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
  public static T Await<T>(this ValueTask<T> task, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (task.IsCompleted)
    {
      return task.Result;
    }

    cancellation.ThrowIfCancellationRequested();

    return task.AsTask().Await(timeout, cancellation);
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
  public static ValueTask<T> Await<T>(this ValueTask<T> task, out T result, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (task.IsCompleted)
    {
      result = task.Result;
      return task;
    }

    cancellation.ThrowIfCancellationRequested();

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
  public static ValueTask Execute(this ValueTask task, Action<ValueTask> success = null, Action<ValueTask> failure = null, Action<ValueTask> cancellation = null) => task.ExecuteAsync(success, failure, cancellation).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async ValueTask ExecuteAsync(this ValueTask task, Action<ValueTask> success = null, Action<ValueTask> failure = null, Action<ValueTask> cancellation = null)
  {
    await task.ConfigureAwait(false);

    if (task.IsCompletedSuccessfully && success is not null)
    {
      success(task);
    }
    else if (task.IsFaulted && failure is not null)
    {
      failure(task);
    }
    else if (task.IsCanceled && cancellation is not null)
    {
      cancellation(task);
    }
  }
}