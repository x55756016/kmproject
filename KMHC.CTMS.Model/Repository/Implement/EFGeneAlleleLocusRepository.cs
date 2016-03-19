﻿/*
 * 描述:定义IGeneAlleleLocus的EF实现
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
    public class EFGeneAlleleLocusRepository : BaseRepository<GN_GENEALLELELOCUS>, IGeneAlleleLocusRepository
    {
        /// <summary>
        /// 添加等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Add(GN_GENEALLELELOCUS entity)
        {
            base.Insert(entity);
            return entity.ID;
        }

        /// <summary>
        /// 编辑等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Edit(GN_GENEALLELELOCUS entity)
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
            GN_GENEALLELELOCUS entity = Get(id);
            if (entity != null)
            {
                entity.ISDELETED = true;
                base.Update(entity);
            }
            return false;
        }

        /// <summary>
        /// 获取等位基因
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GN_GENEALLELELOCUS Get(string id)
        {
            return base.Find(id);
        }

        /// <summary>
        /// 获取等位基因列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<GN_GENEALLELELOCUS> GetList(Expression<Func<GN_GENEALLELELOCUS, bool>> predicate = null)
        {
            return base.FindAll(predicate).ToList();
        }
    }
}
