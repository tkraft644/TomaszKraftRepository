using System.ComponentModel.DataAnnotations;

namespace MiodyKraftowe.Models
{
        public class CartItem
        {
                [Key]
                public int CartItemModelID { get; set; }
                public int ProductId { get; set; }
                public string ProductName { get; set; }
                public int Quantity { get; set; }
                public decimal Price { get; set; }
                public decimal Total
                {
                        get { return Quantity * Price; }
                }
                public string Image { get; set; }

                public CartItem()
                {
                }
                public CartItem(Produkt product)
                {
                        ProductId = product.Id;
                        ProductName = product.Nazwa;
                        Price = product.Cena;
                        Quantity = 1;
                        Image = product.ObrazekURL;
                }
    }
}
