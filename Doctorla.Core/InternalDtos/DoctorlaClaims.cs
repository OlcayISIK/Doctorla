using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.InternalDtos
{
    public class DoctorlaClaims
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
    }
}
