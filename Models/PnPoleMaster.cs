using System;
using System.Collections.Generic;

//#nullable disable

namespace GeniusMapViewer.Models
{
    public partial class PnPoleMaster
    {
        public string NodeId { get; set; }
        public int NodeType { get; set; }
        public string PoleNumber { get; set; }
        public int MaterialType { get; set; }
        public double? Height { get; set; }
        public int CuId { get; set; }
        public DateTime? DtNumPainted { get; set; }
    }
}
