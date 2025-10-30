namespace Voluntariado.Models
{
    public class VolunteerOffer
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int UserId { get; set; } // Ofertante
        public User? User { get; set; }
    }

}
