using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFResetPasswordLogRepository :BaseRepository<CTMS_SYS_RESETPASSWORDLOG>, IResetPasswordLogRepository
    {
        public EFResetPasswordLogRepository() : base(new CRDatabase()) { }

        public ResetPasswordLog GetResetPasswordLogByID(string resetLogID)
        {
            CTMS_SYS_RESETPASSWORDLOG resetPasswordLog = FindOne(p => p.ID == resetLogID && p.USERSTATUS == 0);
            return LoadModelFromEntity(resetPasswordLog);
        }

        public bool AddResetPasswordLog(ResetPasswordLog resetPasswordLog)
        {
            CTMS_SYS_RESETPASSWORDLOG resetLog = LoadEntityFromModel(resetPasswordLog);
            bool result = Insert(resetLog);
            return result;
        }

        protected ResetPasswordLog LoadModelFromEntity(CTMS_SYS_RESETPASSWORDLOG entity)
        {
            if (entity == null)
                return null;

            ResetPasswordLog model = new ResetPasswordLog();
            model.ID = entity.ID;
            model.InputTime = (DateTime)entity.INPUTTIME;
            model.UserID = entity.USERID;
            model.ResetType = (int)entity.RESETTYPE;
            model.Status = (int)entity.USERSTATUS;

            return model;
        }

        protected CTMS_SYS_RESETPASSWORDLOG LoadEntityFromModel(ResetPasswordLog model)
        {
            if (model == null)
                return null;

            CTMS_SYS_RESETPASSWORDLOG entity = new CTMS_SYS_RESETPASSWORDLOG();
            entity.ID = model.ID;
            entity.INPUTTIME = (DateTime?)model.InputTime;
            entity.USERID = model.UserID;
            entity.RESETTYPE = (decimal?)model.ResetType;
            entity.USERSTATUS = (decimal?)model.Status;

            return entity;
        }


        public bool UpdateResetPasswordLog(ResetPasswordLog resetPasswordLog)
        {
            CTMS_SYS_RESETPASSWORDLOG resetLog = LoadEntityFromModel(resetPasswordLog);
            bool result = Update(resetLog);
            return result;
        }
    }
}
