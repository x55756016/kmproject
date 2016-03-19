using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Authorization;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义UserTypeRoles的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20160203      刘佳豪              创建 
     *  
     */
    public interface IUserTypeRolesRepository
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddUserTypeRoles(UserTypeRoles model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="userTypeRoleId"></param>
        /// <returns></returns>
        bool DeleteUserTypeRolesById(string userTypeRoleId);

        /// <summary>
        /// 根据用户类型获取实体列表
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        List<UserTypeRoles> GetUserTypeRoleByUserType(int userType);

        /// <summary>
        /// 更新模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateUserTypeRoles(UserTypeRoles model);
    }
}
