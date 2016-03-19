/*
 * 描述:定义临床路径实体
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151210      张志明              创建 
 *  
 */

using KMHC.CTMS.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class GuideLine : BaseModel
    {
        /// <summary>
        /// 主键，GUID
        /// </summary>
        public string ID { get; set; }
        
        /// <summary>
        /// 临床路径编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 临床路径名称
        /// </summary>
        public string Name { get; set; }



        /// <summary>
        /// 父临床路径ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 父临床路径ID
        /// </summary>
        public string ParentName { get; set; }


        /// <summary>
        /// 父临床路径
        /// </summary>
        public List<ParentGuideLine> ParentList { get; set; } 






        /// <summary>
        /// 是否继承父路径的可入条件
        /// </summary>
        public bool IsInherit { get; set; }

        /// <summary>
        /// 临床依据
        /// </summary>
        public string ClinicalBasis { get; set; }

        /// <summary>
        /// 进入条件的逻辑运算符
        /// </summary>
        public LogicOperator EnterLogicalOperator { get; set; }

        /// <summary>
        /// 进入条件的逻辑运算符
        /// </summary>
        public string EnterLogicalOperatorText { get { return EnumHelper.GetDescription(EnterLogicalOperator); } }

        /// <summary>
        /// 进入条件列表
        /// </summary>
        public List<ConditionItem> EnterCondItemList { get; set; }

        /// <summary>
        /// 进入条件列表
        /// </summary>
        public string EnterCondItemListText
        {
            get 
            {
                if (EnterCondItemList == null || EnterCondItemList.Count <= 0)
                {
                    return null;
                }
                return string.Join(",", EnterCondItemList.Select(o => o.DisplayName));
            }
        }

        /// <summary>
        /// 退出条件的逻辑运算符
        /// </summary>
        public LogicOperator OutLogicalOperator { get; set; }

        /// <summary>
        /// 退出条件的逻辑运算符
        /// </summary>
        public string OutLogicalOperatorText { get { return EnumHelper.GetDescription(OutLogicalOperator); } }

        /// <summary>
        /// 退出条件列表
        /// </summary>
        public List<ConditionItem> OutCondItemList { get; set; }

        /// <summary>
        /// 退出条件列表
        /// </summary>
        public string OutCondItemListText
        {
            get
            {
                if (OutCondItemList == null || OutCondItemList.Count <= 0)
                {
                    return null;
                }
                return string.Join(",", OutCondItemList.Select(o => o.DisplayName));
            }
        }

        /// <summary>
        /// 元数据列表
        /// </summary>
        public List<GuideLineData> MetaDataList { get; set; }



        /// <summary>
        /// 推荐流程(临时使用)
        /// </summary>
        public string RecommendProcess { get; set; }


        /// <summary>
        /// 产品数据列表
        /// </summary>
        public List<GuidelineProduct> Products{ get; set; }
    }




    /// <summary>
    /// 用于存放当前结点的父guideline
    /// </summary>
    public class ParentGuideLine
    {
        public string Id { get; set; }
        public string GuideLineId { get; set; }

        public string ParentName { get; set; }
        public string PARENTID { get; set; }
    }



    public class GuideLine_Select
    {
        public string ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string PARENTID { get; set; }
        public int ISINHERIT { get; set; }
        //public string CLINICALBASIS { get; set; }
        //public int ENTERLOGICALOPERATOR { get; set; }
        //public string ENTERCONDITEMIDS { get; set; }
        //public int OUTLOGICALOPERATOR { get; set; }
        //public string OUTCONDITEMIDS { get; set; }
        //public string CREATEUSERID { get; set; }
        //public string CREATEUSERNAME { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        //public string EDITUSERID { get; set; }
        //public string EDITUSERNAME { get; set; }
        //public Nullable<System.DateTime> EDITDATETIME { get; set; }
        //public string OWNERID { get; set; }
        //public string OWNERNAME { get; set; }
        //public int ISDELETED { get; set; }
        //public string RECOMMENDPROCESS { get; set; }


        public int Depth { get; set; }

        public int Width { get; set; }

    }
}
