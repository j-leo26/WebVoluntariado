using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntariado.Models
{
    public class Application
    {
        public int Id { get; set; }

        // Relación con User
        public int UserId { get; set; }
        public User User { get; set; }

        // Relación con VolunteerOffer
        public int VolunteerOfferId { get; set; }       // 👈 Clave foránea
        public VolunteerOffer VolunteerOffer { get; set; }  // 👈 Propiedad de navegación

        // Otros campos
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
    }
}
