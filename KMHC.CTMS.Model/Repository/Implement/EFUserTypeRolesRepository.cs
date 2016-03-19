using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFUserTypeRolesRepository : BaseRepository<CTMS_SYS_USERTYPEROLES>,IUserTypeRolesRepository
    {
        public bool AddUserTypeRoles(UserTypeRoles model)
        {
            return Insert(LoadEntityFromModel(model));
        }

        public bool DeleteUserTypeRolesById(string userTypeRoleId)
        {
            return DeleteById(userTypeRoleId);
        }

        public List<UserTypeRoles> GetUserTypeRoleByUserType(int userType)
        {
            return FindAll(p => p.USERTYPE == userType).Select(LoadModelFromEntity).ToList();
        }

        protected UserTypeRoles LoadModelFromEntity(CTMS_SYS_USERTYPEROLES entity)
        {
            if (entity == null)
                return null;

            UserTypeRoles model = new UserTypeRoles();
            model.UserTypeRoleId = entity.USERTYPEROLEID;
            model.UserType = (int)entity.USERTYPE;
            model.RoleId = entity.ROLEID;
            model.RoleName = entity.ROLENAME;
            model.CreateUserId = entity.CREATEUSERID;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.CreateDateTime = (DateTime)entity.CREATEDATETIME;
            model.EditUserId = entity.EDITUSERID;
            model.EditUserName = entity.EDITUSERNAME;
            model.EditDateTime = (DateTime)entity.EDITDATETIME;
            return model;
        }

        protected CTMS_SYS_USERTYPEROLES LoadEntityFromModel(UserTypeRoles model)
        {
            if (model == null)
                return null;

            CTMS_SYS_USERTYPEROLES entity = new CTMS_SYS_USERTYPEROLES();
            entity.USERTYPEROLEID = model.UserTypeRoleId;
            entity.USERTYPE = model.UserType;
            entity.ROLEID = model.RoleId;
            entity.ROLENAME = model.RoleName;
            entity.CREATEUSERID = model.CreateUserId;
            entity.CREATEUSERNAME = model.CreateUserName;
            entity.CREATEDATETIME = model.CreateDateTime;
            entity.EDITUSERID = model.EditUserId;
            entity.EDITUSERNAME = model.EditUserName;
            entity.EDITDATETIME = model.EditDateTime;
            return entity;
        }


        public bool UpdateUserTypeRoles(UserTypeRoles model)
        {
            return Update(LoadEntityFromModel(model));
        }
    }
}
