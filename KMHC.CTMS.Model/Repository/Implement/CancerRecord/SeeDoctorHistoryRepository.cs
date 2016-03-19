using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class SeeDoctorHistoryRepository : BaseRepository<HR_SEEDOCTORHISTORY>
    {
        public SeeDoctorHistoryRepository(): base(new CRDatabase())
        {
        }

        public List<SeeDoctorHistory> GetList(Expression<Func<HR_SEEDOCTORHISTORY, bool>> predicate = null)
        {
            IEnumerable<HR_SEEDOCTORHISTORY> list = null;
            list= base.FindAll(predicate);

            List<SeeDoctorHistory> modelList = new List<SeeDoctorHistory>();
            if (list != null && list.Count() > 0)
            {
                foreach (HR_SEEDOCTORHISTORY entity in list)
                {
                    modelList.Add(EntityToModel(entity));
                }
            }

            return modelList;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private SeeDoctorHistory EntityToModel(HR_SEEDOCTORHISTORY entity)
        {
            if (entity != null)
            {
                SeeDoctorHistory model = new SeeDoctorHistory()
                {
                    HISTORYID=entity.HISTORYID,
                    PERSONID=entity.PERSONID,
                    DIAGNOSISTIME=entity.DIAGNOSISTIME,
                    DIAGNOSIS=entity.DIAGNOSIS,
                    ICD10=entity.ICD10,
                    HOSPITAL=entity.HOSPITAL,
                    DIAGNOSISREPORT=entity.DIAGNOSISREPORT,
                    MEDICALHISATTACHMENT=entity.MEDICALHISATTACHMENT,
                    //REMARK=entity.REMARK
                };

                return model;
            }

            return null;
        }
    }
}
