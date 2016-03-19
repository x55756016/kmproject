using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Product;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义Products的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20150121      刘佳豪              创建 
     *  
     */
    public interface IProductsRepository
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddProducts(Products model);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateProducts(Products model);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool DeleteProductsById(string productId);

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Products GetProductsById(string productId);

        /// <summary>
        /// 获取全部对象
        /// </summary>
        /// <returns></returns>
        List<Products> GetAllProducts();
    }
}
