/*
 * 描述:定义DrugBank作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151117      林德力             创建  
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IDrugBankRepository
    {
        /// <summary>
        /// 根据DBId或者药品名称进行查询
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="drugName"></param>
        /// <returns></returns>
        DrugBank GetDrugInfo(string dbId, string drugName);
    }
}