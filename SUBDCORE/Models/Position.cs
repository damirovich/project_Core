using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employees>();
        }

        public byte IdPosition { get; set; }
        public string Position1 { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
