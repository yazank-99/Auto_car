using project.Models;
using System.ComponentModel.DataAnnotations;

namespace project.ViewModels
{
    public class ContactModel:BaseEntity
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Required]
        public string ContactEmail { get; set; }
        public string ContactSubject { get; set; }
        [Required]
        public string ContactQuestion { get; set; }
    }
}
