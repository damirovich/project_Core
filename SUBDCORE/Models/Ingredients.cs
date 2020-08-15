using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Ingredients
    {
        public short IdIngredients { get; set; }
        public int? Manufacturing { get; set; }
        public short? RawMaterials { get; set; }
        public double? Quantity { get; set; }

        public virtual FinishedProducts ManufacturingNavigation { get; set; }
        public virtual RawMaterials RawMaterialsNavigation { get; set; }
    }
}
