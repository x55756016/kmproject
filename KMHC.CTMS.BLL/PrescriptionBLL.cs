/*
 * 描述:精准用药的功能实现
 *  
 * 修订历史:    
 * 日期                修改人              Email                  内容
 * 20151121   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.BLL
{
    public class PrescriptionBLL
    {
        private CRDatabase context = new CRDatabase();

        /// <summary>
        /// 获取用药历史
        /// </summary>
        /// <param name="disId"></param>
        /// <returns></returns>
        public PrescritionModel GetDrugUseList(string disId,int id=0)
        {
            var list = new List<DrugUse>();
            context.CR_DRUGUSE.Where(u => u.DISEASEID == disId &&(id==0||u.ID==id)).ToList().ForEach(p=>list.Add(EntityToModel(p)));
            PrescritionModel pm = new PrescritionModel();
            pm.OldDrug = list.Where(u => u.ISNEWDRUG == "0").ToList();
            pm.NewDrug = list.Where(u => u.ISNEWDRUG == "1").ToList();
            pm.InterEffect = new List<DrugEffect2>();
            pm.GeneEffect = new List<GeneEffect>();

            (from a in context.CR_DRUGEFFECT
                join b in context.DUG_CNDRUG on a.DRUGID equals b.ID
                join c in context.DUG_DRUG on a.INTERDRUGID equals c.DRUGBANKID
                where a.DISEASEID == disId
                select new
                {
                    a.ID,
                    a.DISEASEID,
                    a.DRUGID,
                    b.NAME,
                    a.INTERDRUGID,
                    INTERNAME = c.NAME,
                    a.DRUGINTERACTION
                }).ToList().ForEach(p => pm.InterEffect.Add(new DrugEffect2()
                {
                    DISEASEID = p.DISEASEID,
                    DRUGID = p.DRUGID,
                    NAME = p.NAME,
                    DRUGINTERACTION = p.DRUGINTERACTION,
                    ID = p.ID,
                    INTERDRUGID = p.INTERDRUGID,
                    INTERNAME = p.INTERNAME
                }));

            (context.CR_GENEEFFECT.Join(context.DUG_CNDRUG, a => a.DRUGID, b => b.ID, (a, b) => new {a, b})
                .Where(@t => @t.a.DISEASEID == disId)
                .Select(@t => new {@t.a.DISEASEID, @t.a.DRUGID, @t.a.ID, @t.a.GENEDESC, @t.a.GENENAME,@t.b.NAME})).ToList()
                .ForEach(p => pm.GeneEffect.Add(new GeneEffect()
                {
                    ID = p.ID,
                    DISEASEID = p.DISEASEID,
                    DRUGID = p.DRUGID,
                    DRUGNAME = p.NAME,
                    GENENAME = p.GENENAME,
                    GENEDESC = p.GENEDESC
                }));

            return pm;
        }


        /// <summary>
        /// 添加用药情况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDrugUse(DrugUse model)
        {
            context.CR_DRUGUSE.Add(ModelToEntity(model));

            if (model.InterEffect.Any())
            {
                foreach (var drugEffect2 in model.InterEffect)
                {
                    var item = ModelToEntity(drugEffect2);
                    item.DISEASEID = model.DISEASEID;
                    item.DRUGID = model.DRUGID;
                    context.CR_DRUGEFFECT.Add(item);
                }
            }
            if (model.GeneEffect.Any())
            {
                foreach (var drugGeneInfo in model.GeneEffect)
                {
                    var item = ModelToEntity(drugGeneInfo);
                    item.DISEASEID = model.DISEASEID;
                    item.DRUGID = model.DRUGID;
                    context.CR_GENEEFFECT.Add(item);
                }
            }

            return context.SaveChanges()>0;
        }



        public bool UpdateDrugUse(DrugUse model)
        {
            var entity = context.CR_DRUGUSE.FirstOrDefault(u => u.ID == model.ID);

            if (entity!=null)
            {
                entity.TREATMENTDISEASES = model.TREATMENTDISEASES;
                entity.USEDAY = model.USEDAY;
                entity.DOSE = model.DOSE;
                entity.UNITS = model.UNITS;
                entity.STARTTIME = model.STARTTIME;
                entity.ENDTIME = model.ENDTIME;
                entity.TIMESADAY = model.TIMESADAY;
            }
            return context.SaveChanges() > 0;
        }





        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteDrugUse(int id)
        {
            var entity = context.CR_DRUGUSE.Find(id);
            if (entity != null)
            {
                context.CR_DRUGUSE.Remove(entity);

                //1.首先删除自己对其他药物的影响  
                context.CR_DRUGEFFECT.Where(u => u.DRUGID == entity.DRUGID && u.DISEASEID == entity.DISEASEID).ToList()
                    .ForEach(p => context.CR_DRUGEFFECT.Remove(p));

                //2.删除其他药物对自己的影响

                var firstOrDefault = context.DUG_CNDRUG.FirstOrDefault(u => u.ID == entity.DRUGID);
                if (firstOrDefault != null)
                {
                    var drugBankId = firstOrDefault.DRUGBANKID;

                    context.CR_DRUGEFFECT.Where(u => u.INTERDRUGID == drugBankId && u.DISEASEID == entity.DISEASEID)
                        .ToList()
                        .ForEach(p => context.CR_DRUGEFFECT.Remove(p));
                }

                context.CR_GENEEFFECT.Where(u => u.DRUGID == entity.DRUGID && u.DISEASEID == entity.DISEASEID).ToList()
                    .ForEach(p => context.CR_GENEEFFECT.Remove(p));
            }
            return context.SaveChanges() > 0;
        }


        /// <summary>
        /// 查看药品是否有冲突
        /// </summary>
        /// <param name="diseaseId"></param>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public AddDrugEffect CheckDrugUse(string diseaseId, string drugGuid, string userId)
        {

            AddDrugEffect adeModel = new AddDrugEffect();
            List<DrugEffect2> interList = new List<DrugEffect2>();
            var drugId = context.DUG_CNDRUG.Where(u => u.ID == drugGuid).FirstOrDefault().DRUGBANKID;

            //1.检查药与药的作用
            List<string> userList = new List<string>();

           (context.CR_DRUGUSE.Join(context.DUG_CNDRUG, a => a.DRUGID, b => b.ID, (a, b) => new {a, b})
               .Where(@t => @t.a.DISEASEID == diseaseId)
               .Select(@t => new
               {
                   @t.b.DRUGBANKID
               })
               ).ToList().ForEach(p=> userList.Add(p.DRUGBANKID));

            //正向关联
            context.DUG_DRUGINTERACTIONS
                .Where(u => (u.DRUGBANKID == drugId && userList.Contains(u.LINKDRUGBANKID)))
                .Distinct().ToList()
                .ForEach(u => interList.Add(
                    new DrugEffect2()
                    {
                        INTERDRUGID = u.LINKDRUGBANKID,
                        NAME = u.NAME,
                        DRUGINTERACTION = u.DESCRIPTION
                    }));


            //反向关联
            //(context.DUG_DRUGINTERACTIONS.Join(context.DUG_DRUG, a => a.DrugBankId, b => b.DrugBankId,
            //    (a, b) => new {a, b}).Where(@t => @t.a.LinkDrugBankId == drugId && userList.Contains(@t.a.DrugBankId)).Select(@t => new
            //    {
            //        DrugName = @t.b.Name,
            //        Description = @t.a.Description
            //    }))
            //    .ToList()
            //    .ForEach(p => interList.Add(
            //        new DrugInteractionInfo()
            //        {
            //            DrugName = p.DrugName,
            //            Description = p.Description
            //        }));


            //2.检查药与基因的作用
            //取得用户的基因类型
            var userGeneList = new List<string>();
            List<GeneEffect> geneList = new List<GeneEffect>();

            context.HR_USERGENE.Join(context.GN_GENE, a => a.GENEID, p => p.ID, (a, p) => new {a, p})
                .Where(@t => @t.a.USERID == userId)
                .Select(@t => new {@t.p.GENENAME}).ToList().ForEach(p => userGeneList.Add(p.GENENAME.ToString()));


            (from a in context.DUG_ENZYMEPO
                join p in context.DUG_ENZYMES on a.ITEMID equals p.ID
                join c in context.DUG_DRUG on p.DRUGBANKID equals c.DRUGBANKID
                where p.DRUGBANKID == drugId && userGeneList.Contains(a.GENENAME)
                select new {p.ID, DrugName = c.NAME, GeneName = a.GENENAME}).ToList()
                .ForEach(u => geneList.Add(new GeneEffect()
                {
                    ID = u.ID,
                    GENENAME = u.GeneName,
                    GENEDESC = ""
                }));

            geneList.ForEach(u => context.DUG_ENZYMESACTIONS.Where(p => p.ITEMID == u.ID).ToList().ForEach(k => u.GENEDESC=u.GENEDESC+","+k.ACTIONS));
            adeModel.InterEffect = interList;
            adeModel.GeneEffect = geneList;
            return adeModel;
        }

        #region 模型映射

        private CR_DRUGUSE ModelToEntity(DrugUse model)
        {
            if (model != null)
            {
                var entity = new CR_DRUGUSE()
                {
                    ID = context.Database.SqlQuery<int>("select CR_DRUGUSE_ID.nextval from dual").FirstOrDefault(),
                    DISEASEID = model.DISEASEID,
                    USERID = model.USERID,
                    DRUGTYPE = model.DRUGTYPE,
                    DRUGNAME = model.DRUGNAME,
                    DRUGID = model.DRUGID,
                    TREATMENTDISEASES = model.TREATMENTDISEASES,
                    STARTTIME = model.STARTTIME,
                    ENDTIME = model.ENDTIME,

                    TIMESADAY = model.TIMESADAY,
                    DOSE = model.DOSE,
                    UNITS = model.UNITS,
                    USEDAY=model.USEDAY,
                    ISNEWDRUG = model.ISNEWDRUG,
                    CREATEUSERID = model.CREATEUSERID,
                    CREATEDATETIME = model.CREATEDATETIME,
                    EDITUSERID = model.EDITUSERID,
                    EDITDATETIME = model.EDITDATETIME,
                    ISDELETED = model.ISDELETED,
                    OWNERID = model.OWNERID
                };
                return entity;
            }
            return null;
        }

        private DrugUse EntityToModel(CR_DRUGUSE entity)
        {
            if (entity != null)
            {
                var model = new DrugUse()
                {
                    ID = entity.ID,
                    DISEASEID = entity.DISEASEID,
                    USERID = entity.USERID,
                    DRUGTYPE = entity.DRUGTYPE,
                    DRUGNAME = entity.DRUGNAME,
                    DRUGID = entity.DRUGID,
                    TREATMENTDISEASES = entity.TREATMENTDISEASES,
                    STARTTIME = entity.STARTTIME,
                    ENDTIME = entity.ENDTIME,
                    TIMESADAY = entity.TIMESADAY,
                    DOSE = entity.DOSE,
                    UNITS = entity.UNITS,
                    USEDAY = entity.USEDAY,
                    ISNEWDRUG = entity.ISNEWDRUG,
                    CREATEUSERID = entity.CREATEUSERID,
                    CREATEDATETIME = entity.CREATEDATETIME,
                    EDITUSERID = entity.EDITUSERID,
                    EDITDATETIME = entity.EDITDATETIME,
                    ISDELETED = entity.ISDELETED,
                    OWNERID = entity.OWNERID
                };
                return model;
            }
            return null;
        }

        private CR_DRUGEFFECT ModelToEntity(DrugEffect2 model)
        {
            if (model!=null)
            {
                var entity = new CR_DRUGEFFECT()
                {
                    ID = context.Database.SqlQuery<int>("select CR_DRUGEFFECT_ID.nextval from dual").FirstOrDefault(),
                    DISEASEID = model.DISEASEID,
                    DRUGID = model.DRUGID,
                    INTERDRUGID = model.INTERDRUGID,
                    DRUGINTERACTION = model.DRUGINTERACTION
                };
                return entity;
            }
            return null;
        }
        private DrugEffect2 EntityToModel(CR_DRUGEFFECT entity)
        {
            if (entity != null)
            {
                var model = new DrugEffect2()
                {
                    ID = entity.ID,
                    DISEASEID = entity.DISEASEID,
                    DRUGID = entity.DRUGID,
                    INTERDRUGID = entity.INTERDRUGID,
                    DRUGINTERACTION = entity.DRUGINTERACTION
                };
                return model;
            }
            return null;
        }

        private CR_GENEEFFECT ModelToEntity(GeneEffect model)
        {
            if (model!=null)
            {
                var entity = new CR_GENEEFFECT()
                {
                    ID = context.Database.SqlQuery<int>("select CR_GENEEFFECT_ID.nextval from dual").FirstOrDefault(),
                     DISEASEID = model.DISEASEID,
                     DRUGID = model.DRUGID,
                     GENENAME = model.GENENAME,
                     GENEDESC = model.GENEDESC
                };
                return entity;
            }
            return null;
        }

        private GeneEffect EntityToModel(CR_GENEEFFECT entity)
        {
            if (entity!=null)
            {

                var model = new GeneEffect()
                {
                   ID=entity.ID,
                   DISEASEID = entity.DISEASEID,
                   DRUGID = entity.DRUGID,
                   GENENAME = entity.GENENAME,
                   GENEDESC = entity.GENEDESC
                };
                return model;
            }
            return null;
        }
        #endregion
    }
}
