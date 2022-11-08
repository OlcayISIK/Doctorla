using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Doctorla.Data.Entities.SystemUsers;

namespace Doctorla.Data.Entities.SystemPackages
{
    public class RelUserDepartment : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IDate { get; set; }
        public int IUser { get; set; }
        public DateTime? UUDate { get; set; }
        public int? UUser { get; set; }
        public int Price { get; set; }
        public int SessionTime { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public Department Department { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
