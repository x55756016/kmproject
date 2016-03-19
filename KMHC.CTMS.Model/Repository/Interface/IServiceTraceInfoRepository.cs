using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerProcess;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义ServiceTraceInfo的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20160115      刘佳豪              创建 
     *  
     */
    public interface IServiceTraceInfoRepository
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddServiceTraceInfo(ServiceTraceInfo model);

        /// <summary>
        /// 根据客服id获取跟踪信息
        /// </summary>
        /// <param name="createUserId"></param>
        /// <returns></returns>
        List<ServiceTraceInfo> GetServiceTraceInfoByCreateUserId(string createUserId);
    }
}
