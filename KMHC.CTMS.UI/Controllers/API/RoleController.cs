using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Authorization;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.UI.Attribute;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class RoleController : ApiController
    {
        RoleBLL bll = new RoleBLL();

        public IHttpActionResult Get(int CurrentPage, string RoleName = "")
        {
            //申明参数
            int _pageSize = 15;

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "CreateDateTime",
                    Order = OrderEnum.asc
                };

                Expression<Func<CTMS_SYS_ROLE, bool>> predicate = p => p.ISDELETED == false;
                if (!string.IsNullOrEmpty(RoleName))
                {
                    predicate = p => p.ISDELETED == false && p.ROLENAME.Contains(RoleName);
                }

                var list = bll.GetPageData(pageInfo, predicate);

                DictionaryService _dictionary = new DictionaryService();
                List<Dictionary> dics = _dictionary.GetDictionaryByCategory("SystemCategory");

                if (dics.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string _value = list[i].SystemCategory.ToString();
                        Dictionary duc = dics.Where(p => p.value.Equals(_value)).FirstOrDefault();
                        list[i].CategoryValue = duc == null ? "" : duc.text;
                    }
                }

                Response<IEnumerable<Role>> response = new Response<IEnumerable<Role>>
                {
                    Data = list,
                    PagesCount = pageInfo.PagesCount
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Get()
        {
            //申明返回对象
            ExtRole model = new ExtRole();
            model.ExtFuns = new List<ExtFun>();

            FunctionBLL fctionBLL = new FunctionBLL();
            PermissionBLL perBLL = new PermissionBLL();
            List<Function> Functions = fctionBLL.GetList(p => p.ISMENU && !string.IsNullOrEmpty(p.PARENTID) && !p.ISPUBLIC && !p.ISDELETED);
            List<Permission> Permissions = perBLL.GetList();

            foreach (Function fun in Functions)
            {
                ExtFun extFun = new ExtFun();
                extFun.FunctionID = fun.FunctionID;
                extFun.FunctionCode = fun.FunctionCode;
                extFun.FunctionName = fun.FunctionName;
                extFun.IsMenu = fun.IsMenu;
                extFun.Permissions = Permissions;

                model.ExtFuns.Add(extFun);
            }

            //返回
            return Ok(model);
        }

        public IHttpActionResult Get(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest("非法请求！");
            }

            //申明返回对象
            ExtRole model = new ExtRole();
            Role role = bll.Get(roleId);
            if (role == null)
            {
                return BadRequest("该记录不存在！");
            }
            model.RoleID = role.RoleID;
            model.RoleName = role.RoleName;
            model.SystemCategory = role.SystemCategory;
            model.Remark = role.Remark;
            model.ExtFuns = new List<ExtFun>();

            FunctionBLL fctionBLL = new FunctionBLL();
            RoleFunctionBLL rfBLL = new RoleFunctionBLL();
            PermissionBLL perBLL = new PermissionBLL();

            List<Function> Functions = fctionBLL.GetList(p => p.ISMENU && !string.IsNullOrEmpty(p.PARENTID) && !p.ISPUBLIC && !p.ISDELETED);
            List<Permission> Permissions = perBLL.GetList();
            List<RoleFunction> roleFuns = rfBLL.GetList(p => !p.ISDELETED && p.ROLEID.Equals(roleId));
            foreach (Function fun in Functions)
            {
                List<Permission> _temPermission = new List<Permission>();

                foreach (Permission pm in Permissions)
                {
                    Permission _temp = new Permission();
                    _temp.PermissionID = pm.PermissionID;
                    _temp.PermissionName = pm.PermissionName;
                    _temp.PermissionValue = pm.PermissionValue;
                    _temp.PermissionCode = pm.PermissionCode;
                    _temp.Remark = pm.Remark;
                    _temp.CreateUserID = pm.CreateUserID;
                    _temp.CreateUserName = pm.CreateUserName;
                    _temp.CreateDateTime = pm.CreateDateTime;
                    _temp.EditUserID = pm.EditUserID;
                    _temp.EditUserName = pm.EditUserName;
                    _temp.EditTime = pm.EditTime;
                    _temp.OwnerID = pm.OwnerID;
                    _temp.OwnerName = pm.OwnerName;
                    _temp.IsDeleted = pm.IsDeleted;

                    foreach (RoleFunction roleFun in roleFuns)
                    {
                        if (roleFun.FunctionID == fun.FunctionID && roleFun.PermissionValue == pm.PermissionValue)
                        {
                            _temp.Remark = roleFun.DataRange;
                            _temp.IsDeleted = true;
                            break;
                        }
                    }

                    _temPermission.Add(_temp);
                }

                ExtFun extFun = new ExtFun();
                extFun.FunctionID = fun.FunctionID;
                extFun.FunctionCode = "";
                extFun.FunctionName = fun.FunctionName;
                extFun.IsMenu = fun.IsMenu;
                extFun.Permissions = _temPermission;

                model.ExtFuns.Add(extFun);
            }

            return Ok(model);
        }

        public IHttpActionResult Post([FromBody]Request<ExtRole> request)
        {
            try
            {
                IList<RoleFunction> roleFuns = request.Data.RoleFuns;
                string roleId = request.Data.RoleID;
                string roleName = request.Data.RoleName;
                int systemCategory = (int)request.Data.SystemCategory;
                string remark = request.Data.Remark;

                MetaDataBLL metaBLL = new MetaDataBLL();
                List<MetaData> metaDatas = metaBLL.GetList(string.Empty);

                RoleFunctionBLL rfBLL = new RoleFunctionBLL();
                Role mode = new Role();
                if (string.IsNullOrEmpty(roleId))
                {
                    mode.RoleName = roleName;
                    mode.SystemCategory = systemCategory;
                    mode.Remark = remark;
                    mode.CreateDateTime = DateTime.Now;

                    roleId = bll.Add(mode);
                    if (string.IsNullOrEmpty(roleId))
                    {
                        return BadRequest("异常");
                    }

                    foreach (RoleFunction fun in roleFuns)
                    {
                        fun.RoleID = roleId;
                        fun.CreateDateTime = DateTime.Now;

                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        sb.AppendFormat("<data version=\"2.0\">");

                        string[] _arr1 = fun.DataRange.Split(',');
                        foreach (string str in _arr1)
                        {
                            string[] _arr2 = str.Split('#');
                            if (_arr2.Length != 5)
                            {
                                continue;
                            }
                            int _id = int.Parse(_arr2[1]);
                            string col = GetValue(metaDatas, _id);
                            sb.AppendFormat("<item>");
                            sb.AppendFormat("<relationship>{0}</relationship>", _arr2[3]);
                            sb.AppendFormat("<nameID>{0}</nameID>", _arr2[1]);
                            sb.AppendFormat("<name>{0}</name>", _arr2[0]);
                            sb.AppendFormat("<operation>{0}</operation>", _arr2[2]);
                            sb.AppendFormat("<value>{0}</value>", _arr2[4]);
                            sb.AppendFormat("<column>{0}</column>", col);
                            sb.AppendFormat("</item>");
                        }
                        sb.AppendFormat("</data>");

                        rfBLL.Add(fun);
                    }
                }
                else
                {
                    mode = bll.Get(roleId);
                    if (mode == null)
                    {
                        return BadRequest("该记录不存在！");
                    }
                    mode.RoleName = roleName;
                    mode.SystemCategory = systemCategory;
                    mode.Remark = remark;
                    mode.EditTime = DateTime.Now;

                    bll.Edit(mode);
                    List<RoleFunction> hasRF = rfBLL.GetList(p => p.ROLEID.Equals(roleId) && p.ISDELETED == false);
                    foreach (RoleFunction fun in hasRF)
                    {
                        rfBLL.Delete(fun.RoleFunctionID);
                    }

                    foreach (RoleFunction fun in roleFuns)
                    {
                        fun.RoleID = roleId;
                        fun.CreateDateTime = DateTime.Now;

                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        sb.AppendFormat("<data version=\"2.0\">");

                        string[] _arr1 = fun.DataRange.Split(',');
                        foreach (string str in _arr1)
                        {
                            string[] _arr2 = str.Split('#');
                            if (_arr2.Length != 5)
                            {
                                continue;
                            }
                            int _id = int.Parse(_arr2[1]);
                            string col = GetValue(metaDatas, _id);
                            sb.AppendFormat("<item>");
                            sb.AppendFormat("<relationship>{0}</relationship>", _arr2[3]);
                            sb.AppendFormat("<nameID>{0}</nameID>", _arr2[1]);
                            sb.AppendFormat("<name>{0}</name>", _arr2[0]);
                            sb.AppendFormat("<operation>{0}</operation>", _arr2[2]);
                            sb.AppendFormat("<value>{0}</value>", _arr2[4]);
                            sb.AppendFormat("<column>{0}</column>", col);
                            sb.AppendFormat("</item>");
                        }
                        sb.AppendFormat("</data>");
                        fun.DataRange = sb.ToString();

                        rfBLL.Add(fun);
                    }
                }

                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常！");
            }
        }

        public IHttpActionResult Delete(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest("非法请求！");
            }

            try
            {
                RoleFunctionBLL rfBLL = new RoleFunctionBLL();
                IList<RoleFunction> roleFuns = rfBLL.GetList(p => !p.ISDELETED && p.ROLEID.Equals(roleId));
                foreach (RoleFunction fun in roleFuns)
                {
                    fun.IsDeleted = true;
                    rfBLL.Edit(fun);
                }

                bll.Delete(roleId);

                return Ok("ok");
            }
            catch
            {
                return BadRequest("异常！");
            }
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        //[ApiAuth(AuthCode="ctms_sys_role")]
        public IHttpActionResult Get(string keyWord, string key)
        {
            try
            {
                List<Role> list = new RoleBLL().GetList(keyWord);
                Response<List<Role>> response = new Response<List<Role>>
                {
                    Data = list,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        /// <summary>
        /// 获取列名值
        /// </summary>
        /// <param name="metaDatas"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetValue(List<MetaData> metaDatas, int id)
        {
            string val = string.Empty;
            foreach (MetaData meta in metaDatas)
            {
                if (meta.ID == id)
                {
                    val = meta.DataSourceColumn;
                    break;
                }
            }

            return val;
        }
    }
}