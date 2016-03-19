using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Examine;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义ExamineTemplateItemOptions的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151231      刘佳豪              创建 
     *  
     */
    public interface IExamineTemplateItemOptionsRepository
    {
        /// <summary>
        /// 添加检验模版项选项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddExamineTemplateItemOptions(ExamineTemplateItemOptions model);

        /// <summary>
        /// 更新检验模版项选项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateExamineTemplateItemOptions(ExamineTemplateItemOptions model);

        /// <summary>
        /// 根据id删除模版项选项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteExamineTemplateItemOptionsById(string id);

        /// <summary>
        /// 根据模版项id删除模版项选项
        /// </summary>
        /// <param name="templateItemId"></param>
        /// <returns></returns>
        bool DeleteExamineTemplateItemByTemplateItemId(string templateItemId);

        /// <summary>
        /// 根据模版项id获取所有模版项选项
        /// </summary>
        /// <param name="templateItemId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        List<ExamineTemplateItemOptions> GetExamineTemplateItemOptionsByTemplateItemId(string templateItemId, ref PageInfo pageInfo);

        /// <summary>
        /// 根据id获取模版项选项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineTemplateItemOptions GetExamineTemplateItemOptionsById(string id);

        /// <summary>
        /// 通过关键字来获取
        /// </summary>
        /// <param name="templateItemId"></param>
        /// <param name="kwd"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        List<ExamineTemplateItemOptions> GetExamineTemplateItemOptionsByKwd(string templateItemId, string kwd, ref PageInfo pageInfo);
    }
}
