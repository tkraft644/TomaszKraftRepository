using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiodyKraftowe.Controllers;


namespace MiodyKraftowe.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tytul jest wymagany")]
        public string Tytul { get; set; }
        [Required(ErrorMessage = "Tresc jest wymagana")]
        public string Tresc { get; set; }
        public string Autor { get; set; }
        public DateTimeOffset DataPublikacji { get; set; } = DateTime.Now;
        public DateTimeOffset DataModyfikacji { get; set; } = DateTimeOffset.Now;
        public int DisplayOrder { get; set; }
        public bool Widocznosc { get; set; }
        public bool Usuniety { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        [FileExtension]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile? Obrazek { get; set; }
        public string? ObrazekURL { get; set; }


    }
}
