using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Product;
using KMHC.CTMS.Model.Repository.Implement;

namespace KMHC.CTMS.BLL.Product
{
    /*
     * 描述:定义产品/服务操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20160121      刘佳豪              创建 
     *  
     */
    public class ProductsService
    {
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddProducts(Products model)
        {
            using (EFProductsRepository _rsp = new EFProductsRepository())
            {
                return _rsp.AddProducts(model);
            }
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProducts(Products model)
        {
            using (EFProductsRepository _rsp = new EFProductsRepository())
            {
                return _rsp.UpdateProducts(model);
            }
        }

        /// <summary>
        /// 根据id删除产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProductsById(string productId)
        {
            using (EFProductsRepository _rsp = new EFProductsRepository())
            {
                return _rsp.DeleteProductsById(productId);
            }
        }

        /// <summary>
        /// 根据id获取产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Products GetProductsById(string productId)
        {
            using (EFProductsRepository _rsp = new EFProductsRepository())
            {
                return _rsp.GetProductsById(productId);
            }
        }

        /// <summary>
        /// 获取全部产品
        /// </summary>
        /// <returns></returns>
        public List<Products> GetAllProducts()
        {
            using (EFProductsRepository _rsp = new EFProductsRepository())
            {
                return _rsp.GetAllProducts();
            }
        }
    }
}
