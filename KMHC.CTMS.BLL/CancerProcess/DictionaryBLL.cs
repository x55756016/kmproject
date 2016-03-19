/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2016/2/16  邓柏生                                      创建 
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

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class DictionaryBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(HrDictionary model)
        {
            if (model == null)
                return string.Empty;

            using (DictionaryDAL dal = new DictionaryDAL())
            {
                HR_DICTIONARY entity = ModelToEntity(model);
                entity.DICTIONARYID = string.IsNullOrEmpty(model.DictionaryId) ? Guid.NewGuid().ToString() : model.DictionaryId;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public HrDictionary GetOne(Expression<Func<HR_DICTIONARY, bool>> predicate = null)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
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
        public IEnumerable<HrDictionary> GetList(PageInfo page, Expression<Func<HR_DICTIONARY, bool>> predicate = null)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
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
        public IEnumerable<HrDictionary> GetList(Expression<Func<HR_DICTIONARY, bool>> predicate = null)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
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
        public bool Edit(HrDictionary model)
        {
            if (model == null) return false;
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                HR_DICTIONARY entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private HR_DICTIONARY ModelToEntity(HrDictionary model)
        {
            if (model != null)
            {
                var entity = new HR_DICTIONARY()
                {
                    DICTIONARYID = model.DictionaryId,
                    FATHERID = model.FatherId,
                    DICTIONCATEGORY = model.DictionCategory,
                    DICTIONARYNAME = model.DictionaryName,
                    DICTIONARYVALUE = model.DictionaryValue,
                    CREATEUSER = model.CreateUser,
                    CREATEDATE = model.CreateDate,
                    UPDATEUSER = model.UpdateUser,
                    UPDATEDATE = model.UpdateDate,
                    DICTIONCATEGORYNAME = model.DictionCategoryName,
                    REMARK = model.Remark,
                    SYSTEMNAME = model.SystemName,
                    SYSTEMCODE = model.SystemCode,
                    ORDERNUMBER = model.OrderNumber,
                    SYSTEMNEED = model.SystemNeed,
                    ISDELETED = model.IsDeleted
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
        private HrDictionary EntityToModel(HR_DICTIONARY entity)
        {
            if (entity != null)
            {
                var model = new HrDictionary()
                {
                    DictionaryId = entity.DICTIONARYID,
                    FatherId = entity.FATHERID,
                    DictionCategory = entity.DICTIONCATEGORY,
                    DictionaryName = entity.DICTIONARYNAME,
                    DictionaryValue = entity.DICTIONARYVALUE,
                    CreateUser = entity.CREATEUSER,
                    CreateDate = entity.CREATEDATE,
                    UpdateUser = entity.UPDATEUSER,
                    UpdateDate = entity.UPDATEDATE,
                    DictionCategoryName = entity.DICTIONCATEGORYNAME,
                    Remark = entity.REMARK,
                    SystemName = entity.SYSTEMNAME,
                    SystemCode = entity.SYSTEMCODE,
                    OrderNumber = entity.ORDERNUMBER,
                    SystemNeed = entity.SYSTEMNEED,
                    IsDeleted = entity.ISDELETED
                };

                return model;
            }
            return null;
        }
    }
}
