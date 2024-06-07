using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Sale:BaseEntity
    {
        [DisplayName("Id")]
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        [ForeignKey("AppUser")]
        public Guid UserSaleId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
