using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.Region
{
    public class Country : Entity, IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
        [JsonIgnore]
        public ICollection<City> Cities { get; set; }
    }
}
