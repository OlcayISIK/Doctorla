using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Shared.Blog
{
    public class BlogPostTagRelation : Entity
    {
        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
