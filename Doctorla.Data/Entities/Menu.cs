using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Menu : IBaseEntity, IType
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IDate { get; set; }
        public int IUser { get; set; }
        public DateTime? UUDate { get; set; }
        public int? UUser { get; set; }
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
