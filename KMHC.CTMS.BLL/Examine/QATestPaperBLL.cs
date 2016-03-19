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
    public class QATestPaperBLL:BaseBLL
    {
        private readonly string logTitle = "访问QATestPaperBLL类";
        public QATestPaperBLL()
        { 
        }

        /// <summary>
        /// 新增随访方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(QATestPaper model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                db.Set<CTMS_QA_TESTPAPER>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.TestPaperID;
            }
        }

        /// <summary>
        /// 修改随访方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(QATestPaper model)
        {
            if (string.IsNullOrEmpty(model.TestPaperID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的QATestPaper实体!");
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
                LogService.WriteInfoLog(logTitle, "试图删除为空的QATestPaper实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                CTMS_QA_TESTPAPER entity = db.Set<CTMS_QA_TESTPAPER>().Find(id);
                if (entity != null)
                {
                    db.Set<CTMS_QA_TESTPAPER>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据ID获取随访方案
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public QATestPaper Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_QA_TESTPAPER entity = db.Set<CTMS_QA_TESTPAPER>().Find(id);
                if (entity == null ) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 根据ID获取随访方案
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<QATestPaper> GetList(string keyword)
        {
            using (DbContext db = new CRDatabase())
            {
                return db.Set<CTMS_QA_TESTPAPER>().Where(o => o.TESTPAPERNAME.Contains(keyword)).Select(EntityToModel).ToList();
            }
        }

       

        public  CTMS_QA_TESTPAPER ModelToEntity(QATestPaper model)
        {
            if (model == null) return null;
            CTMS_QA_TESTPAPER entity = new CTMS_QA_TESTPAPER()
            {
                TESTPAPERID = string.IsNullOrEmpty(model.TestPaperID) ? Guid.NewGuid().ToString() : model.TestPaperID,
                TESTPAPERNAME = model.TestPaperName,
                TESTPAPERCODE = model.TestPaperCode,
                TESTPAPERSOURCE = model.TestPaperSource,
                TESTPAPERDESCRIPTION = model.TestPaperDescription,
                DISEASECODE = model.DiseaseCode,
                DISEASENAME = model.DiseaseName,
                TOTOLSCORE = model.TotalScore
            };
            base.ModelToEntity<QATestPaper, CTMS_QA_TESTPAPER>(model, entity);
            return entity;
        }

        public QATestPaper EntityToModel(CTMS_QA_TESTPAPER entity)
        {
            if (entity == null) return null;
            QATestPaper model= new QATestPaper()
            {
                TestPaperID = entity.TESTPAPERID,
                TestPaperName = entity.TESTPAPERNAME,
                TestPaperCode = entity.TESTPAPERCODE,
                TestPaperSource = entity.TESTPAPERSOURCE,
                TestPaperDescription = entity.TESTPAPERDESCRIPTION,
                DiseaseCode = entity.DISEASECODE,
                DiseaseName = entity.DISEASENAME,
                TotalScore = entity.TOTOLSCORE
            };
            base.EntityToModel<CTMS_QA_TESTPAPER, QATestPaper>(entity, model);
            return model;
        }
    }
}
