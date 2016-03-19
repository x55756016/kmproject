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
     * 描述:定义ExamineTemplate的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151230      刘佳豪              创建 
     *  
     */
    public interface IExamineTemplateRepository
    {
        /// <summary>
        /// 通过名称来获取
        /// </summary>
        /// <returns></returns>
        List<ExamineTemplates> GetExamineTemplatesByName(int type,string name, ref PageInfo pageInfo);

        /// <summary>
        /// 通过描述来获取
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        List<ExamineTemplates> GetExamineTemplatesByDescription(int type,string description, ref PageInfo pageInfo);

        /// <summary>
        /// 通过id来获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineTemplates GetExamineTemplateById(string id);

        /// <summary>
        /// 获取所有检验模版数据
        /// </summary>
        /// <returns></returns>
        List<ExamineTemplates> GetExamineTemplates(int type,ref PageInfo pageInfo);

        /// <summary>
        /// 通过关键字来获取
        /// </summary>
        /// <returns></returns>
        List<ExamineTemplates> GetExamineTemplatesByKwd(string kwd, ref PageInfo pageInfo);

        /// <summary>
        /// 添加检验模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddExamineTemplates(ExamineTemplates model);

        /// <summary>
        /// 更新检验模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateExamineTemplates(ExamineTemplates model);

        /// <summary>
        /// 根据id删除检验模版数据
        /// </summary>
        /// <param name="id"></param>
        bool DeleteExamineTemplates(string id);

        /// <summary>
        /// 根据编码获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<ExamineTemplates> GetExamineTemplatesByCode(string code);

        /// <summary>
        /// 通过类型获取数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<ExamineTemplates> GetExamineTemplateByType(int type);
    }
}
