using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Donations
    {
        public int Id { get; set; }
        public DateTime IDate { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DonationCompany { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
        public int? AppointmentId { get; set; }
        public double Price { get; set; }
        public DonationType DonationType { get; set; } // 0:Appointment  - 1:Support

    }
}
