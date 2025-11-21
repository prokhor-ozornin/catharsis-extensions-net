namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for async tasks and multitasking types.</para>
/// </summary>
/// <seealso cref="ValueTask"/>
public static class ValueTaskExtensions
{
  /// <param name="task"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(ValueTask<T> task)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="Await(ValueTask, TimeSpan?, CancellationToken)"/>
    /// <seealso cref="Await{T}(ValueTask{T}, out T, TimeSpan?, CancellationToken)"/>
    public T Await(TimeSpan? timeout = null, CancellationToken cancellation = default)
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
    /// <param name="result"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <returns>Back self-reference to the given <paramref name="task"/>.</returns>
    /// <seealso cref="Await(ValueTask, TimeSpan?, CancellationToken)"/>
    /// <seealso cref="Await{T}(ValueTask{T}, TimeSpan?, CancellationToken)"/>
    public ValueTask<T> Await(out T result, TimeSpan? timeout = null, CancellationToken cancellation = default)
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
  }

  /// <param name="task"></param>
  extension(ValueTask task)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <returns>Back self-reference to the given <paramref name="task"/>.</returns>
    /// <seealso cref="Await{T}(ValueTask{T}, TimeSpan?, CancellationToken)"/>
    /// <seealso cref="Await{T}(ValueTask{T}, out T, TimeSpan?, CancellationToken)"/>
    public ValueTask Await(TimeSpan? timeout = null, CancellationToken cancellation = default)
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
    /// <param name="success"></param>
    /// <param name="failure"></param>
    /// <param name="cancellation"></param>
    /// <returns>Back self-reference to the given <paramref name="task"/>.</returns>
    /// <seealso cref="ExecuteAsync(ValueTask, Action{ValueTask}, Action{ValueTask}, Action{ValueTask})"/>
    public ValueTask Execute(Action<ValueTask> success = null, Action<ValueTask> failure = null, Action<ValueTask> cancellation = null) => task.ExecuteAsync(success, failure, cancellation).Await();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="success"></param>
    /// <param name="failure"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="Execute(ValueTask, Action{ValueTask}, Action{ValueTask}, Action{ValueTask})"/>
    public async ValueTask ExecuteAsync(Action<ValueTask> success = null, Action<ValueTask> failure = null, Action<ValueTask> cancellation = null)
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
}