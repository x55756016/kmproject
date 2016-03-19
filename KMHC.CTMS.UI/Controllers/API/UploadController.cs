using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.BLL;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/Upload")]
    public class UploadController : ApiController
    {
        private UserInfoService _user;

        public UploadController()
        {
            _user = new UserInfoService();
        }

        /// <summary>
        /// Post上传处理
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Post(bool? identFileName = null)
        {
            //接受文件
            if (HttpContext.Current.Request.Files.Count <= 0)
                return BadRequest("异常");
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string modeCode = HttpContext.Current.Request.Form["ModelCode"];
            string modeName = HttpContext.Current.Request.Form["ModeName"];
            string formID = HttpContext.Current.Request.Form["FormID"];

            //文件保存路径
            string filePath = HttpContext.Current.Server.MapPath("~/Upload/");

            //文件格式判断
            string[] exts = new string[] { ".xls", ".xlsx", ".doc", ".docx", ".jpg", ".gif", ".png", ".jpeg",".ppt",".pptx",".pdf",".bmp" };

            //申明返回
            List<FileUpload> response = new List<FileUpload>();
         
            try
            {
                UserInfo userInfo = _user.GetCurrentUser();
                if (userInfo == null)
                    return BadRequest("用户未登录");

                IFileUploadRepository _repository = new EFFileUploadRepository();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string fileName = file.FileName;
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    if (!exts.Contains(fileExt.ToLower()))
                    {
                        return BadRequest("非法文件！");
                    }
                    /*if (fileExt == ".xls" || fileExt == ".xlsx")
                    {
                        filePath = HttpContext.Current.Server.MapPath("~/Upload/XLS/" + formID + "/");
                    }*/
                    if (string.IsNullOrEmpty(modeCode))
                    {
                        return BadRequest("异常请求");
                    }
                    if (string.IsNullOrEmpty(formID))
                    {
                        formID = userInfo.UserId;
                    }
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string newName = System.Guid.NewGuid().ToString("N") + fileExt;
                        
                    string strPath = filePath + newName;
                    file.SaveAs(strPath);

                    if (identFileName == true)
                    {
                        FileUpload model = _repository.Get(modeCode, formID).FirstOrDefault();
                        if (model == null)
                        {
                            //新增记录
                            model = new FileUpload();
                            model.FormId = formID;
                            model.FileName = fileName;
                            model.FilePath = newName;
                            model.ModelCode = modeCode;
                            model.ModelName = modeName;
                            model.CreatTime = DateTime.Now;
                            model.CreatBy = userInfo.UserId;
                            model.ModifyBy = userInfo.UserId;
                            model.ModifyTime = DateTime.Now;
                            string id = _repository.Add(model);

                            if (string.IsNullOrEmpty(id))
                            {
                                response.Add(null);
                            }
                            else
                            {
                                model.FileUploadid = id;
                                response.Add(model);
                            }
                        }
                        else
                        {
                            //记录存在，则更新记录
                            model.FileName = fileName;
                            model.FilePath = newName;
                            model.ModifyTime = DateTime.Now;
                            model.ModifyBy = userInfo.UserId;
                            bool result = _repository.Edit(model);
                            if (!result)
                            {
                                response.Add(null);
                            }
                            else
                            {
                                response.Add(model);
                            }
                        }
                    }
                    else
                    {
                        //新增记录
                        FileUpload model = new FileUpload();
                        model.FormId = formID;
                        model.FileName = fileName;
                        model.FilePath = newName;
                        model.ModelCode = modeCode;
                        model.ModelName = modeName;
                        model.CreatTime = DateTime.Now;
                        model.CreatBy = userInfo.UserId;
                        model.ModifyBy = userInfo.UserId;
                        model.ModifyTime = DateTime.Now;
                        string id = _repository.Add(model);

                        if (string.IsNullOrEmpty(id))
                        {
                            response.Add(null);
                        }
                        else
                        {
                            model.FileUploadid = id;
                            response.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest("异常"+ex.ToString());
            }

            //返回
            return Ok(response);
        }

        public IHttpActionResult Get(string ModelCode, string FormID)
        {
            IFileUploadRepository _repository = new EFFileUploadRepository();
            IEnumerable<FileUpload> _list = new List<FileUpload>();

            _list = _repository.Get(ModelCode, FormID);

            return Ok(_list);
        }

        public IHttpActionResult Delete(string FileID)
        {
            IFileUploadRepository _repository = new EFFileUploadRepository();

            _repository.Delete(FileID);
            return Ok("ok");
        }
    }
}
