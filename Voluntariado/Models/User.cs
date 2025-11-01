using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Voluntariado.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [Display(Name = "Contraseña")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Rol")]
        public int RoleId { get; set; }

        public Role? Role { get; set; }

        // Nuevo campo
        [Display(Name = "Creado por administrador")]
        public bool CreatedByAdmin { get; set; } = false;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }
}
