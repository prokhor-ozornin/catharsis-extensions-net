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
    ///   <para></para>
    /// </summary>
    /// <param name="subscriptions"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subscriptions"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Subscription> Active(this IEnumerable<Subscription> subscriptions)
    {
      Assertion.NotNull(subscriptions);
      
      return subscriptions.Where(subscription => subscription != null && subscription.Active);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subscriptions"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subscriptions"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Subscription> Inactive(this IEnumerable<Subscription> subscriptions)
    {
      Assertion.NotNull(subscriptions);

      return subscriptions.Where(subscription => subscription != null && !subscription.Active);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subscriptions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static Subscription WithToken(this IEnumerable<Subscription> subscriptions, string token)
    {
      Assertion.NotNull(subscriptions);

      return subscriptions.FirstOrDefault(subscription => subscription != null && subscription.Token == token);
    }
  }
}