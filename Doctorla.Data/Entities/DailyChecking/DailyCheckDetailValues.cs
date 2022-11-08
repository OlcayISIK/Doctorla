using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.DailyChecking
{
    public class DailyCheckDetailValues
    {
        public int Id { get; set; }
        public int DailyCheckDetailId { get; set; }
        public string Value { get; set; }
        public int TitleOrder { get; set; }
        public DateTime IDate { get; set; }
        public string Key { get; set; }
        [JsonIgnore]
        public DailyCheckDetail DailyCheckDetail { get; set; }
    }
}
