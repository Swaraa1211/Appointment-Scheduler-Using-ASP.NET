using Microsoft.AspNetCore.Mvc;

namespace Appointment.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
