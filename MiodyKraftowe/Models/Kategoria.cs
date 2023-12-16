using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiodyKraftowe.Models
{
    public class Kategoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nazwa { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only!!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
