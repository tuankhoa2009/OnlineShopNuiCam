using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;

        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<ProductCategory> listAllData()
        {

            return db.ProductCategories.Where(s => s.Status == true).OrderByDescending(x => x.DisplayOrder).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            var objCategoryDetail = db.ProductCategories.FirstOrDefault(s => s.ID == id);
            if (objCategoryDetail != null)
            {
                return objCategoryDetail;
            }
            return null;
        }

    }
}
