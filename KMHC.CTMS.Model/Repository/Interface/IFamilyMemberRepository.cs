/*
 * 描述:定义家庭成员作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151026  johnny                                      创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IFamilyMemberRepository
    {
        /// <summary>
        /// 保存家庭成员
        /// </summary>
        /// <returns></returns>
        int Add(FamilyMember familyMember);

        /// <summary>
        /// 修改家庭成员
        /// </summary>
        /// <returns></returns>
        bool Edit(FamilyMember familyMember);
        /// <summary>
        /// 获取指定家庭成员
        /// </summary>
        /// <param name="fMemberId"></param>
        /// <returns></returns>
        FamilyMember Get(int fMemberId);

        /// <summary>
        /// 根据户主名称查询家庭成员
        /// </summary>
        /// <returns></returns>
        IEnumerable<FamilyMember> GetFMemberList(int familyId);


        void Delete(int id);
    }
}
