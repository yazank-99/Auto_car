using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace project.Models
{
    public class CarShop : BaseEntity
    {
        [DisplayName("Id")]
        public int CarShopId { get; set; }

        [DisplayName("Image")]
        public string CarShopImgUrl1 { get; set; }
        [DisplayName("Image2")]
        public string CarShopImgUrl2 { get; set; }
        [DisplayName("Image3")]
        public string CarShopImgUrl3 { get; set; }
        [DisplayName("Price")]
        public int CarShopPrice { get; set; }
        [DisplayName("Gear")]
        public string CarShopGear { get; set; }
        [DisplayName("Walking")]
        public int CarShopWalking { get; set; }
        [DisplayName("Brand")]

        public string CarShopBrand { get; set; }
        [DisplayName("Model")]

        public string CarShopTypeModel { get; set; }
        [DisplayName("facturing year")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.DateTime)]
        public string CarShopManufacturingyear { get; set; }
        [DisplayName("Color")]
        public string CarShopColor { get; set; }
    }
}
