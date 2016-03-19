using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web.Mvc;


namespace KMHC.CTMS.UI.Controllers
{
    public class ElderManageController : ExamineTemplateController
    {
        public ActionResult CreateElderRecord()
        {
            //ViewBag.Content = GenerateHtml4Template("T100");//老年人建档模板代码
            return View();
        }
    }
}
