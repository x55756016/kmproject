using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义RESETPASSWORDLOG的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151123      刘佳豪              创建 
     *  
     */
    public interface IResetPasswordLogRepository
    {
        /// <summary>
        /// 根据id来获取重置记录
        /// </summary>
        /// <param name="resetLogID">主键id</param>
        /// <returns></returns>
        ResetPasswordLog GetResetPasswordLogByID(string resetLogID);

        /// <summary>
        /// 添加重置密码记录
        /// </summary>
        /// <param name="resetPasswordLog">重置密码记录</param>
        /// <returns></returns>
        bool AddResetPasswordLog(ResetPasswordLog resetPasswordLog);

        /// <summary>
        /// 修改重置密码记录信息
        /// </summary>
        /// <param name="resetPasswordLog"></param>
        /// <returns></returns>
        bool UpdateResetPasswordLog(ResetPasswordLog resetPasswordLog);
    }
}
