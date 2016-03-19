/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/12/4 14:49:46     邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.CancerRecord;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.CancerRecord
{
    public class LaboratoryTestItemBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(LaboratoryTestItem model)
        {
            if (model == null)
                return string.Empty;

            using (LaboratoryTestItemDAL dal = new LaboratoryTestItemDAL())
            {
                HR_LABORATORYTESTITEM entity = ModelToEntity(model);
                entity.TESTITEMID = string.IsNullOrEmpty(model.TestitemId) ? Guid.NewGuid().ToString("N") : model.TestitemId;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public LaboratoryTestItem Get(Expression<Func<HR_LABORATORYTESTITEM, bool>> predicate = null)
        {
            using (LaboratoryTestItemDAL dal = new LaboratoryTestItemDAL())
            {
                HR_LABORATORYTESTITEM entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LaboratoryTestItem> Get()
        {
            using (LaboratoryTestItemDAL dal = new LaboratoryTestItemDAL())
            {
                List<HR_LABORATORYTESTITEM> entitys = dal.Get().ToList();
                List<LaboratoryTestItem> list = new List<LaboratoryTestItem>();
                entitys.ForEach(e => list.Add(EntityToModel(e)));

                return list;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<LaboratoryTestItem> GetList(PageInfo page)
        {
            using (LaboratoryTestItemDAL dal = new LaboratoryTestItemDAL())
            {
                var list = dal.Get();

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(LaboratoryTestItem model)
        {
            if (model == null) return false;
            using (LaboratoryTestItemDAL dal = new LaboratoryTestItemDAL())
            {
                HR_LABORATORYTESTITEM entity = ModelToEntity(model);

                return dal.Edit(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            using (LaboratoryTestItemDAL dal = new LaboratoryTestItemDAL())
            {
                return dal.Delete(id);
            }
        }

        /// <summary>
        /// 根据实验室结果ID获取实验项
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public List<LaboratoryTestItem> GetListByRId(string resultId)
        {
            if (string.IsNullOrEmpty(resultId)) return null;
            using (LaboratoryTestItemDAL dal = new LaboratoryTestItemDAL())
            {
                IQueryable<HR_LABORATORYTESTITEM> entitys = dal.Get().Where(p=>p.LABRESULTID==resultId).OrderBy("ORDERNUMBER");
                var list = entitys.Select(EntityToModel).ToList();

                return list;
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private HR_LABORATORYTESTITEM ModelToEntity(LaboratoryTestItem model)
        {
            if (model != null)
            {
                HR_LABORATORYTESTITEM entity = new HR_LABORATORYTESTITEM()
                {
                    TESTITEMID = model.TestitemId,
                    ITEMID = model.ItemId,
                    ORDERNUMBER = model.Ordernumber,
                    ITEMNAME = model.ItemName,
                    ITEMNAMEEN = model.ItemNameEN,
                    ITEMVALUE=model.ItemValue,
                    RESLUT = model.Reslut,
                    UOM = model.Uom,
                    NORMALVALUE = model.NormalValue,
                    REFERENCEVALUE = model.ReferenceValue,
                    LABRESULTID = model.LabresultId,
                    INSPECTEDMEANS=model.InspectedMeans,
                    ITEMUNITID = model.ItemUnitId
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
        private LaboratoryTestItem EntityToModel(HR_LABORATORYTESTITEM entity)
        {
            if (entity != null)
            {
                LaboratoryTestItem model = new LaboratoryTestItem()
                {
                    TestitemId = entity.TESTITEMID,
                    ItemId = entity.ITEMID,
                    Ordernumber = entity.ORDERNUMBER,
                    ItemName = entity.ITEMNAME,
                    ItemNameEN = entity.ITEMNAMEEN,
                    ItemValue=entity.ITEMVALUE,
                    Reslut = entity.RESLUT,
                    Uom = entity.UOM,
                    NormalValue = entity.NORMALVALUE,
                    ReferenceValue = entity.REFERENCEVALUE,
                    LabresultId = entity.LABRESULTID,
                    InspectedMeans=entity.INSPECTEDMEANS,
                    ItemUnitId = entity.ITEMUNITID
                };
                
                return model;
            }

            return null;
        }

    }
}
