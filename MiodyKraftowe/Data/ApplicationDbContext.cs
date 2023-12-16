using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiodyKraftowe.Models;

namespace MiodyKraftowe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Post> Posty { get; set; }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderModelDetail> OrdersDetail { get; set; }
        public DbSet<Wiadomosc> Wiadomosci { get; set; }

    }
}