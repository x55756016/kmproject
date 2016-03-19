/*
 * 描述:定义FD_Member作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              Email              内容
 * 20151102      张志明              			创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IFD_MemberRepository
    {
        /// <summary>
        /// 添加家族成员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        string Add(FD_Member member);

        /// <summary>
        /// 编辑家族成员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        bool Edit(FD_Member member);

        /// <summary>
        /// 删除家族成员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        bool Delete(string ID);

        /// <summary>
        /// 根据用户账号获取家族成员
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<FD_Member> GetByUserID(string userID);

        /// <summary>
        /// 批量添加家族成员
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool AddList(List<FD_Member> list);
    }
}
