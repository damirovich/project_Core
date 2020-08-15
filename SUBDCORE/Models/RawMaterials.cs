using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class RawMaterials
    {
        public RawMaterials()
        {
            Ingredients = new HashSet<Ingredients>();
            PurchaseOfCheese = new HashSet<PurchaseOfCheese>();
        }

        public short IdRawMaterials { get; set; }
        public string Names { get; set; }
        public int? UnitOfMeasure { get; set; }
        public decimal? Summ { get; set; }
        public double? Quantity { get; set; }

        public virtual UnitOfMeasure UnitOfMeasureNavigation { get; set; }
        public virtual ICollection<Ingredients> Ingredients { get; set; }
        public virtual ICollection<PurchaseOfCheese> PurchaseOfCheese { get; set; }
    }
}
