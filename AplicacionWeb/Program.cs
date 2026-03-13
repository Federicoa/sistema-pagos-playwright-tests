using AplicacionWeb.Data;
using Microsoft.EntityFrameworkCore;
using Dominio;

namespace AplicacionWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<SistemaPagosContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<SistemaPagosContext>();

                    db.Database.EnsureCreated();

                    if (!db.Usuarios.Any())
                    {
                        var equipo = new Equipo("Administracion");
                        db.Equipos.Add(equipo);
                        db.SaveChanges();

                        var gerente = new Usuario(
                            "Admin",
                            "Gerente",
                            "12345678",
                            equipo,
                            DateTime.Now.AddYears(-2),
                            RolUsuario.GERENTE
                        );

                        var empleado = new Usuario(
                            "Juan",
                            "Empleado",
                            "12345678",
                            equipo,
                            DateTime.Now.AddYears(-1),
                            RolUsuario.EMPLEADO
                        );

                        db.Usuarios.Add(gerente);
                        db.Usuarios.Add(empleado);

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Usuario}/{action=Index}/{id?}");

            app.Run();
        }
    }
}