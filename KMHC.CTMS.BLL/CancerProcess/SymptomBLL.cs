/*
 * 描述:症状体系BLL
 *  
 * 修订历史: 
 * 日期          修改人              Email                   内容
 * 2016/1/5      邓柏生                                      创建 
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
    public class SymptomBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(Symptom model)
        {
            if (model == null)
                return string.Empty;

            using (SymptomDAL dal = new SymptomDAL())
            {
                CTMS_SYMPTOM entity = ModelToEntity(model);
                entity.ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Symptom GetOne(Expression<Func<CTMS_SYMPTOM, bool>> predicate = null)
        {
            using (SymptomDAL dal = new SymptomDAL())
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
        public IEnumerable<Symptom> GetList(PageInfo page, Expression<Func<CTMS_SYMPTOM, bool>> predicate = null)
        {
            using (SymptomDAL dal = new SymptomDAL())
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
        public IEnumerable<Symptom> GetList(Expression<Func<CTMS_SYMPTOM, bool>> predicate = null)
        {
            using (SymptomDAL dal = new SymptomDAL())
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
        public bool Edit(Symptom model)
        {
            if (model == null) return false;
            using (SymptomDAL dal = new SymptomDAL())
            {
                CTMS_SYMPTOM entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CTMS_SYMPTOM ModelToEntity(Symptom model)
        {
            if (model != null)
            {
                var entity = new CTMS_SYMPTOM()
                {
                    ID = model.ID,
                    USERID = model.UserId,
                    SYMPTOMNAME = model.SymptomName,
                    DICTSYMPTOMID = model.DictsymptomId,
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
        private Symptom EntityToModel(CTMS_SYMPTOM entity)
        {
            if (entity != null)
            {
                var model = new Symptom()
                {
                    ID = entity.ID,
                    UserId = entity.USERID,
                    SymptomName = entity.SYMPTOMNAME,
                    DictsymptomId = entity.DICTSYMPTOMID,
                    CreateDate = entity.CREATEDATE,
                    EditDate = entity.EDITDATE
                };

                return model;
            }
            return null;
        }
    }
}
