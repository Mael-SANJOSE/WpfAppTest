using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using WpfAppTest.Data.Context;
using WpfAppTest.Data.Entities.Common;
using WpfAppTest.Data.Repositories.Interfaces.Common;

namespace WpfAppTest.Data.Repositories.Services.Common
{
    /// <summary>
    /// Contact repository methods
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsContext _context;

        public ContactRepository(ContactsContext context)
        {
            _context = context;
        }

        #region CRUD methods
        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="contact">The contact to create</param>
        /// <returns></returns>
        public async Task CreateAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get a contact by id
        /// </summary>
        /// <param name="id">The searched contact id</param>
        /// <returns>The contact associated to id or null if not found</returns>
        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Get a contact by firstname and lastname
        /// </summary>
        /// <param name="firstname">The searched contact firstname</param>
        /// <param name="lastname">The searched contact lastname</param>
        /// <returns>The contact associated to firstname and lastname or null if not found</returns>
        public async Task<Contact?> GetByFirstnameAndLastnameAsync(string firstname, string lastname)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Firstname == firstname && c.Lastname == lastname);
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>A contact list</returns>
        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="contact">The contact to update</param>
        /// <returns></returns>
        public async Task Update(Contact contact)
        {
            _context.Contacts.Update(contact);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contact">The contact to delete</param>
        /// <returns></returns>
        public async Task Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);

            await _context.SaveChangesAsync();
        }       
        #endregion CRUD methods
    }
}
