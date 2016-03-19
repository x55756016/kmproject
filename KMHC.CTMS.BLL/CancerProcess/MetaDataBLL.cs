/*
 * 描述:定义元数据业务类
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
    public class MetaDataBLL : BaseBLL
    {
        private readonly string logTitle = "访问MetaDataBLL类";
        public MetaDataBLL()
        {

        }

        /// <summary>
        /// 新增元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(MetaData model)
        {
            if (model == null) return 0;
            using (DbContext db = new CRDatabase())
            {
                db.Set<CTMS_METADATA>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MetaData model)
        {
            if (model == null || model.ID <= 0)
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的MetaData实体!");
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
                LogService.WriteInfoLog(logTitle, "试图删除为空的MetaData实体!");
                throw new KeyNotFoundException();
            }
            MetaData model = Get(id);
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
        public MetaData Get(int id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_METADATA entity = db.Set<CTMS_METADATA>().Find(id);
                if (entity == null || entity.ID <= 0) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MetaData> GetList(string keyWord)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_METADATA> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<CTMS_METADATA>().AsNoTracking().Where(o => !o.ISDELETED && o.DISPLAYNAME.Contains(keyWord)).ToList();
                }
                else
                {
                    query = db.Set<CTMS_METADATA>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                }
                List<MetaData> list = (from m in query select EntityToModel(m)).ToList();
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
                IEnumerable<CTMS_METADATA> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<CTMS_METADATA>().AsNoTracking().Where(o => !o.ISDELETED && o.DISPLAYNAME.Contains(keyWord)).ToList();
                }
                else
                {
                    query = db.Set<CTMS_METADATA>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                }
                List<TreeItem> treeList = new List<TreeItem>();
                int forderID = -1;
                foreach (CTMS_METADATA entity in query)
                {
                    var find = treeList.Find(o => o.text.Equals(entity.CATEGORY??""));
                    if (find == null)
                    {
                        find = new TreeItem() { text = entity.CATEGORY??"", value = forderID--, nodes = new List<TreeItem>() };
                        treeList.Add(find);
                    }
                    find.nodes.Add(new TreeItem() { text = entity.DISPLAYNAME, value = entity.ID });
                }
                return treeList;
            }
        }

        /// <summary>
        /// 获取元数据的值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetValueOfMetaDataByID(int id, List<string> paramNames, List<string> paramValues)
        {
            MetaData data = Get(id);
            if (data == null) return string.Empty;
            return GetValueOfMetaData(data, paramNames, paramValues);
        }

        /// <summary>
        /// 获取元数据的值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetValueOfMetaData(MetaData data, List<string> paramNames, List<string> paramValues)
        {
            using (DbContext db = new CRDatabase())
            {
                if (data == null)
                {
                    return string.Empty;
                }
                string retVal = null;
                switch (data.DataSourceType)
                { 
                    case DataSourceType.Table:
                        retVal = base.ExcuteScalar<string>(string.Format("select {0} from {1} where 1=1 {2}", data.DataSourceColumn, data.DataSource, GetParamsOfMetaData(data, paramNames, paramValues)));
                        break;
                    case DataSourceType.Func:
                        retVal = base.ExcuteScalar<string>(string.Format("select {0}({1}) from dual", data.DataSource, GetParamsOfMetaData(data, paramNames, paramValues)));
                        break;
                    case DataSourceType.StoreProcess:
                        retVal = "";//Todo
                        break;
                    default:
                        break;
                }
                return retVal??string.Empty;
            }   
        }

        /// <summary>
        /// 获取元数据参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetParamsOfMetaData(MetaData data, List<string> paramNames, List<string> paramValues)
        {
            if (data == null)
            {
                return string.Empty;
            }
            string retVal = string.Empty;
            List<MetaDataParam> paras = new MetaDataParamBLL().GetList(data.ID);
            if(paras==null||paras.Count==0) return retVal;
            switch (data.DataSourceType)
            {
                case DataSourceType.Table:
                    foreach (MetaDataParam para in paras)
                    {
                        retVal += string.Format(" and {0}={1}",para.ParamName,para.ParamValue);
                    }
                    break;
                case DataSourceType.Func:
                     foreach (MetaDataParam para in paras)
                    {
                        retVal += (!string.IsNullOrEmpty(retVal) ? "," : "") + para.ParamValue;
                    };
                    break;
                case DataSourceType.StoreProcess:
                    retVal = "";//Todo
                    break;
                default:
                    break;
            }
            //替换参数
            for (int i = 0; i < paramNames.Count; i++)
            {
                retVal = retVal.Replace(string.Format("@{0}@", paramNames[i]), paramValues[i]);
            }
            return retVal;
        }


        private CTMS_METADATA ModelToEntity(MetaData model)
        {
            if (model == null) return null;
            return new CTMS_METADATA()
            {
                ID = model.ID <= 0 ? base.GetMaxId("CTMS_METADATA", "ID") : model.ID,
                CATEGORY = model.Category,
                DATASOURCE = model.DataSource,
                DATASOURCECOLUMN = model.DataSourceColumn,
                DATASOURCETYPE = (int)model.DataSourceType,
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

        private MetaData EntityToModel(CTMS_METADATA entity)
        {
            if (entity == null) return null;
            return new MetaData()
            {
                ID = entity.ID,
                Category = entity.CATEGORY,
                DataSource = entity.DATASOURCE,
                DataSourceColumn = entity.DATASOURCECOLUMN,
                DataSourceType = (DataSourceType)entity.DATASOURCETYPE,
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
                IsDeleted = entity.ISDELETED
            };
        }
    }
}
