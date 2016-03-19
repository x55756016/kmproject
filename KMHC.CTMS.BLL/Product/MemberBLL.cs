/*
 * 描述:会员功能编辑
 *  
 * 修订历史: 
 * 日期              修改人              Email                  内容
 * 20160122   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Product;

namespace KMHC.CTMS.BLL.Product
{
    public class MemberBLL
    {
        /// <summary>
        /// 获取单个会员信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public MemberModel GetMember(string memberId)
        {
            using (var context = new CRDatabase())
            {
                var model = new MemberModel();
                model = EntityToModel(context.CTMS_MEMBER.FirstOrDefault(k => k.MEMBERID == memberId));
                if (model!=null)
                {
                    model.LevelString = model.MEMBERLEVEL.ToString();
                    model.menberProductList = new List<MemberProducts>();

                    (from memPro in context.CTMS_MEMBERPRODUCTS
                        join products in context.CTMS_PRODUCTS on memPro.PRODUCTID equals products.PRODUCTID
                        join dictionary in context.HR_DICTIONARY on memPro.PRODUCTUNIT equals dictionary.DICTIONARYVALUE
                        where memPro.MEMBERID == model.MEMBERID
                        select new
                        {
                            MEMBERPRODUCTID = memPro.MEMBERPRODUCTID,
                            MEMBERNAME = products.PRODUCTNAME,
                            MEMBERID = memPro.MEMBERID,
                            PRODUCTID = memPro.PRODUCTID,
                            PRODUCTUNIT = memPro.PRODUCTUNIT,
                            PRODUCTUNITName = dictionary.DICTIONARYNAME,
                            PRODUCTNUMBER = memPro.PRODUCTNUMBER,
                            MEMPRODESCRIPT = memPro.MEMPRODESCRIPT
                        }
                        ).ToList().ForEach(p => model.menberProductList.Add(new MemberProducts()
                        {
                            MEMBERPRODUCTID = p.MEMBERPRODUCTID,
                            MEMBERNAME = p.MEMBERNAME,
                            MEMBERID = p.MEMBERID,
                            PRODUCTID = p.PRODUCTID,
                            PRODUCTUNIT = p.PRODUCTUNIT,
                            PRODUCTUNITName = p.PRODUCTUNITName,
                            PRODUCTNUMBER = p.PRODUCTNUMBER,
                            MEMPRODESCRIPT = p.MEMPRODESCRIPT
                        }));
                }
                return model;
            }
        }

        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<MemberModel> GetMemberList(PageInfo pageInfo,int name=0)
        {
            using (var context = new CRDatabase())
            {
                List<MemberModel> list = new List<MemberModel>();

                var entityList =
                    context.CTMS_MEMBER.Where(p => (p.MEMBERLEVEL==name || name==0 ))
                        .Paging(ref pageInfo)
                        .ToList();

                (from entity in entityList
                    join dic in context.HR_DICTIONARY on entity.MEMBERLEVEL.ToString() equals dic.DICTIONARYVALUE
                 where dic.DICTIONCATEGORY == "MemberLevel"
                        select new
                    {
                        entity.MEMBERID,
                        entity.MEMBERNAME,
                        entity.MEMBERLEVEL,
                        entity.MEMBERPRICE,
                        MEMBERLEVELName = dic.DICTIONARYNAME,
                        entity.MEMBERUNIT,
                        entity.MEMBERDESCRIPT,
                    }
                    ).OrderBy(p=>p.MEMBERLEVEL).ToList().ForEach(
                        p => list.Add(new MemberModel()
                        {
                            MEMBERID = p.MEMBERID,
                            MEMBERNAME = p.MEMBERNAME,
                            MEMBERLEVEL = p.MEMBERLEVEL,
                            MEMBERPRICE = p.MEMBERPRICE,
                            MEMBERLEVELName = p.MEMBERLEVELName,
                            MEMBERUNIT = p.MEMBERUNIT,
                            MEMBERDESCRIPT = p.MEMBERDESCRIPT,
                        })); 
                return list;
            }
        }

        public List<MemberModel> GetMemberList()
        {
            using (var context = new CRDatabase())
            {
                return
                    context.Set<CTMS_MEMBER>()
                        .AsNoTracking()
                        .OrderBy(o => o.MEMBERLEVEL)
                        .ToList()
                        .Select((p) => new MemberModel()
                        {
                            MEMBERID = p.MEMBERID,
                            MEMBERNAME = p.MEMBERNAME,
                            MEMBERLEVEL = p.MEMBERLEVEL,
                            MEMBERPRICE = p.MEMBERPRICE,
                            MEMBERUNIT = p.MEMBERUNIT,
                            MEMBERDESCRIPT = p.MEMBERDESCRIPT,
                        }).ToList();
            }
        }

        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMemberList(MemberModel model)
        {
            using (var context = new CRDatabase())
            {
                var entity = ModelToEntity(model);


                context.CTMS_MEMBER.Add(entity);
                foreach (var item in model.menberProductList)
                {
                    item.MEMBERID = entity.MEMBERID;
                    context.CTMS_MEMBERPRODUCTS.Add(ModelToEntity(item));
                }
                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新会员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMemberList(MemberModel model)
        {
            using (var context = new CRDatabase())
            {
                var entity = context.CTMS_MEMBER.FirstOrDefault(p => p.MEMBERID == model.MEMBERID);
                if (entity!=null)
                {
                    //1.更新主表字段
                    entity.MEMBERNAME = model.MEMBERNAME;
                    entity.MEMBERDESCRIPT = model.MEMBERDESCRIPT;
                    entity.MEMBERLEVEL = model.MEMBERLEVEL;
                    entity.MEMBERPRICE = model.MEMBERPRICE;

                    //2.删除原先服务
                    context.CTMS_MEMBERPRODUCTS.Where(p => p.MEMBERID == model.MEMBERID).ToList().ForEach(k => context.CTMS_MEMBERPRODUCTS.Remove(k));

                    //3.新增服务
                    foreach (var item in model.menberProductList)
                    {
                        item.MEMBERID = model.MEMBERID;
                        context.CTMS_MEMBERPRODUCTS.Add(ModelToEntity(item));
                    }
                }
                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool DeleteMember(string memberId)
        {
            using (var context = new CRDatabase())
            {
                var entity = context.Set<CTMS_MEMBER>().Find(memberId);
                if (entity!=null)
                {
                    context.Set<CTMS_MEMBER>().Remove(entity);     
                    context.CTMS_MEMBERPRODUCTS.Where(p=>p.MEMBERID ==memberId).ToList().ForEach(k=>context.CTMS_MEMBERPRODUCTS.Remove(k));
                }
                return context.SaveChanges() > 0;
            }
        }

        #region 模型映射

        public CTMS_MEMBER ModelToEntity(MemberModel model)
        {
            if (model != null)
            {
                var entity = new CTMS_MEMBER()
                {
                    MEMBERID = Guid.NewGuid().ToString(),
                    MEMBERNAME = model.MEMBERNAME,
                    MEMBERLEVEL = model.MEMBERLEVEL,
                    MEMBERPRICE = model.MEMBERPRICE,
                    MEMBERUNIT = model.MEMBERUNIT,
                    MEMBERDESCRIPT = model.MEMBERDESCRIPT,
                };
                return entity;
            }
            return null;
        }

        public CTMS_MEMBERPRODUCTS ModelToEntity(MemberProducts model)
        {
            if (model != null)
            {
                var entity = new CTMS_MEMBERPRODUCTS()
                {
                    MEMBERPRODUCTID = model.MEMBERPRODUCTID,
                    MEMBERID = model.MEMBERID,
                    PRODUCTID = model.PRODUCTID,
                    PRODUCTUNIT = model.PRODUCTUNIT,
                    PRODUCTNUMBER = model.PRODUCTNUMBER,
                    MEMPRODESCRIPT = model.MEMPRODESCRIPT
                };
                return entity;
            }
            return null;
        }


        public MemberModel EntityToModel(CTMS_MEMBER entity)
        {
            if (entity!=null)
            {
                var model = new MemberModel()
                {
                    MEMBERID = entity.MEMBERID,
                    MEMBERNAME = entity.MEMBERNAME,
                    MEMBERLEVEL = entity.MEMBERLEVEL,
                    MEMBERPRICE = entity.MEMBERPRICE,
                    MEMBERUNIT = entity.MEMBERUNIT,
                    MEMBERDESCRIPT = entity.MEMBERDESCRIPT,
                };
                return model;
            }
            return null;
        }

        public MemberProducts EntityToModel(CTMS_MEMBERPRODUCTS entity)
        {
            if (entity != null)
            {
                var model = new MemberProducts()
                {
                    MEMBERPRODUCTID = entity.MEMBERPRODUCTID,
                    MEMBERID = entity.MEMBERID,
                    PRODUCTID = entity.PRODUCTID,
                    PRODUCTUNIT = entity.PRODUCTUNIT,
                    PRODUCTNUMBER = entity.PRODUCTNUMBER,
                    MEMPRODESCRIPT = entity.MEMPRODESCRIPT

                };
                return model;
            }
            return null;
        }

        #endregion
    }
}
