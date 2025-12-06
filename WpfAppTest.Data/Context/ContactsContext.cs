using Microsoft.EntityFrameworkCore;
using WpfAppTest.Data.Entities.Common;

namespace WpfAppTest.Data.Context
{
    public class ContactsContext : DbContext
    {
        #region Attributes
        private const string DEFAULT_SCHEMA = @"Common";
        #endregion Attributes

        #region Constructors
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {
        }

        //public ContactsContext()
        //{
        //}
        #endregion Constructors

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // Chaîne de connexion
        //        optionsBuilder.UseSqlServer(@"Server=ZEPHYR\SQLSERVERMSJ;Database=CONTACTS;Trusted_Connection=True;Encrypt=False;");
        //    }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Schéma par défaut
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
        }
        #endregion Methods

        #region Entities
        public DbSet<Contact> Contacts { get; set; }
        #endregion Entities
    }
}
