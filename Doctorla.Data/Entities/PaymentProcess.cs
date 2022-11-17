using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class PaymentProcess
    {
        public int Id { get; set; }
        public DateTime IDate { get; set; }
        public int userId { get; set; }
        public int ServiceId { get; set; }
        public string ProcessMessage { get; set; }
    }
}
