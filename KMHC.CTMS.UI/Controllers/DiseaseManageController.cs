using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers
{
    public class DiseaseManageController : ExamineTemplateController
    {
        /// <summary>
        /// 高血压管理建档
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateHBpRecord()
        {
            //ViewBag.Content = GenerateHtml4Template("T102");//高血压管理建档模板
            return View();
        }
        /// <summary>
        /// 糖尿病管理建档
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateDIABRecord()
        {
            //ViewBag.Content = GenerateHtml4Template("T105");//糖尿病管理建档模板
            return View();
        }
	}
}