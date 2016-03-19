/*
 * 描述:公共上传BLL
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/12/22 15:36:20     邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.CancerRecord;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.CancerRecord
{
    public class FileUploadBLL
    {
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public FileUpload Get(Expression<Func<HR_FILEUPLOAD, bool>> predicate = null)
        {
            using (FileUploadDAL dal = new FileUploadDAL())
            {
                return EntityToModel(dal.GetOne(predicate));
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<FileUpload> GetList(PageInfo page, Expression<Func<HR_FILEUPLOAD, bool>> predicate = null)
        {
            using (FileUploadDAL dal = new FileUploadDAL())
            {
                var list = dal.Get(predicate);

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<FileUpload> GetList(Expression<Func<HR_FILEUPLOAD, bool>> predicate = null)
        {
            using (FileUploadDAL dal = new FileUploadDAL())
            {
                var list = dal.Get(predicate);

                return list.Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(FileUpload model)
        {
            if (model == null) return false;
            using (FileUploadDAL dal = new FileUploadDAL())
            {
                HR_FILEUPLOAD entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private HR_FILEUPLOAD ModelToEntity(FileUpload model)
        {
            if (model != null)
            {
                var entity = new HR_FILEUPLOAD()
                {
                    FILEUPLOADID = model.FileUploadid,
                    MODELNAME = model.ModelName,
                    MODELCODE = model.ModelCode,
                    FORMID = model.FormId,
                    FILEPAH = model.FilePath,
                    FILENAME = model.FileName,
                    CREATTIME = model.CreatTime,
                    CREATBY = model.CreatBy,
                    MODIFYTIME = model.ModifyTime,
                    MODIFYBY = model.ModifyBy,
                    REMARK = model.Remark
                };

                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
