using Microsoft.AspNetCore.Http;
using project.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.ViewModels
{
    public class SaleModel:BaseEntity
    {
        [DisplayName("Id")]
        public int SaleId { get; set; }
        [DisplayName("Sale Date")]
        public DateTime SaleDate { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
