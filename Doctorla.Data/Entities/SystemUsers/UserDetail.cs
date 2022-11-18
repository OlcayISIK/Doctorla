using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemUsers
{
    public class UserDetail : Entity
    {
        public string UserJob { get; set; }
        public string Note { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Bloodtype { get; set; } // Kan Tipi
        public string ChronicDiseases { get; set; } //Kronik Hastalık
        public string FamilyDiseases { get; set; } //Ailede Bulunan Hastalıklar
        public string RegularlyMedicines { get; set; } // Düzcenli Kullanılan İlaçlar
        public string SurgicalHistory { get; set; } // Ameliyat geçmişi
        public string HearUs { get; set; } // Bizi Nereden Duydunuz
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
