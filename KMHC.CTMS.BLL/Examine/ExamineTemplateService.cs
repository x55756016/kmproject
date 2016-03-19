using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Examine;
using KMHC.CTMS.Model.Repository.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement;

namespace KMHC.CTMS.BLL.Examine
{
    /*
     * 描述:定义检验模版操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151230      刘佳豪              创建 
     * 20160116      林德力             搜索模板列表增加类型条件（没有用到repository模式，直接引用了ef）
     */
    public class ExamineTemplateService
    {
        /// <summary>
        /// 通过检验模版名称获取检验模版数据
        /// </summary>
        /// <param name="name">检验模版名称</param>
        /// <returns>检验模版数据列表</returns>
        public List<ExamineTemplates> GetExamineTemplatesByName(int type,string name, ref PageInfo pageInfo)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.GetExamineTemplatesByName(type,name, ref pageInfo);
            }
        }

        /// <summary>
        /// 通过检验模版描述获取检验模版数据
        /// </summary>
        /// <param name="description">检验模版描述</param>
        /// <returns>检验模版数据列表</returns>
        public List<ExamineTemplates> GetExamineTemplatesByDescription(int type,string description,ref PageInfo pageInfo)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.GetExamineTemplatesByDescription(type,description, ref pageInfo);
            }
        }

        /// <summary>
        /// 通过id获取检验模版数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamineTemplates GetExamineTemplateById(string id)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.GetExamineTemplateById(id);
            }
        }

        /// <summary>
        /// 获取所有的检验模版数据
        /// </summary>
        /// <returns></returns>
        public List<ExamineTemplates> GetExamineTemplates(int type,ref PageInfo pageInfo)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.GetExamineTemplates(type,ref pageInfo);
            }
        }

        /// <summary>
        /// 通过关键字获取检验模版数据
        /// </summary>
        /// <param name="kwd"></param>
        /// <returns></returns>
        public List<ExamineTemplates> GetExamineTemplateByKwd(string kwd, ref PageInfo pageInfo)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.GetExamineTemplatesByKwd(kwd, ref pageInfo);
            }
        }




        public List<ExamineTemplates> GetExamineTemplateList(string kwd, string type, PageInfo pageInfo)
        {
            using (var db = new CRDatabase())
            {
                List<ExamineTemplates> list = new List<ExamineTemplates>();

                var typeList = new List<string>();
                if (!string.IsNullOrEmpty(type))
                {
                    type.Split(',').ToList().ForEach(p => typeList.Add(p));
                }

                Guid g = new Guid();
                if (Guid.TryParse(kwd, out g))
                {

                    //(from template in db.CTMS_ADM_EXAMINETEMPLATES
                    //    join dic in db.HR_DICTIONARY on template.TEMPLATECODE equals dic.DICTIONARYID
                    //    where template.ID == kwd && template.ISDELETED == 0
                    //          && (string.IsNullOrEmpty(type) || dic.DICTIONARYVALUE == type)
                    //    select template).Paging(ref pageInfo).ToList().ForEach(p => list.Add(LoadModelFromEntity(p)));
                    (from template in db.CTMS_ADM_EXAMINETEMPLATES
                     join dic in db.HR_DICTIONARY on template.TEMPLATECODE equals dic.DICTIONARYID
                     where template.ID == kwd && template.ISDELETED == 0
                           && (typeList.Count==0 || type.Contains(dic.DICTIONARYVALUE))
                     select template).Paging(ref pageInfo).ToList().ForEach(p => list.Add(LoadModelFromEntity(p)));
                }
                else
                {
                    (from template in db.CTMS_ADM_EXAMINETEMPLATES
                     join dic in db.HR_DICTIONARY on template.TEMPLATECODE equals dic.DICTIONARYID
                     where (string.IsNullOrEmpty(kwd) || template.DESCRIPTION.Contains(kwd)) && template.ISDELETED == 0
                             && (typeList.Count == 0 ||  type.Contains(dic.DICTIONARYVALUE))
                     select template).Paging(ref pageInfo).ToList().ForEach(p => list.Add(LoadModelFromEntity(p)));
                }
                return list;
            }
        }


        public List<ExamineTemplates> GetExamineTemplateListMore(string kwd, List<string> typeList, PageInfo pageInfo)
        {
            using (var db = new CRDatabase())
            {
                List<ExamineTemplates> list = new List<ExamineTemplates>();


                (from template in db.CTMS_ADM_EXAMINETEMPLATES
                    join dic in db.HR_DICTIONARY on template.TEMPLATECODE equals dic.DICTIONARYID
                    where template.ISDELETED == 0 && typeList.Contains(dic.DICTIONARYVALUE)
                    select template).Paging(ref pageInfo).ToList().ForEach(p => list.Add(LoadModelFromEntity(p)));



              
                return list;
            }
        }













        private ExamineTemplates LoadModelFromEntity(CTMS_ADM_EXAMINETEMPLATES entity)
        {
            if (entity == null)
                return null;

            ExamineTemplates model = new ExamineTemplates();
            model.Id = entity.ID;
            model.Name = entity.NAME;
            model.Description = entity.DESCRIPTION;
            model.Type = (int)entity.TYPE;
            model.TemplateCode = entity.TEMPLATECODE;
            model.CreateUserId = entity.CREATEUSERID;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.CreateDateTime = (DateTime)entity.CREATEDATETIME;
            model.EditUserId = entity.EDITUSERID;
            model.EditUserName = entity.EDITUSERNAME;
            model.EditDateTime = (DateTime)entity.EDITDATETIME;
            model.IsDeleted = (int)entity.ISDELETED;

            return model;
        }







        /// <summary>
        /// 添加检验模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddExamineTemplates(ExamineTemplates model)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.AddExamineTemplates(model);
            }
        }

        /// <summary>
        /// 更新检验模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExamineTemplates(ExamineTemplates model)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.UpdateExamineTemplates(model);
            }
        }

        /// <summary>
        /// 根据id删除检验模版数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteExamineTemplates(string id)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.DeleteExamineTemplates(id);
            }
        }

        /// <summary>
        /// 根据编码获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<ExamineTemplates> GetExamineTemplatesByCode(string code)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.GetExamineTemplatesByCode(code);
            }
        }

        /// <summary>
        /// 根据类型获取数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ExamineTemplates> GetExamineTemplateByType(int type)
        {
            using (EFExamineTemplateRepository _rsp = new EFExamineTemplateRepository())
            {
                return _rsp.GetExamineTemplateByType(type);
            }
        }
    }
}
