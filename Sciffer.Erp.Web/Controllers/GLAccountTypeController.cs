using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;
using System.Net;

namespace Sciffer.Erp.Web.Controllers
{
    public class GLAccountTypeController : Controller
    {
        private readonly IGLAccountTypeService _gl;
        private readonly IGenericService _Generic;
        public GLAccountTypeController(IGLAccountTypeService gl, IGenericService gen)
        {
            _gl = gl;
            _Generic = gen;
        }

        public ActionResult Index()
        {
           // ViewBag.datasource = _gl.GetAll();
            return View();
        }
        public JsonResult GetAccountType()
        {
            var res = _gl.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _gl.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Data(ref_gl_acount_type value)
        {
            var data = 0;
            var data1 = true;
            data = _Generic.CheckDuplicate(value.gl_account_type_description,"", "", "glaccounttype", value.gl_account_type_id);


            bool duplicate = false;
            if (data > 0)
            {

                duplicate = false;
                return Json(duplicate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (value.gl_account_type_id == 0)
                {
                    data1 = _gl.Add(value);
                }
                else
                {
                    data1 = _gl.Update(value);
                }

                // duplicate = true;
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }
        public void ExportToExcel(string GridModel)
        {
            ExcelExport exp = new ExcelExport();
            var DataSource = _gl.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "GLAccountType.xlsx", ExcelVersion.Excel2010, false, false, "flat-saffron");
        }
        public void ExportToWord(string GridModel)
        {
            WordExport exp = new WordExport();
            var DataSource = _gl.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "GLAccountType.docx", false, false, "flat-saffron");
        }
        public void ExportToPdf(string GridModel)
        {
            PdfExport exp = new PdfExport();
            var DataSource = _gl.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "GLAccountType.pdf", false, false, "flat-saffron");
        }
        private GridProperties ConvertGridObject(string gridProperty)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    Type type = property.PropertyType;
                    string serialize = serializer.Serialize(ds.Value);
                    object value = serializer.Deserialize(serialize, type);
                    property.SetValue(gridProp, value, null);
                }
            }
            return gridProp;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _gl.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
