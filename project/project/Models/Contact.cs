using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Contact:BaseEntity
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Required]
        public string ContactEmail{ get; set; }
        public string ContactSubject { get; set; }
        [Required]
        public string ContactQuestion { get; set; }
    }
}
