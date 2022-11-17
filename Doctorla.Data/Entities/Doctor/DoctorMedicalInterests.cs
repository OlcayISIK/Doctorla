using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.Doctor
{
    public class DoctorMedicalInterests : IDoctorDetails
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen İlgi Alanını Boş Geçmeyiniz")]
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public int DoctorDetailId { get; set; }
        [JsonIgnore]
        public DoctorDetail DoctorDetail { get; set; }
    }
}
