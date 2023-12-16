using System.ComponentModel.DataAnnotations;

namespace MiodyKraftowe.Models
{
    public class OrderModelDetail
    {
        [Key]
        public int ID { get; set; }
        public int OrderId { get; set; }
        public OrderModel Order { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public string? Image { get; set; }

    }
}
