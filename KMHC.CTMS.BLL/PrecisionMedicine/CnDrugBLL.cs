/*
 * 描述:
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2015/11/13 16:38:01     邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.PrecisionMedicine;

namespace KMHC.CTMS.BLL.PrecisionMedicine
{
    public class CnDrugBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(CnDrug model)
        {
            if (model == null)
                return string.Empty;
            using (CnDrugDAL dal = new CnDrugDAL())
            {
                DUG_CNDRUG entity = ModelToEntity(model);
                entity.ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString("N") : model.ID;
                entity.CREATEDATETIME = (model.CreateDateTime != null && model.CreateDateTime.HasValue) ? model.CreateDateTime.Value : DateTime.Now;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>

        public List<CnDrug> Get()
        {
            using (CnDrugDAL dal = new CnDrugDAL())
            {
                List<DUG_CNDRUG> entitys = dal.Get().ToList();
                List<CnDrug> list = new List<CnDrug>();
                foreach (DUG_CNDRUG item in entitys)
                {
                    list.Add(EntityToModel(item));
                }

                return list;
            }
        }


        public List<CnDrug> Get(Expression<Func<DUG_CNDRUG, bool>> predicate = null)
        {
            using (CnDrugDAL dal = new CnDrugDAL())
            {
                IEnumerable<DUG_CNDRUG> list = dal.FindAll(predicate);
                List<CnDrug> modelList = new List<CnDrug>();

                foreach (DUG_CNDRUG entity in list)
                {
                    modelList.Add(EntityToModel(entity));
                }

                return modelList;
            }
        }

        public CnDrug Get(string id)
        {
            using (CnDrugDAL dal = new CnDrugDAL())
            {
                DUG_CNDRUG entitys = dal.Get(p => p.ID == id);

                return EntityToModel(entitys);
            }
        }

        public bool Edit(CnDrug model)
        {
            using (CnDrugDAL dal = new CnDrugDAL())
            {
                if (model == null) return false;

                DUG_CNDRUG entitys = ModelToEntity(model);
                entitys.CREATEDATETIME = DateTime.Now;
                
                return dal.Edit(entitys);
            }
        }

        public IEnumerable<CnDrug> GetList(PageInfo page, string Name, string PinYin, string Indication,string IsPrescription = "", string IsMedicalInsurance = "", string TypeName = "", string KindName = "")
        {
            using (CnDrugDAL dal = new CnDrugDAL())
            {
                var list = dal.FindAll(p => !p.ISDELETED);
                if (!string.IsNullOrEmpty(Name))
                {
                    list = list.Where(o => o.NAME.ToLower().Contains(Name.ToLower()) || o.COMMONNAME.ToLower().Contains(Name.ToLower()) || o.ENNAME.ToLower().Contains(Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(PinYin))
                {
                    list = list.Where(o => o.PINYINCODE.ToLower().Contains(PinYin.ToLower()));
                }
                if (!string.IsNullOrEmpty(Indication))
                {
                    list = list.Where(o => o.INDICATION.Contains(PinYin));
                }
                if (!string.IsNullOrEmpty(IsPrescription) && IsPrescription == "false")
                {
                    list = list.Where(o => o.ISPRESCRIPTION == false);
                }
                if (!string.IsNullOrEmpty(IsPrescription) && IsPrescription == "true")
                {
                    list = list.Where(o => o.ISPRESCRIPTION == true);
                }
                if (!string.IsNullOrEmpty(IsMedicalInsurance) && IsMedicalInsurance == "false")
                {
                    list = list.Where(o => o.ISMEDICALINSURANCE == false);
                }
                if (!string.IsNullOrEmpty(IsMedicalInsurance) && IsMedicalInsurance == "true")
                {
                    list = list.Where(o => o.ISMEDICALINSURANCE == true);
                }
                if (!string.IsNullOrEmpty(TypeName))
                {
                    list = list.Where(o => o.TYPENAME.Equals(TypeName));
                }
                if (!string.IsNullOrEmpty(KindName))
                {
                    list = list.Where(o => o.KINDNAME.Equals(KindName));
                }

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private DUG_CNDRUG ModelToEntity(CnDrug model)
        {
            if (model != null)
            {
                DUG_CNDRUG entity = new DUG_CNDRUG()
                {
                    ID = model.ID,
                    DRUGBANKID = model.DrugBankID,
                    NAME = model.Name,
                    PINYINCODE = model.PinyinCode,
                    ENNAME = model.EnName,
                    COMMONNAME = model.CommonName,
                    TYPENAME = model.TypeName,
                    KINDNAME = model.KindName,
                    ISPRESCRIPTION = model.IsPrescription,
                    ISMEDICALINSURANCE = model.IsMedicalInsurance,
                    COMPANY = model.Company,
                    PACK = model.Pack,
                    DOSAGEFORMS = model.DosageForms,
                    DOSAGE = model.Dosage,
                    DESCRIPTION = model.Description,
                    INDICATION = model.Indication,
                    CONTENT = model.Content,
                    PRICE = model.Price,
                    CONTRAINDICATION=model.Contraindication,
                    NOTICE=model.Notice,
                    ADVERSE=model.Adverse,
                    CREATEUSERID = model.CreateUserID,
                    CREATEUSERNAME = model.CreateUserName,
                    CREATEDATETIME = model.CreateDateTime,
                    EDITUSERID = model.EditUserID,
                    EDITUSERNAME = model.EditUserName,
                    EDITDATETIME = model.EditTime,
                    OWNERID = model.OwnerID,
                    OWNERNAME = model.OwnerName,
                    ISDELETED = model.IsDeleted
                };

                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private CnDrug EntityToModel(DUG_CNDRUG entity)
        {
            if (entity != null)
            {
                CnDrug model = new CnDrug()
                {
                    ID = entity.ID,
                    DrugBankID = entity.DRUGBANKID,
                    Name = entity.NAME,
                    PinyinCode = entity.PINYINCODE,
                    EnName = entity.ENNAME,
                    CommonName = entity.COMMONNAME,
                    TypeName = entity.TYPENAME,
                    KindName = entity.KINDNAME,
                    IsPrescription = entity.ISPRESCRIPTION,
                    IsMedicalInsurance = entity.ISMEDICALINSURANCE,
                    Company = entity.COMPANY,
                    Pack = entity.PACK,
                    DosageForms = entity.DOSAGEFORMS,
                    Dosage = entity.DOSAGE,
                    Description = entity.DESCRIPTION,
                    Indication = entity.INDICATION,
                    Content = entity.CONTENT,
                    Price = entity.PRICE,
                    Contraindication=entity.CONTRAINDICATION,
                    Notice=entity.NOTICE,
                    Adverse=entity.ADVERSE,
                    CreateUserID = entity.CREATEUSERID,
                    CreateUserName = entity.CREATEUSERNAME,
                    CreateDateTime = entity.CREATEDATETIME,
                    EditUserID = entity.EDITUSERID,
                    EditUserName = entity.EDITUSERNAME,
                    EditTime = entity.EDITDATETIME,
                    OwnerID = entity.OWNERID,
                    OwnerName = entity.OWNERNAME,
                    IsDeleted = entity.ISDELETED
                };

                return model;
            }

            return null;
        }

       
    }
}
