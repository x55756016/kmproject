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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.BLL.PrecisionMedicine
{
    public class GeneAlleleService
    {

        /// <summary>
        /// 添加等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Add(GeneAllele model)
        {
            using (EFGeneAlleleRepository repository = new EFGeneAlleleRepository())
            {
                if (model == null) return string.Empty;
                GN_GENEALLELE entity = ModelToEntity(model);
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
        public bool Edit(GeneAllele model)
        {
            using (EFGeneAlleleRepository repository = new EFGeneAlleleRepository())
            {
                if (model == null || string.IsNullOrEmpty(model.ID)) return false;
                GN_GENEALLELE entity = ModelToEntity(model);
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
            using (EFGeneAlleleRepository repository = new EFGeneAlleleRepository())
            {
                return repository.Delete(id);
            }
        }

        /// <summary>
        /// 获取等位基因
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GeneAllele Get(string id)
        {
            using (EFGeneAlleleRepository repository = new EFGeneAlleleRepository())
            {
                GN_GENEALLELE entity = repository.Get(id);
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 根据基因ID获取等位基因列表
        /// </summary>
        /// <param name="geneID"></param>
        /// <returns></returns>
        public List<GeneAllele> GetList(string geneID)
        {
            using (EFGeneAlleleRepository repository = new EFGeneAlleleRepository())
            {
                List<GeneAllele> modelList = new List<GeneAllele>();
                if (!string.IsNullOrEmpty(geneID))
                {
                    var list = repository.FindAll(o => o.GENEID.Equals(geneID) && !o.ISDELETED).ToList(); ;
                    modelList=(from g in list select EntityToModel(g)).ToList();
                }
                return modelList;
            }
        }

        /// <summary>
        /// 根据基因ID获取等位基因列表
        /// </summary>
        /// <param name="geneID"></param>
        /// <returns></returns>
        public List<GeneAllele> GetList(string geneID, ref PageInfo pageInfo)
        {
            using (EFGeneAlleleRepository repository = new EFGeneAlleleRepository())
            {
                List<GeneAllele> modelList = new List<GeneAllele>();
                if (!string.IsNullOrEmpty(geneID))
                {
                    modelList = repository.FindAll(o => o.GENEID.Equals(geneID) && !o.ISDELETED).Paging(ref pageInfo).Select(EntityToModel).ToList(); 
                }
                return modelList;
            }
        }



        /// <summary>
        /// 获取用户allele的描述信息
        /// </summary>
        /// <returns></returns>
        public IList<string> GetUserGeneDesc(string id)
        {
            using (CRDatabase context = new CRDatabase())
            {
                var list = new List<string>();
                (context.HR_USERGENE.AsNoTracking().Join(context.GN_GENEALLELE, allele => allele.ALLELE1ID, a1 => a1.ID,
                    (allele, a1) => new {allele, a1})
                    .Where(p => p.allele.USERID == id)
                    .Select(p => new
                    {
                        p.a1.DESCRIPTION
                    })).Distinct()
                    .Where(k => !string.IsNullOrEmpty(k.DESCRIPTION))
                    .ToList()
                    .ForEach(p => list.Add(p.DESCRIPTION));

                (context.HR_USERGENE.AsNoTracking().Join(context.GN_GENEALLELE, allele => allele.ALLELE2ID, a1 => a1.ID,
                    (allele, a1) => new {allele, a1})
                    .Where(p => p.allele.USERID == id)
                    .Select(p => new
                    {
                        p.a1.DESCRIPTION
                    })).Distinct()
                    .Where(k => !string.IsNullOrEmpty(k.DESCRIPTION))
                    .ToList()
                    .ForEach(p => list.Add(p.DESCRIPTION));
                return list.Distinct().ToList();
            }
        }




        private GN_GENEALLELE ModelToEntity(GeneAllele model)
        {
            return new GN_GENEALLELE() 
            {
                ID=model.ID,
                GENEALLELENAME=model.GeneAlleleName,
                GENEID=model.GeneID,
                SYNONYMNAME=model.SynonymName,
                STANDARDNAME=model.StandardName,
                DESCRIPTION=model.Description,
                PROTEIN=model.Protein,
                ENZYMATICACTIVITY=model.EnzymaticActivity,

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

        private GeneAllele EntityToModel(GN_GENEALLELE entity)
        {
            return new GeneAllele()
            {
                ID = entity.ID,
                GeneAlleleName = entity.GENEALLELENAME,
                GeneID = entity.GENEID,
                SynonymName = entity.SYNONYMNAME,
                StandardName = entity.STANDARDNAME,
                Description=entity.DESCRIPTION,
                Protein=entity.PROTEIN,
                EnzymaticActivity=entity.ENZYMATICACTIVITY,

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
