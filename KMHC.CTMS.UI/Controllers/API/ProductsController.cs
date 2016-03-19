using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Product;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.Product;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class ProductsController : ApiController
    {
        private ProductsService _p;
        private DictionaryService _dictionary;

        public ProductsController()
        {
            _p = new ProductsService();
            _dictionary = new DictionaryService();
        }

        public IHttpActionResult Get([FromUri]Request<string> req)
        {
            try
            {
                List<Products> list = _p.GetAllProducts();
                if (!string.IsNullOrEmpty(req.Keyword))
                    list = list.FindAll(p => p.ProductName.Contains(req.Keyword));
                List<Dictionary> ProductTypes = _dictionary.GetDictionaryByCategory("ProductType").FirstOrDefault().nodes;
                List<Dictionary> ProductUnits = _dictionary.GetDictionaryByCategory("ProductUnit").FirstOrDefault().nodes;
                list.ForEach(delegate(Products item)
                {
                    Dictionary type = ProductTypes.FirstOrDefault(p => p.value.Trim() == item.ProductType.ToString());
                    if (type != null)
                        item.ProductTypeText = type.text;

                    Dictionary unit = ProductUnits.FirstOrDefault(p => p.value.Trim() == item.ProductUnit.ToString());
                    if (unit != null)
                        item.ProductUnitText = unit.text;
                });
                Response<List<Products>> rsp = new Response<List<Products>>();
                rsp.Data = list;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ProductsController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<Products> req)
        {
            try
            {
                Products model = req.Data;
                bool result = false;
                if (string.IsNullOrEmpty(model.ProductId))
                {
                    string productId = Guid.NewGuid().ToString();
                    model.ProductId = productId;
                    result = _p.AddProducts(model);
                }
                else
                {
                    result = _p.UpdateProducts(model);
                }
                if (!result)
                    return BadRequest("操作失败");
                Response<string> rsp = new Response<string>();
                rsp.Data = "操作成功";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ProductsController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<Products> req)
        {
            try
            {
                string id = req.Keyword;
                Guid g = new Guid ();
                if (!Guid.TryParse(id, out g))
                    return BadRequest("参数错误！");

                bool result = _p.DeleteProductsById(id);
                if (!result)
                    return BadRequest("操作失败！");

                Response<string> rsp = new Response<string>();
                rsp.Data = "操作成功";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ProductsController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
