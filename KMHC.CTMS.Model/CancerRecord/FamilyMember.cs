/*
 * ����:��ͥ��Աʵ��
 *  
 * �޶���ʷ: 
 * ����       �޸���              Email                  ����
 * 20151016  ������                                      ���� 
 *  
 */

using System;

namespace KMHC.CTMS.Model.CancerRecord
{
    public partial class FamilyMember 
    {
        public int FMemberId { get; set; }

        //public int ID { get; set; }
        public int FamilyId { get; set; }
        public string MemberName { get; set; }
        public string CardNo { get; set; }
        public short RelateId { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<long> CreatorUserId { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<long> LastModifierUserId { get; set; }
        public Nullable<long> DeleterUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
