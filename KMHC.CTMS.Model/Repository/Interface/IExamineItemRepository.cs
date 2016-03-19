/*
 * 描述:定义ExamineItem对外访问的接口,ExmineItem是一个独立的aggerate root
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151016   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IExamineItemRepository 
    {

        /// <summary>
        /// 新增检查项目
        /// </summary>
        /// <returns></returns>
        int Add(ExamineItem item);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        bool Edit(ExamineItem item);

        /// <summary>
        /// 获取检查项目
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        ExamineItem Get(int itemId);

        /// <summary>
        /// 根据项目编号获取检查项目
        /// </summary>
        /// <param name="itemno"></param>
        /// <returns></returns>
        ExamineItem Get(string itemno);

    }
}
