/*
 * 描述:定义IGeneAllele的EF实现
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151113      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFGeneAlleleRepository : BaseRepository<GN_GENEALLELE>, IGeneAlleleRepository
    {
        public EFGeneAlleleRepository()
            : base(new CRDatabase())
        {
            
        }
        /// <summary>
        /// 添加等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Add(GN_GENEALLELE entity)
        {
            base.Insert(entity);
            return entity.ID;
        }

        /// <summary>
        /// 编辑等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Edit(GN_GENEALLELE entity)
        {
            return base.Update(entity);
        }

        /// <summary>
        /// 删除等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            GN_GENEALLELE entity = Get(id);
            if (entity != null)
            {
                entity.ISDELETED = 1;
                base.Update(entity);
            }
            return false;
        }

        /// <summary>
        /// 获取等位基因
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GN_GENEALLELE Get(string id)
        {
            return base.Find(id);
        }

        /// <summary>
        /// 获取等位基因列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<GN_GENEALLELE> GetList(Expression<Func<GN_GENEALLELE, bool>> predicate = null)
        {
            return base.FindAll(predicate).ToList();
        }
    }
}
