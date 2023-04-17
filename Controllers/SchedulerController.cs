using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Appointment.Models;
namespace Appointment.Controllers
{
    public class SchedulerController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;


        public SchedulerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("DBAppointmentContextConnection"));
        }
        public List<SchedulerModel> GetAppointments()
        {
            List<SchedulerModel> ScheduleList = new();
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("FETCH_APPOINTMENTS", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SchedulerModel scheduler = new();
                scheduler.AppointmentId = (int)reader["AppointmentId"];
                scheduler.Title = (string)reader["Title"];
                scheduler.Description = (string)reader["Description"];
                scheduler.StartTime = (DateTime)reader["StartTime"];
                scheduler.EndTime = (DateTime)reader["EndTime"];
                scheduler.Location = (string)reader["Location"];
                ScheduleList.Add(scheduler);
            }

            reader.Close();
            _Connection.Close();

            return ScheduleList;

        }

        // GET: SchedulerController
        public ActionResult Index()
        {
            return View(GetAppointments());
        }

         SchedulerModel GetAppointments(int id)
         {
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("GET_APPOINTMENT", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", id);

            SqlDataReader reader = cmd.ExecuteReader();

            SchedulerModel scheduler = new();

            while (reader.Read())
            {

                scheduler.AppointmentId = (int)reader["AppointmentId"];
                scheduler.Title = (string)reader["Title"];
                scheduler.Description = (string)reader["Description"];
                scheduler.StartTime = (DateTime)reader["StartTime"];
                scheduler.EndTime = (DateTime)reader["EndTime"];
                scheduler.Location = (string)reader["Location"];
            }
            return scheduler;
        }
        SchedulerModel GetAppointment(int id)
        {
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("GET_APPOINTMENT", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", id);

            SqlDataReader reader = cmd.ExecuteReader();

            SchedulerModel scheduler = new();

            while (reader.Read())
            {

                scheduler.AppointmentId = (int)reader["AppointmentId"];
                scheduler.Title = (string)reader["Title"];
                scheduler.Description = (string)reader["Description"];
                scheduler.StartTime = (DateTime)reader["StartTime"];
                scheduler.EndTime = (DateTime)reader["EndTime"];
                scheduler.Location = (string)reader["Location"];
            }
            return scheduler;
        }

        // GET: SchedulerController/Details/5
        public ActionResult Details(int id)
        {
            return View(GetAppointment(id));
        }

        void InsertAppointment(SchedulerModel scheduler)
        {
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("ADD_APPOINTMENT", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", scheduler.Title);
            cmd.Parameters.AddWithValue("@Description", scheduler.Description);
            cmd.Parameters.AddWithValue("@StartTime", scheduler.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", scheduler.EndTime);
            cmd.Parameters.AddWithValue("@Location", scheduler.Location);

            cmd.ExecuteNonQuery();

            _Connection.Close();

        }

        // GET: SchedulerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchedulerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchedulerModel scheduler)
        {
            try
            {
                InsertAppointment(scheduler);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchedulerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetAppointments(id));
        }
        void UpdateAppointment(int id, SchedulerModel scheduler)
        {
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("EDIT_APPOINTMENTS", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", scheduler.Title);
            cmd.Parameters.AddWithValue("@Description", scheduler.Description);
            cmd.Parameters.AddWithValue("@StartTime", scheduler.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", scheduler.EndTime);
            cmd.Parameters.AddWithValue("@Location", scheduler.Location);
            cmd.Parameters.AddWithValue("@AppointmentId", id);

            cmd.ExecuteNonQuery();

            _Connection.Close();
        }
        // POST: SchedulerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SchedulerModel scheduler)
        {
            try
            {
                UpdateAppointment(id, scheduler);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchedulerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetAppointments(id));
        }

        // POST: SchedulerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SchedulerModel scheduler)
        {
            try
            {
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Appointments WHERE AppointmentId=@AppointmentId",
                    _Connection);

                cmd.Parameters.AddWithValue("@AppointmentId", id);
                cmd.ExecuteNonQuery();

                _Connection.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public bool ShowDiv = false;
        private List<SchedulerModel> _meetingList = new List<SchedulerModel>();
        public IActionResult Search()
        {
            ViewData["ShowDiv"] = ShowDiv;
            return View();
        }
        [HttpPost]
        public IActionResult Search(string _search)
        {
            _Connection.Open();
            ShowDiv = true;
            ViewData["ShowDiv"] = ShowDiv;

            string searchQuery = $"SELECT * FROM Appointments WHERE Title LIKE '%{_search}%' OR Description LIKE '%{_search}%' OR Location LIKE '%{_search}%';";
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

    }
}
