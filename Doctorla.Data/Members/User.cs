using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Members
{
    public class User : Member
    {
        public string Job { get; set; }
        public string Note { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public BloodType BloodType { get; set; }
        public string ChronicDiseases { get; set; } //Kronik Hastalık
        public string FamilyDiseases { get; set; } //Ailede Bulunan Hastalıklar
        public string RegularlyTakenMedicines { get; set; } // Düzcenli Kullanılan İlaçlar
        public string SurgicalHistory { get; set; } // Ameliyat geçmişi
        //public string HearUs { get; set; } // Bizi Nereden Duydunuz
    }
}
