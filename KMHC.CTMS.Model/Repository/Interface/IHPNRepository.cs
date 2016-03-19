using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IHPNRepository
    {
        IQueryable<HPN_TEMPLATE> getTemplateList();

        HPN_TEMPLATE getTemplateByName(string templateName);

        IQueryable<HPN_TEMPLATEITEM> getTemplateItemList(string templateName);

        HPN_TEMPLATEITEM getTemplateItemByName(string templateName, string name);

        IQueryable<HPN_RESULTDESC> getResultDescListByTemplateID(string templateName);

        //HPN_TESTRESULT1 getTestResultByTestNo(string testno);

        List<HPN_RESULTDESC> getResultDescByScore(string templateName, double score);

        IQueryable<HPN_TEMPLATEITEMOPTIONS> getTemplateItemOptionsByName(string templateName, string templateItemName);

        List<HPN_TEMPLATEITEMOPTIONS> getTemplateItemOptionsListByTemplateName(string templateName);
    }
}
