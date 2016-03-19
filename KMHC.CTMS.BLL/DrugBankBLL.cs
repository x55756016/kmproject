/*
 * 描述:创建DrugBank的BLL层
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 *    		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository;

namespace KMHC.CTMS.BLL
{
   public class DrugBankBLL
   {
       private CRDatabase context = new CRDatabase();



       /// <summary>
       /// 药物相互作用查询
       /// </summary>
       /// <param name="dbId"></param>
       /// <param name="drugName"></param>
       /// <returns></returns>
       public List<DrugBank> GetDrugInteration(string dbId)
       {
           List<DrugBank> drugList = new List<DrugBank>();


           var drugbankId = context.DUG_CNDRUG.Where(p => p.ID == dbId).FirstOrDefault().DRUGBANKID;

           context.DUG_DRUG.Where(u => u.DRUGBANKID == drugbankId).ToList().ForEach(p => drugList.Add(EntityToModel(p)));

           if (drugList.Count==1)
           {
               var drugModel = drugList[0];
               context.DUG_DRUGINTERACTIONS.Join(context.DUG_DRUG, c => c.LINKDRUGBANKID, drug => drug.DRUGBANKID,
                   (c, drug) => new
                   {
                       DrugBankid=c.DRUGBANKID,
                       LinkDrugBankId = c.LINKDRUGBANKID,
                       DrugName  = c.NAME,
                       CreateTime = drug.CREATETIME,
                       Description = drug.DESCRIPTION,
                       Toxicity = drug.TOXICITY,
                       DrugInter = c.DESCRIPTION
                   })
                   .Where(p => p.DrugBankid == drugModel.DrugBankId)
                   .ToList().ForEach(u => drugModel.DrugInteractionInfos.Add(new DrugInteractionInfo()
                   {
                       DrugBankId = u.LinkDrugBankId,
                       DrugName = u.DrugName,
                       CreateTime = u.CreateTime,
                       Description = u.Description,
                       Toxicity = u.Toxicity,
                       DrugInter = u.DrugInter
                   }));
           }
           return drugList;
       }


       /// <summary>
       /// 获取药典基因信息
       /// </summary>
       /// <param name="dbId"></param>
       /// <param name="drugName"></param>
       /// <returns></returns>
       public IList<DrugBank> GetDrugGeneInfo(string dbId)
       {

           var drugbankId = context.DUG_CNDRUG.Where(p => p.ID == dbId).FirstOrDefault().DRUGBANKID;



           List<DrugBank> drugList = new List<DrugBank>();

           context.DUG_DRUG.Where(u => u.DRUGBANKID == drugbankId).ToList().ForEach(p => drugList.Add(EntityToModel(p)));

           if (drugList.Count == 1)
           {
               foreach (var drugModel in drugList)
               {
                   DrugBank dbModel = drugList[0];

                   //1.加载转运酶信息
                   context.DUG_TRANSPORTERS.Where(u => u.DRUGBANKID == dbModel.DrugBankId)
                       .ToList()
                       .ForEach(p => dbModel.Transporters.Add(new Transporters()
                       {
                           Id = p.ID,
                           DrugBankId = p.DRUGBANKID,
                           TransportersId = p.TRANSPORTERSID,
                           Name = p.NAME,
                           Organism = p.ORGANISM
                       }));

                   dbModel.Transporters.ToList().ForEach(p =>
                       context.DUG_TRANSPORTERACTIONS.Where(u => u.ITEMID == p.Id).ToList().ForEach(k =>
                           p.TransporterActions.Add(new TransporterActions()
                           {
                               Id = k.ID,
                               ItemId = k.ITEMID,
                               TransportersId = k.TRANSPORTERSID,
                               Action = k.ACTION
                           })
                           ));

                   dbModel.Transporters.ToList().ForEach(p =>
                       context.DUG_TRANSPORTERSPO.Where(u => u.ITEMID == p.Id).ToList().ForEach(k =>
                           p.TransportersPo.Add(new TransportersPo()
                           {
                               Id = k.ID,
                               ItemId = k.ITEMID,
                               TransportersId = k.TRANSPORTERSID,
                               PolypeptideId = k.POLYPEPTIDEID,
                               GeneName = k.GENENAME
                           })));


                   ////2.加载靶点信息
                   context.DUG_TARGETS.Where(u => u.DRUGBANKID == dbModel.DrugBankId)
                       .ToList()
                       .ForEach(p => dbModel.Targets.Add(new Targets()
                       {
                           Id = p.ID,
                           DrugBankId = p.DRUGBANKID,
                           TargetId = p.TARGETID,
                           Name = p.NAME,
                           Organism = p.ORGANISM
                       }));

                   dbModel.Targets.ToList().ForEach(p =>
                       context.DUG_TARGETACTIONS.Where(u => u.ITEMID == p.Id).ToList().ForEach(k =>
                           p.TargetActions.Add(new TargetActions()
                           {
                               Id = k.ID,
                               ItemId = k.ITEMID,
                               TargetId = k.TARGETID,
                               Action = k.ACTION
                           })
                           ));

                   dbModel.Targets.ToList().ForEach(p =>
                       context.DUG_TARGETSPO.Where(u => u.ITEMID == p.Id).ToList().ForEach(k =>
                           p.TargetsPo.Add(new TargetsPo()
                           {
                               Id = k.ID,
                               ItemId = k.ITEMID,
                               TargetId = k.TARGETID,
                               PolypeptideId = k.POLYPEPTIDEID,
                               GeneName = k.GENENAME
                           })));



                   ////3.加载代谢酶信息
                   context.DUG_ENZYMES.Where(u => u.DRUGBANKID == dbModel.DrugBankId)
                       .ToList()
                       .ForEach(p => dbModel.Enzymes.Add(new Enzymes()
                       {
                           Id = p.ID,
                           DrugBankId = p.DRUGBANKID,
                           EnzymesId = p.ENZYMESID,
                           Name = p.NAME,
                           Organism = p.ORGANISM
                       }));

                   dbModel.Enzymes.ToList().ForEach(p =>
                       context.DUG_ENZYMESACTIONS.Where(u => u.ITEMID == p.Id).ToList().ForEach(k =>
                           p.EnzymesActions.Add(new EnzymesActions()
                           {
                               Id = k.ID,
                               ItemId = k.ITEMID,
                               EnzymesId = k.ENZYMESID,
                               Actions = k.ACTIONS
                           })
                           ));

                   dbModel.Enzymes.ToList().ForEach(p =>
                       context.DUG_ENZYMEPO.Where(u => u.ITEMID == p.Id).ToList().ForEach(k =>
                           p.EnzymePo.Add(new EnzymePo()
                           {
                               Id = k.ID,
                               ItemId = k.ITEMID,
                               EnzymeId = k.ENZYMEID,
                               PolypeptideId = k.POLYPEPTIDEID,
                               GeneName = k.GENENAME
                           })));

               }
           }
           return drugList;
       }






       private DrugBank EntityToModel(DUG_DRUG entity)
       {
           if (entity != null)
           {
               var model = new DrugBank()
               {
                   Id = entity.ID,
                   DrugBankId = entity.DRUGBANKID,
                   Name = entity.NAME,
                   Description = entity.DESCRIPTION,
                   CreateTime = entity.CREATETIME,
                   //public DateTime? UpdateTime { get; set; }
                   //public string DirectParent { get; set; }
                   //public string SuperClass { get; set; }
                   //public string Indication { get; set; }
                   //public string Pharmacodynamics { get; set; }
                   //public string MechanismOfAction { get; set; }
                     Toxicity =entity.TOXICITY
                   //public string Metabolism { get; set; }
                   //public string Absorption { get; set; }
                   //public string ProteinBinding { get; set; }
               };
               return model;
           }
           return null;
       }





   }
}
