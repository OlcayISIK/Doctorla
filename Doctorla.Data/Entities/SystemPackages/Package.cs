using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemPackages
{
    public class Package : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IDate { get; set; }
        public int IUser { get; set; }
        public DateTime? UUDate { get; set; }
        public int? UUser { get; set; }
        public string PackegeName { get; set; }
        public string Description { get; set; }
        public bool IsPremium { get; set; }
        public bool IsDoctor { get; set; }
        public bool IsDoctorSpec { get; set; }

        public virtual IEnumerable<PackageOffers> Offers { get; set; }
        public virtual IEnumerable<RelPackageDetail> RelPackageDetail { get; set; }
        public virtual IEnumerable<RelPackageValues> RelPackageValues { get; set; }
    }
}
