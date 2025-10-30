using Voluntariado.Data;
using Voluntariado.Models;

public class UserService
{
    private readonly ApplicationDbContext _context;
    public UserService(ApplicationDbContext context) => _context = context;

    public List<User> GetAllUsers() => _context.Users.ToList();

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}

