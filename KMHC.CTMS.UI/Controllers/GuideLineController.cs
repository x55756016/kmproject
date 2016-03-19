using System.Web.Mvc;
using KMHC.CTMS.BLL.CancerProcess;

namespace KMHC.CTMS.UI.Controllers
{
    public class GuideLineController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }




        /// <summary>
        /// 根据guideline父节点，取子节点的ID
        /// </summary>
        /// <param name="guidelineCode"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetGuideLineChild(string guidelineCode)
        {
            GuideLineBLL glbll = new GuideLineBLL();
            var list = glbll.GetChildList(guidelineCode);
            return Json(list,JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 关闭流程
        /// </summary>
        /// <returns></returns>
        public JsonResult CloseEvent(string eventId,string applyId)
        {
            UserEventBLL ueBll = new UserEventBLL();
            ueBll.CloseEvent(eventId);
            ueBll.CloseApply(applyId);
            return Json("ok",JsonRequestBehavior.AllowGet);
        }





    }
}
