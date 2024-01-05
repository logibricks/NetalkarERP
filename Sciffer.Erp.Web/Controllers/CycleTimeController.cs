using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using System.IO;
using Excel;
using System.Data;
using System.Text.RegularExpressions;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Implementation;

namespace Sciffer.Erp.Web.Controllers
{
    public class CycleTimeController : Controller
    {
        private readonly ScifferContext _scifferContext;
        private readonly ICycleTimeService _cycletimeService;
        private readonly IGenericService _Generic;
        private
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
        public CycleTimeController(ScifferContext scifferContext, CycleTimeService cycletimeService, IGenericService gen)
        {
            _scifferContext = scifferContext;
            _cycletimeService = cycletimeService;
            _Generic = gen;
        }
        // GET: CycleTime

        [CustomAuthorizeAttribute("CYT")]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorizeAttribute("CYT")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cycletime = _cycletimeService.Get((int)id);
            if (cycletime == null)
            {
                return HttpNotFound();
            }
            ViewBag.machinelist = new SelectList(_Generic.GetallMachineList(), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.processlist = new SelectList(_Generic.GetProcessList(), "operation_id", "process_code");
            return View(cycletime);
        }

        [HttpPost]
        public ActionResult Edit(cycle_time_excel cycle_time_excel)
        {
            List<cycle_time_excel> cycle_Time_Excels = new List<cycle_time_excel>();

            cycle_Time_Excels.Add(cycle_time_excel);

            var issaved = _cycletimeService.AddExcel(cycle_Time_Excels);
            if (issaved)
            {
                TempData["data"] = "Saved";
                return Json("Saved", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BlockCycleTime(int cyclet_time_id)
        {
            var issaved = _cycletimeService.BlockCycleTime(cyclet_time_id);
            if (issaved)
            {
                TempData["data"] = "Saved";
                return Json("Saved", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public Boolean ValidateVendorExcelColumns(string[] VendorColumnArray)
        {
            if (VendorColumnArray.Contains("ItemName") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Operation") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Machine") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CycleTime") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Other") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("LoadingUnloading") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("EffectiveDate") == false)
            {
                return false;
            }
            //if (VendorColumnArray.Contains("IncentiveRate") == false)
            //{
            //    return false;
            //}

            return true;
        }
        public ActionResult UploadFiles()
        {
            var rowCount = 0;
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
                    int contcol = 0, vencol = 0, glcol = 0, procol = 0;
                    string uploadtype = Request.Params[0];
                    List<cycle_time_excel> cycle_time_excel = new List<cycle_time_excel>();
                    if (result.Tables.Count == 0)
                    {
                        errorList++;
                        error[error.Length - 1] = "File is Empty!";
                        errorMessage = "File is Empty!";
                    }
                    else
                    {
                        foreach (DataTable sheet in result.Tables)
                        {

                            if (sheet.TableName == "CycleTimeDetails")
                            {
                                string[] VendorColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    VendorColumnArray[vencol] = ary1.ToString();
                                    vencol = vencol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    var count = TempSheetRows.Count();

                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        rowCount++;

                                        var col1 = a[Array.IndexOf(VendorColumnArray, "ItemName")].ToString();
                                        var col2 = a[Array.IndexOf(VendorColumnArray, "Operation")].ToString();
                                        var col3 = a[Array.IndexOf(VendorColumnArray, "Machine")].ToString();
                                        var col4 = a[Array.IndexOf(VendorColumnArray, "CycleTime")].ToString();
                                        var col5 = a[Array.IndexOf(VendorColumnArray, "LoadingUnloading")].ToString();
                                        var col6 = a[Array.IndexOf(VendorColumnArray, "EffectiveDate")].ToString();
                                        var col7 = a[Array.IndexOf(VendorColumnArray, "IncentiveRate")].ToString();
                                        if (col1 == "" && col2 == "" && col3 == "" && col4 == "" && col5 == "" && col6 == "")
                                        {
                                            break;
                                        }


                                        if (ValidateVendorExcelColumns(VendorColumnArray) == true)
                                        {
                                            cycle_time_excel IDVM = new cycle_time_excel();
                                            var item_name = a[Array.IndexOf(VendorColumnArray, "ItemName")].ToString();
                                            if (item_name == "")
                                            {
                                                errorList++;
                                                error[error.Length - 1] = item_name.ToString();
                                                errorMessage = "Add Item Name.";
                                            }
                                            else
                                            {
                                                var item_id = _scifferContext.REF_ITEM.Where(x => x.ITEM_NAME == item_name).FirstOrDefault();
                                                if (item_id == null)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = item_name.ToString();
                                                    errorMessage = item_name + " not found.";
                                                }
                                                else
                                                {
                                                    IDVM.item_id = item_id.ITEM_ID;
                                                }
                                            }
                                            var operation_name = a[Array.IndexOf(VendorColumnArray, "Operation")].ToString();
                                            if (operation_name == "")
                                            {
                                                errorList++;
                                                error[error.Length - 1] = operation_name.ToString();
                                                errorMessage = "Add Operation.";
                                            }
                                            else
                                            {
                                                var operation_id = _scifferContext.ref_mfg_process.Where(x => x.process_description == operation_name).FirstOrDefault();
                                                if (operation_id == null)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = operation_name.ToString();
                                                    errorMessage = operation_name + " not found.";
                                                }
                                                else
                                                {
                                                    IDVM.operation_id = operation_id.process_id;
                                                }
                                            }
                                            var machine_name = a[Array.IndexOf(VendorColumnArray, "Machine")].ToString();
                                            if (machine_name == "")
                                            {
                                                errorList++;
                                                error[error.Length - 1] = machine_name.ToString();
                                                errorMessage = "Add Machine.";
                                            }
                                            else
                                            {
                                                var machine_id = _scifferContext.ref_machine.Where(x => x.machine_name == machine_name).FirstOrDefault();
                                                if (machine_id == null)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = machine_name.ToString();
                                                    errorMessage = machine_name + " not found.";
                                                }
                                                else
                                                {
                                                    IDVM.machine_id = machine_id.machine_id;
                                                }
                                            }
                                            var cycle_time = a[Array.IndexOf(VendorColumnArray, "CycleTime")].ToString();
                                            if (cycle_time == "")
                                            {
                                                errorList++;
                                                error[error.Length - 1] = cycle_time.ToString();
                                                errorMessage = "Add Cycle Time.";
                                            }
                                            else
                                            {
                                                IDVM.cycle_time = int.Parse(cycle_time);
                                            }
                                            var loading_unloading = a[Array.IndexOf(VendorColumnArray, "LoadingUnloading")].ToString();
                                            if (loading_unloading == "")
                                            {
                                                IDVM.loading_unloading = 0;
                                            }
                                            else
                                            {
                                                IDVM.loading_unloading = int.Parse(loading_unloading);
                                            }
                                            var Others = a[Array.IndexOf(VendorColumnArray, "Other")].ToString();
                                            if (Others == "")
                                            {
                                                IDVM.other = 0;
                                            }
                                            else
                                            {
                                                IDVM.other = int.Parse(Others);
                                            }
                                            var effective_date = a[Array.IndexOf(VendorColumnArray, "EffectiveDate")].ToString();
                                            if (effective_date == null)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = effective_date.ToString();
                                                errorMessage = "Add Effective Date.";
                                            }
                                            else
                                            {
                                                IDVM.effective_date = DateTime.Parse(effective_date);
                                            }
                                            var incentive_rate = a[Array.IndexOf(VendorColumnArray, "IncentiveRate")].ToString();
                                            IDVM.incentive_rate = 0;

                                            cycle_time_excel.Add(IDVM);

                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Check Headers name.";
                                            errorMessage = "Check header !";
                                        }
                                    }

                                }

                            }

                        }
                    }
                    excelReader.Close();

                    if (errorMessage == "")
                    {

                        var isSucess = _cycletimeService.AddExcel(cycle_time_excel);
                        if (isSucess)
                        {
                            errorMessage = "success";
                            return Json(errorMessage, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            errorMessage = "Failed";
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
            //return Json(new { Status = Message, text = errorList, error = error, errorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
            return Json(errorList + " - " + errorMessage, JsonRequestBehavior.AllowGet);
        }
    }
}