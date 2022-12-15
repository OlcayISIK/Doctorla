using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto.Payment
{
    public class PaymentDto
    {
        public string IpAddress { get; set; }
        public long AppointmentId { get; set; }
        public CardDetailDto CardDetailDto { get; set; }
        public BillingAddressDto BillingAddressDto { get; set; }
    }
}
