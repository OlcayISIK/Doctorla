using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Doctorla.Data.Entities.SystemUsers;
using Doctorla.Data.Members;

namespace Doctorla.Data.Entities
{
    public class UsedCampaign : Entity
    {
        public int CampaignId { get; set; }
        public int UserId { get; set; }
        public DateTime UsedDate { get; set; }
        [JsonIgnore]
        public virtual Campaign Campaign { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public decimal UsedAmount { get; set; }
    }
}
