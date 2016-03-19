using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Common;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义Dictionary的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151207      刘佳豪              创建 
     *  
     */
    public interface IDictionaryRepository
    {
        /// <summary>
        /// 通过字典类型获取一棵树
        /// </summary>
        /// <param name="category">字典类型</param>
        /// <returns>一棵树</returns>
        List<Dictionary> GetDictionaryByCategory(string category);

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        bool AddDictionary(Dictionary dic);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        bool EditDictionary(Dictionary dic);

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        Dictionary GetDictionaryById(string dictionaryId);

        /// <summary>
        /// 根据id删除对象
        /// </summary>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        void DeleteDictionary(string dictionaryId);
    }
}
