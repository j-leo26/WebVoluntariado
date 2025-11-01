using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using BCrypt.Net;

namespace Voluntariado.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context) => _context = context;

        [BindProperty]
        public User User { get; set; } = new();

        public List<SelectListItem> RoleOptions { get; set; } = new();

        private bool IsAuthorized()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Administrador";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthorized())
                return RedirectToPage("/AccessDenied");

            RoleOptions = await _context.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                })
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsAuthorized())
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == User.Email);
            if (emailExists)
            {
                ModelState.AddModelError("User.Email", "⚠️ El correo ya está registrado.");
                await OnGetAsync();
                return Page();
            }

            User.CreatedByAdmin = true;

            if (!string.IsNullOrWhiteSpace(User.PasswordHash))
                User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.PasswordHash);

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Usuario creado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
