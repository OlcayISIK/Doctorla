using Doctorla.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data
{
    public class HospitalBoundEntity : Entity
    {
        public long HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
