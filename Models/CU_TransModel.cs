using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusMapViewer.Models
{
    public class CU_TransModel
    {


        public int cuTransId { get; set; }
        public string nodeId { get; set; }
        public string referenceId { get; set; }
        public string referenceType { get; set; }
        public int segmentId { get; set; }
        public string cuId { get; set; }
        public string jaIdInstallation { get; set; }
        public Nullable<DateTime> dtInstalled { get; set; }
        public Nullable<DateTime> dtActivated { get; set; }
        public string jaIdRemoval { get; set; }
        public Nullable<DateTime> dtRemoved { get; set; }
        public bool dU_Owned { get; set; }
        
    }
}
