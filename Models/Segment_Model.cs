using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeniusMapViewer.Models
{
    public class Segment_Model
    { 
            public int m_sg_id { get; set; }
            public int m_sgg_id { get; set; }
            public string segmentId { get; set; }
            public string segmentType { get; set; }
            public string nodeIdFrom { get; set; }
            public string nodeIdTo { get; set; }
            public int wireCuId { get; set; }
            public int derivedDistanceNdNd { get; set; }
            public int sagPercent { get; set; }
            public int actualWireLength { get; set; }
            public int jaId { get; set; }
            public string parentSegmentId { get; set; }
            public int autoSequence { get; set; }
            public int left { get; set; }
            public int right { get; set; }
            public string negParentSegId { get; set; }
            public string fieldOrientationCode { get; set; }
            public string connectivityOrientationCode { get; set; }
            public string ssgId { get; set; }
            public string LocalparentSegmentId { get; set; }
            public string LocalnegParentSegId { get; set; }

     

    }
}