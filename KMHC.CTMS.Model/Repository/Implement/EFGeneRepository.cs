/*
 * 描述:定义IGene的EF实现
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151113      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Repository;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFGeneRepository : BaseRepository<GN_GENE>, IGeneRepository
    {
        // private readonly IBaseRepository<GENE> repository;

        public EFGeneRepository()
            : base(new CRDatabase())
        {
            
        }

        /// <summary>
        /// 添加基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Add(GN_GENE entity)
        {
            base.Insert(entity);
            return entity.ID;
        }

        /// <summary>
        /// 编辑基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Edit(GN_GENE entity)
        {
            return base.Update(entity);
        }


        /// <summary>
        /// 删除基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            GN_GENE entity = Get(id);
            if (entity != null)
            {
                entity.ISDELETED = 1;
                base.Update(entity);
            }
            return false;
        }

        /// <summary>
        /// 查询基因字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GN_GENE Get(string id)
        {
            return base.Find(id);
        } 
    }
}
