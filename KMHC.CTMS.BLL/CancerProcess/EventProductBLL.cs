/*
 * 描述:
 *  
 * 修订历史: 
 * 日期        修改人              Email                   内容
 * 2016/1/23   邓柏生                                      创建 
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
    public class EventProductBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(EventProduct model)
        {
            if (model == null)
                return string.Empty;

            using (EventProductDAL dal = new EventProductDAL())
            {
                CTMS_EVENTPRODUCT entity = ModelToEntity(model);
                entity.EVENTPRODUCTID = string.IsNullOrEmpty(model.EventProductId) ? Guid.NewGuid().ToString() : model.EventProductId;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public EventProduct Get(Expression<Func<CTMS_EVENTPRODUCT, bool>> predicate = null)
        {
            using (EventProductDAL dal = new EventProductDAL())
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
        public IEnumerable<EventProduct> GetList(PageInfo page, Expression<Func<CTMS_EVENTPRODUCT, bool>> predicate = null)
        {
            using (EventProductDAL dal = new EventProductDAL())
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
        public IEnumerable<EventProduct> GetList(Expression<Func<CTMS_EVENTPRODUCT, bool>> predicate = null)
        {
            using (EventProductDAL dal = new EventProductDAL())
            {
                return dal.Get(predicate).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(EventProduct model)
        {
            if (model == null) return false;
            using (EventProductDAL dal = new EventProductDAL())
            {
                CTMS_EVENTPRODUCT entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CTMS_EVENTPRODUCT ModelToEntity(EventProduct model)
        {
            if (model != null)
            {
                var entity = new CTMS_EVENTPRODUCT()
                {
                    EVENTPRODUCTID = model.EventProductId,
                    EVENTID = model.EventId,
                    PRODUCTID = model.ProductID,
                    PRODUCTNAME = model.ProductName,
                    PRODUCTPRICE = model.ProductPrice,
                    PRODUCTDES = model.ProductDes,
                    ISALREADYBUY=model.IsAlreadyBuy
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
        private EventProduct EntityToModel(CTMS_EVENTPRODUCT entity)
        {
            if (entity != null)
            {
                var model = new EventProduct()
                {
                    EventProductId=entity.EVENTPRODUCTID,
                    EventId=entity.EVENTID,
                    ProductID=entity.PRODUCTID,
                    ProductName=entity.PRODUCTNAME,
                    ProductPrice=entity.PRODUCTPRICE,
                    ProductDes=entity.PRODUCTDES,
                    IsAlreadyBuy=entity.ISALREADYBUY
                };

                return model;
            }
            return null;
        }
    }
}
