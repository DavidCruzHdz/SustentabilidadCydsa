using CYDSA_Sustentabilidad.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class TransferenciaMateriaPrimaController : Controller
    {
        // GET: TransferenciaMateriaPrima
        Entities db = new Entities();
        public ActionResult TransferenciaMateriaPrima()
        {
            return View();
        }


        //public ActionResult ImportCSVCustomer()
        //{
        //    ImportCSVData model = new ImportCSVData();
        //    HttpPostedFileBase upfile = Request.Files["File"];
        //    model.File = upfile;
        //    var dt = ParseCSVData(model);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        var dcRec = new DataColumn("ShouldImport", typeof(bool));
        //        dcRec.DefaultValue = false;
        //        dt.Columns.Add(dcRec);
        //        model.Data = dt;
        //        List<SelectListItem> Names = getColumnNames();
        //        model.Names = Names;
        //        return PartialView("_ImportedCSVCustomer", model);
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        if (!file.FileName.EndsWith(".xls") && !file.FileName.EndsWith(".xlsx"))
        //            return View();

        //        var fileName = DateTime.Now.ToString("yyyyMMddHHmm.") + file.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
        //        SaveFile(file, fileName);
        //        UploadRecordsToDataBase(fileName);
        //        return RedirectToAction("TransferenciaMateriaPrima");
        //    }

        //    // Tu podras decidir que hacer aqui
        //    // si el archivo es nulo
        //    return View();

        //}

        private void SaveFile(HttpPostedFileBase file, string fileName)
        {
            var path = System.IO.Path.Combine(Server.MapPath("~/Content/Files/"), fileName);
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);

            using (var sw = new System.IO.FileStream(path, System.IO.FileMode.Create))
            {
                sw.Write(data, 0, data.Length);
            }
        }


        //private void UploadRecordsToDataBase(string fileName)
        //{
        //    var records = new List<MateriaPrima>();
        //    using (var stream = System.IO.File.Open(Path.Combine(Server.MapPath("~/Content/Files/"), fileName), FileMode.Open, FileAccess.Read))
        //    {
        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {
        //            while (reader.Read())
        //            {
        //                records.Add(new MateriaPrima
        //                {
        //                    Email = reader.GetString(0),
        //                    Credits = int.Parse(reader.GetValue(1).ToString()),
        //                    RecordDate = DateTime.Now,
        //                    IsActive = true,
        //                });
        //            }
        //        }
        //    }

        //    if (records.Any())
        //    {
        //        db.MateriaPrima.AddRange(records);
        //        db.SaveChanges();
        //    }
        //}



    }
}