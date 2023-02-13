using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class PartNumber
    {
        public long Id { get; set; }
        public string PnInterchangeable { get; set; }
        public string Pn { get; set; }
        public string PnDescription { get; set; }
        public string Category { get; set; }
        public string StockUom { get; set; }
        public string HazardousMaterial { get; set; }
        public string Status { get; set; }
        public string ShelfLifeFlag { get; set; }
        public long ShelfLifeDays { get; set; }
        public string Chapter { get; set; }
        public string Section { get; set; }
        public decimal AvarangeCost { get; set; }
        public decimal StandardCost { get; set; }
        public decimal SecondaryCost { get; set; }
        public string ToolCalibrationFlag { get; set; }
        public long ToolLifeDays { get; set; }
        public string HazardousMateriaNo { get; set; }
        public string ToolControlItem { get; set; }
        public string InventoryType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
