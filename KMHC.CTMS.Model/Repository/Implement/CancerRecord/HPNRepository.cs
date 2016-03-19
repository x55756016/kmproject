using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class HPNRepository : IHPNRepository
    {
        private readonly DbContext _db = new CRDatabase();

        public List<HPN_RESULTDESC> getResultDescByScore(string templateName, double score)
        {
            List<HPN_RESULTDESC> list = new List<HPN_RESULTDESC>();
            HPN_RESULTDESC result = (from x in _db.Set<HPN_RESULTDESC>() where x.TEMPLATENAME == templateName && x.MINSCORES <= (decimal)score && x.MAXSCORES >= (decimal)score select x).FirstOrDefault();
            if (result == null)
            {
                var query2 = from x in _db.Set<HPN_RESULTDESC>() where x.TEMPLATENAME == templateName select x;
                HPN_RESULTDESC temp = query2.OrderByDescending(p => p.MAXSCORES).FirstOrDefault();
                if (temp == null)
                {
                    return list;
                }
                if (temp.MAXSCORES < (decimal)score)
                {
                    result = temp;
                    list.Add(result);
                }

            }
            else
            {
                list.Add(result);
            }

            return list;
        }

        public IQueryable<HPN_RESULTDESC> getResultDescListByTemplateID(string templateName)
        {
            return from x in _db.Set<HPN_RESULTDESC>() where x.TEMPLATENAME == templateName select x;
        }

        public HPN_TEMPLATE getTemplateByName(string templateName)
        {
            var t = (from x in _db.Set<HPN_TEMPLATE>() where x.TEMPLATENAME == templateName select x).FirstOrDefault();
            if(t == null)
                return t;
            var items = (from x in _db.Set<HPN_TEMPLATEITEM>() where x.TEMPLATENAME == t.TEMPLATENAME select x).OrderBy(p => p.ITEMINDEX).ToList();
            items.ForEach(delegate(HPN_TEMPLATEITEM item)
            {
                var options = (from x in _db.Set<HPN_TEMPLATEITEMOPTIONS>() where x.TEMPLATENAME == t.TEMPLATENAME && x.TEMPLATEITEMNAME == item.KWD select x).OrderBy(p => p.INDEXATTR).ToList();
                item.Options = options;
            });
            t.Questions = items;
            return t;
        }

        public HPN_TEMPLATEITEM getTemplateItemByName(string templateName, string name)
        {
            return (from x in _db.Set<HPN_TEMPLATEITEM>() where x.TEMPLATENAME == templateName && x.KWD == name select x).FirstOrDefault();
        }

        public IQueryable<HPN_TEMPLATEITEM> getTemplateItemList(string templateName)
        {
            return from x in _db.Set<HPN_TEMPLATEITEM>() where x.TEMPLATENAME == templateName select x;
        }

        public IQueryable<HPN_TEMPLATEITEMOPTIONS> getTemplateItemOptionsByName(string templateName, string templateItemName)
        {
            return from x in _db.Set<HPN_TEMPLATEITEMOPTIONS>()
                   where x.TEMPLATENAME == templateName && x.TEMPLATEITEMNAME == templateItemName
                   select x;
        }

        public IQueryable<HPN_TEMPLATE> getTemplateList()
        {
            return _db.Set<HPN_TEMPLATE>().Where(p => p.TEMPLATESTATUS == 1).OrderBy(p => p.TEMPLATEINDEX);
        }


        public List<HPN_TEMPLATEITEMOPTIONS> getTemplateItemOptionsListByTemplateName(string templateName)
        {
            return (from x in _db.Set<HPN_TEMPLATEITEMOPTIONS>() where x.TEMPLATENAME == templateName select x).ToList();
        }
    }
}
