using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using Voluntariado.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VoluntariadoConnection")));

builder.Services.AddRazorPages();

// Agregar caché distribuida y sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Servicio para hashear contraseñas
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Habilitar middleware de sesión
app.UseSession();

app.MapRazorPages();

// Crear roles predeterminados si no existen
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    var rolesPredeterminados = new[]
    {
        "Administrador",
        "Voluntario",
        "Ofertante"
    };

    foreach (var nombreRol in rolesPredeterminados)
    {
        if (!context.Roles.Any(r => r.Name == nombreRol))
        {
            context.Roles.Add(new Role { Name = nombreRol });
        }
    }

    context.SaveChanges();
}

app.Run();
