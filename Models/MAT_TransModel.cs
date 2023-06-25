using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusMapViewer.Models
{
    public class MAT_TransModel
    {

        public int matTransId { get; set; }
        public int cuTransId { get; set; }
        public string matId { get; set; }
        public int? actualQty { get; set; }
        public bool brandNewStatus { get; set; }
        public int cost { get; set; }
        public string currency { get; set; }

    }


    public class Mat_trans_with_desc
    {
        public MAT_TransModel matTran { get; set; }
        public string Description { get; set; }
    }




}
