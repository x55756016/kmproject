/*
 * 描述:IPersonRepository的EntityFramework实现
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151016   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFPersonInfoRepository :BaseRepository<HR_PERSONINFO>, IPersonInfoRepository
    {
        private readonly IBaseRepository<HR_PERSONINFO> repository;

        public  EFPersonInfoRepository()
        {
            repository = new BaseRepository<HR_PERSONINFO>(new CRDatabase());
        }

        public int Add(PersonInfo personInfo)
        {
            var maxId = repository.GetMaxId("HR_PERSONINFO", "PERSONID");
            var entity = ModelToEntity(personInfo);
            entity.PERSONID = maxId;
            entity.CREATEDATE = DateTime.Now;
            //entity.CreatorUserId
            repository.Insert(entity);
            return maxId;
        }


        public bool Edit(PersonInfo personInfo)
        {
            var entity = ModelToEntity(personInfo);
            return entity != null && repository.Update(entity);
        }

        public PersonInfo Get(int personId)
        {
            return EntityToModel(repository.FindOne(o => o.PERSONID == personId));
        }

        public PersonInfo Get(string idno)
        {
            return EntityToModel(repository.FindOne(o => o.IDNUMBER.Contains(idno)));
        }

        public IEnumerable<PersonInfo> GetPersonList(PageInfo page, string name)
        {
            var list = repository.FindAll(o => o.NAME == name).Paging(ref page).ToList();
            var personInfos = list.Select(EntityToModel).ToList();
            return personInfos;
        }

        public int AddExamineRecord(PersonInfo person, ExamineRecord record)
        {
          var recordRepository = new BaseRepository<HR_EXAMINERECORD>(new CRDatabase());
          var maxId = recordRepository.GetMaxId("HR_EXAMINERECORD", "EXAMID");
          var entity = ModelToEntity(record);
          entity.PERSONID = maxId;
          entity.CREATEDATE = DateTime.Now;
          //entity.CreatorUserId
          recordRepository.Insert(entity);
          return maxId;

        }

        public void DeleteExamineRecord(int personId, int recordId)
        {
            var recordRepository = new BaseRepository<HR_EXAMINERECORD>(new CRDatabase());
            recordRepository.Delete(recordId);
        }

        public IEnumerable<ExamineRecord> GetExamineRecords(int personId)
        {
            var recordRepository = new BaseRepository<HR_EXAMINERECORD>(new CRDatabase());
            var list = recordRepository.FindAll(o => o.PERSONID == personId).ToList();
            var personInfos = list.Select(EntityToModel).ToList();
            return personInfos;
        }

        public int GetMaxId(string table, string keyId)
        {
            return new CRDatabase().Database.SqlQuery<int>(string.Format("select {0}_{1}.nextval from dual ", table, keyId)).FirstOrDefault();
        }


        #region 实体模型映射

        private HR_PERSONINFO ModelToEntity(PersonInfo model)
        {
            if (model != null)
            {
                var entity = new HR_PERSONINFO()
                {
                    PERSONID=model.PersonId,
                    PERSONNO = model.PersonNo,
                    NAME = model.Name,
                    GENDER = model.Gender,
                    BIRTHDATE = model.BirthDate,
                    COUNTRY = model.Country,
                    NATIONALITY = model.Nationality,
                    MARRIAGESTATUS = model.MarriageStatus,
                    IDTYPE = model.IDType,
                    IDNUMBER = model.IDNumber,
                    PHONE = model.Phone,
                    CONTACTNAME = model.ContactName,
                    CONTACTPHONE = model.ContactPhone,
                    EMAILADDRESS = model.EmailAddress,
                    CENSUSREGISTERFLAG = model.CensusRegisterFlag,
                    CENSUSADDRESSCODE = model.CensusAddressCode,
                    CENSUSADDRESSNAME = model.CensusAddressName,
                    CENSUSPOSTCODE = model.CensusPostCode,
                    CURRENTADDRESSCODE = model.CurrentAddressCode,
                    CURRENTADDRESSNAME = model.CurrentAddressName,
                    CURRENTPOSTCODE = model.CurrentPostCode,
                    COMPANY = model.Company,
                    HIREDATE = model.HireDate,
                    OCCUPATIONCLASS = model.OccupationClass,
                    EDUCATIONLEVEL = model.EducationLevel,
                    INSURANCETYPE = model.InsuranceType,
                    INSURANCETYPENAME = model.InsuranceTypeName,
                    PAYMETHOD = model.PayMethod,
                    ABOTYPE = model.ABOType,
                    RHTYPE = model.RHType,
                    ALLERGYHISTORY = model.AllergyHistory,
                    RISKFACTORS = model.RiskFactors,
                    DISABILITYSTATUS = model.DisabilityStatus,
                    COMMUNITY = model.Community,
                    COMMUNITYCONTACT = model.CommunityContact,
                    COMMUNITYCONTACTPHONE = model.CommunityContactPhone,
                    RESPONSIBLEORGANIZATION = model.ResponsibleOrganization,
                    RESPONSIBLEORGANIZATIONID = model.ResponsibleOrganizationID,
                    RESPONSIBLEDOCTOR = model.ResponsibleDoctor,
                    RESPONSIBLEDOCTORPHONE = model.ResponsibleDoctorPhone,
                    CREATEDATE = DateTime.Now, //todo
                    CREATEBY = "", //todo
                    UPDATEDATE = null,
                    UPDATEBY = "", //
                    STATUS = 0, //todo
                    //BLOODTYPE = "" //todo
                    RECORDNO=model.RecordNo,
                };

                return entity;
            }

            return null;
        }

        private HR_EXAMINERECORD ModelToEntity(ExamineRecord model)
        {
            if (model != null)
            {
                var entity = new HR_EXAMINERECORD()
                {
                    EXAMNO = model.ExamNo,
                    EXAMTYPE = model.ExamType,
                    VISITWAY = model.VisitWay,
                    DOCTORNAME = model.Doctor,
                    EXAMDATE = model.ExamDate,
                    CREATEDATE = DateTime.Now,
                    CREATEBY = "", //todo
                    UPDATEDATE = null, //todo
                    UPDATEBY = "",
                    STATUS = 0,
                };
                return entity;
            }
            return null;

        }
        private PersonInfo EntityToModel(HR_PERSONINFO entity)
        {
            if (entity != null)
            {
                var model = new PersonInfo()
                {
                    PersonId = entity.PERSONID,
                    Name = entity.NAME,
                    Gender = entity.GENDER,
                    BirthDate = entity.BIRTHDATE,
                    Country = entity.COUNTRY,
                    Nationality = entity.NATIONALITY,
                    MarriageStatus = entity.MARRIAGESTATUS,
                    IDType = entity.IDTYPE,
                    IDNumber = entity.IDNUMBER,
                    Phone = entity.PHONE,
                    ContactName = entity.CONTACTNAME,
                    ContactPhone = entity.CONTACTPHONE,
                    EmailAddress = entity.EMAILADDRESS,
                    CensusRegisterFlag = entity.CENSUSREGISTERFLAG,
                    CensusAddressCode = entity.CENSUSADDRESSCODE,
                    CensusAddressName = entity.CENSUSADDRESSNAME,
                    CensusPostCode = entity.CENSUSPOSTCODE,
                    CurrentAddressCode = entity.CURRENTADDRESSCODE,
                    CurrentAddressName = entity.CURRENTADDRESSNAME,
                    CurrentPostCode = entity.CURRENTPOSTCODE,
                    Company = entity.COMPANY,
                    HireDate = entity.HIREDATE,
                    OccupationClass = entity.OCCUPATIONCLASS,
                    EducationLevel = entity.EDUCATIONLEVEL,
                    InsuranceType = entity.INSURANCETYPE,
                    InsuranceTypeName = entity.INSURANCETYPENAME,
                    PayMethod = entity.PAYMETHOD,
                    ABOType = entity.ABOTYPE,
                    RHType = entity.RHTYPE,
                    AllergyHistory = entity.ALLERGYHISTORY,
                    RiskFactors = entity.RISKFACTORS,
                    DisabilityStatus = entity.DISABILITYSTATUS,
                    Community = entity.COMMUNITY,
                    CommunityContact = entity.COMMUNITYCONTACT,
                    CommunityContactPhone = entity.COMMUNITYCONTACTPHONE,
                    ResponsibleOrganization = entity.RESPONSIBLEORGANIZATION,
                    ResponsibleOrganizationID = entity.RESPONSIBLEORGANIZATIONID,
                    ResponsibleDoctor = entity.RESPONSIBLEDOCTOR,
                    ResponsibleDoctorPhone = entity.RESPONSIBLEDOCTORPHONE,
                    RecordNo=entity.RECORDNO,
                };
                return model;
            }
            return null;
        }

        public static ExamineRecord EntityToModel(HR_EXAMINERECORD entity)
        {
            if (entity != null)
            {
                var model = new ExamineRecord()
                {
                    ExamId = entity.EXAMID,
                    ExamNo = entity.EXAMNO,
                    ExamType = entity.EXAMTYPE,
                    ExamDate = entity.EXAMDATE ?? DateTime.Now,
                    PersonId = entity.PERSONID ?? 0,
                    Doctor = entity.DOCTORNAME,
                    VisitWay = entity.VISITWAY
                };
                return model;
            }
            return null;
        }
        #endregion


    }
}
