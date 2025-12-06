using WpfAppTest.UI.Services.Interfaces;

namespace WpfAppTest.UI.Services.Services
{
    public class Messenger : IMessenger
    {
        private readonly Dictionary<Type, List<WeakReference>> _subscribers = new();

        public void Send<TMessage>(TMessage message)
        {
            Type messageType = typeof(TMessage);

            if (!_subscribers.ContainsKey(messageType))
                return;

            List<WeakReference> subscriberList = _subscribers[messageType];

            // Nettoyer les références mortes
            subscriberList.RemoveAll(wr => !wr.IsAlive);

            foreach (WeakReference weakRef in subscriberList.ToList())
            {
                if (weakRef.Target is Action<TMessage> action)
                {
                    action(message);
                }
            }
        }

        public void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            Type messageType = typeof(TMessage);

            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType] = new List<WeakReference>();
            }

            _subscribers[messageType].Add(new WeakReference(action));
        }

        public void Unregister<TMessage>(object recipient)
        {
            Type messageType = typeof(TMessage);

            if (_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType].Clear();
            }
        }
    }
}
