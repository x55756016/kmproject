/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Examine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.Examine
{
    public class QATestSolutionBLL:BaseBLL
    {
        private readonly string logTitle = "访问QATestSolutionBLL类";
        public QATestSolutionBLL()
        { 
        }

        /// <summary>
        /// 新增随访方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(QATestSolution model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                db.Set<CTMS_QA_TESTSOLUTION>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.SolutionID;
            }
        }

        /// <summary>
        /// 修改随访方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(QATestSolution model)
        {
            if (string.IsNullOrEmpty(model.SolutionID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的QATestSolution实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除随访方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的QATestSolution实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                CTMS_QA_TESTSOLUTION entity = db.Set<CTMS_QA_TESTSOLUTION>().Find(id);
                if (entity != null)
                {
                    db.Set<CTMS_QA_TESTSOLUTION>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据ID获取随访方案
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public QATestSolution Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_QA_TESTSOLUTION entity = db.Set<CTMS_QA_TESTSOLUTION>().Find(id);
                if (entity == null ) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 根据ID获取随访方案
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<QATestSolution> GetList(string keyword)
        {
            using (DbContext db = new CRDatabase())
            {
                return db.Set<CTMS_QA_TESTSOLUTION>().Where(o => o.SOLUTIONNAME.Contains(keyword)).Select(EntityToModel).ToList();
            }
        }

       

        public  CTMS_QA_TESTSOLUTION ModelToEntity(QATestSolution model)
        {
            if (model == null) return null;
            CTMS_QA_TESTSOLUTION entity = new CTMS_QA_TESTSOLUTION()
            {
                SOLUTIONID = string.IsNullOrEmpty(model.SolutionID) ? Guid.NewGuid().ToString() : model.SolutionID,
                SOLUTIONCODE = model.SolutionCode,
                SOLUTIONNAME = model.SolutionName,
                SOLUTIONSOURCE = model.SolutionSource,
                SOLUTIONDESCRIPTION = model.SolutionDescription,
                DISEASECODE = model.DiseaseCode,
                DISEASENAME = model.DiseaseName,
            };
            base.ModelToEntity<QATestSolution, CTMS_QA_TESTSOLUTION>(model, entity);
            return entity;
        }

        public QATestSolution EntityToModel(CTMS_QA_TESTSOLUTION entity)
        {
            if (entity == null) return null;
            QATestSolution model= new QATestSolution()
            {
                SolutionID = entity.SOLUTIONID,
                SolutionCode = entity.SOLUTIONCODE,
                SolutionName = entity.SOLUTIONNAME,
                SolutionSource = entity.SOLUTIONSOURCE,
                SolutionDescription = entity.SOLUTIONDESCRIPTION,
                DiseaseCode = entity.DISEASECODE,
                DiseaseName = entity.DISEASENAME,
            };
            base.EntityToModel<CTMS_QA_TESTSOLUTION, QATestSolution>(entity, model);
            return model;
        }
    }
}
