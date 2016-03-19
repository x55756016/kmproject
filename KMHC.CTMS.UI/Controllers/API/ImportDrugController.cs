using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Model.PrecisionMedicine;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ImportDrug")]
    public class ImportDrugController : ApiController
    {
        private CnDrugBLL service = new CnDrugBLL();

        public IHttpActionResult Post()
        {
            //接受文件
            if (HttpContext.Current.Request.Files.Count <= 0)
                return BadRequest("异常");
            HttpPostedFile file = HttpContext.Current.Request.Files[0];

            try
            {
                //文件保存路径
                string filePath = HttpContext.Current.Server.MapPath("~/Upload/XLS/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                //文件格式判断
                string[] exts = new string[] { ".xls", ".xlsx", ".doc", ".docx", ".jpg", ".gif", ".png", ".jpeg" };
                string fileName = file.FileName;
                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                if (!exts.Contains(fileExt))
                {
                    return BadRequest("非法文件！");
                }

                //文件保存
                string newName = System.Guid.NewGuid().ToString("N") + fileExt;
                string strPath = filePath + newName;
                file.SaveAs(strPath);

                FileStream fs = File.OpenRead(strPath);
                IWorkbook wk = null;
                try
                {
                    wk = new HSSFWorkbook(fs);   //把xls文件中的数据写入wk中
                }
                catch
                {
                    fs = File.OpenRead(strPath);
                    wk = new XSSFWorkbook(fs);
                }
                fs.Dispose();


                for (int i = 0; i < wk.NumberOfSheets; i++)  //NumberOfSheets是myxls.xls中总共的表数
                {
                    ISheet sheet = wk.GetSheetAt(i);   //读取当前表数据
                    for (int j = 1; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row != null)
                        {
                            CnDrug model;
                            string name = GetCellValue(row.GetCell(0));
                            if (string.IsNullOrEmpty(name))
                            {
                                continue;
                            }

                            model = service.Get(p => p.NAME == name).FirstOrDefault();
                            if (model != null)
                            {
                                continue;
                            }

                            model = new CnDrug();
                            model.Name = name;
                            model.CommonName = GetCellValue(row.GetCell(1));
                            model.EnName = GetCellValue(row.GetCell(2), 100);
                            model.IsDeleted = false;
                            model.TypeName = GetCellValue(row.GetCell(3));
                            model.KindName = GetCellValue(row.GetCell(4));
                            model.IsMedicalInsurance = false;
                            model.IsPrescription = false;
                            model.Pack = GetCellValue(row.GetCell(5), 200);
                            model.Dosage = GetCellValue(row.GetCell(6), 300);
                            model.DosageForms = GetCellValue(row.GetCell(7));
                            model.Indication = GetCellValue(row.GetCell(8), 300);
                            model.Description = GetCellValue(row.GetCell(9), 300);
                            model.Content = GetCellValue(row.GetCell(10), 300);
                            model.DrugBankID = GetCellValue(row.GetCell(11));

                            service.Add(model);
                        }
                    }
                }

            }
            catch
            {
                return BadRequest("异常");
            }

            return Ok();
        }


        /// <summary>
        /// 获取单元格内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private string GetCellValue(ICell cell, int maxLength = 0)
        {
            string str = "";
            if (cell != null)
            {
                if (maxLength == 0)
                    str = cell.ToString();
                else
                    str = cell.ToString().Length >= maxLength ? cell.ToString().Substring(0, maxLength) : cell.ToString();
            }

            return str;
        }
    }
}
