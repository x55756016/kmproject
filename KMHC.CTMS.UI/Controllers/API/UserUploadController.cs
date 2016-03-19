using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/userUpload")]
    public class UserUploadController : ApiController
    {
        FileUploadBLL bll = new FileUploadBLL();

        public IHttpActionResult Get(int CurrentPage, string Date = "")
        {
            //申明参数
            int _pageSize = 10;
            int count = 1;

            try
            {
                UserInfo currentUser = new UserInfoService().GetCurrentUser();
                string userId = currentUser == null ? "" : currentUser.UserId;

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "CreatTime",
                    Order = OrderEnum.desc
                };
                Expression<Func<HR_FILEUPLOAD, bool>> predicate = p => true;
                if (string.IsNullOrEmpty(Date))
                {
                    predicate = p => p.CREATBY.Equals(userId);
                }
                else
                {
                    DateTime dt_start = DateTime.Parse(Date);
                    DateTime dt_end = dt_start.AddHours(24);
                    predicate = p => p.CREATBY.Equals(userId) && p.CREATTIME >= dt_start && p.CREATTIME <= dt_end;
                }

                var list = bll.GetList(pageInfo, predicate);
                count = pageInfo.Total / _pageSize;
                if (pageInfo.Total % _pageSize > 0)
                {
                    count += 1;
                }

                Response<IEnumerable<FileUpload>> response = new Response<IEnumerable<FileUpload>>
                {
                    Data = list,
                    PagesCount =count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Post([FromBody]Request<IList<FileUpload>> request)
        {
            try
            {
                IList<FileUpload> list = request.Data;
                foreach (FileUpload item in list)
                {
                    item.FormId = "";
                    bll.Edit(item);
                }

                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }
    }
}
