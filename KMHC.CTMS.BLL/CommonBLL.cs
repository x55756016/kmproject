/*
 * 描述:公共方法的逻辑层
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20160122   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;

namespace KMHC.CTMS.BLL
{
    public class CommonBLL
    {

        public List<Area> GetArea(long parentId=0)
        {
            using (var context = new CRDatabase())
            {
                List<Area> list = new List<Area>();
                context.HR_AREA.Where(k=>k.PARENTID==parentId).Select(p => new
                {
                    AREAID = p.AREAID,
                    AreaName = p.AREANAME,
                    ParentId = p.PARENTID
                }).ToList().ForEach(p => list.Add( new Area()
                {
                    AreaId = p.AREAID,
                    AreaName = p.AreaName,
                    ParentId = p.ParentId
                }));
                return list;
            }
        }

        public string GetDictionaryName(string category, string value)
        {
            using (var context = new CRDatabase())
            {
                var model = context.HR_DICTIONARY.FirstOrDefault(p => p.DICTIONCATEGORY == category && p.DICTIONARYVALUE == value);
                return model == null ? model.DICTIONARYNAME : "";
            }
        }


        #region 模型映射

        public Area EntityToModel(HR_AREA entity)
        {
            if (entity != null)
            {
                var model = new Area();
                return model;
            }
            return null;
        }

        #endregion

    }
}
