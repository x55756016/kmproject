using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KMHC.CTMS.Model.CancerRecord;

namespace KMHC.CTMS.UI.Controllers
{
    public class ExamineTemplateController : BaseViewController
    {
        /// <summary>
        /// 生成模板的HTML
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        //protected string GenerateHtml4Template(string templateId)
        //{
        //    var tpl = _context.Templates.FirstOrDefault(m => m.TemplateCode == templateId);
        //    if (tpl == null)
        //    {
        //        return "";
        //    }
        //    return GenerateHtml4Categories(tpl.Categories);
        //}

        /// <summary>
        /// 根据分类生成HTML
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
//        protected string GenerateHtml4Categories(IEnumerable<Category> categories)
//        {
//            if (categories == null) return "";

//            var sb = new StringBuilder();
//            foreach (var c in categories)
//            {
//                sb.AppendFormat(@"
//                    <div class=""panel panel-default"">
//                        <div class=""panel-heading"">
//                            <i class=""icon-user-md icon-large""></i> {0}
//                        </div>
//                        <div class=""panel panel-body form-horizontal"">", c.Name);
//                var items =
//                    _context.ItemCategories.Where(m => m.CategoryCode == c.CategoryCode).Select(i => i.Item).ToList();
//                foreach (var item in items)
//                {
//                    #region 生成项目的HTML

//                    sb.Append("<div class=\"form-group\">");
//                    sb.AppendFormat("<label class=\"control-label col-md-2\" for=\"_{0}\">{1}</label>", item.ID, item.ItemName);
//                    sb.Append("<div class=\"col-md-10\">");
//                    switch (item.ValueType)
//                    {
//                        case "L":
//                            sb.AppendFormat("<div class=\"form-inline\"><label class=\"checkbox checkbox-inline\"><input type=\"checkbox\" id=\"_{0}\" itemId=\"{0}\" itemCode=\"{1}\" /></label></div>", item.ID, item.ItemCode);
//                            break;
//                        case "S2":
//                            sb.Append(GenerateSelect(item.ID, item.ItemCode, _codeRepo.GetCode(item.CodeValue)));
//                            break;
//                        case "S3":
//                            sb.Append(GenerateMultiSelect(item.ID, item.ItemCode, _codeRepo.GetCode(item.CodeValue)));
//                            break;
//                        case "DT":
//                            sb.AppendFormat("<input type=\"text\" class=\"form-control\" datetime=\"yyyy-MM-dd: HH:mm:ss\" id=\"_{0}\" itemId=\"{0}\" itemCode=\"{1}\"  datepicker/>", item.ID, item.ItemCode);
//                            break;
//                        case "D":
//                            sb.AppendFormat("<input type=\"text\" class=\"form-control\" datetime=\"yyyymmdd\" id=\"_{0}\" itemId=\"{0}\" itemCode=\"{1}\"   datepicker/>", item.ID, item.ItemCode);
//                            break;
//                        default:
//                            sb.AppendFormat("<input type=\"text\" class=\"form-control\" itemId=\"{0}\" id=\"_{0}\" itemCode=\"{1}\" />", item.ID, item.ItemCode);
//                            break;
//                    }
//                    sb.Append("</div>");
//                    sb.Append("</div>");


//                    #endregion
//                }
//                //如果有子类，那么递归产生子类HTML
//                if (c.SubCategories != null)
//                {
//                    sb.Append(GenerateHtml4Categories(c.SubCategories));
//                }

//                sb.AppendFormat("</div></div>");
//            }
//            return sb.ToString();
//        }

        /// <summary>
        /// 生成选项
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemCode"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        //protected string GenerateSelect(int itemId, string itemCode, Code code)
        //{
        //    if (code == null || code.Options == null) return "";

        //    StringBuilder sb = new StringBuilder(string.Format("<select class=\"form-control\" id=\"_{0}\" itemId=\"{0}\" itemCode=\"{1}\">", itemId, itemCode));
        //    foreach (var option in code.Options)
        //    {
        //        sb.AppendFormat("<option value=\"{0}\">{1}</option>", option.Value, option.Name);
        //    }
        //    sb.Append("</select>");
        //    return sb.ToString();
        //}

        /// <summary>
        /// 生成多选的HTML
        /// </summary>
        /// <returns></returns>
        //protected string GenerateMultiSelect(int itemId, string itemCode, Code code)
        //{
        //    if (code == null || code.Options == null) return "";

        //    StringBuilder sb = new StringBuilder(string.Format("<select class=\"form-control\" id=\"_{0}\" itemId=\"{0}\" itemCode=\"{1}\" style=\"width: 100%;\" multiselect>", itemId, itemCode));
        //    foreach (var option in code.Options)
        //    {
        //        sb.AppendFormat("<option value=\"{0}\">{1}</option>", option.Value, option.Name);
        //    }
        //    sb.Append("</select>");
        //    return sb.ToString();
        //}
    }
}