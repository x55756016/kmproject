/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Common;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class GuideLineBLL
    {
         private readonly string logTitle = "访问GuideLineBLL类";
        public GuideLineBLL()
        {

        }

        /// <summary>
        /// 新增元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(GuideLine model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                model.ID = Guid.NewGuid().ToString();
                db.Set<CTMS_GUIDELINE>().Add(ModelToEntity(model));
                if (model.MetaDataList != null && model.MetaDataList.Count > 0)
                {
                    foreach (GuideLineData data in model.MetaDataList)
                    {
                        data.GuideLineID = model.ID;
                        db.Set<CTMS_GUIDELINEDATA>().Add(new GuideLineDataBLL().ModelToEntity(data));
                    }
                }
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(GuideLine model)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的GuideLine实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                foreach (GuideLineData data in model.MetaDataList)
                {
                    db.Entry(new GuideLineDataBLL().ModelToEntity(data)).State = EntityState.Modified;
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的GuideLine实体!");
                throw new KeyNotFoundException();
            }
            GuideLine model = GetSimpleModel(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取元数据
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public GuideLine GetSimpleModel(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_GUIDELINE entity = db.Set<CTMS_GUIDELINE>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.ID)) return null;
                return EntityToModel2(entity);
            }
        }



        public GuideLine Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_GUIDELINE entity = db.Set<CTMS_GUIDELINE>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.ID)) return null;
                return EntityToModel(entity);
            }
        }



        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<GuideLine> GetList(string keyWord)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_GUIDELINE> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<CTMS_GUIDELINE>().AsNoTracking().Where(o => !o.ISDELETED && (o.NAME.Contains(keyWord) || o.CODE.Contains(keyWord))).ToList();
                }
                else
                {
                    query = db.Set<CTMS_GUIDELINE>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                }
                List<GuideLine> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<GuideLine> GetList(PageInfo pageInfo,string keyWord)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_GUIDELINE> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<CTMS_GUIDELINE>().AsNoTracking().Where(o => !o.ISDELETED && (o.NAME.Contains(keyWord) || o.CODE.Contains(keyWord))).Paging(ref pageInfo).ToList();
                }
                else
                {
                    query = db.Set<CTMS_GUIDELINE>().AsNoTracking().Where(o => !o.ISDELETED).Paging(ref pageInfo).ToList();
                }
                List<GuideLine> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }


        /// <summary>
        /// 根据父节点取得子节点
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public IList<GuideLine> GetChildList(string parentCode)
        {
            using (var db = new CRDatabase())
            {
                IList<GuideLine> list = new List<GuideLine>();


                //
                if (!string.IsNullOrEmpty(parentCode))
                {
                    db.Database.SqlQuery<GuideLine_Select> //用GuideLine一直匹配不上，类型有问题
                        ("select ID,code,NAME,PARENTID from CTMS_GUIDELINE connect by prior ID=PARENTID start with PARENTID='" +
                         parentCode + "'").ToList()
                        .ForEach(
                            k => list.Add(new GuideLine()
                            {
                                ID = k.ID,
                                Code = k.CODE,
                                Name = k.NAME
                            }));
                }
                else
                {
                    db.CTMS_GUIDELINE.AsNoTracking().Where(p => p.ISDELETED == false).ToList().ForEach(
                        k => list.Add(new GuideLine()
                        {
                            ID = k.ID,
                            Code = k.CODE,
                            Name = k.NAME
                        }));
                }
                return list;
            }
        }

        /// <summary>
        /// 获取满足条件的GuideLine列表
        /// </summary>
        /// <param name="paramNames"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public List<GuideLine> GetMatchList(List<string> paramNames, List<string> paramValues)
        { 
            List<GuideLine> list=new List<GuideLine>();
            ConditionItemBLL condItemBLL = new ConditionItemBLL();
            if(paramNames==null) paramNames=new List<string>();
            if (paramValues == null) paramValues = new List<string>();
            if (paramNames.Count != paramValues.Count) 
            {
                throw new Exception("参数名称和值必须成对出现!");
            }
            List<GuideLine> allList = GetList("");
            if (paramNames.Count == 0)
            {
                return allList;
            }
            foreach (GuideLine gl in allList)
            {
                if (gl.EnterCondItemList == null || gl.EnterCondItemList.Count == 0)
                {
                    //list.Add(gl);
                    continue;
                }
                bool isMatch= IsMatch(gl.EnterLogicalOperator, gl.EnterCondItemList, paramNames, paramValues);
                if (isMatch)
                {
                    list.Add(gl);
                }
            }
            return list;
        }

        /// <summary>
        /// 是否满足进入条件
        /// </summary>
        /// <returns></returns>
        public bool EnterConditionIsMatch(string guideLineID, List<string> paramNames, List<string> paramValues)
        {
            if (string.IsNullOrEmpty(guideLineID))
            {
                throw new Exception("临床路径ID不能为空!");
            }
            GuideLine gl = Get(guideLineID);
            if (gl == null)
            {
                throw new Exception("临床路径ID有误!");
            }
            return IsMatch(gl.EnterLogicalOperator, gl.EnterCondItemList, paramNames, paramValues);
        }

        /// <summary>
        /// 是否满足退出条件
        /// </summary>
        /// <returns></returns>
        public bool OuterConditionIsMatch(string guideLineID, List<string> paramNames, List<string> paramValues)
        {
            if (string.IsNullOrEmpty(guideLineID))
            {
                throw new Exception("临床路径ID不能为空!");
            }
            GuideLine gl = Get(guideLineID);
            if (gl == null)
            {
                throw new Exception("临床路径ID有误!");
            }
            if (gl.OutCondItemList == null || gl.OutCondItemList.Count == 0)
            {
                return false;//todo 条件项为空时返回false？
            }
            return IsMatch(gl.OutLogicalOperator, gl.OutCondItemList, paramNames, paramValues);
        }

        /// <summary>
        /// 是否满足逻辑条件
        /// </summary>
        /// <param name="logicOperator">逻辑运算</param>
        /// <param name="condList">条件列表</param>
        /// <param name="paramNames">参数列表</param>
        /// <param name="paramValues">参数值列表</param>
        /// <returns></returns>
        public bool IsMatch(LogicOperator logicOperator, List<ConditionItem> condList, List<string> paramNames, List<string> paramValues)
        {
            if (condList == null || condList.Count == 0)
            {
                return true;//todo 条件项为空时返回true？
            }
            bool isMatch = false;
            ConditionItemBLL condItemBLL = new ConditionItemBLL();
            switch (logicOperator)
            {
                case LogicOperator.And:
                    isMatch = true;
                    foreach (ConditionItem item in condList)
                    {
                        isMatch = isMatch && condItemBLL.IsMatch(item, paramNames, paramValues);
                    }
                    break;
                case LogicOperator.Or:
                    isMatch = false;
                    foreach (ConditionItem item in condList)
                    {
                        isMatch = isMatch || condItemBLL.IsMatch(item, paramNames, paramValues);
                    }
                    break;
                case LogicOperator.Not:
                    isMatch = !condItemBLL.IsMatch(condList[0], paramNames, paramValues);
                    break;
                default:
                    break;
            }
            return isMatch;
        }

        /// <summary>
        /// 获取元数据选择器查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<TreeItem> GetTreeList(string keyWord)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_GUIDELINE> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<CTMS_GUIDELINE>().AsNoTracking().Where(o => !o.ISDELETED && o.NAME.Contains(keyWord)).ToList();
                }
                else
                {
                    query = db.Set<CTMS_GUIDELINE>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                }
                List<TreeItem> treeList = new List<TreeItem>();
                //int forderID = -1;
                //foreach (CTMS_GUIDELINE entity in query)
                //{
                //    var find = treeList.Find(o => o.text.Equals(entity.CATEGORY??""));
                //    if (find == null)
                //    {
                //        find = new TreeItem() { text = entity.CATEGORY??"", value = forderID--, nodes = new List<TreeItem>() };
                //        treeList.Add(find);
                //    }
                //    find.nodes.Add(new TreeItem() { text = entity.DISPLAYNAME, value = entity.ID });
                //}
                //Todo
                return treeList;
            }
        }

        /// <summary>
        /// 保存父类parentid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentList"></param>
        /// <returns></returns>
        public bool SaveParentGuideLine(string id,List<ParentGuideLine> parentList)
        {

            using (var context = new CRDatabase())
            {
                context.CTMS_PARENTGUIDELINE.Where(p => p.GUIDELINEID == id)
                    .ForEachAsync(k => context.CTMS_PARENTGUIDELINE.Remove(k));

                parentList.ForEach(p=> p.GuideLineId=id);
                parentList.ForEach(p=> context.CTMS_PARENTGUIDELINE.Add(ModelToEntity(p)));

                return context.SaveChanges() > 0;
            }

        }



        public List<ParentGuideLine> GetParentGuideLineList(string id)
        {
            using(var context  = new CRDatabase())
            {
                var list = new List<ParentGuideLine>();
               context.CTMS_PARENTGUIDELINE.Where(p => p.GUIDELINEID == id).ToList().ForEach(k=> list.Add(EntityToModel(k)));
               return list;
            }
        }


        private ParentGuideLine EntityToModel(CTMS_PARENTGUIDELINE entity)
        {
            if (entity!=null)
            {
                var model = new ParentGuideLine()
                {
                    Id = entity.ID,
                    GuideLineId = entity.GUIDELINEID,
                    PARENTID = entity.PARENTID,
                    ParentName = string.IsNullOrEmpty(entity.PARENTID) ? null : GetSimpleModel(entity.PARENTID).Name,
                };
                return model;
            }
            return null;
        }

        private CTMS_PARENTGUIDELINE ModelToEntity(ParentGuideLine model)
        {
            if (model != null)
            {
                var entity = new CTMS_PARENTGUIDELINE()
                {
                    ID = Guid.NewGuid().ToString(),
                    GUIDELINEID=model.GuideLineId,
                    PARENTID=model.PARENTID 
                };
                return entity;
            }
            return null;
        }




        private CTMS_GUIDELINE ModelToEntity(GuideLine model)
        {
            if (model == null) return null;
            return new CTMS_GUIDELINE()
            {
                ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID,
                NAME = model.Name,
                CODE = model.Code,
                CLINICALBASIS = model.ClinicalBasis,
                ISINHERIT = model.IsInherit,
                PARENTID = model.ParentID,
                ENTERLOGICALOPERATOR = (int)model.EnterLogicalOperator,
                ENTERCONDITEMIDS = (model.EnterCondItemList == null || model.EnterCondItemList.Count <= 0) ? null : string.Join(",", model.EnterCondItemList.Where(o=>o.ID>0).Select(o => o.ID.ToString())),
                OUTLOGICALOPERATOR = (int)model.OutLogicalOperator,
                OUTCONDITEMIDS = (model.OutCondItemList == null || model.OutCondItemList.Count <= 0) ? null : string.Join(",", model.OutCondItemList.Where(o => o.ID > 0).Select(o => o.ID.ToString())),
                
                CREATEDATETIME = model.CreateDateTime,
                CREATEUSERID = model.CreateUserID,
                CREATEUSERNAME = model.CreateUserName,
                EDITDATETIME = model.EditTime,
                EDITUSERID = model.EditUserID,
                EDITUSERNAME = model.EditUserName,
                OWNERID = model.OwnerID,
                OWNERNAME = model.OwnerName,
                ISDELETED = model.IsDeleted,
                RECOMMENDPROCESS = model.RecommendProcess
            };
        }

        private GuideLine EntityToModel(CTMS_GUIDELINE entity)
        {
            if (entity == null) return null;
            return new GuideLine()
            {
                ID = entity.ID,
                Name = entity.NAME,
                Code = entity.CODE,
                ClinicalBasis = entity.CLINICALBASIS,
                IsInherit = entity.ISINHERIT,
                ParentID = entity.PARENTID,


                ParentList = GetParentGuideLineList(entity.ID),


                ParentName = string.IsNullOrEmpty(entity.PARENTID) ? null : GetSimpleModel(entity.PARENTID).Name,
                EnterLogicalOperator = (LogicOperator)entity.ENTERLOGICALOPERATOR,
                EnterCondItemList = string.IsNullOrEmpty(entity.ENTERCONDITEMIDS) ? new List<ConditionItem>() : new ConditionItemBLL().GetCondItemListByIDs(entity.ENTERCONDITEMIDS),
                OutLogicalOperator = (LogicOperator)entity.OUTLOGICALOPERATOR,
                OutCondItemList = string.IsNullOrEmpty(entity.OUTCONDITEMIDS) ? new List<ConditionItem>() : new ConditionItemBLL().GetCondItemListByIDs(entity.OUTCONDITEMIDS),
                MetaDataList = new GuideLineDataBLL().GetDataList(entity.ID),

                CreateDateTime = entity.CREATEDATETIME,
                CreateUserID = entity.CREATEUSERID,
                CreateUserName = entity.CREATEUSERNAME,
                EditTime = entity.EDITDATETIME,
                EditUserID = entity.EDITUSERID,
                EditUserName = entity.EDITUSERNAME,
                OwnerID = entity.OWNERID,
                OwnerName = entity.OWNERNAME,
                IsDeleted = entity.ISDELETED,
                RecommendProcess = entity.RECOMMENDPROCESS
            };
        }

        private GuideLine EntityToModel2(CTMS_GUIDELINE entity)
        {
            if (entity == null) return null;
            return new GuideLine()
            {
                ID = entity.ID,
                Name = entity.NAME,
            };
        }


    }
}
