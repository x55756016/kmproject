using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IDiseaseHistoryRepository
    {

        IEnumerable<DiseaseHistory> GetList(PageInfo page, string userId, string name);


        DiseaseHistory Get(string disId);

        bool Add(DiseaseHistory model);

        bool Update(DiseaseHistory model);

        bool Delete(string id);



        #region TreatmentHistory操作


        IEnumerable<TreatmentHistory> GetTreatmentHistoryList(string disId);


        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="disease"></param>
        /// <param name="treatment"></param>
        /// <returns></returns>
        int AddTreatmentHistory(TreatmentHistory treatment);


        void DelTreatmentHistory(string treatmentId);

        #endregion







    }
}