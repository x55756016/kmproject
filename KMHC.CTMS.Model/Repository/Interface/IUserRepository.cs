using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IUserRepository
    {
        IQueryable<HPN_Users> getUsers();

        HPN_Users getUserByEmail(string email);

        HPN_Users getUserByMobilePhone(string mobilephone);
    }
}
