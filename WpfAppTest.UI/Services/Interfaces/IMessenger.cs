namespace WpfAppTest.UI.Services.Interfaces
{
    public interface IMessenger
    {
        void Send<TMessage>(TMessage message);
        void Register<TMessage>(object recipient, Action<TMessage> action);
        void Unregister<TMessage>(object recipient);
    }
}
