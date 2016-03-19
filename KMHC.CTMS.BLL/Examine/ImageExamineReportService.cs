using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Examine;
using KMHC.CTMS.Model.Repository.Implement;

namespace KMHC.CTMS.BLL.Examine
{
    /*
     * 描述:定义影像检验报告操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20160106      刘佳豪              创建 
     *  
     */
    public class ImageExamineReportService
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddImageExamineReport(ImageExamineReport model)
        {
            using (EFImageExamineReportRepository _rsp = new EFImageExamineReportRepository())
            {
                return _rsp.AddImageExamineReport(model);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateImageExamineReport(ImageExamineReport model)
        {
            using (EFImageExamineReportRepository _rsp = new EFImageExamineReportRepository())
            {
                return _rsp.UpdateImageExamineReport(model);
            }
        }

        /// <summary>
        /// 删除数据(软删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteImageExamineReport(string id)
        {
            using (EFImageExamineReportRepository _rsp = new EFImageExamineReportRepository())
            {
                return _rsp.DeleteImageExamineReport(id);
            }
        }

        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ImageExamineReport GetImageExamineReportById(string id)
        {
            using (EFImageExamineReportRepository _rsp = new EFImageExamineReportRepository())
            {
                return _rsp.GetImageExamineReportById(id);
            }
        }

        /// <summary>
        /// 根据用户id获取数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ImageExamineReport> GetImageExamineReportByUserId(string userId)
        {
            using (EFImageExamineReportRepository _rsp = new EFImageExamineReportRepository())
            {
                return _rsp.GetImageExamineReportByUserId(userId);
            }
        }
    }
}
