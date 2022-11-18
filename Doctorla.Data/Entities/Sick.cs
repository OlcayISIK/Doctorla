using Doctorla.Data.Entities.SystemAppoinments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Sick : Entity, IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public ICollection<Appointment> Appointment { get; set; }

    }
}
