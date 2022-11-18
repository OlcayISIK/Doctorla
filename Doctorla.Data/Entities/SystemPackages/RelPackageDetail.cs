using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemPackages
{
    public class RelPackageDetail : Entity
    {
        public int PackageId { get; set; }
        public int PackageDetailId { get; set; }
        [JsonIgnore]
        public Package Package { get; set; }
        public PackageDetail PackageDetail { get; set; }
    }
}
