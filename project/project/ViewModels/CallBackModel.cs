using project.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.ViewModels
{
    public class CallBackModel:BaseEntity
    {
        [DisplayName("Id")]

        public int CallBackId { get; set; }
        [DisplayName("Name")]

        public string CallBackName { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]

        [Required]
        public string CallBackEmail { get; set; }
        [DisplayName("Phone")]
        public int CallBackPhone { get; set; }
        [DisplayName("Choose Service")]

        public string CallBackChooseService { get; set; }
        [DisplayName("Comment")]
        public string CallBackComment { get; set; }
    }
} 
