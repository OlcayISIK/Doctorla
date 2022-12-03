using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doctorla.Data.Members;

namespace Doctorla.Data.Entities
{
    public class RoleGroup : IType
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
