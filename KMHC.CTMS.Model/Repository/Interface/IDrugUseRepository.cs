/*
 * 描述:定义DrugUse作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151116      林德力             创建  
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
    public interface IDrugUseRepository
    {

        /// <summary>
        /// 新增用药情况
        /// </summary>
        /// <returns></returns>
        bool Add(DrugUse model);


        /// <summary>
        /// 更新用药情况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(DrugUse model);


        #region 生活习惯操作
        bool AddHabits(Habits mdoel);

        #endregion


        #region 药物作用操作
        bool AddDrugEffect(DrugEffect mdoel);

        #endregion


    }
}