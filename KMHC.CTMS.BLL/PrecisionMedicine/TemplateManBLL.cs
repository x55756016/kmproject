/*
 * 描述:表单模板管理
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 *20151201    		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.BLL.PrecisionMedicine
{
    public class TemplateManBLL
    {
        //private CRDatabase _context = new CRDatabase();

        /// <summary>
        /// 获取模板栏目列表
        /// </summary>
        /// <returns></returns>
        public IList<ExamineCategory> GetExamCatetoryList()
        {
            var list = new List<ExamineCategory>();
            using (CRDatabase _context = new CRDatabase())
            {
                _context.HR_EXAMINECATEGORY.Where(u => u.PARENTCODE != null && u.PARENTID != 0 && u.ISDELETED == 0)
                    .ToList()
                    .ForEach(p => list.Add(EntityToModel(p)));
            }
            return list;
        }


        public IList<ExamineTemplate> GetTemplateListForDocResult( )
        {
            var list = new List<ExamineTemplate>();
            using (CRDatabase _context = new CRDatabase())
            {
                _context.HR_EXAMINETEMPLATE.Where(p => p.ISDELETED == "0")
                    .ToList()
                    .ForEach(p => list.Add(EntityToModel(p)));

                foreach (var examineTemplate in list)
                {
                    examineTemplate.TemplateItems = new List<HR_TemplateItemDTO>();

                    (from temp in _context.HR_TEMPLATEITEM
                                   join unit in _context.HR_TEMPLATEITEMUNIT

                                   on temp.ITEMID equals unit.ITEMID into JoinedTmpUnit

                                   from temps in JoinedTmpUnit.DefaultIfEmpty()
                                    where temp.TEMPLATEID == examineTemplate.TEMPLATEID

                                   select new
                                   {
                                       ITEMID = temp.ITEMID,
                                       TEMPLATEID =temp.TEMPLATEID,
                                       ITEMNAME = temp.ITEMNAME,
                                       ITEMNAMEENG = temp.ITEMNAMEENG,
                                       ITEMUNITID = temps.ITEMUNITID,
                                       UNITNAME  = temps.UNITNAME,
                                       UNITDESC = temps.UNITDESC
                                   }
                                   ).ToList().ForEach(p=> examineTemplate.TemplateItems.Add(new HR_TemplateItemDTO()
                                   {
                                       ITEMID = p.ITEMID,
                                       TEMPLATEID = p.TEMPLATEID,
                                       ITEMNAME = p.ITEMNAME,
                                       ITEMNAMEENG = p.ITEMNAMEENG,
                                       ITEMUNITID = p.ITEMUNITID,
                                       UNITNAME = p.UNITNAME,
                                       UNITDESC = p.UNITDESC
                                   }));
                    //_context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == examineTemplate.TEMPLATEID).ToList().ForEach(k => examineTemplate.TemplateItems.Add(EntityToModel(k)));

                    //examineTemplate.TemplateItems.ToList()
                    //    .ForEach(p => _context.HR_TEMPLATEITEMUNIT.Where(k => k.ITEMID == p.ITEMID).ToList()
                    //        .ForEach(m => p.ItemUnit.Add(EntityToModel(m))));
                }
            }
            return list;
        }


        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns></returns>
        public IList<ExamineTemplate> GetTemplatesList(PageInfo page,string key)
        {
            var list = new List<ExamineTemplate>();
            using (CRDatabase _context = new CRDatabase())
            {
                _context.HR_EXAMINETEMPLATE.Where(
                    p => (string.IsNullOrEmpty(key) || p.TEMPLATENAME.Contains(key)) && p.ISDELETED == "0")
                    .Paging(ref page).ToList().ForEach(p => list.Add(EntityToModel(p)));

                foreach (var examineTemplate in list)
                {
                    examineTemplate.TemplateItems = new List<HR_TemplateItemDTO>();

                    _context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == examineTemplate.TEMPLATEID).OrderBy(p=>p.SORT).ToList().ForEach(k => examineTemplate.TemplateItems.Add(EntityToModel(k)));

                    examineTemplate.TemplateItems.ToList()
                        .ForEach(p => _context.HR_TEMPLATEITEMUNIT.Where(k => k.ITEMID == p.ITEMID).ToList()
                            .ForEach(m => p.ItemUnit.Add(EntityToModel(m))));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取模板具体信息
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public ExamineTemplate GetTemplateInfo(string templateId)
        {
            using (CRDatabase _context = new CRDatabase())
            {
                var model = EntityToModel(_context.HR_EXAMINETEMPLATE.FirstOrDefault(p => p.TEMPLATEID == templateId));

                if (model != null)
                {
                    model.TemplateItems = new List<HR_TemplateItemDTO>();

                    _context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == model.TEMPLATEID).ToList().ForEach(k=>model.TemplateItems.Add(EntityToModel(k)));

                    model.TemplateItems.ToList()
                        .ForEach(p => _context.HR_TEMPLATEITEMUNIT.Where(k => k.ITEMID == p.ITEMID).ToList()
                            .ForEach(m => p.ItemUnit.Add(EntityToModel(m))));

                    //model.TemplateItems = new List<TemplateItem>();
                    //_context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == model.TEMPLATEID).ToList()
                    //    .ForEach(k => model.TemplateItems.Add(EntityToModel(k)));
                }
                return model;
            }
        }

        /// <summary>
        /// 获取全部检查项
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemNo"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public IList<ExamineItems> GetExamItemsList(PageInfo page, string itemNo, string itemName)
        {
            using (CRDatabase _context = new CRDatabase())
            {
                var list = new List<ExamineItems>();
                _context.HR_EXAMINEITEMS.Where(
                    p =>
                        (string.IsNullOrEmpty(itemNo) || p.ITEMNO.Contains(itemNo)) &&
                        (string.IsNullOrEmpty(itemName) || p.ITEMNAME.Contains(itemName)))
                    .Paging(ref page).ToList().ForEach(k => list.Add(EntityToModel(k)));
                return list;
            }
        }


        /// <summary>
        /// 更新模板配置
        /// </summary>
        /// <param name="etModel"></param>
        /// <returns></returns>
        public bool UpdateTemplate(ExamineTemplate etModel)
        {
            using (CRDatabase _context = new CRDatabase())
            {
                if (etModel != null)
                {

                    _context.Entry(ModelToEntity(etModel)).State = EntityState.Modified;

                    //_context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == etModel.TEMPLATEID)
                    //    .ToList()
                    //    .ForEach(k => _context.HR_TEMPLATEITEM.Remove(k));
                    //删除掉关联的项以及项的单位
                    foreach (var item in _context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == etModel.TEMPLATEID).ToList())
                    {
                        _context.HR_TEMPLATEITEMUNIT.Where(p => p.ITEMID == item.ITEMID)
                            .ForEachAsync(k => _context.HR_TEMPLATEITEMUNIT.Remove(k));
                        _context.HR_TEMPLATEITEM.Remove(item);
                    }

                    //_context.Database.ExecuteSqlCommand("delete from Students where StudentId = @studentId", new SqlParameter("@studentId", 5));
                    //foreach (var templateItem in etModel.TemplateItems)
                    //{
                    //    templateItem.TEMPLATEID = etModel.TEMPLATEID;
                    //    templateItem.ITEMID = Guid.NewGuid().ToString();
                    //    //_context.HR_TEMPLATEITEM.Add(ModelToEntity(templateItem));
                    //}
                    foreach (var item in etModel.TemplateItems?? new List<HR_TemplateItemDTO>())
                    {
                        item.TEMPLATEID = etModel.TEMPLATEID;
                        _context.Set<HR_TEMPLATEITEM>().Add(ModelToEntity(item));

                        //3.添加项单位
                        foreach (var unitDto in item.ItemUnit?? new List<HR_TemplateItemUnitDTO>())
                        {
                            unitDto.ITEMID = item.ITEMID;
                            _context.Set<HR_TEMPLATEITEMUNIT>().Add(ModelToEntity(unitDto));
                        }
                    }
                }
                return _context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 添加模板配置
        /// </summary>
        /// <param name="etModel"></param>
        /// <returns></returns>
        public bool AddTemplate(ExamineTemplate etModel)
        {
            //using (CRDatabase _context = new CRDatabase())
            //{

            //    UserInfo currentUser = new UserInfoService().GetCurrentUser();
            //    etModel.TEMPLATEID = Guid.NewGuid().ToString();
            //    etModel.CREATEDATETIME = System.DateTime.Now;
            //    etModel.EDITDATETIME = System.DateTime.Now;
            //    etModel.CREATEUSERID = currentUser.UserId;
            //    etModel.EDITUSERID = currentUser.UserId;
            //    etModel.OWNERID = currentUser.UserId;
            //    etModel.ISDELETED = "0";
            //    _context.HR_EXAMINETEMPLATE.Add(ModelToEntity(etModel));
            //    foreach (var templateItem in etModel.TemplateItems)
            //    {
            //        templateItem.TEMPLATEID = etModel.TEMPLATEID;
            //        templateItem.ITEMID = Guid.NewGuid().ToString();
            //        _context.HR_TEMPLATEITEM.Add(ModelToEntity(templateItem));
            //    }
            //    return _context.SaveChanges() > 0;
            //}

            var context = DbSessionFactory.GetCurrentDbContext();


            etModel.TEMPLATEID = Guid.NewGuid().ToString();

            //1.添加主表
            context.Set<HR_EXAMINETEMPLATE>().Add(ModelToEntity(etModel));

            //2.添加项
            foreach (var item in etModel.TemplateItems?? new List<HR_TemplateItemDTO>())
            {
                item.TEMPLATEID = etModel.TEMPLATEID;
                context.Set<HR_TEMPLATEITEM>().Add(ModelToEntity(item));

                //3.添加项单位
                foreach (var unitDto in item.ItemUnit?? new List<HR_TemplateItemUnitDTO>())
                {
                    unitDto.ITEMID = item.TEMPLATEID;
                    context.Set<HR_TEMPLATEITEMUNIT>().Add(ModelToEntity(unitDto));
                }
            }
            return context.SaveChanges()>0;
        }

        public bool DeleteTemplate(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            using (CRDatabase _context = new CRDatabase())
            {

                var entity = _context.Set<HR_EXAMINETEMPLATE>().Find(id);
                if (entity != null)
                {
                    entity.ISDELETED = "1";
                    //_context.Set<HR_EXAMINETEMPLATE>().Remove(entity);
                    //_context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == id)
                    //    .ToList()
                    //    .ForEach(k => _context.HR_TEMPLATEITEM.Remove(k));
                }
                return _context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取模板检查项列表
        /// </summary>
        /// <param name="templateid">模板ID</param>
        /// <returns></returns>
        public IList<TemplateItem> GetTemplateItemList(string templateid)
        {
            using (CRDatabase _context = new CRDatabase())
            {

                var list = new List<TemplateItem>();
                //_context.HR_TEMPLATEITEM.Where(p => p.TEMPLATEID == templateid)
                //    .ToList()
                //    .ForEach(p => list.Add(EntityToModel(p)));

                return list;
            }
        }

        #region 模板项值范围设置


        /// <summary>
        /// 获取检查项值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemNo"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public ItemStandVal GetItemsStandVal(string id)
        {
            using (CRDatabase _context = new CRDatabase())
            {

                var list = new List<ItemStandVal>();

                _context.HR_ITEMSTANDVAL.Join(_context.HR_EXAMINEITEMS, itemValue => itemValue.ITEMNO,
                    item => item.ITEMNO,
                    (itemValue, item) => new {itemValue, item})
                    .Where(
                        k => k.itemValue.ID == id).ToList().ForEach(p => list.Add(new ItemStandVal()
                        {
                            ID = p.itemValue.ID,
                            ITEMNO = p.itemValue.ITEMNO,
                            NAMEENG = p.itemValue.NAMEENG,
                            ItemName = p.item.ITEMNAME,
                            MINVALUE = p.itemValue.MINVALUE,
                            MAXVALUE = p.itemValue.ITEMMAXVALUE,
                            UNIT = p.itemValue.UNIT,
                            EDITDATETIME = p.itemValue.EDITDATETIME,
                            CREATEDATETIME = p.itemValue.CREATEDATETIME
                        }));
                return list.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取全部检查项值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemNo"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public IList<ItemStandVal> GetItemsStandValList(PageInfo page, string itemName)
        {
            var list = new List<ItemStandVal>();

            //var q = (from itemval in _context.HR_ITEMSTANDVAL
            //         join item in _context.HR_EXAMINEITEMS on itemval.ITEMNO equals item.ITEMNO
            //         where itemval.ISDELETED == "0").Paging(ref page).ToList();

            //var q2 = _context.HR_ITEMSTANDVAL.Join(_context.HR_EXAMINEITEMS, itemval => itemval.ITEMNO,
            //    item => item.ITEMNO, (itemval, item) => new {itemval, item})
            //    .Where(@t => @t.itemval.ISDELETED == "0")
            //    .Paging(ref page)
            //    .Select(p => new {p.itemval, p.item.ITEMNAME})
            //    .ToList();
            using (CRDatabase _context = new CRDatabase())
            {

                _context.HR_ITEMSTANDVAL.Join(_context.HR_EXAMINEITEMS, itemValue => itemValue.ITEMNO,
                    item => item.ITEMNO,
                    (itemValue, item) => new {itemValue, item})
                    .Where(
                        k =>
                            (string.IsNullOrEmpty(itemName) || k.item.ITEMNAME.StartsWith(itemName)) &&
                            k.itemValue.ISDELETED == "0").OrderByDescending(p => p.itemValue.ID)
                    .Skip((page.PageIndex - 1)*page.PageSize)
                    .Take(page.PageSize).ToList().ForEach(p => list.Add(new ItemStandVal()
                    {
                        ID = p.itemValue.ID,
                        ITEMNO = p.itemValue.ITEMNO,
                        NAMEENG = p.itemValue.NAMEENG,
                        ItemName = p.item.ITEMNAME,
                        MINVALUE = p.itemValue.MINVALUE,
                        MAXVALUE = p.itemValue.ITEMMAXVALUE,
                        UNIT = p.itemValue.UNIT,
                        EDITDATETIME = p.itemValue.EDITDATETIME,
                        CREATEDATETIME = p.itemValue.CREATEDATETIME
                    }));
            }
            return list;
        }

        /// <summary>
        /// 添加值设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult AddItemStandVal(ItemStandVal model)
        {

            BaseResult br = new BaseResult() {Succeeded = true};

            using (CRDatabase _context = new CRDatabase())
            {

                bool flag = !_context.HR_ITEMSTANDVAL.Any(p => p.ITEMNO == model.ITEMNO && p.ISDELETED == "0");
                if (flag)
                {
                    try
                    {
                        _context.HR_ITEMSTANDVAL.Add(ModelToEntity(model));
                        _context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        br.Succeeded = false;
                        br.Error = e.Message.ToString();
                        throw;
                    }
                }
                else
                {
                    br.Succeeded = false;
                    br.Error = "该项已添加标准值返回，请检查";
                }
            }
            return br;
        }


        /// <summary>
        /// 更新值设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult UpdateItemStandVal(ItemStandVal model)
        {
            BaseResult br = new BaseResult() {Succeeded = true};
            try
            {
                using (CRDatabase _context = new CRDatabase())
                {

                    var entity = _context.HR_ITEMSTANDVAL.FirstOrDefault(p => p.ID == model.ID);

                    if (entity != null)
                    {
                        entity.NAMEENG = model.NAMEENG;
                        entity.EDITDATETIME = model.EDITDATETIME;
                        entity.EDITUSERID = model.EDITUSERID;
                        entity.MINVALUE = model.MINVALUE;
                        entity.ITEMMAXVALUE = model.MAXVALUE;
                        entity.UNIT = model.UNIT;
                        _context.Entry(entity).State = EntityState.Modified;
                        _context.SaveChanges();
                        //_context.Entry(ModelToEntity(model)).State = EntityState.Modified;
                        //_context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                br.Succeeded = false;
                br.Error = e.Message.ToString();
            }
            return br;
        }


        public BaseResult DeleteItemStandVal(string id)
        {
            BaseResult br = new BaseResult() {Succeeded = true};
            using (CRDatabase _context = new CRDatabase())
            {

                var entity = _context.HR_ITEMSTANDVAL.FirstOrDefault(p => p.ID == id);
                if (entity != null)
                {
                    entity.ISDELETED = "1";
                    try
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        br.Succeeded = false;
                        br.Error = e.Message.ToString();
                    }
                }
            }
            return br;
        }

        public HR_ITEMSTANDVAL ModelToEntity(ItemStandVal model)
        {
            if (model!=null)
            {
                var entity = new HR_ITEMSTANDVAL()
                {
                    ID = model.ID,
                    ITEMNO = model.ITEMNO,
                    NAMEENG = model.NAMEENG,
                    MINVALUE = model.MINVALUE,
                    ITEMMAXVALUE = model.MAXVALUE,
                    UNIT = model.UNIT,
                    CREATEUSERID = model.CREATEUSERID,
                    CREATEDATETIME = model.CREATEDATETIME,
                    EDITUSERID = model.EDITUSERID,
                    EDITDATETIME = model.EDITDATETIME,
                    ISDELETED = model.ISDELETED,
                    OWNERID = model.OWNERID
                };
                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ItemStandVal EntityToModel(HR_ITEMSTANDVAL entity)
        {
            if (entity != null)
            {
                var model = new ItemStandVal()
                {
                    ID = entity.ID,
                    ITEMNO = entity.ITEMNO,
                    NAMEENG = entity.NAMEENG,
                    MINVALUE = entity.MINVALUE,
                    MAXVALUE = entity.ITEMMAXVALUE,
                    UNIT = entity.UNIT,
                    CREATEUSERID = entity.CREATEUSERID,
                    CREATEDATETIME = entity.CREATEDATETIME,
                    EDITUSERID = entity.EDITUSERID,
                    EDITDATETIME = entity.EDITDATETIME,
                    ISDELETED = entity.ISDELETED,
                    OWNERID = entity.OWNERID
                };
                return model;
            }
            return null;
        }

        /// <summary>
        /// 根据ItemNo获取单条记录
        /// </summary>
        /// <param name="itemNo"></param>
        /// <returns></returns>
        public ItemStandVal GetStandVal(string itemNo)
        {
            using (CRDatabase _context = new CRDatabase())
            {
               return EntityToModel( _context.HR_ITEMSTANDVAL.FirstOrDefault(p => p.ISDELETED == "0" && p.ITEMNO == itemNo));
            }
        }

        #endregion


        #region 模型映射

        public ExamineTemplate EntityToModel(HR_EXAMINETEMPLATE entity)
        {
            if (entity!=null)
            {
                var model = new ExamineTemplate()
                {
                    TEMPLATEID = entity.TEMPLATEID,
                    TEMPLATENAME = entity.TEMPLATENAME,
                    DESCRIPTION = entity.DESCRIPTION,
                    CREATEUSERID = entity.CREATEUSERID,
                    CREATEDATETIME = entity.CREATEDATETIME,
                    EDITUSERID = entity.EDITUSERID,
                    EDITDATETIME = entity.EDITDATETIME,
                    ISDELETED = entity.ISDELETED,
                    OWNERID = entity.OWNERID
                };
                return model;
            }
            return null;
        }

        public HR_EXAMINETEMPLATE ModelToEntity(ExamineTemplate model)
        {
            if (model != null)
            {
                var entity = new HR_EXAMINETEMPLATE()
                {
                    TEMPLATEID = model.TEMPLATEID,
                    TEMPLATENAME = model.TEMPLATENAME,
                    DESCRIPTION = model.DESCRIPTION,
                    CREATEUSERID = model.CREATEUSERID,
                    CREATEDATETIME = model.CREATEDATETIME,
                    EDITUSERID = model.EDITUSERID,
                    EDITDATETIME = model.EDITDATETIME,
                    ISDELETED = model.ISDELETED,
                    OWNERID = model.OWNERID
                };
                return entity;
            }
            return null;
        }


        public HR_TemplateItemDTO EntityToModel(HR_TEMPLATEITEM entity)
        {
            if (entity != null)
            {
                var model = new HR_TemplateItemDTO()
                {
                    ITEMID = entity.ITEMID,
                    TEMPLATEID = entity.TEMPLATEID,
                    //ITEMCODE = entity.ITEMCODE,
                    ITEMNAME = entity.ITEMNAME,
                    ITEMNAMEENG = entity.ITEMNAMEENG,
                   SORT = entity.SORT,
                    //VALUETYPE = entity.VALUETYPE,
                    //CODEVALUE = entity.CODEVALUE,
                    //CATETORYID = entity.CATETORYID,
                };
                return model;
            }
            return null;
        }

        public HR_TEMPLATEITEM ModelToEntity(HR_TemplateItemDTO model)
        {
            if (model!=null)
            {
                var entity = new HR_TEMPLATEITEM()
                {
                    ITEMID = model.ITEMID,
                    TEMPLATEID = model.TEMPLATEID,
                    //ITEMCODE = model.ITEMCODE,
                    ITEMNAME = model.ITEMNAME,
                    ITEMNAMEENG = model.ITEMNAMEENG,
                    SORT = model.SORT,
                    //VALUETYPE = model.VALUETYPE,
                    //CODEVALUE = model.CODEVALUE,
                    //CATETORYID = model.CATETORYID,
                };
                return entity;
            }
            return null;
        }

        public ExamineItems EntityToModel(HR_EXAMINEITEMS entity)
        {
            if (entity != null)
            {
                var model = new ExamineItems()
                {
                    ITEMID = entity.ITEMID,
                    ITEMNO = entity.ITEMNO,
                    ITEMNAME = entity.ITEMNAME,
                    DESCRIPTION = entity.DESCRIPTION,
                    VALUETYPE = entity.VALUETYPE,
                    CODEVALUE = entity.CODEVALUE
                    //CREATEDATE = entity.CREATEDATE,
                    //CREATEBY = entity.CREATEBY,
                    //UPDATEBY = entity.UPDATEBY,
                    //UPDATEDATE = entity.UPDATEDATE,
                    //STATUS = entity.STATUS,
                    //CATEGORYID = entity.CATEGORYID,
                };
                return model;
            }
            return null;
        }

        public ExamineCategory EntityToModel(HR_EXAMINECATEGORY entity)
        {
            if (entity!=null)
            {
                var model = new ExamineCategory()
                {
                       ID =entity.ID,
                       CATEGORYCODE = entity.CATEGORYCODE,
                       NAME = entity.NAME,
                       DESCRIPTION = entity.DESCRIPTION,
                       PARENTCODE = entity.PARENTCODE
                       //PARENTID = entity.PARENTID,
                       //CREATEDATETIME = entity.CREATEDATETIME,
                       //CREATEUSERID = entity.CREATEUSERID,
                       //EDITDATETIME = entity.EDITDATETIME,
                       //EDITUSERID = entity.EDITUSERID,
                       //DeleterUserId = entity.DeleterUserId,
                       //DeletionTime = entity.DeletionTime,
                       //ISDELETED = entity.ISDELETED
                };
                return model;
            }
            return null;
        }


        public HR_TemplateItemUnitDTO EntityToModel(HR_TEMPLATEITEMUNIT entity)
        {
            if (entity!=null)
            {
                var model  = new HR_TemplateItemUnitDTO()
                {
                    ITEMID = entity.ITEMID,
                    ITEMUNITID = entity.ITEMUNITID,
                    UNITDESC = entity.UNITDESC,
                    UNITNAME = entity.UNITNAME
                };
                return model;
            }
            return null;
        }

        public HR_TEMPLATEITEMUNIT ModelToEntity(HR_TemplateItemUnitDTO model)
        {
            if (model != null)
            {
                var entity = new HR_TEMPLATEITEMUNIT()
                {
                    ITEMID = model.ITEMID,
                    ITEMUNITID = model.ITEMUNITID,
                    UNITDESC = model.UNITDESC,
                    UNITNAME = model.UNITNAME
                };
                return entity;
            }
            return null;
        }
        #endregion
    }
}

