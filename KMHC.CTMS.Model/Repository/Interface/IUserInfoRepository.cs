using System.Collections.Generic;
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义UserInfo的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151113      刘佳豪              创建 
     *  
     */
    public interface IUserInfoRepository
    {
        /// <summary>
        /// 通过登录名获取用户对象
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        UserInfo GetUserInfoByLoginName(string loginName);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddUserInfo(UserInfo user);

        /// <summary>
        /// 通过邮箱获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        UserInfo GetUserInfoByEmail(string email);

        /// <summary>
        /// 通过手机获取用户信息
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        UserInfo GetUserInfoByMobilePhone(string mobilePhone);

        /// <summary>
        /// 通过id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetUserInfoByID(string id);

        /// <summary>
        /// 根据id更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UpdateUserInfo(UserInfo user);

        /// <summary>
        /// 根据用户类型获取数据
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        List<UserInfo> GetUsersListByUserType(int userType);
    }
}
