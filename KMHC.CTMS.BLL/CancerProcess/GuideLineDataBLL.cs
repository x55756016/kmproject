/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class GuideLineDataBLL
    {
        private readonly string logTitle = "访问GuideLineDataBLL类";

        public GuideLineDataBLL()
        {
        }
    
        /// <summary>
        /// 根据GuideLineID获取GuideLine元数据
        /// </summary>
        /// <param name="GuideLineID"></param>
        /// <returns></returns>
        public List<GuideLineData> GetDataList(string GuideLineID)
        {
            using (DbContext db = new CRDatabase())
            {
               var query= db.Set<CTMS_GUIDELINEDATA>().AsNoTracking().Where(o => !o.ISDELETED && o.GUIDELINEID.Equals(GuideLineID)).ToList();
               return query.Select(o => EntityToModel(o)).ToList();
            }

        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(GuideLineData model)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的GuideLineData实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        public CTMS_GUIDELINEDATA ModelToEntity(GuideLineData model)
        {
            if (model == null) return null;
            return new CTMS_GUIDELINEDATA()
            {
                ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID,
                GUIDELINEID = model.GuideLineID,
                TEXT = model.Text,
                VALUE = model.Value,

                CREATEDATETIME = model.CreateDateTime,
                CREATEUSERID = model.CreateUserID,
                CREATEUSERNAME = model.CreateUserName,
                EDITDATETIME = model.EditTime,
                EDITUSERID = model.EditUserID,
                EDITUSERNAME = model.EditUserName,
                OWNERID = model.OwnerID,
                OWNERNAME = model.OwnerName,
                ISDELETED = model.IsDeleted
            };
        }

        public GuideLineData EntityToModel(CTMS_GUIDELINEDATA entity)
        {
            if (entity == null) return null;
            return new GuideLineData()
            {
                ID = entity.ID,
                GuideLineID = entity.GUIDELINEID,
                Text = entity.TEXT,
                Value = entity.VALUE,

                CreateDateTime = entity.CREATEDATETIME,
                CreateUserID = entity.CREATEUSERID,
                CreateUserName = entity.CREATEUSERNAME,
                EditTime = entity.EDITDATETIME,
                EditUserID = entity.EDITUSERID,
                EditUserName = entity.EDITUSERNAME,
                OwnerID = entity.OWNERID,
                OwnerName = entity.OWNERNAME,
                IsDeleted = entity.ISDELETED
            };
        }
    }
}
