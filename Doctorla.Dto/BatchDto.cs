using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto
{
    public class BatchDto<T>
    {
        public BatchDto()
        {
            AddedItems = new List<T>();
            UpdatedItems = new List<T>();
            RemovedItems = new List<T>();
        }

        public List<T> AddedItems { get; set; }
        public List<T> UpdatedItems { get; set; }
        public List<T> RemovedItems { get; set; }
    }
}
