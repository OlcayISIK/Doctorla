using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doctorla.Data.Entities.DailyChecking;

namespace Doctorla.Data.Entities.SystemPackages
{
    public class PackageChat
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int DailyCheckId { get; set; }
        public bool IsDoctor { get; set; }
        public bool IsFile { get; set; }

        public DailyCheck DailyCheck { get; set; }
    }
}
