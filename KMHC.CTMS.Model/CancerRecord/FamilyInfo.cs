/*
 * 描述:家庭实体
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 * 20151016  刘美方                                      创建 
 *  
 */

using System;

namespace KMHC.CTMS.Model.CancerRecord
{
    public partial class FamilyInfo 
    {
        public int FamilyId { get; set; }
        public int HouseholderId { get; set; }
        public string HouseholderName { get; set; }
        public string CardNo { get; set; }
        public Nullable<int> Nationality { get; set; }
        public Nullable<int> ProvinceId { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> TownId { get; set; }
        public string Address { get; set; }
        public Nullable<int> CommunityId { get; set; }
        public string ZipCode { get; set; }

    }
}
