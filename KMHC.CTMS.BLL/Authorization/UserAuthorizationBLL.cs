/*
 * 描述:获取用户权限
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20160229   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL;
using KMHC.CTMS.Model.Authorization;

namespace KMHC.CTMS.BLL.Authorization
{
    public class UserAuthorizationBLL
    {
        private readonly DbContext _context = DbSessionFactory.GetCurrentDbContext();

        /// <summary>
        /// 根据userid，获取对当前菜单code的权限
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="funId">权限菜单</param>
        public List<UserFunction> GetUserAuthByCode(string userId, string funId)
        {
            List<UserFunction> list = new List<UserFunction>();
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(funId))
            {
                return list;
            }
            _context.Database.SqlQuery<UserFunction>(
                "select distinct roleFun.functionid,permi.permissioncode " +
                "from CTMS_SYS_RoleFunction roleFun " +
                "inner join CTMS_SYS_Function fun on roleFun.functionid=fun.functionid and fun.isdeleted=0 " +
                "inner join CTMS_SYS_Permission permi on roleFun.permissionvalue = permi.permissionvalue and permi.isdeleted=0 " +
                "where  fun.functionid='" + funId + "' and  roleFun.isdeleted=0 and " +
                "roleFun.roleid in (select userrole.roleid " +
                "from CTMS_SYS_UserInfo userinfo " +
                "inner join CTMS_SYS_UserRole userrole on userinfo.userid = userrole.userid and userrole.isdeleted=0 " +
                "where userinfo.isdeleted=0 and userinfo.userid='" + userId + "')").ToList().ForEach(
                    p => list.Add(new UserFunction()
                    {
                        //ROLEID = p.ROLEID,
                        FUNCTIONCODE = p.FUNCTIONCODE,
                        FUNCTIONID = p.FUNCTIONID,
                        //PERMISSIONVALUE = p.PERMISSIONVALUE,
                        PERMISSIONCODE = p.PERMISSIONCODE
                    }));
            return list;
        }
    }
}
