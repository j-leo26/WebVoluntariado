using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using BCrypt.Net; // 👈 necesario para encriptar

namespace Voluntariado.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = new();

        public List<SelectListItem> RoleOptions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.Users.FindAsync(id);

            if (User == null)
                return NotFound();

            await LoadRolesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadRolesAsync();
                return Page();
            }

            try
            {
                // Verificar duplicado de correo al editar
                bool emailExists = await _context.Users
                    .AnyAsync(u => u.Email == User.Email && u.Id != User.Id);

                if (emailExists)
                {
                    ModelState.AddModelError("User.Email", "⚠️ El correo electrónico ya está registrado.");
                    await LoadRolesAsync();
                    return Page();
                }

                // Buscar el usuario original en la base de datos
                var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == User.Id);
                if (existingUser == null)
                    return NotFound();

                // ✅ Si la contraseña cambió, encriptar la nueva
                if (!string.IsNullOrWhiteSpace(User.PasswordHash) &&
                    User.PasswordHash != existingUser.PasswordHash)
                {
                    // Encriptar nueva contraseña
                    User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.PasswordHash);
                }
                else
                {
                    // Mantener la contraseña anterior si no se cambió
                    User.PasswordHash = existingUser.PasswordHash;
                }

                _context.Attach(User).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "✅ Usuario actualizado correctamente.";
                return RedirectToPage("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == User.Id))
                    return NotFound();

                throw;
            }
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
