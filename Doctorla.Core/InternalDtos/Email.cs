using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.InternalDtos
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> EmailToList { get; set; }
        public List<string> EmailCcList { get; set; }
        public List<string> EmailBccList { get; set; }
    }
}
