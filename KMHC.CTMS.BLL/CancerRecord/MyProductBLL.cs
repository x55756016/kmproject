/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.BLL.Product;
using KMHC.CTMS.Common;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.CancerRecord
{
    public class MyProductBLL : BaseBLL
    {
    private readonly string logTitle = "访问MyProductBLL类";
        public MyProductBLL()
        {

        }

        /// <summary>
        /// 新增我的产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(MyProduct model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                db.Set<CTMS_MYPRODUCT>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改我的产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MyProduct model)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的MyProduct实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除我的产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的MyProduct实体!");
                throw new KeyNotFoundException();
            }
            MyProduct model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取我的产品
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MyProduct Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                if (string.IsNullOrEmpty(id)) return null;
                CTMS_MYPRODUCT entity = db.Set<CTMS_MYPRODUCT>().Find(id);
                if (entity == null) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 根据ID获取我的产品
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public bool BuyProduct(string userID, string ProductID)
        {
            using (DbContext db = new CRDatabase())
            {
                DateTime now = DateTime.Now;
                if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(ProductID))
                {
                    throw new Exception("用户ID或产品ID为空,购买失败！");
                }
                CTMS_SYS_USERINFO user = db.Set<CTMS_SYS_USERINFO>().Find(userID);
                CTMS_PRODUCTS product = db.Set<CTMS_PRODUCTS>().Find(ProductID);
                if (user == null || product == null)
                {
                    throw new Exception("错误的用户ID或产品ID");
                }
                if (product.SALEPRICE > user.ACCOUNT) throw new Exception("余额不足！");

                //用户表
                decimal salePrice = product.SALEPRICE.HasValue ? product.SALEPRICE.Value : 0;
                user.ACCOUNT -= salePrice;
                db.Entry(user).State = EntityState.Modified;

                //记录表
                CTMS_ACCOUNTRECORD accountRecord = new CTMS_ACCOUNTRECORD()
                {
                    ID = Guid.NewGuid().ToString(),
                    USERID = user.USERID,
                    ACCOUNT = salePrice,
                    ACCOUNTDESCRIPTION = string.Format("购买{0}", product.PRODUCTNAME),
                    BALANCE = -1,
                    LOGINNAME = user.LOGINNAME,
                    ORDERID = "",
                    PRODUCTID = product.PRODUCTID,
                    PRODUCTNAME = product.PRODUCTNAME,
                    SPENDTYPE = (int)SpendType.Buy,
                    CREATEDATETIME = now,
                    CREATEUSERID = user.USERID,
                    CREATEUSERNAME = user.LOGINNAME
                };
                db.Set<CTMS_ACCOUNTRECORD>().Add(accountRecord);

                //我的产品表
                CTMS_MYPRODUCT myProduct = new CTMS_MYPRODUCT()
                {
                    ID = Guid.NewGuid().ToString(),
                    USERID = user.USERID,
                    LOGINNAME = user.LOGINNAME,
                    PRODUCTID = product.PRODUCTID,
                    PRODUCTNUM = 1,
                    STARTDATE = DateTime.Now.Date,
                    ENDDATE = DateTime.Now.Date.AddYears(1).AddDays(-1),
                    CREATEUSERID = user.USERID,
                    CREATEUSERNAME = user.LOGINNAME,
                    CREATEDATETIME = now,
                };
                db.Set<CTMS_MYPRODUCT>().Add(myProduct);

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 购买会员
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public bool BuyMember(string userID, string memberID)
        {
            
            using (DbContext db = new CRDatabase())
            {
                DateTime now = DateTime.Now;
                DateTime startDate=now.Date;
                DateTime endDate=now.Date.AddYears(1).AddDays(-1);
                UserInfo userInfo = new UserInfoService().GetUserInfoByID(userID);
                MemberModel buyMember = new MemberBLL().GetMember(memberID);
                if (userInfo == null || buyMember == null)
                {
                    throw new Exception("错误的用户ID或会员ID");
                }
                //判断余额
                if (userInfo.Account < buyMember.MEMBERPRICE.Value)
                {
                    throw new Exception("余额不足,购买失败!");
                }
                //判断会员等级
                if (userInfo.Member.MEMBERLEVEL.Value >= buyMember.MEMBERLEVEL)
                {
                    throw new Exception("不能购买相同或更低级别的会员!");
                }
                //1.扣钱+升级会员
                CTMS_SYS_USERINFO user=db.Set<CTMS_SYS_USERINFO>().Find(userID);
                user.ACCOUNT -= buyMember.MEMBERPRICE.Value;
                user.MEMBERID = buyMember.MEMBERID;
                user.MEMBERSTARTDATE = startDate;
                user.MEMBERENDDATE = endDate;
                db.Entry(user).State=EntityState.Modified;
                //2.添加商品
                List<string> productDescList=new List<string>();
                foreach (MemberProducts memberProduct in buyMember.menberProductList)
                {
                    if (!memberProduct.PRODUCTNUMBER.HasValue || Convert.ToInt32(memberProduct.PRODUCTNUMBER.Value) <= 0) continue;
                    for (int i = 0; i < Convert.ToInt32(memberProduct.PRODUCTNUMBER.Value);i++ )
                    {
                        db.Set<CTMS_MYPRODUCT>().Add(new CTMS_MYPRODUCT()
                        {
                            CREATEDATETIME = now,
                            CREATEUSERID = userInfo.UserId,
                            CREATEUSERNAME = userInfo.LoginName,
                            ENDDATE = endDate,
                            ID = Guid.NewGuid().ToString(),
                            LOGINNAME = userInfo.LoginName,
                            PRODUCTID = memberProduct.PRODUCTID,
                            PRODUCTNUM = 1,
                            STARTDATE = startDate,
                            USERID = userInfo.UserId
                        });
                    }
                    var product=db.Set<CTMS_PRODUCTS>().Find(memberProduct.PRODUCTID);
                    if(product!=null)
                    {
                        productDescList.Add(string.Format("{0}*{1}", product.PRODUCTNAME, Convert.ToInt32(memberProduct.PRODUCTNUMBER.Value)));
                    }
                }
                //3.提示记录
                db.Set<CTMS_ACCOUNTRECORD>().Add(new CTMS_ACCOUNTRECORD()
                {
                    ACCOUNT = buyMember.MEMBERPRICE.Value,
                    ACCOUNTDESCRIPTION = string.Format("购买{0}", buyMember.MEMBERNAME),
                    BALANCE = -1,
                    CREATEDATETIME = now,
                    CREATEUSERID = userInfo.UserId,
                    CREATEUSERNAME = userInfo.LoginName,
                    ID = Guid.NewGuid().ToString(),
                    LOGINNAME = userInfo.LoginName,
                    PRODUCTID = "",
                    PRODUCTNAME = string.Format("{0}（包含商品/服务：{1}）", buyMember.MEMBERNAME, string.Join(",", productDescList)),
                    //PRODUCTNAME = string.Format("{0}", buyMember.MEMBERNAME),
                    SPENDTYPE = (int)SpendType.Buy,
                    USERID = userInfo.UserId,
                });
                return db.SaveChanges() > 0;
            } 
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MyProduct> GetList(string userID)
        {
            using (DbContext db = new CRDatabase())
            {
                var query = db.Set<CTMS_MYPRODUCT>().AsNoTracking().Where(o => !o.ISDELETED);
                if (!string.IsNullOrEmpty(userID))
                {
                    query = query.Where(o => o.USERID.Equals(userID));
                }
                List<MyProduct> list=new List<MyProduct>();
                query.ToList().ForEach((o)=> {
                    MyProduct pro=list.Find(p => p.UserID.Equals(o.USERID) && p.ProductID.Equals(o.PRODUCTID) && p.StartDate == o.STARTDATE && p.EndDate == o.ENDDATE);
                    if (pro != null)
                    {
                        pro.ProductNum += o.PRODUCTNUM;
                    }
                    else
                    {
                        List<Dictionary> ProductTypes = new DictionaryService().GetDictionaryByCategory("ProductType").FirstOrDefault().nodes;
                        List<Dictionary> ProductUnits = new DictionaryService().GetDictionaryByCategory("ProductUnit").FirstOrDefault().nodes;
                        MyProduct myProduct = EntityToModel(o);
                        var type = ProductTypes.Find(d => d.value.Trim().Equals(myProduct.Product.ProductType.ToString()));
                        var unit = ProductUnits.Find(d => d.value.Trim().Equals(myProduct.Product.ProductUnit.ToString()));
                        myProduct.Product.ProductTypeText = (type == null ? "" : type.text);
                        myProduct.Product.ProductUnitText = (unit == null ? "" : unit.text);
                        list.Add(myProduct);
                    }
                });
                return list.OrderByDescending(q=>q.StartDate).ToList();
            }
        }

        private CTMS_MYPRODUCT ModelToEntity(MyProduct model)
        {
            if (model == null) return null;
            return new CTMS_MYPRODUCT()
            {
                ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID,
                LOGINNAME = model.LoginName,
                PRODUCTID = model.ProductID,
                PRODUCTNUM = model.ProductNum,
                STARTDATE = model.StartDate,
                ENDDATE = model.EndDate,
                USERID = model.UserID,
                ISUSED = model.IsUsed,
                USEDDATE = model.UsedDate,


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

        private MyProduct EntityToModel(CTMS_MYPRODUCT entity)
        {
            if (entity == null) return null;
            return new MyProduct()
            {
                ID = entity.ID,
                LoginName = entity.LOGINNAME,
                UserID = entity.USERID,
                ProductID = entity.PRODUCTID,
                ProductNum = entity.PRODUCTNUM,
                StartDate = entity.STARTDATE,
                EndDate = entity.ENDDATE,
                IsUsed = entity.ISUSED,
                UsedDate = entity.USEDDATE,
                Product = string.IsNullOrEmpty(entity.PRODUCTID) ? null : new ProductsService().GetProductsById(entity.PRODUCTID),

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
