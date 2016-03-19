using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class Community
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int CommunityId { get; set; }
        public string CommunityName { get; set; }

        public string CommunityAddress { get;set; }

        [StringLength(200)]
        public string CommunityContactName { get; set; }

        [StringLength(50)]
        public string CommunityContactPhone { get; set; }

        [StringLength(50)]
        public string ComminityArea { get; set; }


        [StringLength(50)]
        public string ProvinceId { get; set; }
        [StringLength(50)]
        public string CityId { get; set; }
        [StringLength(50)]
        public string CountryId { get; set; }
        [StringLength(50)]
        public string TownId { get; set; }

        public string Address { get; set; }

    }
}