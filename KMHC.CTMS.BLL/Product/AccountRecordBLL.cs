/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Common;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.Product
{
    public class AccountRecordBLL:BaseBLL
    {
        private readonly string logTitle = "访问AccountRecordBLL类";
        public AccountRecordBLL()
        {

        }

        /// <summary>
        /// 新增账单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(AccountRecord model)
        {
            if (model == null) return string.Empty;
            if(string.IsNullOrEmpty(model.AccountDescription))
            {
                model.AccountDescription = string.Format("{0}{1}", model.SpendTypeText, model.ProductName);
            }
           
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_USERINFO user= db.Set<CTMS_SYS_USERINFO>().Find(model.UserID);
                if (model.Balance==-1 && model.Account > user.ACCOUNT) throw new Exception("余额不足！");
                user.ACCOUNT += model.Balance* model.Account;
                db.Entry(user).State = EntityState.Modified;
                //Todo 对应服务次数加1

                db.Set<CTMS_ACCOUNTRECORD>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改账单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(AccountRecord model)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的AccountRecord实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的AccountRecord实体!");
                throw new KeyNotFoundException();
            }
            AccountRecord model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取账单
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public AccountRecord Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                if (string.IsNullOrEmpty(id)) return null;
                CTMS_ACCOUNTRECORD entity = db.Set<CTMS_ACCOUNTRECORD>().Find(id);
                if (entity == null) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<AccountRecord> GetList(string userID,DateTime startDate,DateTime endDate)
        {
            using (DbContext db = new CRDatabase())
            {
                DateTime date = endDate.Date.AddDays(1);
                return db.Set<CTMS_ACCOUNTRECORD>()
                    .AsNoTracking()
                    .Where
                    (
                        o => !o.ISDELETED
                        && o.USERID.Equals(userID)
                        && o.CREATEDATETIME >= startDate.Date
                        && o.CREATEDATETIME < date
                    )
                   .OrderByDescending(o => o.CREATEDATETIME)
                   .Select(EntityToModel)
                   .ToList();
            }
        }

        private CTMS_ACCOUNTRECORD ModelToEntity(AccountRecord model)
        {
            if (model == null) return null;
            return new CTMS_ACCOUNTRECORD()
            {
                ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID,
                USERID = model.UserID,
                LOGINNAME = model.LoginName,
                ORDERID = model.OrderID,
                PRODUCTID = model.ProductID,
                PRODUCTNAME = model.ProductName,
                BALANCE = model.Balance,
                ACCOUNT = model.Account,
                ACCOUNTDESCRIPTION = model.AccountDescription,
                SPENDTYPE = (int)model.SpendType,


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

        private AccountRecord EntityToModel(CTMS_ACCOUNTRECORD entity)
        {
            if (entity == null) return null;
            return new AccountRecord()
            {
                ID = entity.ID,
                UserID = entity.USERID,
                LoginName = entity.LOGINNAME,
                OrderID = entity.ORDERID,
                ProductID = entity.PRODUCTID,
                ProductName = entity.PRODUCTNAME,
                Balance = entity.BALANCE,
                Account = entity.ACCOUNT,
                AccountDescription = entity.ACCOUNTDESCRIPTION,
                SpendType = (SpendType)entity.SPENDTYPE,


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
