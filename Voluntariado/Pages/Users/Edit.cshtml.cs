using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using BCrypt.Net;

namespace Voluntariado.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) => _context = context;

        [BindProperty]
        public User User { get; set; } = new();

        public List<SelectListItem> RoleOptions { get; set; } = new();

        private bool IsAuthorized()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Administrador";
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (!IsAuthorized())
                return RedirectToPage("/AccessDenied");

            User = await _context.Users.FindAsync(id);
            if (User == null)
                return NotFound();

            await LoadRolesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsAuthorized())
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
            {
                await LoadRolesAsync();
                return Page();
            }

            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == User.Email && u.Id != User.Id);

            if (emailExists)
            {
                ModelState.AddModelError("User.Email", "⚠️ El correo electrónico ya está registrado.");
                await LoadRolesAsync();
                return Page();
            }

            var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == User.Id);
            if (existingUser == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(User.PasswordHash) && User.PasswordHash != existingUser.PasswordHash)
                User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.PasswordHash);
            else
                User.PasswordHash = existingUser.PasswordHash;

            _context.Attach(User).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Usuario actualizado correctamente.";
            return RedirectToPage("Index");
        }

        private async Task LoadRolesAsync()
        {
            RoleOptions = await _context.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                })
                .ToListAsync();
        }
    }
}
