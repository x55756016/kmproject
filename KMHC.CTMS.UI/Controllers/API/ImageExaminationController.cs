/*
 * 描述:影像学检验报告处理
 *  
 * 修订历史: 
 * 日期        修改人              Email                   内容
 * 2015/11/6   邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ImageExamination")]
    public class ImageExaminationController : ApiController
    {
        private IImageExaminationRepository _repository = new EFImageExaminationRepository();

        public IHttpActionResult Get(string historyID)
        {
            //异常检测
            if (string.IsNullOrEmpty(historyID))
            {
                return BadRequest("请求异常！");
            }

            //获取数据
            try
            {
                ImageExResponse response = new ImageExResponse();
                ImageExamination imageEx = _repository.Get(historyID);
                response.ImageEx = imageEx;
                if (imageEx != null) {
                    response.Files = new EFFileUploadRepository().Get("ImageExamination", historyID);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ImageExamination> request)
        {
            //获取参数
            var model = request.Data;

            try
            {
                if (string.IsNullOrEmpty(model.ImageExamID))//新增插入
                {
                    _repository.Add(model);
                }
                else//修改更新 
                {
                    _repository.Edit(model);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        public IHttpActionResult Get(string ImageExamID, string HistoryID)
        {

            //新增插入
            if (!string.IsNullOrEmpty(ImageExamID) && !string.IsNullOrEmpty(HistoryID))
            {
                ImageExamination _model = _repository.Get(HistoryID);
                if (_model == null)
                {
                    _model = new ImageExamination();
                    _model.ImageExamID = ImageExamID;
                    _model.HistoryID = HistoryID;
                    _repository.Add(_model);
                }
            }

            return Ok("true");
        }
    }

    public class ImageExResponse
    {
        public ImageExamination ImageEx;
        public List<FileUpload> Files;
    }
}