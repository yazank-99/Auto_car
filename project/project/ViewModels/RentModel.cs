using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using project.Models;
using System;

namespace project.ViewModels
{
    public class RentModel:BaseEntity
    {
        public int RentId { get; set; }
        public DateTime RentDate { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
