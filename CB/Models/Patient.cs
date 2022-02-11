using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CB.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }

        public ICollection<Appointment> Appointments;

    }
}
