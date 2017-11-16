using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndignadoFramework.Entities
{
    
    public class WebFeed
    {
        public enum TipoFuente { RSS, ATOM, VIDEO, OTRO };

        public TipoFuente Tipo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Language { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string Copyright { get; set; }
        public List<WebFeedNode> Nodes { get; set; }

    }
}
