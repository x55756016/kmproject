/*
 * 描述:地区字典实体
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 * 20151016  刘美方                                      创建 
 *  
 */

namespace KMHC.CTMS.Model.CancerRecord
{
    public class Area
    {
        public long AreaId { get; set; }

        public string AreaName { get; set; }

        public long ParentId { get; set; }
    }
}
