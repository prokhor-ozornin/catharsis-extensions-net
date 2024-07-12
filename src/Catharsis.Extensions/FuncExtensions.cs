namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for async tasks and multitasking types.</para>
/// </summary>
/// <seealso cref="Func{T, TResult}"/>
public static class FuncExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="function"></param>
  /// <param name="options"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="function"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTask{T}(Func{object, T}, object, TaskCreationOptions, CancellationToken)"/>
  public static Task<T> ToTask<T>(this Func<T> function, TaskCreationOptions options = TaskCreationOptions.None, CancellationToken cancellation = default) => function is not null ? new Task<T>(function, cancellation, options) : throw new ArgumentNullException(nameof(function));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="function"></param>
  /// <param name="state"></param>
  /// <param name="options"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="function"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTask{T}(Func{T}, TaskCreationOptions, CancellationToken)"/>
  public static Task<T> ToTask<T>(this Func<object, T> function, object state, TaskCreationOptions options = TaskCreationOptions.None, CancellationToken cancellation = default) => function is not null ? new Task<T>(function, state, cancellation, options) : throw new ArgumentNullException(nameof(function));
}