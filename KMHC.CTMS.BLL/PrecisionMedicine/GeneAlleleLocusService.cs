/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.BLL.PrecisionMedicine
{
    public class GeneAlleleLocusService
    {

        /// <summary>
        /// 添加等位基因位点
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Add(GeneAlleleLocus model)
        {
            using (EFGeneAlleleLocusRepository repository = new EFGeneAlleleLocusRepository())
            {
                if (model == null) return string.Empty;
                GN_GENEALLELELOCUS entity = ModelToEntity(model);
                entity.ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID;
                entity.CREATEDATETIME = (model.CreateDateTime != null && model.CreateDateTime.HasValue) ? model.CreateDateTime.Value : DateTime.Now;
                UserInfo currentUser = new UserInfoService().GetCurrentUser();
                if (currentUser != null)
                {
                    entity.CREATEUSERID = currentUser.UserId;
                    entity.CREATEUSERNAME = currentUser.LoginName;
                }
                entity.EDITDATETIME = entity.CREATEDATETIME;
                entity.EDITUSERID = entity.CREATEUSERID;
                entity.EDITUSERNAME = entity.CREATEUSERNAME;

                return repository.Add(entity);
            }
        }

        /// <summary>
        /// 编辑等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Edit(GeneAlleleLocus model)
        {
            using (EFGeneAlleleLocusRepository repository = new EFGeneAlleleLocusRepository())
            {
                if (model == null || string.IsNullOrEmpty(model.ID)) return false;
                GN_GENEALLELELOCUS entity = ModelToEntity(model);
                entity.EDITDATETIME = DateTime.Now;
                UserInfo currentUser = new UserInfoService().GetCurrentUser();
                if (currentUser != null)
                {
                    entity.EDITUSERID = currentUser.UserId;
                    entity.EDITUSERNAME = currentUser.LoginName;
                }

                return repository.Edit(entity);
            }
        }

        /// <summary>
        /// 删除等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            using (EFGeneAlleleLocusRepository repository = new EFGeneAlleleLocusRepository())
            {
                return repository.Delete(id);
            }
        }

        /// <summary>
        /// 获取等位基因位点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GeneAlleleLocus Get(string id)
        {
            using (EFGeneAlleleLocusRepository repository = new EFGeneAlleleLocusRepository())
            {
                GN_GENEALLELELOCUS entity = repository.Get(id);
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 根据等位基因获取等位基因位点列表
        /// </summary>
        /// <param name="geneAlleleID"></param>
        /// <returns></returns>
        public List<GeneAlleleLocus> GetList(string geneAlleleID)
        {
            using (EFGeneAlleleLocusRepository repository = new EFGeneAlleleLocusRepository())
            {
                List<GeneAlleleLocus> modelList = new List<GeneAlleleLocus>();
                if (!string.IsNullOrEmpty(geneAlleleID))
                {
                    modelList = repository.FindAll(o => o.GENEALLELEID.Equals(geneAlleleID) && !o.ISDELETED).Select(EntityToModel).ToList();
                }
                return modelList;
            }
        }

        /// <summary>
        /// 根据等位基因获取等位基因位点列表
        /// </summary>
        /// <param name="GeneAlleleID"></param>
        /// <returns></returns>
        public List<GeneAlleleLocus> GetList(string geneAlleleID,ref PageInfo pageInfo)
        {
            using (EFGeneAlleleLocusRepository repository = new EFGeneAlleleLocusRepository())
            {
                List<GeneAlleleLocus> modelList = new List<GeneAlleleLocus>();
                if (!string.IsNullOrEmpty(geneAlleleID))
                {
                    modelList = repository.FindAll(o => o.GENEALLELEID.Equals(geneAlleleID) && !o.ISDELETED).Paging(ref pageInfo).Select(EntityToModel).ToList();
                }
                return modelList;
            }

        }


        private GN_GENEALLELELOCUS ModelToEntity(GeneAlleleLocus model)
        {
            return new GN_GENEALLELELOCUS()
            {
                ID = model.ID,
                GENEALLELEID = model.GeneAlleleID,
                LOCUSTYPE = model.LocusType,
                STARTPOSITION = model.StartPosition,
                ENDPOSITION = model.EndPosition,
                STANDARDVALUE = model.StandardValue,
                VARIANTVALUE = model.VariantValue,
                VARIANTTYPE = model.VariantType,
                VARIANTCATEGORY = model.VariantCategory,
                VARIANTCODE = model.VariantCode,
                HGVSNAME=model.HGVSName,
                AMINOACIDEFFECT=model.AminoAcidEffect,
                VARIANTGUIDELINE=model.VariantGuideLine,

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

        private GeneAlleleLocus EntityToModel(GN_GENEALLELELOCUS entity)
        {
            return new GeneAlleleLocus()
            {
                ID = entity.ID,
                GeneAlleleID = entity.GENEALLELEID,
                LocusType = entity.LOCUSTYPE,
                StartPosition = entity.STARTPOSITION.HasValue ? Convert.ToInt32(entity.STARTPOSITION.Value) : 0,
                EndPosition = entity.ENDPOSITION.HasValue ? Convert.ToInt32(entity.ENDPOSITION.Value) : 0,
                StandardValue = entity.STANDARDVALUE,
                VariantType = entity.VARIANTTYPE,
                VariantValue = entity.VARIANTVALUE,
                VariantCategory = entity.VARIANTCATEGORY,
                VariantCode = entity.VARIANTCODE,
                AminoAcidEffect=entity.AMINOACIDEFFECT,
                HGVSName=entity.HGVSNAME,
                VariantGuideLine=entity.VARIANTGUIDELINE,

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
