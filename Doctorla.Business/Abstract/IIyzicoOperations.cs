using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Dto;
using Doctorla.Dto.Payment;
using Doctorla.Dto.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Abstract
{
    public interface IIyzicoOperations
    {
        #region User
        Task<Result<bool>> PayForAppointment(PaymentDto paymentDto);
        #endregion
    }
}
