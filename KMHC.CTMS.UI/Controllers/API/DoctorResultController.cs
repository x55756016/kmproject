using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.BLL.PrecisionMedicine;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/doctorResult")]
    public class DoctorResultController : ApiController
    {
        private LaboratoryResultBLL bll = new LaboratoryResultBLL();

        public IHttpActionResult Get(int CurrentPage,string Hid)
        {
            //申明参数
            int _pageSize = 10;

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "LABRESULTID",
                    Order = OrderEnum.asc
                };
                var list = bll.GetList(pageInfo,Hid);
                Response<IEnumerable<LaboratoryResult>> response = new Response<IEnumerable<LaboratoryResult>>
                {
                    Data = list,
                    PagesCount = pageInfo.Total / _pageSize
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
        /// 获取检验报告数据
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string id,string hid)
        {
            //异常检测
            if (string.IsNullOrEmpty(hid))
            {
                return BadRequest("数据异常");
            }

            //申明返回
            LabResultResponse response = new LabResultResponse();

            //获取数据
            var model = bll.Get(p => p.LABRESULTID == id);
            if (model != null)
            {
                response.LabResult = model;
                response.LabItems = new LaboratoryTestItemBLL().GetListByRId(id);
            }
            response.FileUplpads = new EFFileUploadRepository().Get("LaboratoryResult", hid);

            //返回
            return Ok(response);
        }

        /// <summary>
        /// 检验报告保存数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IHttpActionResult SaveResult([FromBody]Request<LabResultResponse> request)
        {
            //获取参数
            LaboratoryResult _model = request.Data.LabResult;
            IList<LaboratoryTestItem> _itemsModel = request.Data.LabItems;

            //申明检查项BLL
            LaboratoryTestItemBLL ltItemBLL = new LaboratoryTestItemBLL();

            try
            {
                string _labresultId = string.Empty;
                if (string.IsNullOrEmpty(_model.LabresultId))//新增插入
                {
                    _labresultId = bll.Add(_model);
                    if (string.IsNullOrEmpty(_labresultId))
                    {
                        throw new Exception("新增失败！");
                    }
                }
                else//修改更新
                {
                    _labresultId = _model.LabresultId;
                    bll.Edit(_model);
                }

                //获取原有具体检查项目
                List<LaboratoryTestItem> oldItems = ltItemBLL.GetListByRId(_labresultId);
                foreach (LaboratoryTestItem item in _itemsModel)
                {
                    if (string.IsNullOrEmpty(item.TestitemId))
                    {
                        item.LabresultId = _labresultId;
                        ltItemBLL.Add(item);
                    }
                    else
                    {
                        ltItemBLL.Edit(item);

                        oldItems.RemoveAll(p => p.TestitemId == item.TestitemId);
                    }
                }

                foreach (LaboratoryTestItem item in oldItems)
                {
                    ltItemBLL.Delete(item.TestitemId);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// 检验目的数据获取
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            TemplateManBLL tmBLL = new TemplateManBLL();

            //获取检验目的列表
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = 1,
                PageSize = 150,
                Order = OrderEnum.desc,
                OrderField = "CREATEDATETIME"
            };

            //获取检验目对应的详细项目
            IEnumerable<ExamineTemplate> list = tmBLL.GetTemplatesList(pageInfo,null);

            foreach (ExamineTemplate item in list)
            {
                IEnumerable<TemplateItem> _items = tmBLL.GetTemplateItemList(item.TEMPLATEID).OrderBy(p=>p.SORT);
                item.TemplateItems = GetExItems(_items);
            }

            return Ok(list);
        }

        private List<TemplateItem> GetExItems(IEnumerable<TemplateItem> items)
        {
            TemplateManBLL tmBLL = new TemplateManBLL();
            List<TemplateItem> list = new List<TemplateItem>();
            foreach (TemplateItem item in items)
            {
                if (!string.IsNullOrEmpty(item.ITEMCODE))
                {
                    var standVal = tmBLL.GetStandVal(item.ITEMCODE);
                    if (standVal != null)
                    {
                        item.MAXVALUE = standVal.MAXVALUE;
                        item.MINVALUE = standVal.MINVALUE;
                        item.NAMEENG = standVal.NAMEENG;
                        item.UNIT = standVal.UNIT;
                    }
                }

                list.Add(item);
            }

            return list;
        }

        public IHttpActionResult Delete(string ID)
        {
            bll.Delete(ID);

            return Ok("ok");
        }
    }
}
