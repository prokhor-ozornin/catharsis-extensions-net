using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Subscription"/>.</para>
  ///   <seealso cref="Subscription"/>
  /// </summary>
  public static class SubscriptionExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of subscriptions, leaving those of active state.</para>
    /// </summary>
    /// <param name="subscriptions">Source sequence of subscriptions to filter.</param>
    /// <returns>Filtered sequence of active subscriptions.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subscriptions"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Subscription> Active(this IEnumerable<Subscription> subscriptions)
    {
      Assertion.NotNull(subscriptions);
      
      return subscriptions.Where(subscription => subscription != null && subscription.Active);
    }

    /// <summary>
    ///   <para>Filters sequence of subscriptions, leaving those of inactive state.</para>
    /// </summary>
    /// <param name="subscriptions">Source sequence of subscriptions to filter.</param>
    /// <returns>Filtered sequence of inactive subscriptions.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subscriptions"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Subscription> Inactive(this IEnumerable<Subscription> subscriptions)
    {
      Assertion.NotNull(subscriptions);

      return subscriptions.Where(subscription => subscription != null && !subscription.Active);
    }

    /// <summary>
    ///   <para>Filters sequence of subscriptions, returning the one with specified unique token.</para>
    /// </summary>
    /// <param name="subscriptions">Source sequence of subscriptions to filter.</param>
    /// <param name="token">Unique token to search for.</param>
    /// <returns>Either subscription with specified unique token, or a <c>null</c> reference if no such subscription could be found.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subscriptions"/> or <paramref name="token"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="token"/> is <see cref="string.Empty"/> string.</exception>
    public static Subscription WithToken(this IEnumerable<Subscription> subscriptions, string token)
    {
      Assertion.NotNull(subscriptions);
      Assertion.NotEmpty(token);

      return subscriptions.FirstOrDefault(subscription => subscription != null && subscription.Token == token);
    }
  }
}