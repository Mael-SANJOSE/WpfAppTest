using WpfAppTest.Core.FunctionalServices.Interfaces;
using WpfAppTest.Core.Models;
using WpfAppTest.Data.Repositories.Interfaces.Common;

namespace WpfAppTest.Core.FunctionalServices.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="contact">The contact to create</param>
        /// <returns></returns>
        public async Task CreateAsync(Contact contact)
        {
            await _repository.CreateAsync(new Data.Entities.Common.Contact() { Firstname = contact.Firstname, Lastname = contact.Lastname });
        }

        /// <summary>
        /// Get a contact by id
        /// </summary>
        /// <param name="id">The searched contact id</param>
        /// <returns>The contact associated to id or null if not found</returns>
        public async Task<Contact?> GetByIdAsync(int id)
        {
            Contact? contact = null;

            Data.Entities.Common.Contact? entityContact = await _repository.GetByIdAsync(id);

            if (entityContact != null)
                contact = new() { Firstname = entityContact.Firstname, Lastname = entityContact.Lastname };

            return contact;
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>A contact list</returns>
        public async Task<List<Contact>> GetAllContactsAsync()
        {
            List<Data.Entities.Common.Contact> contacts = await _repository.GetAllAsync();

            return contacts.Select(c => new Contact { Firstname = c.Firstname, Lastname = c.Lastname }).ToList();
        }

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="contact">The contact to update</param>
        /// <returns></returns>
        public async Task Update(Contact contact)
        {
            Data.Entities.Common.Contact entityContact = new() { Firstname = contact.Firstname, Lastname = contact.Lastname };

            await _repository.Update(entityContact);
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contact">The contact to delete</param>
        /// <returns></returns>
        public async Task Delete(Contact contact)
        {
            Data.Entities.Common.Contact? entityContact = (await _repository.GetAllAsync()).FirstOrDefault(c => c.Firstname == contact.Firstname && c.Lastname == contact.Lastname);

            if (entityContact != null)
                await _repository.Delete(entityContact);
        }
    }
}
