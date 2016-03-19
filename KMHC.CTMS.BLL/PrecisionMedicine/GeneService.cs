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
    public class GeneService
    {
        public GeneService()
        {
            
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="gene"></param>
        /// <returns></returns>
        public string Add(Gene model)
        {
            using (EFGeneRepository repository = new EFGeneRepository())
            {
                if (model == null) return string.Empty;
                GN_GENE entity = ModelToEntity(model);
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
        /// 编辑基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public  bool Edit(Gene model)
        {
            using (EFGeneRepository repository = new EFGeneRepository())
            {
                if (model == null || string.IsNullOrEmpty(model.ID)) return false;
                GN_GENE entity = ModelToEntity(model);
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
        /// 删除基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            using (EFGeneRepository repository = new EFGeneRepository())
            {
                return repository.Delete(id);
            }
        }

        /// <summary>
        /// 根据id查询基因字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Gene Get(string id)
        {
            using (EFGeneRepository repository = new EFGeneRepository())
            {
                GN_GENE entity = repository.Get(id);
                if (entity == null) throw new KeyNotFoundException();
                Gene model = EntityToModel(entity);
                return model;
            }
        }

        public List<Gene> GetList(string geneName)
        {
            using (EFGeneRepository repository = new EFGeneRepository())
            {
                IEnumerable<GN_GENE> list = null;
                if (!string.IsNullOrEmpty(geneName))
                {
                    list = repository.FindAll(o => o.GENENAME.Contains(geneName) && !o.ISDELETED).ToList();
                }
                else
                {
                    list = repository.FindAll(o => !o.ISDELETED).ToList();
                }

                List<Gene> modelList = (from g in list select EntityToModel(g)).ToList();
                return modelList;
            }
        }

        public List<Gene> GetList(string geneName, ref PageInfo pager)
        {
            using (EFGeneRepository repository = new EFGeneRepository())
            {
                IEnumerable<GN_GENE> list = null;
                if (!string.IsNullOrEmpty(geneName))
                {
                    list = repository.FindAll(o => o.GENENAME.Contains(geneName) && !o.ISDELETED).Paging(ref pager).ToList();
                }
                else
                {
                    list = repository.FindAll(o => !o.ISDELETED).Paging(ref pager).ToList();
                }

                List<Gene> modelList = (from g in list select EntityToModel(g)).ToList();
                return modelList;
            }
        }

        private GN_GENE ModelToEntity(Gene model)
        {
            return new GN_GENE()
            {
                ID = model.ID,
                GENENAME = model.GeneName,
                REFSEQUENCE = model.RefSequence,
                SYNONYMNAME = model.SynonymName,              
                
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

        private Gene EntityToModel(GN_GENE entity)
        {
            return new Gene()
            {
                ID = entity.ID,
                GeneName = entity.GENENAME,
                 RefSequence = entity.REFSEQUENCE,
                SynonymName = entity.SYNONYMNAME,

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
