
using Microsoft.AspNetCore.Http;
using project.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.ViewModels
{
    public class CarModel :BaseEntity
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
        [Required]
        public int CarPrice { get; set; }
        [Required]
        [DisplayName("Gear")]
        public string CarGear { get; set; }
        [Required]
        [DisplayName("Walking")]
        public int CarWalking { get; set; }
        public IFormFile UploadImage { get; set; }
        public IFormFile UploadImage2{ get; set; }
        public IFormFile UploadImage3 { get; set; }
        [Required]
        [DisplayName("Brand")]
        public string CarBrand { get; set; }
        [Required]
        [DisplayName("Model")]
        public string CarTypeModel { get; set; }
        [Required]
        [DisplayName("facturing year")]
        public int CarManufacturingyear { get; set; }
        [DisplayName("Color")]
        public string CarColor { get; set; }
        public bool IsSaled { get; set; }
        public bool IsRent { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        [DisplayName("Rent Or Sale")]
        public int RentOrSale { get; set; }

    }
}
