using Excel;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class InitialUploadController : Controller
    {
        private readonly IInitialUploadService  _initialupload;
        private readonly IGenericService _Generic;

        string is_based_depretialtion_method ;
        string[] error = new string[30];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public InitialUploadController(IInitialUploadService initialupload, IGenericService Generic)
        {
            _initialupload = initialupload;
            _Generic = Generic;
        }
        // GET: OpeningAsset
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _initialupload.GetAll();
            return View();
        }

        [HttpPost]
        public JsonResult GetIsBasedOn(string is_based_depretialtion_method_val)
        {
            is_based_depretialtion_method = is_based_depretialtion_method_val;

            TempData["is_based_depretialtion_method"] = is_based_depretialtion_method;

            string output = "SuccessFully Received";
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult CreateExcel(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        var msg = _initialupload.AddExcel(file);
        //        TempData["doc_num"] = msg;
        //        if (msg.Contains("saved"))
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult UploadFiles()
        //{
        //    HttpPostedFileBase file = Request.Files[0];
        //    if (file != null)
        //    {
        //        if (ValidateExcelColumns(columnarray) == true)
        //        {

        //            var isSucess = _initialupload.AddExcel(file);
        //            if (isSucess.Contains("ERROR"))
        //            {
        //                errorList++;
        //                error[0] = isSucess;
        //                errorMessage = isSucess;
        //            }

        //            if (isSucess == "Saved Sucessfully")
        //            {
        //                errorMessage = "success";
        //                return Json(errorMessage, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                errorMessage = "Failed " + errorMessage;
        //            }
        //        }
        //        else
        //        {
        //            errorList++;
        //            error[error.Length - 1] = "Check Headers name.";
        //            errorMessage = "Check header !";
        //        }
        //    }
        //    else
        //    {
        //        errorList++;
        //        error[error.Length - 1] = "Select File to Upload.";
        //        errorMessage = "Select File to Upload.";
        //    }



        //    return RedirectToAction("Index");
        //}


        [HttpPost]

        public ActionResult UploadFiles()
        {
            string isSucess = null;
            for (int m = 0; m < Request.Files.Count; m++)
            {
                HttpPostedFileBase file = Request.Files[m];
                if (file.ContentLength > 0)
                {

                    string extension = System.IO.Path.GetExtension(file.FileName);
                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Uploads"), file.FileName);
                    if (System.IO.File.Exists(path1))
                        System.IO.File.Delete(path1);

                    file.SaveAs(path1);
                    FileStream stream = System.IO.File.Open(path1, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader;
                    if (extension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    excelReader.IsFirstRowAsColumnNames = true;
                    System.Data.DataSet result = excelReader.AsDataSet();
                    int row = 0, col = 0;
                    string[] columnarray = new string[excelReader.FieldCount];

                    string uploadtype = Request.Params[0];
                    string current_id = "";
                    List<intial_upload_excel_vm> intial_upload_excel = new List<Domain.ViewModel.intial_upload_excel_vm>();
                    while (excelReader.Read())
                    {
                        if (row == 0)
                        {
                            for (int i = 0; i <= excelReader.FieldCount - 1; i++)
                            {
                                columnarray[col] = excelReader.GetString(col);
                                col = col + 1;
                            }
                        }
                        else
                        {
                            if (ValidateExcelColumns(columnarray) == true)
                            {
                                string is_based_depretialtion_method = Convert.ToString(TempData["is_based_depretialtion_method"]);

                                string sr_no = excelReader.GetString(Array.IndexOf(columnarray, "SrNo"));
                                if (sr_no != null)
                                {
                                    intial_upload_excel_vm initial_upload = new intial_upload_excel_vm();

                                    if( is_based_depretialtion_method == "wdv_slm")
                                    {
                                       
                                        string asset_code = excelReader.GetString(Array.IndexOf(columnarray, "Asset Code"));
                                        string dep_area = excelReader.GetString(Array.IndexOf(columnarray, "Dep Area"));
                                        string original_cost = excelReader.GetString(Array.IndexOf(columnarray, "Original Cost"));
                                        string acc_depreciation = excelReader.GetString(Array.IndexOf(columnarray, "Accumlated Depreciation"));
                                        string net_value = excelReader.GetString(Array.IndexOf(columnarray, "Net value"));

                                        initial_upload.is_wdv_or_block = true;
                                        initial_upload.asset_code = asset_code;
                                        initial_upload.dep_area = dep_area;
                                        initial_upload.original_cost = original_cost;
                                        initial_upload.acc_depreciation = acc_depreciation;
                                        initial_upload.net_value = net_value;

                                    }

                                    if (is_based_depretialtion_method == "block")
                                    {
                                        string asset_class = excelReader.GetString(Array.IndexOf(columnarray, "ASSET Class"));
                                        string net_wdv_value = excelReader.GetString(Array.IndexOf(columnarray, "Net WDV value"));
                                        string dep_area = excelReader.GetString(Array.IndexOf(columnarray, "Dep Area"));

                                        initial_upload.is_wdv_or_block = false;
                                        initial_upload.asset_class = asset_class;
                                        initial_upload.net_wdv_value = net_wdv_value;
                                        initial_upload.dep_area = dep_area;
                                    }

                                    string capitalization_date = excelReader.GetString(Array.IndexOf(columnarray, "Capitalization Date"));
                                    initial_upload.capitalization_date =  capitalization_date;

                                    intial_upload_excel.Add(initial_upload);
                                
                                }
                            }
                            else
                            {
                                errorList++;
                                error[error.Length - 1] = "Check Headers name.";
                                errorMessage = "Check header !";
                            }
                        }
                        row = row + 1;
                    }
                    excelReader.Close();
                    if (errorMessage == "")
                    {
                        isSucess = _initialupload.AddExcel(intial_upload_excel, intial_upload_excel[0].is_wdv_or_block);
                        if (isSucess.Contains("ERROR"))
                        {
                            errorList++;
                            error[0] = isSucess;
                            errorMessage = isSucess;
                        }

                        if (isSucess == "Saved Sucessfully")
                        {
                            errorMessage = "success";
                            return Json(errorMessage, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            errorMessage = "Failed " + errorMessage;
                        }
                    }
                }
                else
                {
                    errorList++;
                    error[error.Length - 1] = "Select File to Upload.";
                    errorMessage = "Select File to Upload.";
                }
            }
            
            if (errorMessage != null)
            {
                return Json(errorList + " - " + errorMessage, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(Message, JsonRequestBehavior.AllowGet);
            }
        }
    
        public Boolean ValidateExcelColumns(string[] columnarray)
        {
            string is_based_depretialtion_method = Convert.ToString(TempData["is_based_depretialtion_method"]);

            if (columnarray.Contains("SrNo") == false)
            {
                return false;
            }
            if (is_based_depretialtion_method == "wdv_slm")
            {
                if (columnarray.Contains("Asset Code") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Dep Area") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Original Cost") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Accumlated Depreciation") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Net value") == false)
                {
                    return false;
                }
            }
   
            if (columnarray.Contains("Capitalization Date") == false)
            {
                return false;
            }

            if (is_based_depretialtion_method == "block")
            {
                if (columnarray.Contains("ASSET Class") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Net WDV value") == false)
                {
                    return false;
                }
            }
            return true;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_asset_initial_data_header_vm vm = _initialupload.Get((int)id);
            if (vm == null)
            {
                return HttpNotFound();
            }

            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("INITIALUPLOAD"), "document_numbring_id", "category");

            return View(vm);
        }
    }
}