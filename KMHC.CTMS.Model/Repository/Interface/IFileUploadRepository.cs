/*
 * 描述:附件上传表单接口
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2015/11/9      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IFileUploadRepository
    {
        /// <summary>
        /// 保存附件
        /// </summary>
        /// <returns></returns>
        string Add(FileUpload fileUpload);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        bool Edit(FileUpload fileUpload);

        /// <summary>
        /// 获取附件记录
        /// </summary>
        /// <returns></returns>
        FileUpload Get(string fileUploadid);

        /// <summary>
        /// 根据模块代码，表单ID获取数据列表
        /// </summary>
        /// <param name="modeCode"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        List<FileUpload> Get(string modeCode, string formId);

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <param name="fileUploadid"></param>
        /// <returns></returns>
        bool Delete(string fileUploadid);
    }
}
