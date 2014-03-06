using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Represents collection of methods to perform different kinds of assertions on objects.</para>
  /// </summary>
  public static class Assertion
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="message"></param>
    public static void Default<T>(this T value, string message = null) where T : struct
    {
      if (value.Equals(default(T)))
      {
        return;
      }

      if (message.Whitespace())
      {
        throw new ArgumentException("Argument doesn't has a default value for type {0}. Expected : {1}. Actual : {2}".FormatSelf(typeof(T), default(T), value));
      }

      throw new ArgumentException(message);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="sequence"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void Empty(IEnumerable sequence, string message = null)
    {
      Empty(sequence != null ? sequence.Cast<object>() : null, message);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sequence"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void Empty<T>(IEnumerable<T> sequence, string message = null)
    {
      if (sequence != null && sequence.Any())
      {
        throw new ArgumentException(string.Format("Sequence {0} is not empty{1}", sequence.ToListString(), message.Whitespace() ? string.Empty : " : " + message));
      }
    }

    /// <summary>
    ///   <para>Makes an assertion about equality of two objects of the same type, based on their <see cref="object.Equals(object)"/> method. Throws an exception if assertion failed.</para>
    /// </summary>
    /// <param name="expected">First object to be compared.</param>
    /// <param name="actual">Second object to be compared.</param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException">If two objects are not equal.</exception>
    public static void Equal(object expected, object actual, string message = null)
    {
      if (expected == null || actual == null || !Equals(expected, actual))
      {
        throw new ArgumentException(string.Format("Objects were expected to be equal, but there were not. Expected : {0}. Actual : {1}{2}", expected, actual, message.Whitespace() ? string.Empty : " : " + message));
      }
    }

    /// <summary>
    ///   <para>Makes an assertion that specified condition if <c>false</c>. Throws an exception if assertion failed.</para>
    /// </summary>
    /// <param name="condition">Condition to be evaluated.</param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException">If <paramref name="condition"/> if <c>true</c>.</exception>
    /// <seealso cref="True(bool, string)"/>
    public static void False(bool condition, string message = null)
    {
      if (condition)
      {
        throw new ArgumentException("Specified condition is not false{0}", message.Whitespace() ? string.Empty : " : " + message);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="message"></param>
    public static void NotDefault<T>(T value, string message = null) where T : struct
    {
      if (!value.Equals(default(T)))
      {
        return;
      }

      if (message.Whitespace())
      {
        throw new ArgumentException("Argument has a default value for type {0} : {1}".FormatSelf(typeof(T), value));
      }

      throw new ArgumentException(message);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void NotEmpty(IEnumerable collection, string message = null)
    {
      NotEmpty(collection.Cast<object>(), message);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sequence"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void NotEmpty<T>(IEnumerable<T> sequence, string message = null)
    {
      NotNull(sequence);

      if (!sequence.Any())
      {
        throw new ArgumentException(string.Format("Sequence {0} is empty{1}", sequence.ToListString(), message.Whitespace() ? string.Empty : " : " + message));
      }
    }

    /// <summary>
    ///   <para>Makes an assertion about inequality of two objects, based on their <see cref="object.Equals(object)"/> method implementation. Throws an exception if assertion failed.</para>
    /// </summary>
    /// <param name="notExpected">First object to be compared.</param>
    /// <param name="actual">Second object </param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException">If two objects are equal.</exception>
    public static void NotEqual(object notExpected, object actual, string message = null)
    {
      if (notExpected != null && actual != null && Equals(notExpected, actual))
      {
        throw new ArgumentException(string.Format("Objects were expected to be different, but they were equal. Expected {0}, but actual is {1}{2}", notExpected, actual, message.Whitespace() ? string.Empty : " : " + message));
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public static void NotNull(object value, string message = null)
    {
      if (value != null)
      {
        return;
      }

      if (message.Whitespace())
      {
        throw new ArgumentNullException();
      }

      throw new ArgumentNullException(message);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void NotWhitespace(string value, string message = null)
    {
      NotNull(value);

      if (value.Whitespace())
      {
        throw new ArgumentException(string.Format("Given string is either empty or contains only whitespace characters{0}", message.Whitespace() ? string.Empty : " : " + message));
      }
    }

    /// <summary>
    ///   <para>Makes an assertion that specified object reference is a <c>null</c> reference. Throws an exception if assertion failed.</para>
    /// </summary>
    /// <param name="value">Object to evaluate.</param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is not a <c>null</c> reference.</exception>
    public static void Null(object value, string message = null)
    {
      if (value != null)
      {
        throw new ArgumentException(string.Format("Argument was expected to be null, but is {0}{1}", value, message.Whitespace() ? string.Empty : " : " + message));
      }
    }

    /// <summary>
    ///   <para>Makes an assertion that specified condition is <c>true</c>. Throws an exception if assertion failed.</para>
    /// </summary>
    /// <param name="condition">Condition to be evaluated.</param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException">If <paramref name="condition"/> is <c>false</c>.</exception>
    /// <seealso cref="False(bool, string)"/>
    public static void True(bool condition, string message = null)
    {
      if (!condition)
      {
        throw new ArgumentException(string.Format("Specified condition is not true{0}", message.Whitespace() ? string.Empty : " : " + message));
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void Whitespace(string value, string message = null)
    {
      if (!value.Whitespace())
      {
        throw new ArgumentException(string.Format("Given string is not empty{0}", message.Whitespace() ? string.Empty : " : " + message));
      }
    }
  }
}