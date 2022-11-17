using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime PostDate { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public string PostCategory { get; set; }
        public string PostTags { get; set; }
        public string PostAuthor { get; set; }
        public string PostImageUrl { get; set; }
    }
}
