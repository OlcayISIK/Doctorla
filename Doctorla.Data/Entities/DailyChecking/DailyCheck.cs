using Doctorla.Data.Entities.SystemPackages;
using Doctorla.Data.Entities.SystemUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.DailyChecking
{
    public class DailyCheck
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public int NurseId { get; set; }
        public bool IsDoctor { get; set; }
        [NotMapped]
        public string DoctorName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        public bool IsPremium { get; set; }
        public string PackageName { get; set; }
        public string ExtraPhone { get; set; }
        public short Status { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public DateTime AlertDay { get; set; }
        public DateTime? LiveStartDate { get; set; }
        public DateTime? LiveEndDate { get; set; }
        public DateTime? LiveRequestDate { get; set; }
        public bool IsNewData { get; set; }
        public bool IsRequest { get; set; }
        public string LiveRoomName { get; set; }
        public int AlertDayCounter { get; set; }
        public string AlertDayHours { get; set; }
        public string NurseAlertDays { get; set; }
        public string NurseAlertDaysHours { get; set; }
        public string UserInfo { get; set; }
        public string PaymentId { get; set; }
        public decimal PaymentPrice { get; set; }
        public User User { get; set; }
        public User Nurse { get; set; }
        public int PackageId { get; set; }
        public ICollection<PackageChat> PackageChat { get; set; }
        public ICollection<DailyCheckDetail> DailyCheckDetail { get; set; }
        public ICollection<DailyCheckAlerts> DailyCheckAlerts { get; set; }

    }
}
