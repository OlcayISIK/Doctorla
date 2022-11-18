using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Menu : Entity, IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int? ParentId { get; set; }
        public int TypeId { get; set; }
        public MenuType MenuType { get; set; }

        public virtual Menu Parent { get; set; }
        public ICollection<Menu> SubMenus { get; } = new List<Menu>();
    }
}
