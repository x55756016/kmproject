/*
 * 描述:定义元数据参数业务类
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

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

    public class MetaDataParamBLL:BaseBLL
    {
        private readonly string logTitle = "访问MetaDataParamBLL类";
        public MetaDataParamBLL()
        { 
        }

        /// <summary>
        /// 新增元数据参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(MetaDataParam model)
        {
            if (model == null) return 0;
            using (DbContext db = new CRDatabase())
            {
                db.Set<CTMS_METADATAPARAM>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MetaDataParam model)
        {
            if (model == null || model.ID <= 0)
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的MetaDataParam实体!");
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
                LogService.WriteInfoLog(logTitle, "试图删除为空的MetaDataParam实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                CTMS_METADATAPARAM entity = db.Set<CTMS_METADATAPARAM>().Find(id);
                if (entity != null)
                {
                    db.Set<CTMS_METADATAPARAM>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据ID获取元数据
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MetaDataParam Get(int id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_METADATAPARAM entity = db.Set<CTMS_METADATAPARAM>().Find(id);
                if (entity == null || entity.ID <= 0) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MetaDataParam> GetList(int metaDataID)
        {
            using (DbContext db = new CRDatabase())
            {
                var query = db.Set<CTMS_METADATAPARAM>().AsNoTracking().Where(o => o.METADATAID == metaDataID).ToList();
                List<MetaDataParam> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        public  CTMS_METADATAPARAM ModelToEntity(MetaDataParam model)
        {
            if (model == null) return null;
            return new CTMS_METADATAPARAM()
            {
                ID = model.ID <= 0 ? base.GetMaxId("CTMS_METADATAPARAM", "ID") : model.ID,
                METADATAID = model.MetaDataID,
                PARAMNAME = model.ParamName,
                PARAMVALUE = model.ParamValue
            };
        }

        public MetaDataParam EntityToModel(CTMS_METADATAPARAM entity)
        {
            if (entity == null) return null;
            return new MetaDataParam()
            {
                ID = entity.ID,
                MetaDataID = entity.METADATAID,
                ParamName = entity.PARAMNAME,
                ParamValue = entity.PARAMVALUE
            };
        }
    }
}
