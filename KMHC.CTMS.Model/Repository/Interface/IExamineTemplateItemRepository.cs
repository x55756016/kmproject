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
     * 描述:定义ExamineTemplateItems的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151231      刘佳豪              创建 
     *  
     */
    public interface IExamineTemplateItemRepository
    {
        /// <summary>
        /// 添加检验模版项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddExamineTemplateItems(ExamineTemplateItems model);

        /// <summary>
        /// 更新检验模版项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateExamineTemplateItems(ExamineTemplateItems model);

        /// <summary>
        /// 根据id删除模版项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteExamineTemplateItemsById(string id);

        /// <summary>
        /// 根据模版id删除所有模板项
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        bool DeleteExamineTemplateItemByTemplateId(string templateId);

        /// <summary>
        /// 根据模版id获取所有模版项
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        List<ExamineTemplateItems> GetExamineTemplateItemsByTemplateId(string templateId,ref PageInfo pageInfo);

        /// <summary>
        /// 根据id获取模版项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineTemplateItems GetExamineTemplateItemsById(string id);

        /// <summary>
        /// 通过关键字来获取
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="kwd"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        List<ExamineTemplateItems> GetExamineTemplateItemsByKwd(string parentId,string kwd, ref PageInfo pageInfo);
    }
}
