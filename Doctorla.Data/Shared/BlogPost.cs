using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Shared
{
    public class BlogPost : Entity
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Category { get; set; }
        public string CoverPhoto { get; set; }
        public string CoverPhotoMobile { get; set; }
        public string SeoUrl { get; set; }
        public string ThumbnailPhoto { get; set; }
    }
}
