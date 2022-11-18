using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class MenuType : Entity, IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}
