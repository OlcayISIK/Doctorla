using Doctorla.Data.Entities.SystemAppointments;
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
    public class Department : IBaseEntity, IType
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IDate { get; set; }
        public int IUser { get; set; }
        public DateTime? UUDate { get; set; }
        public int? UUser { get; set; }

        public int ParentId { get; set; }
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public ICollection<RelUserDepartment> RelUserDepartments { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
    }
}
