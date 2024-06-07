using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Lastnewsupdate:BaseEntity
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

    }
}
