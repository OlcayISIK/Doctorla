using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.Region
{
    public class County : Entity, IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
