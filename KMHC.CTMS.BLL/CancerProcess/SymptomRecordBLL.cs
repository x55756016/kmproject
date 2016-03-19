/*
 * 描述:
 *  
 * 修订历史: 
 * 日期          修改人              Email                   内容
 * 2016/1/20     邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.CancerProcess;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class SymptomRecordBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(SymptomRecord model)
        {
            if (model == null)
                return string.Empty;

            using (SymptomRecordDAL dal = new SymptomRecordDAL())
            {
                CTMS_SYMPTOMRECORDS entity = ModelToEntity(model);
                entity.ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SymptomRecord GetOne(Expression<Func<CTMS_SYMPTOMRECORDS, bool>> predicate = null)
        {
            using (SymptomRecordDAL dal = new SymptomRecordDAL())
            {
                return EntityToModel(dal.GetOne(predicate));
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<SymptomRecord> GetList(PageInfo page, Expression<Func<CTMS_SYMPTOMRECORDS, bool>> predicate = null)
        {
            using (SymptomRecordDAL dal = new SymptomRecordDAL())
            {
                var list = dal.Get(predicate);

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<SymptomRecord> GetList(Expression<Func<CTMS_SYMPTOMRECORDS, bool>> predicate = null)
        {
            using (SymptomRecordDAL dal = new SymptomRecordDAL())
            {
                var list = dal.Get(predicate);

                return list.Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(SymptomRecord model)
        {
            if (model == null) return false;
            using (SymptomRecordDAL dal = new SymptomRecordDAL())
            {
                CTMS_SYMPTOMRECORDS entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CTMS_SYMPTOMRECORDS ModelToEntity(SymptomRecord model)
        {
            if (model != null)
            {
                var entity = new CTMS_SYMPTOMRECORDS()
                {
                    ID = model.ID,
                    SYMPTOMID = model.SymptomId,
                    USERID = model.UserId,
                    SYMPTOMLEVEL = model.SymptomLevel,
                    DICTSYMLEVELID = model.DictSymLevelId,
                    SYSMPTOMDATE = model.SymptomDate,
                    CREATEDATE = model.CreateDate,
                    EDITDATE = model.EditDate
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
        private SymptomRecord EntityToModel(CTMS_SYMPTOMRECORDS entity)
        {
            if (entity != null)
            {
                var model = new SymptomRecord()
                {
                    ID = entity.ID,
                    SymptomId = entity.SYMPTOMID,
                    UserId = entity.USERID,
                    SymptomLevel = entity.SYMPTOMLEVEL,
                    DictSymLevelId=entity.DICTSYMLEVELID,
                    SymptomDate=entity.SYSMPTOMDATE,
                    CreateDate = entity.CREATEDATE,
                    EditDate = entity.EDITDATE
                };

                return model;
            }
            return null;
        }
    }
}
