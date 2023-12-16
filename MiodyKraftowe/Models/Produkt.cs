using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiodyKraftowe.Controllers;

namespace MiodyKraftowe.Models
{
    public class Produkt
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        public string Nazwa { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Opis { get; set; }

        public float? Wielkosc { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana")]
        public string NazwaKategoria { get; set; }
        public DateTimeOffset DataPublikacji { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Cena jest wymagana")]
        public decimal Cena { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        [FileExtension]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile? Obrazek { get; set; }
        public string? ObrazekURL { get; set; }

    }
}
