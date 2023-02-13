using Azul.Framework.Data.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Data.Entities
{
    public class PartNumberQuantityEntity :DataMappingBase<long>
    {
        public override long Id { get; set; }
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
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }        
    }
}
