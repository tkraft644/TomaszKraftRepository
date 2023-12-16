using System.ComponentModel.DataAnnotations;

namespace MiodyKraftowe.Models
{
    public class Comment
    {
        [Required(ErrorMessage = "Nazwa Autora jest Wymagana")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "Treść jest wymagana")]
        public string Tresc { get; set; }
        public DateTimeOffset DataPublikacji { get; set; } = DateTimeOffset.Now;
        public bool Widocznosc { get; set; }
        public Guid UniqueId { get; set; }
    }
}
