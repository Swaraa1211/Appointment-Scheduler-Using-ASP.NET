using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Appointment.Areas.Identity.Data;
namespace Appointment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("DBAppointmentContextConnection") ?? throw new InvalidOperationException("Connection string 'DBAppointmentContextConnection' not found.");

                                    builder.Services.AddDbContext<DBAppointmentContext>(options =>
                options.UseSqlServer(connectionString));

                                                builder.Services.AddDefaultIdentity<AppointmentUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<DBAppointmentContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Welcome}/{action=Welcome}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}