using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using project.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.ViewModels
{
    public class LastnewsupdateModel :BaseEntity
    {
        [DisplayName("Id")]

        public int LastnewsupdateId { get; set; }
        [DisplayName("Imag")]

        public string LastnewsupdateImgeUrl { get; set; }
        [DisplayName("Name")]

        public string LastnewsupdateName { get; set; }
        [DisplayName("Title")]

        public string LastnewsupdateTitle { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.DateTime)]
        [DisplayName("Date")]

        public string LastnewsupdateDate { get; set; }
        [DisplayName("Desc")]

        public string LastnewsupdateDesc { get; set; }
        public IFormFile UploadImage { get; set; }

    }
}
