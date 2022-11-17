using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemUsers
{
    public class UserType : IType
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public string DefaultName { get; set; }
        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}
