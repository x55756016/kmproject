/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 *    		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{

    public class ExamineTemplate
    {
        public ExamineTemplate()
        {
            TemplateItems = new List<HR_TemplateItemDTO>();
        }


        public string TEMPLATEID { get; set; }
        public string TEMPLATENAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string ISDELETED { get; set; }
        public string OWNERID { get; set; }


        //public IList<TemplateItem> TemplateItems { get; set; }

        public IList<HR_TemplateItemDTO> TemplateItems { get; set; } 


    }

    public class HR_TemplateItemDTO
    {
        public HR_TemplateItemDTO ()
        {
            ItemUnit = new List<HR_TemplateItemUnitDTO>();
        }

        public string ITEMID { get; set; }
        public string TEMPLATEID { get; set; }
        public string ITEMNAME { get; set; }
        public string ITEMNAMEENG { get; set; }
        public Nullable<decimal> SORT { get; set; }
        public List<HR_TemplateItemUnitDTO> ItemUnit { get; set; } 


        /// <summary>
        /// 用于实验室检查录入结果
        /// </summary>
        #region 拓展字段
        public string ITEMUNITID { get; set; }
        public string UNITNAME { get; set; }
        public string UNITDESC { get; set; }

        #endregion
    }

    public class HR_TemplateItemUnitDTO
    {
        public string ITEMUNITID { get; set; }
        public string ITEMID { get; set; }
        public string UNITNAME { get; set; }
        public string UNITDESC { get; set; }
    }



   


    public class TemplateItem
    {
        public string ITEMID { get; set; }
        public string TEMPLATEID { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public Nullable<decimal> SORT { get; set; }
        //public string VALUETYPE { get; set; }
        //public string CODEVALUE { get; set; }
        //public string CATETORYID { get; set; }
        public string ITEMNAMEENG { get; set; }

        #region 扩展字段
        public string NAMEENG { get; set; }
        public decimal? MINVALUE { get; set; }
        public decimal? MAXVALUE { get; set; }
        public string UNIT { get; set; }
        #endregion
    }

    public class ExamineItems
    {
        public int ITEMID { get; set; }
        public string ITEMNO { get; set; }
        public string ITEMNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string VALUETYPE { get; set; }
        public string CODEVALUE { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public string CREATEBY { get; set; }
        public string UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public Nullable<byte> STATUS { get; set; }
        public Nullable<int> CATEGORYID { get; set; }
    }



    public class ExamineCategory
    {
        public decimal ID { get; set; }
        public string CATEGORYCODE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string PARENTCODE { get; set; }
        public decimal PARENTID { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public Nullable<decimal> CREATEUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public Nullable<decimal> EDITUSERID { get; set; }
        public Nullable<decimal> DeleterUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public decimal ISDELETED { get; set; }
    }



    /// <summary>
    /// 项值设置
    /// </summary>
    public class ItemStandVal
    {
        public string ID { get; set; }
        public string ITEMNO { get; set; }


        public string ItemName { get; set; }

        public string NAMEENG { get; set; }
        public Nullable<decimal> MINVALUE { get; set; }
        public Nullable<decimal> MAXVALUE { get; set; }
        public string UNIT { get; set; }
        public string CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string ISDELETED { get; set; }
        public string OWNERID { get; set; }
    }





}
