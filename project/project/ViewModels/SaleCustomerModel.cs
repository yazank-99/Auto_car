using project.Models;
using System.ComponentModel;

namespace project.ViewModels
{
    public class SaleCustomerModel : BaseEntity
    {
        [DisplayName("Id")]

        public int SaleId { get; set; }
        [DisplayName("Img1")]

        public string SaleImgUrl1 { get; set; }
        [DisplayName("Img2")]

        public string SaleImgUrl2 { get; set; }
        [DisplayName("Img3")]

        public string SaleImgUrl3 { get; set; }
        [DisplayName("Price")]

        public int SalePrice { get; set; }
        [DisplayName("Walking")]

        public int SaleWalking { get; set; }
        [DisplayName("Gear")]
        public string SaleGear { get; set; }
    }
}
