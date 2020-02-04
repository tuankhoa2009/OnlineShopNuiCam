using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CredentialDao
    {
        OnlineShopDbContext db = null;

        public CredentialDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Credential> listCredential()
        {
            var listdao = db.Credentials.OrderByDescending(s=>s.UserGroupID).ToList();
            return listdao;
        }
        
        public List<string> ListCredentialRoles(string nameUserGroup)
        {
            var listRole = db.Credentials.Where(x => x.UserGroupID.Contains(nameUserGroup)).ToList();

            return listRole.Select(s=>s.RoleID).ToList(); 
        }
    }
}
