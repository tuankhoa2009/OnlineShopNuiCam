using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.EF;

namespace Model.Dao
{
    public class UserDao
    {
        private OnlineShopDbContext db = null;

        public UserDao ()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert (User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return (entity.ID);
        }

        public User GetById(string username)
        {
            var user = db.Users.FirstOrDefault(x =>x.UserName == username);
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public User SingleUserID (int id)
        {
            var objUser = db.Users.FirstOrDefault(s => s.ID == id);
            if(objUser != null)
            {
                return objUser;
            }

            return null;
        }

        public bool Update (User entity)
        {
            var user = db.Users.FirstOrDefault(p => p.ID == entity.ID);
            if(user != null)
            {
                user.UserName = entity.UserName;
                user.Name = entity.Name;
                user.Password = entity.Password;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var userDelete = db.Users.FirstOrDefault(p => p.ID == id);
            if(userDelete !=null)
            {
                db.Users.Remove(userDelete);
                db.SaveChanges();
                return true;
            }
            return false;

        }


        public IEnumerable<User> listAllUser(string searchString)
        {
            IQueryable<User> model = db.Users;
            if(!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Name.Contains(searchString) || s.UserName.Contains(searchString));
            }
          
            return model.OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == userName);

            if (user != null)
            {
                //var data = (from a in db.Credentials
                //            join b in db.UserGroups on a.UserGroupID equals b.ID
                //            join c in db.Roles on a.RoleID equals c.ID
                //            where b.ID == user.GroupID
                //            select new
                //            {
                //                RoleID = a.RoleID,
                //                UserGroupID = a.UserGroupID
                //            }).AsEnumerable().Select(x => new Credential()
                //            {
                //                RoleID = x.RoleID,
                //                UserGroupID = x.UserGroupID
                //            });
                //return data.Select(x => x.RoleID).ToList();
                var credentialList = db.Credentials.Where(s => s.UserGroupID == user.GroupID).Select(x => x.RoleID).ToList();
                return credentialList;
            }
            else
            {
                return new List<string>{""} ;
            }

        }


        public int Login(string username,string password, bool isLoginAdmin = false)
        {
            var res = db.Users.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if (isLoginAdmin == true) // KT tk là ADMIN hoặc là MOD
            {
                if (res == null)
                {
                    return 0; // Tài khoản hoặc mật khẩu không đúng
                }
                else if (res.GroupID == CommonConstants.ADMIN_GROUP || res.GroupID == CommonConstants.MOD_GROUP)
                {
                    if (res.Status == false)
                        return -1; //Status  == false tài khoản bị khóa
                    else if (res.Status == true)
                        return 1; //Đăng nhập thành công
                    return 0;
                }
                else
                {
                    return -2;
                }
               
            }
            else
            {
                if (res == null)
                {
                    return 0; // Tài khoản hoặc mật khẩu không đúng
                }
                else
                {
                    if (res.Status == false)
                        return -1; //Status  == false tài khoản bị khóa
                    else
                        return 1; //Đăng nhập thành công
                }
               
            }
          

        }


        public bool ChangeStatus(long id)
        {

            var res = db.Users.FirstOrDefault(s => s.ID == id);
            if(res != null)
            {
                res.Status = !res.Status;
                db.SaveChanges();
                return !res.Status;
            }
            return res.Status;
        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(s => s.UserName == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.Users.Count(s => s.UserName == email) > 0;
        }




    }
}
