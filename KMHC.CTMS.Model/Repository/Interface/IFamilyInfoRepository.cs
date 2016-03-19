/*
 * 描述: 定义FamilyInfo对外提供访问的接口
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151023   郝元彦              haoyuanyan@gmail.com    创建 
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
    public interface IFamilyInfoRepository
    {
        /// <summary>
        /// 保存家庭信息
        /// </summary>
        /// <param name="familyInfo"></param>
        /// <returns></returns>
        int Add(FamilyInfo familyInfo);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="familyInfo"></param>
        /// <returns></returns>
        bool Edit(FamilyInfo familyInfo);
        /// <summary>
        /// 获取指定家庭信息
        /// </summary>
        /// <param name="familyId"></param>
        /// <returns></returns>
        FamilyInfo Get(int familyId);

        /// <summary>
        /// 根据户主名称查询家庭信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<FamilyInfo> GetFamilyList(PageInfo page,string name);


        void Delete(int id);
    }
}
