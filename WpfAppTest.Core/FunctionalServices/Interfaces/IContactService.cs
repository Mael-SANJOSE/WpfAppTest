using WpfAppTest.Core.Models;

namespace WpfAppTest.Core.FunctionalServices.Interfaces
{
    /// <summary>
    /// Contact service methods interface
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="contact">The contact to create</param>
        /// <returns></returns>
        Task CreateAsync(Contact contact);

        /// <summary>
        /// Get a contact by id
        /// </summary>
        /// <param name="id">The searched contact id</param>
        /// <returns>The contact associated to id or null if not found</returns>
        Task<Contact?> GetByIdAsync(int id);

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>A contact list</returns>
        Task<List<Contact>> GetAllContactsAsync();

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="contact">The contact to update</param>
        /// <returns></returns>
        Task Update(Contact contact);

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contact">The contact to delete</param>
        /// <returns></returns>
        Task Delete(Contact contact);
    }
}
