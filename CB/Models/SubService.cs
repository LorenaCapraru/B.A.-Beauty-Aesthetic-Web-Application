using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CB.Models
{
    public class SubService
    {
        public int SubServiceId { get; set; }
        public string SubServiceName { get; set; }

        public ICollection<Appointment> Appointments; 

    }
}
