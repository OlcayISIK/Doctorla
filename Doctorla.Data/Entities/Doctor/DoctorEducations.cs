using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.Doctor
{
    public class DoctorEducations : IDoctorDetails
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen Üniversite Adını Boş Geçmeyiniz")]
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public string Branch { get; set; }

        [Required(ErrorMessage = "Lütfen Üniversite Başlangıç  Girin")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        [Required(ErrorMessage = "Lütfen Üniversite Bitiş  Girin")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int DoctorDetailId { get; set; }
        [JsonIgnore]
        public DoctorDetail DoctorDetail { get; set; }
    }
}
