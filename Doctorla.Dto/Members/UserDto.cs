using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto.Members
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string HashedPassword { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Job { get; set; }
        public string Note { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public BloodType BloodType { get; set; }
        public string ChronicDiseases { get; set; } //Kronik Hastalık
        public string FamilyDiseases { get; set; } //Ailede Bulunan Hastalıklar
        public string RegularlyTakenMedicines { get; set; } // Düzcenli Kullanılan İlaçlar
        public string SurgicalHistory { get; set; } // Ameliyat geçmişi
        public AccountStatus Status { get; set; }
    }
}
