using System;

namespace GeniusMapViewer.Models
{
    public class JobActivityModel    {
        public string jaId { get; set; }
        public string jaNumber { get; set; }
        public string joId { get; set; }    
        public string crewMemberList { get; set; }
        public Nullable<int> assetClassId { get; set; }   
        public Nullable<float> jaDaysRequired { get; set; }
        public Nullable<DateTime> proposedStartDate { get; set; }  
        public Nullable<int> jaDaysAccumulated { get; set; }
        public Nullable<DateTime> jaActualStartDate { get; set; }
        public Nullable<DateTime> jaActualFinished { get; set; }
        public Nullable<int> laborCost { get; set; }
        public string laborCostUnit { get; set; }
    }
}
