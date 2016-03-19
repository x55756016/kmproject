using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Examine;

namespace KMHC.CTMS.Model.Repository.Interface
{
    /*
     * 描述:定义ImageExamineReport的对外提供访问的接口
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20160106      刘佳豪              创建 
     *  
     */
    public interface IImageExamineReportRepository
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddImageExamineReport(ImageExamineReport model);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateImageExamineReport(ImageExamineReport model);

        /// <summary>
        /// 删除数据(软删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteImageExamineReport(string id);

        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ImageExamineReport GetImageExamineReportById(string id);

        /// <summary>
        /// 根据用户id获取数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ImageExamineReport> GetImageExamineReportByUserId(string userId);
    }
}
