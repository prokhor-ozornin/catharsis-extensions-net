using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Collection of methods to perform assertions on objects.</para>
  /// </summary>
  public static class Assertion
  {
    /// <summary>
    ///   <para>Asserts that specified value is a default value for its <see cref="Type"/>.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise methods returns normally.</para>
    /// </summary>
    /// <typeparam name="T">Type of subject value.</typeparam>
    /// <param name="self">Value to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="NotDefault{T}(T, string)"/>
    public static void Default<T>(this T self, string message = null) where T : struct
    {
      if (self.Equals(default(T)))
      {
        return;
      }

      if (message.Whitespace())
      {
        throw new ArgumentException($"Argument doesn't has a default value for type {typeof(T)}. Expected : {default(T)}. Actual : {self}");
      }

      throw new ArgumentException(message);
    }

    /// <summary>
    ///   <para>Asserts that specified sequence is empty (does not contain any elements).</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">Sequence to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="Empty{T}(IEnumerable{T}, string)"/>
    /// <seealso cref="NotEmpty(IEnumerable, string)"/>
    public static void Empty(IEnumerable self, string message = null)
    {
      Empty(self?.Cast<object>(), message);
    }
    
    /// <summary>
    ///   <para>Asserts that specified sequence is empty (does not contain any elements).</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in sequence.</typeparam>
    /// <param name="self">Sequence to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="Empty(IEnumerable, string)"/>
    /// <seealso cref="NotEmpty{T}(IEnumerable{T}, string)"/>
    public static void Empty<T>(IEnumerable<T> self, string message = null)
    {
      if (self != null && self.Any())
      {
        throw new ArgumentException($"Sequence {self.ToListString()} is not empty{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }

    /// <summary>
    ///   <para>Asserts that two objects are equal, according to the result of <see cref="object.Equals(object, object)"/> method's call.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">First object to be compared.</param>
    /// <param name="other">Second object to be compared.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="NotEqual(object, object, string)"/>
    public static void Equal(object self, object other, string message = null)
    {
      if (self == null || other == null || !Equals(self, other))
      {
        throw new ArgumentException($"Objects were expected to be equal, but they were not. Expected : {self}; actual : {other}{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }

    /// <summary>
    ///   <para>Asserts that specified logical expression evaluates to false.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">Logical expression to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="True(bool, string)"/>
    public static void False(bool self, string message = null)
    {
      if (self)
      {
        throw new ArgumentException("Specified condition is not false{0}", message.Whitespace() ? string.Empty : " : " + message);
      }
    }

    /// <summary>
    ///   <para>Asserts that specified value is not a default value for its <see cref="Type"/>.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise methods returns normally.</para>
    /// </summary>
    /// <typeparam name="T">Type of subject value.</typeparam>
    /// <param name="self">Value to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="Default{T}(T, string)"/>
    public static void NotDefault<T>(T self, string message = null) where T : struct
    {
      if (!self.Equals(default(T)))
      {
        return;
      }

      if (message.Whitespace())
      {
        throw new ArgumentException($"Argument has a default value for type {typeof(T)} : {self}");
      }

      throw new ArgumentException(message);
    }

    /// <summary>
    ///   <para>Asserts that specified sequence is not empty (contains at least one element).</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">Sequence to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="NotEmpty{T}(IEnumerable{T}, string)"/>
    /// <seealso cref="Empty(IEnumerable, string)"/>
    public static void NotEmpty(IEnumerable self, string message = null)
    {
      NotEmpty(self.Cast<object>(), message);
    }

    /// <summary>
    ///   <para>Asserts that specified sequence is not empty (contains at least one element).</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in sequence.</typeparam>
    /// <param name="self">Sequence to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="NotEmpty(IEnumerable, string)"/>
    /// <seealso cref="Empty{T}(IEnumerable{T}, string)"/>
    public static void NotEmpty<T>(IEnumerable<T> self, string message = null)
    {
      NotNull(self);

      if (!self.Any())
      {
        throw new ArgumentException($"Sequence {self.ToListString()} is empty{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }

    /// <summary>
    ///   <para>Asserts that two objects are not equal, according to the result of <see cref="object.Equals(object, object)"/> method's call.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">First object to be compared.</param>
    /// <param name="other">Second object to be compared.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="Equal(object, object, string)"/>
    public static void NotEqual(object self, object other, string message = null)
    {
      if (self != null && other != null && Equals(self, other))
      {
        throw new ArgumentException($"Objects were expected to be different, but they were equal. Expected {self}, but actual is {other}{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }

    /// <summary>
    ///   <para>Asserts that specified value is not <c>null</c>.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">Object to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentNullException">Thrown when assertion fails.</exception>
    /// <seealso cref="Null(object, string)"/>
    public static void NotNull(object self, string message = null)
    {
      if (self != null)
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
    ///   <para>Asserts that specified string is not a <c>null</c> reference, <see cref="string.Empty"/> or contains only space characters.</para>
    /// </summary>
    /// <param name="self">String to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentNullException">Thrown when assertion fails (<paramref name="self"/> is a <c>null</c> reference).</exception>
    /// <exception cref="ArgumentException">Thrown when assertion fails (<paramref name="self"/> is either <see cref="string.Empty"/> or contains only space characters).</exception>
    /// <seealso cref="Whitespace(string, string)"/>
    public static void NotWhitespace(string self, string message = null)
    {
      NotNull(self);

      if (self.Whitespace())
      {
        throw new ArgumentException($"Given string is either empty or contains only whitespace characters{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }

    /// <summary>
    ///   <para>Asserts that specified value is a <c>null</c> reference.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">Object to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="NotNull(object, string)"/>
    public static void Null(object self, string message = null)
    {
      if (self != null)
      {
        throw new ArgumentException($"Argument was expected to be null, but is {self}{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }

    /// <summary>
    ///   <para>Asserts that specified logical expression evaluates to true.</para>
    ///   <para>If assertion fails, <see cref="ArgumentException"/> will be thrown, otherwise method returns normally.</para>
    /// </summary>
    /// <param name="self">Logical expression to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="False(bool, string)"/>
    public static void True(bool self, string message = null)
    {
      if (!self)
      {
        throw new ArgumentException($"Specified condition is not true{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }

    /// <summary>
    ///   <para>Asserts that specified string is either a <c>null</c> reference, <see cref="string.Empty"/> or contains only space characters.</para>
    /// </summary>
    /// <param name="self">String to be evaluated.</param>
    /// <param name="message">Error message to be used if assertion fails.</param>
    /// <exception cref="ArgumentException">Thrown when assertion fails.</exception>
    /// <seealso cref="NotWhitespace(string, string)"/>
    public static void Whitespace(string self, string message = null)
    {
      if (!self.Whitespace())
      {
        throw new ArgumentException($"Given string is not empty{(message.Whitespace() ? string.Empty : " : " + message)}");
      }
    }
  }
}