using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class Userrepository : DbContext
    {
        //public Userrepository() :
        //    base("PredpriyatieEntities")
        //{ }
        public DbSet<Employees> employees { get; set; }
        public DbSet<Position> roles { get; set; }
    }
}
