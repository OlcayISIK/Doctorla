using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Data.Entities.SystemPackages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Department : Entity, IType
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public ICollection<RelUserDepartment> RelUserDepartments { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
    }
}
