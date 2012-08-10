using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPro.OfflineFirst.MetroApp.Common
{
    public class EventService
    {
        public static EventHandler MakeWeak(EventHandler handler, Action<EventHandler> remove)
        {
            var reference = new WeakReference(handler.Target);

            EventHandler newHandler = null;
            newHandler = (sender, e) =>
            {
                var target = reference.Target;
                if (target != null)
                {
                    handler.Invoke(target, e);
                }
                else
                {
                    // Collected, unhook us
                    remove(newHandler);
                }
            };

            return newHandler;
        }

        public static NotifyCollectionChangedEventHandler MakeWeak(NotifyCollectionChangedEventHandler handler, Action<NotifyCollectionChangedEventHandler> remove)
        {
            var reference = new WeakReference(handler.Target);

            NotifyCollectionChangedEventHandler newHandler = null;
            newHandler = (sender, e) =>
            {
                var target = reference.Target;
                if (target != null)
                {
                    handler.Invoke(target, e);
                }
                else
                {
                    // Collected, unhook us
                    remove(newHandler);
                }
            };

            return newHandler;
        }
    }
}
