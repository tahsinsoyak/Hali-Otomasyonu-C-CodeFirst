using System;
using System.Collections.Generic;
//kütüphane ekledik entity çalışsın diye
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tahsinsoyakhali.Class
{
    //kalıtım yapıyoruz
    class HaliDbContext:DbContext
    {
        public DbSet<Hali> Halis { get; set; }


    }
}
