using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IScoreRepository
    {
        /// <summary>
        /// 保存个人基本资料
        /// </summary>
        /// <returns></returns>
        string Add(Score score);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        bool Edit(Score score);

        /// <summary>
        /// 获取个人基本资料
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        Score Get(string Id);

    }
}
