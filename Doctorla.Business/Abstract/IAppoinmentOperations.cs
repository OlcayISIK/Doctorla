using Doctorla.Data.Entities.SystemAppoinments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Abstract
{
    public interface IAppoinmentOperations
    {
        //GenericServiceListModel<Appointment> GetAppointments(int userId, bool isDoctor, SortingPagingInfo sorting);
        Appointment GetDetail(int appointmentId);
        (bool, string) DeleteAppointment(int userId, int appointmentId, string reason);
        //(bool, string) UpdateApointmentDetail(int userId, int appointmentId, List<FileReqModel> files, string appointmentNote, string appointmentDoctorNote, bool isDoctor = false);
        bool DeleteFile(int fileId);
        bool AppointmentSetPrivateStatus(int appointmentId, bool isPrivate);
        IEnumerable<Appointment> GetDoctorAvailableAppointments(int doctorId, int departmentId, DateTime appointmentDate);
        IEnumerable<Appointment> GetDoctorsHavingAppointmentsInGivenDay(int givenDay);
        //AppointmentBuyResult BuyAppointment(int userId, int appointmentId, string campaigneCode = "");
        (bool, string, string) RequestAppointment(int userId, int doctorId, int departmentId, DateTime reqDate, string note, short requestType);
        //GenericServiceListModel<NotifyWarning> GetAppointmentRequests(int userId, SortingPagingInfo sorting, bool isDoctor = false);
        (bool, string) CreateSession(int userId, int departmentId, DateTime startdate, DateTime? multienddate, int Price = 0, int sessionTime = 0, bool isMultiAppointment = false, int breakCount = 0, bool isPreview = false);
        (bool, string, string) SetAppointmentRequestStatus(int userId, int notifyWarningId, bool isApprove);
        (bool, string, int) ValidateCampaignCode(int userId, string code, int appointmentId);
    }
}
