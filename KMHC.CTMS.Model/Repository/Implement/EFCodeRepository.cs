using System;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System.Collections.Generic;
using System.Linq;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFCodeRepository:ICodeRepository
    {
        private readonly IBaseRepository<HR_REF_CODEFILE> repository;

        public EFCodeRepository()
        {
            repository = new BaseRepository<HR_REF_CODEFILE>(new CRDatabase());
        }
        public Code Get(int codeId)
        {
            return EntityToModel(repository.FindOne(o => o.CODEID == codeId));
        }

        public Code Get(string itemno)
        {
            return EntityToModel(repository.FindOne(o => o.CODENO.Contains(itemno)));
        }


        public IEnumerable<Code> GetList(string[] itemnos)
        {
            var list = repository.FindAll(o => itemnos.Contains(o.CODENO));
            var codes = list.Select(EntityToModel);
            return codes;
        }

        #region 实体模型映射


        private HR_REF_CODEFILE ModelToEntity(Code model)
        {
            if (model != null)
            {
                var entity = new HR_REF_CODEFILE()
                {
                    CODEID = model.CodeId,
                    CODENAME = model.CodeName,
                    CODENO = model.CodeNo,
                    CODEVALUE = model.CodeValue,
                    DESCRIPTION = "",
                    
                };
                return entity;
            }
            return null;

        }
        private Code EntityToModel(HR_REF_CODEFILE model)
        {
            if (model != null)
            {
                var entity = new Code()
                {
                    CodeId = model.CODEID,
                    CodeName = model.CODENAME,
                    CodeNo = model.CODENO,
                    CodeValue = model.CODEVALUE
                };
                return entity;
            }
            return null;
        }

        #endregion
    }
}
