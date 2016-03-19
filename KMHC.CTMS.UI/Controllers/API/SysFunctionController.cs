using KMHC.CTMS.BLL;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class SysFunctionController : ApiController
    {
        public IHttpActionResult Get(string Name = "")
        {
            Expression<Func<CTMS_SYS_FUNCTION, bool>> predicate = p => p.ISDELETED==false;
            if (!string.IsNullOrEmpty(Name))
                predicate = p => p.ISDELETED == false && p.MENUNAME.Contains(Name);

            List<MenuInfo> list = new MenuInfoBLL().GetList(predicate);

            return Ok(list);
        }

        public IHttpActionResult Get(string ID, string Act)
        {
            MenuInfo model = new MenuInfoBLL().GetOne(ID);
            if(model!=null)
            {
                MenuInfo parent = new MenuInfoBLL().GetOne(model.ParentID);
                model.ParentID =(parent==null?"": string.Format("{0}#{1}", parent.ID, parent.Name));
            }

            return Ok(model);
        }

        public IHttpActionResult Post([FromBody]Request<MenuInfo> request)
        {
            try
            {
                MenuInfo model = request.Data;
                bool result = true;
                if (string.IsNullOrEmpty(model.ID))
                {
                    result = Insert(model);
                }
                else
                {
                    result = Update(model);
                }

                return Ok(result == true ? "ok" : "false");
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("SysFunctionController[Post]", ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        public IHttpActionResult Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return BadRequest("非法请求！");
            }

            try
            {
                MenuInfo model = new MenuInfoBLL().GetOne(ID);
                if (model == null)
                {
                    return Ok("ok");
                }

                model.IsDeleted = true;
                new MenuInfoBLL().Edit(model);

                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("SysFunctionController[Delete]", ex.ToString());
                return BadRequest("异常！");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool Insert(MenuInfo model)
        {
            if (string.IsNullOrEmpty(new MenuInfoBLL().Add(model)))
                return false;
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool Update(MenuInfo model)
        {
            return new MenuInfoBLL().Edit(model);
        }
    }
}