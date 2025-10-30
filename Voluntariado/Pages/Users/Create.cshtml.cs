using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using BCrypt.Net; // 👈 Importante para encriptar

namespace Voluntariado.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = new();

        public List<SelectListItem> RoleOptions { get; set; } = new();

        public async Task OnGetAsync()
        {
            RoleOptions = await _context.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            // Validar correo duplicado
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == User.Email);
            if (emailExists)
            {
                ModelState.AddModelError("User.Email", "El correo ya está registrado.");
                await OnGetAsync();
                return Page();
            }

            // ✅ Encriptar la contraseña antes de guardar
            if (!string.IsNullOrWhiteSpace(User.PasswordHash))
            {
                User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.PasswordHash);
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Usuario registrado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
