using System.ComponentModel.DataAnnotations;

namespace Users_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Nomnbre no puede exceder los 50 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del Email no es válido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "La Edad es obligatoria.")]
        [Range(0, 120, ErrorMessage = "La Edad debe ser un número entre 0 y 120.")]
        public int Age { get; set; }
    }
}
