using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Doctorla.Data.Members;

namespace Doctorla.Data.Entities.SystemPackages
{
    public class RelUserDepartment : Entity
    {
        public int Price { get; set; }
        public int SessionTime { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public Department Department { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
