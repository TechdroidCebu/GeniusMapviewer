using System;
using System.Collections.Generic;

//#nullable disable

namespace GeniusMapViewer.Models
{
    public partial class MhManholeMaster
    {
        public string NodeId { get; set; }
        public string Marker { get; set; }
        public int NodeType { get; set; }
        public double? Diameter { get; set; }
        public double? Depth { get; set; }
        public int CuId { get; set; }
    }
}
