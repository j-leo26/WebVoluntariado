using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntariado.Models
{
    public class VolunteerOffer
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        // 🔹 Clave foránea que apunta al modelo User
        [Required]
        public int UserId { get; set; }

        // 🔹 Propiedad de navegación al usuario creador
        public User? User { get; set; }

        public string EmailContact { get; set; } = string.Empty;

        [Range(1, 1000)]
        public int TotalSpots { get; set; } = 1;

        public int ApplicantsCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 🔹 Relación con las postulaciones
        public List<Application>? Applications { get; set; }
    }
}
