using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Car : BaseEntity
    {
        [DisplayName("Id")]
        public int CarId { get; set; }

        [DisplayName("Image")]
        public string CarImgUrl1 { get; set; }
        [DisplayName("Image2")]
        public string CarImgUrl2 { get; set; }
        [DisplayName("Image3")]
        public string CarImgUrl3 { get; set; }
        [DisplayName("Price")]
        public int CarPrice { get; set; }
        [DisplayName("Gear")]
        public string CarGear { get; set; }
        [DisplayName("Walking")]
        public int CarWalking { get; set; }
        [DisplayName("Brand")]

        public string CarBrand { get; set; }
        [DisplayName("Model")]

        public string CarTypeModel { get; set; }
        public int CarManufacturingyear { get; set; }
        [DisplayName("Color")]
        public string CarColor { get; set; }
        public bool IsSaled { get; set; }
        public bool IsRent { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        public int RentOrSale { get; set; }
    }
}
