using Doctorla.Data.Members.DoctorEntity;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.DoctorEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto.Shared
{
    public class SpecialtyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
