﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
        OnlineShopDbContext db = null;

        public FooterDao()
        {
            db = new OnlineShopDbContext();

        }

        public Footer GetFooter ()
        {
            var model = db.Footers.FirstOrDefault(x => x.Status == true);

            return model;

        }



    }
}
