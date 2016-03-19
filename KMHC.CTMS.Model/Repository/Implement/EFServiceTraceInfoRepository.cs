using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFServiceTraceInfoRepository : BaseRepository<CTMS_SERVICETRACEINFO>, IServiceTraceInfoRepository
    {

        protected CTMS_SERVICETRACEINFO LoadEntityFromModel(ServiceTraceInfo model)
        {
            if (model == null)
                return null;

            CTMS_SERVICETRACEINFO entity = new CTMS_SERVICETRACEINFO();
            entity.ID = model.Id;
            entity.TRACETYPE = (int)model.TraceType;
            entity.TRACEDETAIL = model.TraceDetail;
            entity.CREATEDATETIME = model.CreateDateTime;
            entity.CREATEUSERID = model.CreateUserId;
            entity.CREATEUSERNAME = model.CreateUserName;
            entity.FOREVENTID = model.ForEventId;
            entity.ISVALID = model.IsValid;

            return entity;
        }

        protected ServiceTraceInfo LoadModelFromEntity(CTMS_SERVICETRACEINFO entity)
        {
            if (entity == null)
                return null;

            ServiceTraceInfo model = new ServiceTraceInfo();
            model.Id = entity.ID;
            model.TraceType = (CTMS.Common.TraceType)entity.TRACETYPE;
            model.TraceDetail = entity.TRACEDETAIL;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.CreateUserId = entity.CREATEUSERID;
            model.CreateDateTime = (DateTime)entity.CREATEDATETIME;
            model.ForEventId = entity.FOREVENTID;
            model.IsValid = (int)entity.ISVALID;

            return model;
        }

        public bool AddServiceTraceInfo(ServiceTraceInfo model)
        {
            return Insert(LoadEntityFromModel(model));
        }
        
        public List<ServiceTraceInfo> GetServiceTraceInfoByCreateUserId(string createUserId)
        {
            return FindAll(p => p.CREATEUSERID.ToLower() == createUserId.ToLower()).Select(LoadModelFromEntity).ToList();
        }
    }
}
