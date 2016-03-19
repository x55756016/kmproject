using System;
using System.Data.Entity;
using System.Reflection;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System.Linq;
using KMHC.CTMS.Common.Helper;
using System.Collections.Generic;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class TemplateService
    {
        private readonly DbContext _db = new CRDatabase();

        public TemplateService(DbContext _db)
        {
            this._db = _db;
        }

        public HPN_TESTRESULT handleResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            try
            {
                Template template = CreateInstance(testResult.TEMPLATENAME);
                template.setContext(_db);
                double result = template.calculateResult(testResult, testDetails);
                testResult.TESTRESULT = result.ToString();
                return testResult;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public Template CreateInstance(string templateName)
        {
            Template instance;
            if (!string.IsNullOrEmpty(templateName)){
                Assembly a = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "bin\\KMHC.CTMS.Model.dll");
                string className = "KMHC.CTMS.Model.Repository.CancerRecord." + templateName + "Template";
                instance = (Template)a.CreateInstance(className);
            }
            else
                instance = null;
            return instance;
        }
    }
}
