using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Authorization
{
    public class UserTypeRoles
    {
        public string UserTypeRoleId { get; set; }

        public int UserType { get; set; }

        public string UserTypeText
        {
            get
            {
                return ((KMHC.CTMS.Common.UserType)UserType).ToString();
            }
        }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public string CreateUserId { get; set; }

        public string CreateUserName { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string EditUserId { get; set; }

        public string EditUserName { get; set; }

        public DateTime EditDateTime { get; set; }
    }
}
