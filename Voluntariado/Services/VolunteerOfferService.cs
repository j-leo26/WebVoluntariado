using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

public class VolunteerOfferService
{
    private readonly ApplicationDbContext _context;

    public VolunteerOfferService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<VolunteerOffer> GetAllOffers() =>
        _context.VolunteerOffers.Include(o => o.User).ToList();

    public void AddOffer(VolunteerOffer offer)
    {
        _context.VolunteerOffers.Add(offer);
        _context.SaveChanges();
    }
}
