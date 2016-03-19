using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers
{
    public class ScoreScaleController : BaseViewController
    {
        //
        // GET: /ScoreScale/
        public ActionResult Template(int id)
        {
            return View(id);
        }
	}
}