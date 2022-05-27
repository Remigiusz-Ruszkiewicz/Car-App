using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_App.Models
{
    public class Car
    {
        public Guid CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}
