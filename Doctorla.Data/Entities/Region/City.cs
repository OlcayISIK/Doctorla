using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.Region
{
    public class City : Entity, IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [JsonIgnore]
        public ICollection<County> Counties { get; set; }
    }
}
