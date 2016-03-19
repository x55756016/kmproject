using System;
using System.Collections.Generic;
using System.Linq;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository
{
    public class EFAreaRepository : IAreaRepository
    {
        private readonly IBaseRepository<HR_AREA> repository;


        public EFAreaRepository()
        {
            repository = new BaseRepository<HR_AREA>(new CRDatabase());
        }
        

        public IEnumerable<Area> GetAreas(int pid)
        {
            return repository.FindAll().ToList().Select(EntityToModel);
        }

        public IEnumerable<Area> GetAllAreas()
        {
            return repository.FindAll().ToList().Select(EntityToModel);
        }

        #region 实体模型映射


        private Area EntityToModel(HR_AREA model)
        {
            if (model != null)
            {
                var entity = new Area()
                {
                    //AreaId = model.AREAID,
                    //AreaName = model.AREANAME,
                    //ParentId = model.PARENTID
                };
                return entity;
            }
            return null;
        }


        #endregion
    }
}
