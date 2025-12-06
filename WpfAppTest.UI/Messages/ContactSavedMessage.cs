using WpfAppTest.Core.Models;

namespace WpfAppTest.UI.Messages
{
    public class ContactSavedMessage
    {
        public Contact Contact { get; }

        public ContactSavedMessage(Contact contact)
        {
            Contact = contact;
        }
    }
}
