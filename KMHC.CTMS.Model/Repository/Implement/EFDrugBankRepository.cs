/*
 * 描述:定义IDrugBankRepository的接口实现
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20151117   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFDrugBankRepository : IDrugBankRepository
    {
        private readonly IBaseRepository<DUG_DRUG> _repository;


        public EFDrugBankRepository()
        {
            _repository = new BaseRepository<DUG_DRUG>(new CRDatabase());
        }

        public DrugBank GetDrugInfo(string dbId, string drugName)
        {
            var drugModel = EntityToModel(_repository.FindOne(u => u.DRUGBANKID == dbId || u.NAME.Contains(drugName)));
            //1.加载转运酶信息
            var transRepository = new BaseRepository<DUG_TRANSPORTERS>(new CRDatabase());
            var transActionsRepository = new BaseRepository<DUG_TRANSPORTERACTIONS>(new CRDatabase());
            var transPoRepository = new BaseRepository<DUG_TRANSPORTERSPO>(new CRDatabase());

            transRepository.FindAll(u => u.DRUGBANKID == dbId).ToList().ForEach(p => drugModel.Transporters.Add(EntityToModel(p)));

            foreach (var transModel in drugModel.Transporters)
            {
                transActionsRepository.FindAll(u => u.ITEMID == transModel.Id)
                    .ToList()
                    .ForEach(p => transModel.TransporterActions.Add(EntityToModel(p)));
                transPoRepository.FindAll(u => u.ITEMID == transModel.Id)
                    .ToList()
                    .ForEach(p => transModel.TransportersPo.Add(EntityToModel(p)));
            }
            return drugModel;
        }


        #region 实体模型映射

                public DrugBank EntityToModel(DUG_DRUG entity)
                {
                    if (entity != null)
                    {
                        var model = new DrugBank()
                        {
                            Id = entity.ID,
                            DrugBankId = entity.DRUGBANKID,
                            Name = entity.NAME,
                            Description = entity.DESCRIPTION,
                            CreateTime = entity.CREATETIME
                            //public DateTime? UpdateTime { get; set; }
                            //public string DirectParent { get; set; }
                            //public string SuperClass { get; set; }
                            //public string Indication { get; set; }
                            //public string Pharmacodynamics { get; set; }
                            //public string MechanismOfAction { get; set; }
                            //public string Toxicity { get; set; }
                            //public string Metabolism { get; set; }
                            //public string Absorption { get; set; }
                            //public string ProteinBinding { get; set; }
                        };
                        return model;
                    }
                    return null;
                }



                public Transporters EntityToModel(DUG_TRANSPORTERS entity)
                {
                    if (entity != null)
                    {
                        var model = new Transporters()
                        {
                            Id = entity.ID,
                            DrugBankId = entity.DRUGBANKID,
                            TransportersId = entity.TRANSPORTERSID,
                            Name = entity.NAME,
                            Organism = entity.ORGANISM
                        };
                        return model;
                    }
                    return null;
                }


                public TransporterActions EntityToModel(DUG_TRANSPORTERACTIONS entity)
                {
                    if (entity != null)
                    {
                        var model = new TransporterActions()
                        {
                            Id = entity.ID,
                            ItemId = entity.ITEMID,
                            TransportersId = entity.TRANSPORTERSID,
                            Action = entity.ACTION
                        };
                        return model;
                    }
                    return null;
                }

                public TransportersPo EntityToModel(DUG_TRANSPORTERSPO entity)
                {
                    if (entity != null)
                    {
                        var model = new TransportersPo()
                        {
                            Id = entity.ID,
                            ItemId = entity.ITEMID,
                            TransportersId = entity.TRANSPORTERSID,
                            PolypeptideId = entity.POLYPEPTIDEID,
                            GeneName = entity.GENENAME,
                            AminoAcidSequence = entity.AMINOACIDSEQUENCE,
                            GeneSequence = entity.GENESEQUENCE
                        };
                        return model;
                    }
                    return null;
                }


        #endregion
    }
}
