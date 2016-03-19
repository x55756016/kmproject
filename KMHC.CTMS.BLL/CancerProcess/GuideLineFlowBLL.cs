/*
 * 描述:对GuideLineFlow的操作
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20151223   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using Newtonsoft.Json;

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class GuideLineFlowBLL
    {

        private readonly string logTitle = "访问GuideLineFlowBLL类";

        /// <summary>
        /// 构造函数
        /// </summary>
        public GuideLineFlowBLL()
        {

        }

        public List<GuideLine_Select> allList = new List<GuideLine_Select>(); 

        public string GetGuideLineFlow(string id)
        {
            using (var _context = new CRDatabase())
            {
                //1.需要判断这个GuideLine有没有已经保存的路径  否的话需要为这个路径生成路径图片信息

                allList = _context.Database.SqlQuery<GuideLine_Select> //用GuideLine一直匹配不上，类型有问题
                    ("select ID,code,NAME,PARENTID from CTMS_GUIDELINE where isdeleted=0 connect by prior ID=PARENTID start with id='" +id + "'").ToList();

                int level = 0;
                //foreach (var guideLineSelect in allList)
                //{
                    var listsearch = allList.FirstOrDefault(p => string.IsNullOrEmpty(p.PARENTID));
                    listsearch.Depth = 0;
                    GetDepth(allList.Where(p => p.PARENTID == listsearch.ID).ToList(), level);
                //}

                //var d = allList;

                //递归去设置节点坐标
                List<Rootobject> flowList = new List<Rootobject>();

                //Random r = new Random();
                foreach (var guideLineSelect in allList)
                {
                    Rootobject rootobject = new Rootobject();
                    rootobject.id = guideLineSelect.ID;
                    rootobject.flow_id = "1";
                    rootobject.process_name = guideLineSelect.NAME;
                    //rootobject.process_to = ListToLink(allList.Where(p => p.PARENTID == guideLineSelect.ID).ToList());
                    rootobject.process_to =
                        ListToLink(_context.Database.SqlQuery<GuideLine_Select>("select b.id from CTMS_PARENTGUIDELINE a inner join CTMS_GUIDELINE b on a.guidelineid=b.id " +
                                "where b.isdeleted=0 and a.parentid='"+guideLineSelect.ID+"'").ToList());
                    rootobject.icon = "icon-play";
                    //rootobject.style = "width:120px;height:30px;line-height:30px;color:#0e76a8;left:" +  r.Next(10, 1700) + "px;top:" +  r.Next(10, 800) + "px;";
                    rootobject.style = "width:120px;height:30px;line-height:30px;color:#0e76a8;left:" + (guideLineSelect.Width*160+20) + "px;top:" +(guideLineSelect.Depth*90+60) + "px;";

                    flowList.Add(rootobject);
                }
                //递归去判断第几层
                return "{\"total\": " + 18 + ", \"list\": " + flowList.JsonSerialize() + "}";
            }
        }

        public void GetDepth(List<GuideLine_Select> listSearch, int depth)
        {
            //取出同级有多少个
            var maxWidth = allList.Where(p => p.Depth == (depth + 1)).OrderByDescending(p => p.Width).FirstOrDefault() ?? new GuideLine_Select();
            var tempMaxWidth = maxWidth.Depth;

            //确定为第几层
            foreach (var guideLineSelect in listSearch)
            {
                guideLineSelect.Depth = (depth + 1);
                guideLineSelect.Width = (tempMaxWidth++);
                GetDepth(allList.Where(p => p.PARENTID == guideLineSelect.ID).ToList(), guideLineSelect.Depth);
            }
        }
        
        public string ListToLink(List<GuideLine_Select> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ID + ",");
            }
            return sb.ToString() == "" ? "" : sb.ToString().Remove(sb.ToString().Length - 1, 1);
        }

        public IList<GuideLine> GetGuideLineFlowList(PageInfo page, string keyWord)
        {
            var list = new List<GuideLine>();
            using (CRDatabase _context = new CRDatabase())
            {
                //todo：待确定更改之后，需要删除CTMS_GUIDELINEFLOW表
                //_context.CTMS_GUIDELINEFLOW.Where(
                //    p => (string.IsNullOrEmpty(keyWord) || p.GUIDELINENAME.StartsWith(keyWord))
                //        && p.ISDELETED == false).Paging(ref page).ToList().ForEach(k => list.Add(EntityToModel(k)));
                _context.CTMS_GUIDELINE.Where(p => p.ISDELETED == false && string.IsNullOrEmpty(p.PARENTID) && (string.IsNullOrEmpty(keyWord) || p.NAME.StartsWith(keyWord)))
                    .Paging(ref page).ToList().ForEach(k=>list.Add(EntityToModel(k)));
            }
            return list;
        }


        /// <summary>
        /// 添加GuideLineFlow
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGuideLineFlow(GuideLineFlow model)
        {
            using (CRDatabase _context = new CRDatabase())
            {
                _context.CTMS_GUIDELINEFLOW.Add(ModelToEntity(model));
                return _context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新GuideLineFlow
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditGuideLineFlow(GuideLineFlow model)
        {
            using (CRDatabase _context = new CRDatabase())
            {
                var entity = _context.CTMS_GUIDELINEFLOW.FirstOrDefault(p => p.ID == model.ID);
                if (entity != null)
                {
                    //1.将原先流程状态设置成删除
                    entity.ISDELETED = true;

                    GuideLineFlow addModel = new GuideLineFlow();
                    addModel.ID = Guid.NewGuid().ToString();
                    addModel.GUIDELINENAME = entity.GUIDELINENAME;
                    addModel.GUIDELINEINFO = model.GUIDELINEINFO;
                    addModel.CreateUserID = model.CreateUserID;
                    addModel.CreateDateTime = model.CreateDateTime;
                    addModel.EditTime = model.EditTime;
                    addModel.EditUserID = model.EditUserID;
                    addModel.IsDeleted = false;
                    addModel.OwnerID = model.OwnerID;
                    _context.CTMS_GUIDELINEFLOW.Add(ModelToEntity(addModel));
                    return _context.SaveChanges() > 0;
                }
                return false;
            }
        }

        /// <summary>
        /// 删除单个GuideLine对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteGuideLineFlow(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            using (CRDatabase _context = new CRDatabase())
            {
                var entity = _context.CTMS_GUIDELINEFLOW.Find(id);
                if (entity != null)
                {
                    _context.CTMS_GUIDELINEFLOW.Remove(entity);
                }
                return _context.SaveChanges() > 0;
            }
        }

        #region 模型映射


        private GuideLine EntityToModel(CTMS_GUIDELINE entity)
        {
            if (entity == null) return null;
            return new GuideLine()
            {
                  ID  = entity.ID,
                  Code = entity.CODE,
                  Name = entity.NAME,
                  ParentID = entity.PARENTID,
                  CreateDateTime = entity.CREATEDATETIME
            };
        }


        public GuideLineFlow EntityToModel(CTMS_GUIDELINEFLOW entity)
        {
            if (entity != null)
            {
                var model = new GuideLineFlow()
                {
                    ID = entity.ID,
                    GUIDELINENAME = entity.GUIDELINENAME,
                    GUIDELINEINFO = entity.GUIDELINEINFO,
                    CreateUserID = entity.CREATEUSERID,
                    CreateDateTime = entity.CREATEDATETIME,
                    EditUserID = entity.EDITUSERID,
                    EditTime = entity.EDITDATETIME,
                    IsDeleted = entity.ISDELETED,
                    OwnerID = entity.OWNERID
                };
                return model;
            }
            return null;
        }


        public CTMS_GUIDELINEFLOW ModelToEntity(GuideLineFlow model)
        {
            if (model != null)
            {
                var entity = new CTMS_GUIDELINEFLOW()
                {
                    ID = model.ID,
                    GUIDELINENAME = model.GUIDELINENAME,
                    GUIDELINEINFO = model.GUIDELINEINFO,
                    CREATEUSERID = model.CreateUserID,
                    CREATEDATETIME = model.CreateDateTime,
                    EDITUSERID = model.EditUserID,
                    EDITDATETIME = model.EditTime,
                    ISDELETED = model.IsDeleted,
                    OWNERID = model.OwnerID
                };
                return entity;
            }
            return null;
        }



        #endregion



        public class FlowModel
        {
            public string ID { get; set; }
            public int Left { get; set; }
            public int Top { get; set; }
            public string Name { get; set; }
            public List<string> ProcessTo { get; set; }
        }


        public class Rootobject
        {
            public string id { get; set; }
            public string flow_id { get; set; }
            public string process_name { get; set; }
            public string process_to { get; set; }
            public string icon { get; set; }
            public string style { get; set; }


            public int Level { get; set; }

        }

    }
}
