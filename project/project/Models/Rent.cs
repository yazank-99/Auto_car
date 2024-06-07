using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Rent : BaseEntity
    {
        public int RentId { get; set; }
        public DateTime RentDate { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        [ForeignKey("AppUser")]
        public Guid UserRentId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
