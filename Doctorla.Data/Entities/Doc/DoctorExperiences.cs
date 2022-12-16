using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.Doc
{
    public class DoctorExperiences : IDoctorDetails
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen Hastane Adını Boş Geçmeyiniz")]
        public string Name { get; set; }
        public string DefaultName { get; set; }
        public string Branch { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public int DoctorDetailId { get; set; }
        [JsonIgnore]
        public DoctorDetail DoctorDetail { get; set; }
    }
}
