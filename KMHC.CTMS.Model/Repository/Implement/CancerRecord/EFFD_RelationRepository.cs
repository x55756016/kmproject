/*
 * 描述:定义FD_Relation的实现
 *  
 * 修订历史: 
 * 日期          修改人              Email              内容
 * 20151102      张志明              			创建 
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

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class EFFD_RelationRepository:IFD_RelationRepository
    {
        private readonly IBaseRepository<HR_FD_RELATION> repository;

        public EFFD_RelationRepository() 
        {
            repository = new BaseRepository<HR_FD_RELATION>(new CRDatabase());
        }
        
        /// <summary>
        /// 新增家族史关系
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public string Add(FD_Relation relation)
        {
            return string.Empty;
        }

        /// <summary>
        /// 修改家族史关系
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public bool Edit(FD_Relation relation)
        {
            return false;
        }

        /// <summary>
        /// 根据ID删除家族史关系
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            return false;
        }

        public FD_Relation Get(string id)
        {
            var data= repository.FindOne(o => o.ID.Equals(id));
            return data == null ? null : EntityToModel(data);
        }

        /// <summary>
        /// 根据用户获取家族
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<FD_Relation> GetAll()
        {
            List<FD_Relation> list=new List<FD_Relation>();
            var dataList = repository.FindAll();
            if (dataList != null && dataList.Count() > 0)
            {
                foreach (HR_FD_RELATION relation in dataList)
                {
                    list.Add(EntityToModel(relation));
                }
            }
            return list.OrderBy(o => o.Sort).ToList() ;
        }

        /// <summary>
        /// 数据库模型转业务模型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected FD_Relation EntityToModel(HR_FD_RELATION entity)
        {
            FD_Relation model = new FD_Relation()
            {
                Apply = (entity.APPLY.HasValue && entity.APPLY.Value==1),
                Generate = entity.GENERATE,
                Requird = (entity.REQUIRD.HasValue && entity.REQUIRD.Value == 1),
                Sex = entity.SEX.HasValue?entity.SEX.Value.ToString():"0",
                Side = entity.SIDE,
                Sort = entity.SORT.HasValue?Convert.ToInt32(entity.SORT.Value):9999,
                Text = entity.TEXT,
                Value = entity.RELATIONVALUE.ToString(),
                ID=entity.ID
            };
            return model;
        }
    }
}
