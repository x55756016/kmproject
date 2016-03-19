using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Common.Helper;
using System.Linq.Expressions;
using KMHC.CTMS.DAL.Database;
using System.Data.Entity;

namespace KMHC.CTMS.BLL
{
    /*
     * 描述:定义字典操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151208      刘佳豪              创建 
     *  
     */
    public class DictionaryService
    {
        /// <summary>
        /// 添加字典数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool AddDictionary(Dictionary dic)
        {
            using (EFDictionaryRepository _rsp = new EFDictionaryRepository())
            {
                bool result = _rsp.AddDictionary(dic);
                return result;
            }
        }

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool EditDictionary(Dictionary dic)
        {
            using (EFDictionaryRepository _rsp = new EFDictionaryRepository())
            {
                bool result = _rsp.EditDictionary(dic);
                return result;
            }
        }

        /// <summary>
        /// 根据id获取字典数据
        /// </summary>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        public Dictionary GetDictionaryById(string dictionaryId)
        {
            using (EFDictionaryRepository _rsp = new EFDictionaryRepository())
            {
                return _rsp.GetDictionaryById(dictionaryId);
            }
        }

        /// <summary>
        /// 通过类别获取字典数据
        /// </summary>
        /// <param name="dictionaryCategory"></param>
        /// <returns></returns>
        public List<Dictionary> GetDictionaryByCategory(string dictionaryCategory)
        {
            using (EFDictionaryRepository _rsp = new EFDictionaryRepository())
            {
                return _rsp.GetDictionaryByCategory(dictionaryCategory);
            }
        }

        /// <summary>
        /// 根据id删除对象
        /// </summary>
        /// <param name="dictionaryId"></param>
        public void DeleteDictionary(string dictionaryId)
        {
            using (EFDictionaryRepository _rsp = new EFDictionaryRepository())
            {
                _rsp.DeleteDictionary(dictionaryId);
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<Dictionary> GetPageData(PageInfo page, Expression<Func<HR_DICTIONARY, bool>> predicate = null)
        {
            using (DbContext db = new CRDatabase())
            {
                IQueryable<HR_DICTIONARY> query = null;
                query = db.Set<HR_DICTIONARY>().AsNoTracking().Where(predicate);

                //List<Dictionary> list = query.Paging(ref page).Select(EntityToModel).ToList();
                //return list;
                return null;
            }
        }
    }
}
