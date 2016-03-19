/*
 * 描述:定义ImageExamination作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              Email                   内容
 * 2015/11/6     邓柏生                                      创建 
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
    public interface IImageExaminationRepository
    {
        /// <summary>
        /// 保存影像学检查结果
        /// </summary>
        /// <returns></returns>
        string Add(ImageExamination imageExam);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        bool Edit(ImageExamination imageExam);

        /// <summary>
        /// 获取单条影像学检查结果
        /// </summary>
        /// <returns></returns>
        ImageExamination Get(string historyID);

    }
}
