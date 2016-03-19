using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFDiseaseRepository:IDiseaseRepository
    {
        private readonly IBaseRepository<HR_DISEASE> repository;

        public EFDiseaseRepository()
        {
            repository = new BaseRepository<HR_DISEASE>(new CRDatabase());
        }
        public IEnumerable<DiseaseType> GetDiseaseTypes()
        {
            var repo = new BaseRepository<HR_DISEASECATEGORY>(new CRDatabase());
            return repo.FindAll().OrderBy(o=>o.CATEGORYID).Select(EntityToModel).ToList();
        }

        public IEnumerable<Disease> GetDiseases()
        {
            return repository.FindAll().Select(EntityToModel).ToList();
        }

        public IEnumerable<Disease> GetDiseaseList(PageInfo page, string[] types, string key)
        {
            var list = repository.FindAll();
            if (types != null)
            {
                list = list.Where(o => types.Contains(o.CATEGORYCODE));
            }
            if (!string.IsNullOrWhiteSpace(key))
            {
                list = list.Where(o => (o.DISEASECODE.Contains(key) || o.DISEASENAME.Contains(key) || o.PINYINCODE.Contains(key)));
            }
            return list.Paging(ref page).Select(EntityToModel).ToList();
        }

        private Disease EntityToModel(HR_DISEASE model)
        {
            if (model != null)
            {
                var entity = new Disease()
                {
                   Code = model.DISEASECODE,
                   TypeCode = model.CATEGORYCODE,
                   Name = model.DISEASENAME,
                };
                return entity;
            }
            return null;
        }

        private DiseaseType EntityToModel(HR_DISEASECATEGORY model)
        {
            if (model != null)
            {
                var entity = new DiseaseType()
                {
                    Id = model.CATEGORYID,
                    CategoryCode = model.CATEGORYCODE,
                    DiseaseName = model.CATEGORYNAME,
                    ParentId = (model.PARENTID!=null && model.PARENTID.HasValue)?model.PARENTID.Value:0,
                    EndCode = model.ENDCODE,
                    StartCode = model.STARTCODE
                };
                return entity;
            }
            return null;
        }
    }
}
