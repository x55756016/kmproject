/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/11/9  邓柏生                                      创建 
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
    public class EFFileUploadRepository : IFileUploadRepository
    {
        private readonly IBaseRepository<HR_FILEUPLOAD> repository;

        public EFFileUploadRepository()
        {
            repository = new BaseRepository<HR_FILEUPLOAD>(new CRDatabase());
        }

        public string Add(FileUpload fileUpload)
        {
            var entity = ModelToEntity(fileUpload);
            entity.FILEUPLOADID = System.Guid.NewGuid().ToString("N");
            repository.Insert(entity);
            return entity.FILEUPLOADID;
        }

        public bool Edit(FileUpload fileUpload)
        {
            var entity = ModelToEntity(fileUpload);
            return entity != null && repository.Update(entity);
        }

        public FileUpload Get(string fileUploadid)
        {
            return EntityToModel(repository.FindOne(p => p.FILEUPLOADID == fileUploadid));
        }

        public List<FileUpload> Get(string modeCode, string formId)
        {
            List<FileUpload> _list = new List<FileUpload>();
            var models = repository.FindAll(p => p.MODELCODE == modeCode && p.FORMID == formId);
            foreach (var model in models)
            {
                _list.Add(EntityToModel(model));
            }

            return _list;
        }

        public bool Delete(string fileUploadid)
        {
            return repository.DeleteById(fileUploadid);
        }

        private HR_FILEUPLOAD ModelToEntity(FileUpload model)
        {
            if (model != null)
            {
                var entity = new HR_FILEUPLOAD()
                {
                    FILEUPLOADID=model.FileUploadid,
                    MODELNAME=model.ModelName,
                    MODELCODE=model.ModelCode,
                    FORMID=model.FormId,
                    FILEPAH=model.FilePath,
                    FILENAME=model.FileName,
                    CREATTIME = model.CreatTime,
                    CREATBY = model.CreatBy,
                    MODIFYTIME = model.ModifyTime,
                    MODIFYBY = model.ModifyBy,
                    REMARK=model.Remark
                };

                return entity;
            }
            return null;
        }

        private FileUpload EntityToModel(HR_FILEUPLOAD entity)
        {
            if (entity != null)
            {
                var model = new FileUpload()
                {
                    FileUploadid = entity.FILEUPLOADID,
                    ModelName = entity.MODELNAME,
                    ModelCode = entity.MODELCODE,
                    FormId = entity.FORMID,
                    FilePath = entity.FILEPAH,
                    FileName = entity.FILENAME,
                    CreatTime = entity.CREATTIME,
                    CreatBy = entity.CREATBY,
                    ModifyTime = entity.MODIFYTIME,
                    ModifyBy = entity.MODIFYBY,
                    Remark = entity.REMARK
                };

                return model;
            }
            return null;
        }


       
    }
}
