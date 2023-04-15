using Appointment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Appointment.Models;

namespace Appointment.Areas.Identity.Data;

public class DBAppointmentContext : IdentityDbContext<AppointmentUser>
{
    public DBAppointmentContext(DbContextOptions<DBAppointmentContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<Appointment.Models.ListModel>? ListModel { get; set; }

    public DbSet<Appointment.Models.SchedulerModel>? SchedulerModel { get; set; }
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppointmentUser>
{
    public void Configure(EntityTypeBuilder<AppointmentUser> builder)
    {
        builder.Property(x => x.LoginName).HasMaxLength(100);
        builder.Property(x => x.LoginId).ValueGeneratedOnAdd().UseIdentityColumn();
        //throw new NotImplementedException();
    }
}
