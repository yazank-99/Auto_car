namespace project.Models
{
    public class RentCustomer:BaseEntity
    {
        public int RentId { get; set; }
        public string RentImgUrl1 { get; set; }
        public string RentImgUrl2 { get; set; }
        public string RentImgUrl3 { get; set; }
        public int RentPrice { get; set; }
        public string RentGear { get; set; }
        public string RentDate { get; set; }
    }
}
