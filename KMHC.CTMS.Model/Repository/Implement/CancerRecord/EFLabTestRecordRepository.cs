/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2015/11/9      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class EFLabTestRecordRepository : ILabTestRecordRepository
    {
        private readonly IBaseRepository<HR_LABTESTRECORD> repository;

        public EFLabTestRecordRepository(){
            repository = new BaseRepository<HR_LABTESTRECORD>(new CRDatabase());
        }

        public int Add(LabTestRecord record)
        {
            var maxId = repository.GetMaxId("HR_LABTESTRECORD", "RECORDID");
            var entity = ModelToEntity(record);
            entity.RECORDID = maxId;
            entity.CREATEDATE = DateTime.Now;
            repository.Insert(entity);
            return entity.RECORDID;
        }

        public bool Edit(LabTestRecord record)
        {
            var entity = ModelToEntity(record);
            return entity != null && repository.Update(entity);
        }

        public LabTestRecord Get(int recordId)
        {
            return EntityToModel(repository.FindOne(p => p.RECORDID == recordId));
        }

        public LabTestRecord Get(string recordNo)
        {
            return EntityToModel(repository.FindOne(p => p.RECORDNO == recordNo));
        }

        private HR_LABTESTRECORD ModelToEntity(LabTestRecord model)
        {
            if (model != null)
            {
                var entity = new HR_LABTESTRECORD()
                {
                    RECORDID=model.RecordId,
                    RECORDDATE=model.RecordDate,
                    HOSPITAL=model.Hospital,
                    LABDOCTOR=model.LabDoctor,
                    ITEMID=model.ItemId,
                    REPORT=model.Report,
                    //ATTACHMENT=model.Attachment,
                    ITEMNAME=model.Itemname,
                    ITEMNO=model.ItemNo,
                    RECORDNO=model.RecordNo,
                    CREATEDATE=model.CreateDate,
                    CREATEBY=model.CreateBy,
                    UPDATEDATE=model.UpdateDate,
                    UPDATEBY=model.UpdateBy,
                    STATUS=model.Status
                };

                return entity;
            }
            return null;
        }

        private LabTestRecord EntityToModel(HR_LABTESTRECORD entity)
        {
            if (entity != null)
            {
                var model = new LabTestRecord()
                {
                    RecordId = entity.RECORDID,
                    RecordDate = entity.RECORDDATE,
                    Hospital = entity.HOSPITAL,
                    LabDoctor = entity.LABDOCTOR,
                    ItemId = entity.ITEMID,
                    Report = entity.REPORT,
                    //Attachment = entity.ATTACHMENT,
                    Itemname = entity.ITEMNAME,
                    ItemNo = entity.ITEMNO,
                    RecordNo = entity.RECORDNO,
                    CreateDate = entity.CREATEDATE,
                    CreateBy = entity.CREATEBY,
                    UpdateDate = entity.UPDATEDATE,
                    UpdateBy = entity.UPDATEBY,
                    Status = entity.STATUS
                };

                return model;
            }
            return null;
        }
    }
}
