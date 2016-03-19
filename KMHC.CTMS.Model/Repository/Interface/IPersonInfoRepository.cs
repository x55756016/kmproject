/*
 * 描述:定义PersonInfo作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151015   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */

using System.Collections.Generic;
using System.Linq;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IPersonInfoRepository 
    {
        /// <summary>
        /// 保存个人基本资料
        /// </summary>
        /// <returns></returns>
        int Add(PersonInfo personInfo);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        bool Edit(PersonInfo personInfo);

        /// <summary>
        /// 获取个人基本资料
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        PersonInfo Get(int personId);

        /// <summary>
        /// 根据身份证号获得人员信息
        /// </summary>
        /// <param name="idno"></param>
        /// <returns></returns>
        PersonInfo Get(string idno);

        /// <summary>
        /// 根据姓名查询个人资料
        /// </summary>
        /// <returns></returns>
        IEnumerable<PersonInfo> GetPersonList(PageInfo page, string name);

        #region 个人体检记录

        /// <summary>
        /// 新增体检记录
        /// </summary>
        /// <param name="person"></param>
        /// <param name="record"></param>
        int AddExamineRecord(PersonInfo person, ExamineRecord record);

        /// <summary>
        /// 删除体检记录
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="recordId"></param>
        void DeleteExamineRecord(int personId, int recordId);

        /// <summary>
        /// 获取个人体检记录列表
        /// </summary>
        IEnumerable<ExamineRecord> GetExamineRecords(int personId);

        #endregion


    }
}
