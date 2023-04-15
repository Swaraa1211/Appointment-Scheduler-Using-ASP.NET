using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Appointment.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppointmentUser class
public class AppointmentUser : IdentityUser
{
    public int LoginId { get; set; }
    public string LoginName { get; set; }
}

