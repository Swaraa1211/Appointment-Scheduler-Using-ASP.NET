using Appointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Appointment.Controllers
{
    public class SearchController : Controller
    {
        private readonly IConfiguration _Configuration;
        public SearchController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        private SqlConnection _Connection;
        private List<SchedulerModel> _meetingList = new List<SchedulerModel>();

        public bool ShowDiv = false;

        public void Connection()
        {
            string conn = _Configuration.GetConnectionString("DBAppointmentContextConnection");
            _Connection = new SqlConnection(conn);
            _Connection.Open();
        }
        public IActionResult Search()
        {
            ViewData["ShowDiv"] = ShowDiv;
            return View();
        }
        [HttpPost]
        public IActionResult Search(string search)
        {
            Connection();
            ShowDiv = true;
            ViewData["ShowDiv"] = ShowDiv;

            string searchQuery = $"SELECT * FROM Appointments WHERE Title LIKE '{search}%' OR Description LIKE '{search}%' OR Location LIKE '{search}%';";
            Console.WriteLine(searchQuery);
            try
            {
                using (SqlCommand cmd = new SqlCommand(searchQuery, _Connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        SchedulerModel meet = new SchedulerModel();
                        meet.AppointmentId = (int)reader[0];
                        meet.Title = (string)reader[1];
                        meet.Description = (string)reader[2];
                        meet.StartTime = (DateTime)reader[3];
                        meet.EndTime = (DateTime)reader[4];
                        meet.Location = (string)reader[5];
                        _meetingList.Add(meet);


                    }

                    ViewBag.MeetingList = _meetingList;
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(ViewBag);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}