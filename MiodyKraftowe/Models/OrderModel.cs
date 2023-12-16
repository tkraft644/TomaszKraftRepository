using System.ComponentModel.DataAnnotations;

namespace MiodyKraftowe.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderID { get; set; }
        public string? OrderUserID { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Adress { get; set; }
        public string? Status { get; set; }
        [Phone]
        public string? Number { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? GrandTotalShip
        {
            get { return GrandTotal + 20; }
        }
        public ICollection<OrderModelDetail> OrderModelDetails { get; set; }

    }
}
