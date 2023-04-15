using Appointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Appointment.Controllers
{
    public class ListController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;


        public ListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("DBAppointmentContextConnection"));
        }
        public List<ListModel> GetAppointment()
        {
            List<ListModel> ScheduleList = new();
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("FETCH_APPOINTMENTS", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListModel scheduler = new();
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
        public ActionResult List()
        {
            return View(GetAppointment());
        }
    }
}
