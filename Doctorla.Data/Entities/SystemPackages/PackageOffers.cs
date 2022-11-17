using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemPackages
{
    public class PackageOffers : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IDate { get; set; }
        public int IUser { get; set; }
        public DateTime? UUDate { get; set; }
        public int? UUser { get; set; }
        public int PackageId { get; set; }
        public int ValidityDates { get; set; }
        public decimal OldAmount { get; set; }
        public decimal NewAmount { get; set; }
        public bool IsFree { get; set; }
        public bool IsDefault { get; set; }
        [JsonIgnore]
        public Package Package { get; set; }
    }
}
