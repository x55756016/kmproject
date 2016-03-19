using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Examine;
using KMHC.CTMS.Model.Repository.Implement;

namespace KMHC.CTMS.BLL.Examine
{
    /*
     * 描述:定义检验模版项操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151231      刘佳豪              创建 
     *  
     */
    public class ExamineTemplateItemService
    {
        private ExamineTemplateService _et = new ExamineTemplateService();

        /// <summary>
        /// 添加检验模版项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddExamineTemplateItems(ExamineTemplateItems model)
        {
            using (EFExamineTemplateItemRepository _rsp = new EFExamineTemplateItemRepository())
            {
                return _rsp.AddExamineTemplateItems(model);
            }
        }

        /// <summary>
        /// 更新检验模版项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExamineTemplateItems(ExamineTemplateItems model)
        {
            using (EFExamineTemplateItemRepository _rsp = new EFExamineTemplateItemRepository())
            {
                return _rsp.UpdateExamineTemplateItems(model);
            }
        }

        /// <summary>
        /// 根据id删除模版项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteExamineTemplateItemsById(string id)
        {
            using (EFExamineTemplateItemRepository _rsp = new EFExamineTemplateItemRepository())
            {
                return _rsp.DeleteExamineTemplateItemsById(id);
            }
        }

        /// <summary>
        /// 根据模版id删除所有模版项
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public bool DeleteExamineTemplateItemByTemplateId(string templateId)
        {
            using (EFExamineTemplateItemRepository _rsp = new EFExamineTemplateItemRepository())
            {
                return _rsp.DeleteExamineTemplateItemByTemplateId(templateId);
            }
        }

        /// <summary>
        /// 根据模版id获取所有模版项
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<ExamineTemplateItems> GetExamineTemplateItemsByTemplateId(string templateId, ref PageInfo pageInfo)
        {
            using (EFExamineTemplateItemRepository _rsp = new EFExamineTemplateItemRepository())
            {
                List<ExamineTemplateItems> list = _rsp.GetExamineTemplateItemsByTemplateId(templateId,ref pageInfo);
                if (list.Count > 0)
                {
                    ExamineTemplates model = _et.GetExamineTemplateById(list[0].ExamineTemplateId);
                    list.ForEach(p => p.TemplateName = model.Name);
                }
                return list;
            }
        }

        /// <summary>
        /// 根据id获取模版项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamineTemplateItems GetExamineTemplateItemsById(string id)
        {
            using (EFExamineTemplateItemRepository _rsp = new EFExamineTemplateItemRepository())
            {
                ExamineTemplateItems eti = _rsp.GetExamineTemplateItemsById(id);
                if (eti != null)
                {
                    ExamineTemplates model = _et.GetExamineTemplateById(eti.ExamineTemplateId);
                    eti.TemplateName = model.Name;
                }
                return eti;
            }
        }

        /// <summary>
        /// 通过关键字来搜索
        /// </summary>
        /// <param name="kwd"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<ExamineTemplateItems> GetExamineTemplateItemsByKwd(string parentId,string kwd, ref PageInfo pageInfo)
        {
            using (EFExamineTemplateItemRepository _rsp = new EFExamineTemplateItemRepository())
            {
                List<ExamineTemplateItems> list = _rsp.GetExamineTemplateItemsByKwd(parentId,kwd, ref pageInfo);
                if (list.Count > 0)
                {
                    ExamineTemplates model = _et.GetExamineTemplateById(list[0].ExamineTemplateId);
                    list.ForEach(delegate(ExamineTemplateItems item)
                    {
                        item.TemplateName = model.Name;
                    });
                }
                return list;
            }
        }
    }
}
