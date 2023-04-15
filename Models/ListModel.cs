using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Appointment.Models
{
    public class ListModel
    {
        [Key]
        public int AppointmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
    }
}
