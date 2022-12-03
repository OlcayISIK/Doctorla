using Doctorla.Data.Entities.SystemPackages;
using Doctorla.Data.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class PackagePaymentResult : PaymentResult
    {
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Package Package { get; set; }
        [JsonIgnore]
        public int DoctorId { get; set; }
        [JsonIgnore]
        public double Amount { get; set; }
        [JsonIgnore]
        public int ValidityDates { get; set; }
    }
}
