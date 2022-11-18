using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemPackages
{
    public class PackageDetail : Entity
    {
        public int ScreenOrder { get; set; }
        public bool IsPremium { get; set; }
        public bool AddDefaultOfferDays { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<RelPackageDetail> RelPackageDetail { get; set; }
    }
}
