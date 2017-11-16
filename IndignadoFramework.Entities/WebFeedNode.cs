using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndignadoFramework.Entities
{
    public class WebFeedNode
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastUpdateTime { get; set; } 
        public string Summary { get; set; } 
        public string Link { get; set;}

        //Videos
        public string ThumbnailUrl { get; set; }
        public string Updated { get; set; }
    }
}
