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
     * 描述:定义检验模版项选项操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151231      刘佳豪              创建 
     *  
     */
    public class ExamineTemplateItemOptionService
    {
        private ExamineTemplateItemService _eti = new ExamineTemplateItemService();

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddExamineTemplateItemOptions(ExamineTemplateItemOptions model)
        {
            using (EFExamineTemplateItemOptionsRepository _rsp = new EFExamineTemplateItemOptionsRepository())
            {
                return _rsp.AddExamineTemplateItemOptions(model);
            }
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExamineTemplateItemOptions(ExamineTemplateItemOptions model)
        {
            using (EFExamineTemplateItemOptionsRepository _rsp = new EFExamineTemplateItemOptionsRepository())
            {
                return _rsp.UpdateExamineTemplateItemOptions(model);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteExamineTemplateItemOptionsById(string id)
        {
            using (EFExamineTemplateItemOptionsRepository _rsp = new EFExamineTemplateItemOptionsRepository())
            {
                return _rsp.DeleteExamineTemplateItemOptionsById(id);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="templateItemId"></param>
        /// <returns></returns>
        public bool DeleteExamineTemplateItemByTemplateItemId(string templateItemId)
        {
            using (EFExamineTemplateItemOptionsRepository _rsp = new EFExamineTemplateItemOptionsRepository())
            {
                return _rsp.DeleteExamineTemplateItemByTemplateItemId(templateItemId);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="templateItemId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<ExamineTemplateItemOptions> GetExamineTemplateItemOptionsByTemplateItemId(string templateItemId, ref PageInfo pageInfo)
        {
            using (EFExamineTemplateItemOptionsRepository _rsp = new EFExamineTemplateItemOptionsRepository())
            {
                List<ExamineTemplateItemOptions> list = _rsp.GetExamineTemplateItemOptionsByTemplateItemId(templateItemId,ref pageInfo);
                if (list.Count > 0)
                {
                    ExamineTemplateItems model = _eti.GetExamineTemplateItemsById(list[0].ExamineItemId);
                    list.ForEach(p => p.ExamineItemName = model.Name);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamineTemplateItemOptions GetExamineTemplateItemOptionsById(string id)
        {
            using (EFExamineTemplateItemOptionsRepository _rsp = new EFExamineTemplateItemOptionsRepository())
            {
                ExamineTemplateItemOptions etio = _rsp.GetExamineTemplateItemOptionsById(id);
                if (etio != null)
                {
                    ExamineTemplateItems model = _eti.GetExamineTemplateItemsById(etio.ExamineItemId);
                    etio.ExamineItemName = model.Name;
                }
                return etio;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="templateItemId"></param>
        /// <param name="kwd"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<ExamineTemplateItemOptions> GetExamineTemplateItemOptionsByKwd(string templateItemId, string kwd, ref PageInfo pageInfo)
        {
            using (EFExamineTemplateItemOptionsRepository _rsp = new EFExamineTemplateItemOptionsRepository())
            {
                List<ExamineTemplateItemOptions> list = _rsp.GetExamineTemplateItemOptionsByKwd(templateItemId, kwd, ref pageInfo);
                if (list.Count > 0)
                {
                    ExamineTemplateItems model = _eti.GetExamineTemplateItemsById(list[0].ExamineItemId);
                    list.ForEach(p => p.ExamineItemName = model.Name);
                }
                return list;
            }
        }
    }
}
