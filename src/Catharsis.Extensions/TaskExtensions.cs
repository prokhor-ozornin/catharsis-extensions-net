namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for async tasks and multitasking types.</para>
/// </summary>
/// <seealso cref="Task"/>
public static class TaskExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public static Task Await(this Task task, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (task is null) throw new ArgumentNullException(nameof(task));

    if (task.IsCompleted)
    {
      return task;
    }

    cancellation.ThrowIfCancellationRequested();

    if (timeout is not null)
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
  /// <exception cref="ArgumentNullException"></exception>
  public static T Await<T>(this Task<T> task, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (task is null) throw new ArgumentNullException(nameof(task));

    if (task.IsCompleted)
    {
      return task.Result;
    }

    cancellation.ThrowIfCancellationRequested();
    
    if (timeout is not null)
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
  /// <exception cref="ArgumentNullException"></exception>
  public static Task<T> Await<T>(this Task<T> task, out T result, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (task is null) throw new ArgumentNullException(nameof(task));

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
  /// <exception cref="ArgumentNullException"></exception>
  public static Task Execute(this Task task, Action<Task> success = null, Action<Task> failure = null, Action<Task> cancellation = null) => task.ExecuteAsync(success, failure, cancellation).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Execute<T>(this Task<T> task, Action<Task<T>> success = null, Action<Task<T>> failure = null, Action<Task<T>> cancellation = null) => task is not null ? task.ExecuteAsync(success, failure, cancellation).Await() : throw new ArgumentNullException(nameof(task));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task ExecuteAsync(this Task task, Action<Task> success = null, Action<Task> failure = null, Action<Task> cancellation = null)
  {
    if (task is null) throw new ArgumentNullException(nameof(task));

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

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <param name="success"></param>
  /// <param name="failure"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> ExecuteAsync<T>(this Task<T> task, Action<Task<T>> success = null, Action<Task<T>> failure = null, Action<Task<T>> cancellation = null)
  {
    if (task is null) throw new ArgumentNullException(nameof(task));

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

    return task.Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="task"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ValueTask ToValueTask(this Task task) => task is not null ? new ValueTask(task) : throw new ArgumentNullException(nameof(task));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="task"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ValueTask<T> ToValueTask<T>(this Task<T> task) => task is not null ? new ValueTask<T>(task) : throw new ArgumentNullException(nameof(task));
}