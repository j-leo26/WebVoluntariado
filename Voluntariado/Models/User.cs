using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Voluntariado.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}