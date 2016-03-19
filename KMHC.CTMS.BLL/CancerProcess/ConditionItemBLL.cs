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

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class ConditionItemBLL:BaseBLL

    {
         private readonly string logTitle = "访问ConditionItemBLL类";
         public ConditionItemBLL()
        {

        }



         /// <summary>
         /// 新增元数据
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         public int Add(ConditionItem model)
         {
             if (model == null) return 0;
             using (DbContext db = new CRDatabase())
             {
                 db.Set<CTMS_CONDITIONITEM>().Add(ModelToEntity(model));
                 db.SaveChanges();
                 return model.ID;
             }
         }

         /// <summary>
         /// 修改元数据
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         public bool Edit(ConditionItem model)
         {
             if (model == null || model.ID <= 0)
             {
                 LogService.WriteInfoLog(logTitle, "试图修改为空的ConditionItem实体!");
                 throw new KeyNotFoundException();
             }
             using (DbContext db = new CRDatabase())
             {
                 db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                 return db.SaveChanges() > 0;
             }
         }

         /// <summary>
         /// 删除元数据
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         public bool Delete(int id)
         {
             if (id <= 0)
             {
                 LogService.WriteInfoLog(logTitle, "试图删除为空的ConditionItem实体!");
                 throw new KeyNotFoundException();
             }
             ConditionItem model = Get(id);
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
         public ConditionItem Get(int id)
         {
             using (DbContext db = new CRDatabase())
             {
                 CTMS_CONDITIONITEM entity = db.Set<CTMS_CONDITIONITEM>().Find(id);
                 if (entity == null || entity.ID <= 0) return null;
                 return EntityToModel(entity);
             }
         }

         /// <summary>
         /// 获取查询列表
         /// </summary>
         /// <param name="keyWord"></param>
         /// <returns></returns>
         public List<ConditionItem> GetList(string keyWord)
         {
             using (DbContext db = new CRDatabase())
             {
                 IEnumerable<CTMS_CONDITIONITEM> query = null;
                 if (!string.IsNullOrEmpty(keyWord))
                 {
                     query = db.Set<CTMS_CONDITIONITEM>().AsNoTracking().Where(o => !o.ISDELETED && o.DISPLAYNAME.Contains(keyWord)).ToList();
                 }
                 else
                 {
                     query = db.Set<CTMS_CONDITIONITEM>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                 }
                 List<ConditionItem> list = (from m in query select EntityToModel(m)).ToList();
                 return list;
             }
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
                 IEnumerable<CTMS_CONDITIONITEM> query = null;
                 if (!string.IsNullOrEmpty(keyWord))
                 {
                     query = db.Set<CTMS_CONDITIONITEM>().AsNoTracking().Where(o => !o.ISDELETED && o.DISPLAYNAME.Contains(keyWord)).ToList();
                 }
                 else
                 {
                     query = db.Set<CTMS_CONDITIONITEM>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                 }
                 List<TreeItem> treeList = new List<TreeItem>();
                 int forderID = -1;
                 foreach (CTMS_CONDITIONITEM entity in query)
                 {
                     var find = treeList.Find(o => o.text.Equals(entity.CATEGORY??""));
                     if (find == null)
                     {
                         find = new TreeItem() { text = entity.CATEGORY??"", value = forderID--, nodes = new List<TreeItem>() };
                         treeList.Add(find);
                     }
                     //find.nodes.Add(new TreeItem() { text = entity.DISPLAYNAME, value = entity.ID });
                     TreeItem dataNode=new TreeItem();
                     ConstructTree(ref dataNode, EntityToModel(entity));
                     find.nodes.Add(dataNode);
                 }
                 return treeList;
             }
         }

        /// <summary>
        /// 构造条件树形结构
        /// </summary>
        /// <param name="node"></param>
        /// <param name="model"></param>
         public void ConstructTree(ref TreeItem node, ConditionItem model) {
             if (model.CondType == CondType.Custom)
             {
                 node=new TreeItem() { text = model.DisplayName, value = model.ID };
             }
             else
             {
                 node=new TreeItem() { text = string.Format("（{0}）{1}",model.LogicalOperatorText,model.DisplayName) , value = model.ID ,nodes=new List<TreeItem>()};
                 foreach(ConditionItem child in model.ComboCondItemList)
                 {
                    TreeItem childnode = new TreeItem();
                    ConstructTree(ref childnode, child);
                    node.nodes.Add(childnode);
                 }
             }
         }

        /// <summary>
        /// 根据ID获取条件项列表
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
         public List<ConditionItem> GetCondItemListByIDs(string IDs)
         {
             if (string.IsNullOrEmpty(IDs)) return null;
             return IDs.Split(',').Where(o => int.Parse(o)>0).Select(o => Get(int.Parse(o))).ToList();
         }

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="condItem">条件项目</param>
        /// <param name="paramNames">参数名列表</param>
        /// <param name="paramValues">参照值列表</param>
        /// <returns></returns>
         public bool IsMatch(ConditionItem condItem,List<string> paramNames,List<string> paramValues)
         {
             if(condItem==null) return false;
             bool isMatch = false;
             if(condItem.CondType==CondType.Custom)
             {
                 if(condItem.MetaDataID.Value<=0) return true;
                 string metaDataValue = new MetaDataBLL().GetValueOfMetaDataByID(condItem.MetaDataID.Value, paramNames, paramValues);
                 switch(condItem.Operator)
                 {
                     case Operator.Equal:
                         isMatch=(condItem.OperateValue == metaDataValue);
                         break;
                     case Operator.GreaterThen:
                         isMatch = (double.Parse(metaDataValue) > double.Parse(condItem.OperateValue));
                         break;
                     case Operator.GreaterEqual:
                         isMatch = (double.Parse(metaDataValue) > double.Parse(condItem.OperateValue));
                         break;
                     case Operator.LessThen:
                         isMatch = (double.Parse(metaDataValue) < double.Parse(condItem.OperateValue));
                         break;
                     case Operator.LessEqual:
                         isMatch = (double.Parse(metaDataValue) <= double.Parse(condItem.OperateValue));
                         break;
                     case Operator.NotEqual:
                         isMatch = (metaDataValue != condItem.OperateValue);
                         break;
                     case Operator.Contain:
                         isMatch = metaDataValue.Contains(condItem.OperateValue);
                         break;
                     case Operator.NotContain:
                         isMatch = !metaDataValue.Contains(condItem.OperateValue);
                         break;
                     default:
                         isMatch = true;
                         break;
                 }
             }
             else if (condItem.CondType == CondType.Combo)
             { 
                if(condItem.ComboCondItemList==null||condItem.ComboCondItemList.Count==0) return true;
                if (condItem.LogicalOperator == LogicOperator.And)
                {
                    isMatch = true;
                    foreach (ConditionItem item in condItem.ComboCondItemList)
                    {
                        isMatch = isMatch && IsMatch(item, paramNames, paramValues);
                    }
                }
                else if (condItem.LogicalOperator == LogicOperator.Or)
                {
                    isMatch = false;
                    foreach (ConditionItem item in condItem.ComboCondItemList)
                    {
                        isMatch = isMatch || IsMatch(item, paramNames, paramValues);
                    }
                }
                else if (condItem.LogicalOperator == LogicOperator.Not)
                {
                    isMatch = isMatch || IsMatch(condItem.ComboCondItemList[0], paramNames, paramValues);
                }
             }
             return isMatch;   
         }
         
         private CTMS_CONDITIONITEM ModelToEntity(ConditionItem model)
         {
             if (model == null) return null;
             return new CTMS_CONDITIONITEM()
             {
                 ID = model.ID <= 0 ? base.GetMaxId("CTMS_CONDITIONITEM", "ID") : model.ID,
                 CATEGORY = model.Category,
                 CONDTYPE =(int) model.CondType,
                 METADATAID = model.MetaDataID,
                 OPERATOR = (int)model.Operator,
                 OPERATEVALUE=model.OperateValue,
                 LOGICALOPERATOR=(int)model.LogicalOperator,
                 CONDIDS=(model.ComboCondItemList==null||model.ComboCondItemList.Count==0)?"": string.Join(",",model.ComboCondItemList.Select(o=>o.ID.ToString())),
                 DATATYPE = (int)model.DataType,
                 DISPLAYNAME = model.DisplayName,
                 CREATEDATETIME = model.CreateDateTime,
                 CREATEUSERID = model.CreateUserID,
                 CREATEUSERNAME = model.CreateUserName,
                 EDITDATETIME = model.EditTime,
                 EDITUSERID = model.EditUserID,
                 EDITUSERNAME = model.EditUserName,
                 OWNERID = model.OwnerID,
                 OWNERNAME = model.OwnerName,
                 ISDELETED = model.IsDeleted
             };
         }

         private ConditionItem EntityToModel(CTMS_CONDITIONITEM entity)
         {
             if (entity == null) return null;
             ConditionItem model= new ConditionItem()
             {
                 ID = entity.ID,
                 Category = entity.CATEGORY,
                 CondType=(CondType)entity.CONDTYPE,
                 MetaDataID=entity.METADATAID,
                 Operator=(Operator)entity.OPERATOR,
                 OperateValue=entity.OPERATEVALUE,
                 LogicalOperator=(LogicOperator)entity.LOGICALOPERATOR,
                 ComboCondItemList=GetCondItemListByIDs(entity.CONDIDS),
                 DataType = (DataType)entity.DATATYPE,
                 DisplayName = entity.DISPLAYNAME,
                 CreateDateTime = entity.CREATEDATETIME,
                 CreateUserID = entity.CREATEUSERID,
                 CreateUserName = entity.CREATEUSERNAME,
                 EditTime = entity.EDITDATETIME,
                 EditUserID = entity.EDITUSERID,
                 EditUserName = entity.EDITUSERNAME,
                 OwnerID = entity.OWNERID,
                 OwnerName = entity.OWNERNAME,
                 IsDeleted = entity.ISDELETED,
                 MetaDataName=entity.METADATAID.HasValue? new MetaDataBLL().Get(entity.METADATAID.Value).DisplayName:null,
             };
             model.ComboCondItemNames = (model.ComboCondItemList != null && model.ComboCondItemList.Count > 0) ? string.Join(",", model.ComboCondItemList.Select(o => o.DisplayName)) : null;
             return model;
         }

       
    }
}
