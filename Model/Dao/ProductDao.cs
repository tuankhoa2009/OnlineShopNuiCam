using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;

        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Product> listProductAll(string searchString)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Product> listNewProduct(int top)
        {

            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();

        }

        public List<Product> listFutureProduct(int top)
        {

            return db.Products.Where(s => s.TopHot != null).OrderByDescending(x => x.CreatedDate).Take(top).ToList();

        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(s => s.Name).ToList();
        }

        public List<Product> listRelatedProduct(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(s => s.ID != productId && s.CategoryID == product.CategoryID).ToList();

        }

        public IEnumerable<Product> listProductCategoryById(long id)
        {
            IQueryable<Product> model = db.Products;
            model = model.Where(x => x.CategoryID == id);
            return model.OrderByDescending(x => x.CreatedDate).ToList();

        }

        public IEnumerable<Product> listProductCategoryBySearch(string keyWord)
        {
            IQueryable<Product> model = db.Products;
            model = model.Where(x => x.Name.Contains(keyWord));
            return model.OrderByDescending(x => x.CreatedDate).ToList();

        }

        public long InsertProduct(Product model)
        {
            db.Products.Add(model);
            db.SaveChanges();
            return model.ID;
        }


        public Product ViewDetail(long id)
        {
            var itemProduct = db.Products.FirstOrDefault(s => s.ID == id);
            if (itemProduct != null)
            {
                return itemProduct;
            }
            return null;

        }
        public bool Update(Product model)
        {
            var product = db.Products.FirstOrDefault(x => x.ID == model.ID);
            if (product != null)
            {
                product.Name = model.Name;
                product.Code = model.Code;
                product.Price = model.Price;
                product.Description = model.Description;
                product.CreatedDate = model.CreatedDate;
                product.CategoryID = model.CategoryID;
                product.Image = model.Image;
                product.MetaTitle = model.MetaTitle;
                product.Quantity = model.Quantity;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public long Delete (long id)
        {
            var product = db.Products.FirstOrDefault(x => x.ID == id);
            if(product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return product.ID;
            }
            return 0;
        }


        public long ChangeStatusProduct(long id)
        {

            var res = db.Products.FirstOrDefault(s => s.ID == id);
            if (res != null)
            {
                if(res.Satus == 1)
                {
                    res.Satus = 0;
                }
                else
                {
                    res.Satus = 1;
                }
                db.SaveChanges();
                return res.Satus.Value;
            }
            return 0;
        }

    }
}