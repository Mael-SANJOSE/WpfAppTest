using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfAppTest.Data.Entities.Common
{
    [Table("CONTACT", Schema = "Common")]
    public partial class Contact
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; } = string.Empty;
        #endregion Properties
    }
}
