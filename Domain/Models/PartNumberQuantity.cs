using System;

namespace Domain.Models
{
    public class PartNumberQuantity
    {
        public long Id { get; set; }
        public string Pn { get; set; }
        public string Sn { get; set; }
        public string PnInterchangeable { get; set; }
        public string PnDescription { get; set; }
        public string Category { get; set; }
        public string Vendor { get; set; }
        public string QtyAvailable { get; set; }
        public string QtyReserved { get; set; }
        public string QtyInTransfer { get; set; }
        public string QtyPendingRi { get; set; }
        public string QtyUs { get; set; }
        public string QtyInRepair { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
