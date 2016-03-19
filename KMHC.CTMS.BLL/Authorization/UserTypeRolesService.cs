using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.Model.Repository.Implement;

namespace KMHC.CTMS.BLL.Authorization
{
    /*
     * 描述:定义用户类型角色关系操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20160203      刘佳豪              创建 
     *  
     */
    public class UserTypeRolesService
    {
        public bool AddUserTypeRoles(UserTypeRoles model)
        {
            using (EFUserTypeRolesRepository _rsp = new EFUserTypeRolesRepository())
            {
                return _rsp.AddUserTypeRoles(model);
            }
        }

        public bool DeleteUserTypeRolesById(string userTypeRoleId)
        {
            using (EFUserTypeRolesRepository _rsp = new EFUserTypeRolesRepository())
            {
                return _rsp.DeleteUserTypeRolesById(userTypeRoleId);
            }
        }

        public List<UserTypeRoles> GetUserTypeRoleByUserType(int userType)
        {
            using (EFUserTypeRolesRepository _rsp = new EFUserTypeRolesRepository())
            {
                return _rsp.GetUserTypeRoleByUserType(userType);
            }
        }

        public bool UpdateUserTypeRoles(UserTypeRoles model)
        {
            using (EFUserTypeRolesRepository _rsp = new EFUserTypeRolesRepository())
            {
                return _rsp.UpdateUserTypeRoles(model);
            }
        }
    }
}
