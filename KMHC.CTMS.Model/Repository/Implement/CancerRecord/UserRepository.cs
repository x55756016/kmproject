using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _db = new CRDatabase();

        public HPN_Users getUserByEmail(string email)
        {
            return (from x in _db.Set<HPN_Users>() where x.Email == email select x).FirstOrDefault();
        }

        public HPN_Users getUserByMobilePhone(string mobilephone)
        {
            return (from x in _db.Set<HPN_Users>() where x.MobilePhone == mobilephone select x).FirstOrDefault();
        }

        public IQueryable<HPN_Users> getUsers()
        {
            return _db.Set<HPN_Users>();
        }
    }
}
