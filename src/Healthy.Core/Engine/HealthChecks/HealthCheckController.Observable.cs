using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.HealthChecks
{
    partial class HealthCheckController
    {
        public IDisposable Subscribe(IObserver<HealthCheckResult> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(this, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private HealthCheckController _healthCheckRunner;
            private IObserver<HealthCheckResult> _observer;

            public Unsubscriber(HealthCheckController healthCheckRunner, IObserver<HealthCheckResult> observer)
            {
                _healthCheckRunner = healthCheckRunner;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_healthCheckRunner != null && _observer != null && _healthCheckRunner._observers.Contains(_observer))
                    _healthCheckRunner._observers.Remove(_observer);

                _healthCheckRunner = null;
                _observer = null;
            }
        }

        private volatile DiagnosticSubscription _subscriptions;


        // Note that Subscriptions are READ ONLY.   This means you never update any fields (even on removal!)
        private class DiagnosticSubscription : IDisposable
        {
            internal IObserver<KeyValuePair<string, object>> Observer;

            internal HealthCheckController Owner;          // The DiagnosticListener this is a subscription for.  
            internal DiagnosticSubscription Next;                // Linked list of subscribers

            public void Dispose()
            {
                // TO keep this lock free and easy to analyze, the linked list is READ ONLY.   Thus we copy
                for (; ; )
                {
                    DiagnosticSubscription subscriptions = Owner._subscriptions;
                    DiagnosticSubscription newSubscriptions = Remove(subscriptions, this);    // Make a new list, with myself removed.  

                    // try to update, but if someone beat us to it, then retry.  
                    if (Interlocked.CompareExchange(ref Owner._subscriptions, newSubscriptions, subscriptions) == subscriptions)
                    {
#if DEBUG
                        var cur = newSubscriptions;
                        while (cur != null)
                        {
                            Debug.Assert(!(cur.Observer == Observer), "Did not remove subscription!");
                            cur = cur.Next;
                        }
#endif
                        break;
                    }
                }
            }

            // Create a new linked list where 'subscription has been removed from the linked list of 'subscriptions'. 
            private static DiagnosticSubscription Remove(DiagnosticSubscription subscriptions, DiagnosticSubscription subscription)
            {
                if (subscriptions == null)
                {
                    // May happen if the IDisposable returned from Subscribe is Dispose'd again
                    return null;
                }

                if (subscriptions.Observer == subscription.Observer)
                    return subscriptions.Next;
#if DEBUG
                // Delay a bit.  This makes it more likely that races will happen. 
                for (int i = 0; i < 100; i++)
                    GC.KeepAlive("");
#endif
                return new DiagnosticSubscription() { Observer = subscriptions.Observer, Owner = subscriptions.Owner, Next = Remove(subscriptions.Next, subscription) };
            }
        }
    }
}
