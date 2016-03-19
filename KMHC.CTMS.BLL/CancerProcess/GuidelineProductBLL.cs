/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2016/1/22      邓柏生                                      创建 
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
    public class GuidelineProductBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(GuidelineProduct model)
        {
            if (model == null)
                return string.Empty;

            using (GuidelineProductDAL dal = new GuidelineProductDAL())
            {
                CTMS_GUIDELINEPRODUCT entity = ModelToEntity(model);
                entity.GUIDELINEPRODUCTID = string.IsNullOrEmpty(model.GuidelineProductId) ? Guid.NewGuid().ToString() : model.GuidelineProductId;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public GuidelineProduct GetOne(Expression<Func<CTMS_GUIDELINEPRODUCT, bool>> predicate = null)
        {
            using (GuidelineProductDAL dal = new GuidelineProductDAL())
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
        public IEnumerable<GuidelineProduct> GetList(PageInfo page, Expression<Func<CTMS_GUIDELINEPRODUCT, bool>> predicate = null)
        {
            using (GuidelineProductDAL dal = new GuidelineProductDAL())
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
        public IEnumerable<GuidelineProduct> GetList(Expression<Func<CTMS_GUIDELINEPRODUCT, bool>> predicate = null)
        {
            using (GuidelineProductDAL dal = new GuidelineProductDAL())
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
        public bool Edit(GuidelineProduct model)
        {
            if (model == null) return false;
            using (GuidelineProductDAL dal = new GuidelineProductDAL())
            {
                CTMS_GUIDELINEPRODUCT entitys = ModelToEntity(model);

                return dal.Edit(entitys);
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
            using (GuidelineProductDAL dal = new GuidelineProductDAL())
            {
                return dal.Delete(id);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CTMS_GUIDELINEPRODUCT ModelToEntity(GuidelineProduct model)
        {
            if (model != null)
            {
                var entity = new CTMS_GUIDELINEPRODUCT()
                {
                    GUIDELINEPRODUCTID = model.GuidelineProductId,
                    GUIDELINEID = model.GuidelineId,
                    PRODUCTID = model.ProductId,
                    PRODUCTNAME = model.ProductName,
                    PRODUCTPRICE = model.ProductPrice,
                    PRODUCTDES = model.Productdes,
                    ISALREADYBUY = model.IsAlreadyBuy
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
        private GuidelineProduct EntityToModel(CTMS_GUIDELINEPRODUCT entity)
        {
            if (entity != null)
            {
                var model = new GuidelineProduct()
                {
                    GuidelineProductId = entity.GUIDELINEPRODUCTID,
                    GuidelineId = entity.GUIDELINEID,
                    ProductId = entity.PRODUCTID,
                    ProductName = entity.PRODUCTNAME,
                    ProductPrice = entity.PRODUCTPRICE,
                    Productdes = entity.PRODUCTDES,
                    IsAlreadyBuy = entity.ISALREADYBUY
                };

                return model;
            }
            return null;
        }
    }
}
