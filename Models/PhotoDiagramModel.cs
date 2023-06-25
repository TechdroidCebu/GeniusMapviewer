using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusMapViewer.Models
{
    public class PhotoDiagramModel
    {
       
        public int pdId { get; set; }
        public int cuTransId { get; set; }
        public string file { get; set; }
        public string fileType { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime dtSaved { get; set; }
        public bool status { get; set; }

    }
}
