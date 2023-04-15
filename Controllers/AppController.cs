using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Appointment.Models;

namespace Appointment.Controllers
{
    public class AppController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;

        public AppController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("AppointmentDB"));
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            //Console.WriteLine(id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int app)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int app)
        {
            try
            {
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Movies WHERE MovieID=@MovieID",
                    _Connection);

                cmd.Parameters.AddWithValue("@MovieID", id);
                cmd.ExecuteNonQuery();

                _Connection.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
