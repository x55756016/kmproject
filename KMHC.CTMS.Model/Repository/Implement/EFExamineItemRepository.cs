/*
 * 描述:IExamineItemRepository的EF实现
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151016   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */

using System;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFExamineItemRepository : IExamineItemRepository
    {
        private readonly IBaseRepository<HR_EXAMINEITEMS> repository;

        public EFExamineItemRepository()
        {
            repository = new BaseRepository<HR_EXAMINEITEMS>(new CRDatabase());
        }

        public int Add(ExamineItem item)
        {
            var maxId = repository.GetMaxId("HR_EXAMINEITEMS", "ITEMID");
            var entity = ModelToEntity(item);
            entity.ITEMID = maxId;
            entity.CREATEDATE = DateTime.Now;
            //entity.CreatorUserId
            repository.Insert(entity);
            return maxId;
        }

        public bool Edit(ExamineItem item)
        {
            var entity = ModelToEntity(item);
            return entity != null && repository.Update(entity);
        }

        public ExamineItem Get(int itemId)
        {
            return EntityToModel(repository.FindOne(o => o.ITEMID == itemId));
        }

        public ExamineItem Get(string itemno)
        {
            return EntityToModel(repository.FindOne(o => o.ITEMNO.Contains(itemno)));
        }


        #region 实体模型映射


        private HR_EXAMINEITEMS ModelToEntity(ExamineItem model)
        {
            if (model != null)
            {
                var entity = new HR_EXAMINEITEMS()
                {
                    ITEMID = model.ItemId,
                    ITEMNO = model.ItemCode,
                    ITEMNAME = model.ItemName,
                    CODEVALUE = model.CodeValue,
                    DESCRIPTION = model.Description,
                    VALUETYPE = model.ValueType
                };
                return entity;
            }
            return null;

        }
        private ExamineItem EntityToModel(HR_EXAMINEITEMS entity)
        {
            if (entity != null)
            {
                var  model= new ExamineItem()
                {
                    ItemId=entity.ITEMID,
                    ItemCode = entity.ITEMNO,
                    ItemName=entity.ITEMNAME,
                    CodeValue = entity.CODEVALUE,
                    Description=entity.DESCRIPTION,
                    ValueType = entity.VALUETYPE
                };
                return model;
            }
            return null;
        }

        #endregion
    }
}
