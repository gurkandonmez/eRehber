using eRehber.DataAccessLayer.EntityFramework;
using eRehber.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRehber.DataAccessLayer.EntityFrameWork
{
   public class DatabaseContext:  DbContext
    {
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Sube> Sube { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
