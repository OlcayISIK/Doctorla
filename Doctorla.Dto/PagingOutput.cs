using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto
{
    public class PagingOutput
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
    }
}
