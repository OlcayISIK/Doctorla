using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data
{
    public interface IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
    }
}
