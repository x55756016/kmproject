using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.Repository.Implement;

namespace KMHC.CTMS.BLL.CancerProcess
{
    /*
     * 描述:定义客服跟踪信息操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20160115      刘佳豪              创建 
     *  
     */
    public class ServiceTraceInfoService
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddServiceTraceInfo(ServiceTraceInfo model)
        {
            using (EFServiceTraceInfoRepository _rsp = new EFServiceTraceInfoRepository())
            {
                return _rsp.AddServiceTraceInfo(model);
            }
        }

        /// <summary>
        /// 根据客服id获取数据
        /// </summary>
        /// <param name="createUserId"></param>
        /// <returns></returns>
        public List<ServiceTraceInfo> GetServiceTraceInfoByCreateUserId(string createUserId)
        {
            using (EFServiceTraceInfoRepository _rsp = new EFServiceTraceInfoRepository())
            {
                return _rsp.GetServiceTraceInfoByCreateUserId(createUserId);
            }
        }
    }
}
