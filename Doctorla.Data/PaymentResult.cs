using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data
{
    public class PaymentResult
    {
        public PaymentStatus Status { get; set; }
        public string Description { get; set; }
    }
}
