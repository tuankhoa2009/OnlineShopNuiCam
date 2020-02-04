using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class SlideDao
    {
        OnlineShopDbContext db = null;

        public SlideDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Slide> listAllData()
        {

            return db.Slides.Where(s => s.Status == true).OrderByDescending(x => x.DisplayOrder).ToList();
        }


    }
}
