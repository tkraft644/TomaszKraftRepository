using System.ComponentModel.DataAnnotations;

namespace MiodyKraftowe.Models
{
    public class Wiadomosc
    {
        [Key]
        public Guid Id { get; set; }
        public string Imie { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Tresc jest wymagana")]
        public string Tresc { get; set; }
        public DateTimeOffset Data { get; set; } = DateTime.Now;
    }
}
