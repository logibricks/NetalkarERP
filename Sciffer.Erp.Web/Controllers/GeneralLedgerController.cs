using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using System.Web;
using System.Linq;
using System.IO;
using Excel;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class GeneralLedgerController : Controller
    {
        private readonly IGeneralLedgerService _general;
        private readonly IGLAccountTypeService _gl;
        private readonly IGenericService _Generic;
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public GeneralLedgerController(IGeneralLedgerService general, IGenericService gen, IGLAccountTypeService gl)
        {
            _general = general;
            _Generic = gen;
            _gl = gl;
        }

        // GET: GeneralLedger
        [CustomAuthorizeAttribute("GL")]
        public ActionResult Index(string name,string searchstring)
        {
            if (name == null)
            {
                name = "Assets";
            }
            var gen_led_id = _general.GenID(name);
            var res = _general.GetTreeVeiwList(gen_led_id);            
            if (!String.IsNullOrEmpty(searchstring))
            {
               
            }
            ViewBag.datasource = res;
            return View(res);
        }

        public ActionResult GetTree(string name)
        {
            if (name == null)
            {
                name = "Assests";
            }
            var gen_led_id = _general.GenID(name);
            var res = _general.GetTreeVeiwList(gen_led_id);
            return View(res);
        }
        public ActionResult GetGeneralLedger()
        {
            var res = _general.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _general.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult InlineDelete(int key)
        {
            var des = _general.Delete(key);
            return Json(des,JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute("GL")]
        public ActionResult Create()
        {
            ViewBag.accounttype = new SelectList(_gl.GetAll(), "gl_account_type_id", "gl_account_type_description");
            ViewBag.generalledger = new SelectList(_general.GetAll(), "gl_ledger_code", "gl_ledger_name");
            return PartialView("Create_PV", ViewBag);

        }

        [HttpPost]
        public ActionResult Create(ref_general_ledger value)
        {
            var x = value.gl_account_type;
            var res = _gl.Get(x);
            
            if (value.gl_parent_ledger_code == "0")
            {
                value.gl_level = 1; 
            }
            else
            {
               var gl_level = _general.ParentCode(value.gl_parent_ledger_code).Split('-');
               value.gl_level = (int.Parse(gl_level[3]) + 1);
            }      
            
            var isValid = _general.Add(value);
            if (isValid == true)
            {
                return RedirectToAction("Index", new { name = res.gl_account_type_description });
            }

            ViewBag.accounttype = new SelectList(_gl.GetAll(), "gl_account_type_id", "gl_account_type_description");
            ViewBag.generalledger = new SelectList(_general.GetAll(), "gl_ledger_code", "gl_ledger_name");

            //ViewBag.generalledger = null;
            return RedirectToAction("Index");
        }

        [CustomAuthorizeAttribute("GL")]
        public ActionResult Edit(int id)
        {

            ViewBag.accounttype = new SelectList(_gl.GetAll(), "gl_account_type_id", "gl_account_type_description");
            ViewBag.generalledger = new SelectList(_general.GetAll(), "gl_ledger_code", "gl_ledger_name_drop");
            ref_general_ledger journal_entry = _general.Get(id);

            ViewBag.journal_entry = journal_entry;

            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            //return View(journal_entry);
            return PartialView("Edit_PV", ViewBag);
        }

        public ActionResult Delete(int id)
        {
            _general.Delete(id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_general_ledger vm)
        {
            var x = vm.gl_account_type;
            var res = _gl.Get(x);
            var gl_level = _general.ParentCode(vm.gl_parent_ledger_code).Split('-');
            vm.gl_level = (int.Parse(gl_level[3]) + 1);
           // if(vm.gl_head_account==1 || vm.gl_head_account == 2) { }
            ViewBag.accounttype = new SelectList(_gl.GetAll(), "gl_account_type_id", "gl_account_type_description");
            ViewBag.generalledger = new SelectList(_general.GetAll(), "gl_ledger_code", "gl_ledger_name");

            var isValid = _general.Update(vm);
            if (isValid == true)
            {
                return RedirectToAction("Index", new { name = res.gl_account_type_description });
            }

            return RedirectToAction("Index");
        }

        public ActionResult FillParentList(int id)
        {
            var paymentService = _general.Get_Parent_Ledger(id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            for (int m = 0; m < Request.Files.Count; m++)
            {
                HttpPostedFileBase file = Request.Files[m]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
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

                    List<ref_ledger_vm> ref_ledger_vm = new List<ref_ledger_vm>();
                    //List<customer_balance_detail_VM> bldetails = new List<customer_balance_detail_VM>();
                    if (result.Tables.Count == 0)
                    {
                        errorList++;
                        error[error.Length - 1] = "File is Empty!";
                        errorMessage = "File is Empty!";
                    }
                    else
                    {
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
                                    string sr_no = excelReader.GetString(Array.IndexOf(columnarray, "SrNo"));
                                    if (sr_no != null)                                  
                                    {
                                        if (ref_ledger_vm.Count != 0)
                                        {
                                            ref_ledger_vm IDVM = new ref_ledger_vm();
                                            IDVM.gl_account_type_name = excelReader.GetString(Array.IndexOf(columnarray, "GeneralLedgerType")).ToLower();
                                            var general_ledger_type_id = _gl.GetID(IDVM.gl_account_type_name);
                                            if (general_ledger_type_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IDVM.gl_account_type_name;
                                                errorMessage = IDVM.gl_account_type_name + "You have added Incorrect General ledger type";
                                            }
                                            else
                                            {
                                                IDVM.gl_account_type = general_ledger_type_id;
                                            }

                                            var gl_ledger_code = excelReader.GetString(Array.IndexOf(columnarray, "GeneralLedgerCode")).Trim();
                                            var gl_ledger_code_duplicate = ref_ledger_vm.Where(x => x.gl_ledger_code == gl_ledger_code).FirstOrDefault();
                                            if (gl_ledger_code_duplicate == null)
                                            {
                                                IDVM.gl_ledger_code = gl_ledger_code;
                                            }
                                            else
                                            {
                                                errorList++;
                                                error[error.Length - 1] = gl_ledger_code;
                                                errorMessage = gl_ledger_code + "GL Ledger Code is Already Exist";
                                            }
                                            IDVM.gl_ledger_name = excelReader.GetString(Array.IndexOf(columnarray, "GeneralLedgerName")).Trim();
                                            var head_account = excelReader.GetString(Array.IndexOf(columnarray, "HeadAccount")).ToLower();
                                            if (head_account == "head")
                                            {
                                                IDVM.gl_head_account = 1;
                                            }
                                            else if (head_account == "account")
                                            {
                                                IDVM.gl_head_account = 2;
                                            }
                                            else
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IDVM.gl_ledger_name;
                                                errorMessage = IDVM.gl_ledger_name + "Add Correct Spell of head Or account";
                                            }
                                            var parent_ledger_code = excelReader.GetString(Array.IndexOf(columnarray, "ParentLedgerCode"));
                                            if (parent_ledger_code != null)
                                            {
                                                var parent_code = _general.ParentCode(parent_ledger_code);
                                                if (parent_code == "")
                                                {
                                                    var excel_parent_code = ref_ledger_vm.Where(x => x.gl_ledger_code == parent_ledger_code && x.gl_head_account == 1).FirstOrDefault();
                                                    if (excel_parent_code == null)
                                                    {
                                                        errorList++;
                                                        errorMessage = parent_ledger_code +"Parent Code Not Exist!";
                                                        error[error.Length - 1] = parent_ledger_code;
                                                    }
                                                    else
                                                    {
                                                        if (excel_parent_code.gl_account_type_name == IDVM.gl_account_type_name)
                                                        {
                                                            IDVM.gl_parent_ledger_code = excel_parent_code.gl_ledger_code;
                                                            IDVM.gl_level = excel_parent_code.gl_level + 1;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = parent_ledger_code;
                                                            errorMessage = parent_ledger_code+ "Parent Ledger Code Added has Different Gl Account Type!";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    var data = parent_code.Split('-');
                                                    if (data[1] == IDVM.gl_account_type_name)
                                                    {
                                                        IDVM.gl_parent_ledger_code = data[0];
                                                        IDVM.gl_level = (int.Parse(data[3]) + 1);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = parent_ledger_code + IDVM.gl_account_type_name;
                                                        errorMessage = parent_ledger_code + IDVM.gl_account_type_name + "Parent Ledger Code Added has Different Gl Account Type!";
                                                    }
                                                    //IDVM.gl_parent_ledger_code = parent_code;

                                                }

                                            }
                                            else
                                            {
                                                errorList++;
                                                error[error.Length - 1] = parent_ledger_code + "is null";
                                                errorMessage = "You have Added null value!";
                                            }

                                            //var allow_manual_entry = excelReader.GetString(Array.IndexOf(columnarray, "AllowManualEntry")).ToLower();
                                            //if (allow_manual_entry == "yes")
                                            //{
                                            //    IDVM.allow_manual_entry = true;
                                            //}
                                            //else if (allow_manual_entry == "no")
                                            //{
                                            //    IDVM.allow_manual_entry = false;
                                            //}
                                            //else
                                            //{
                                            //    errorList++;
                                            //    error[error.Length - 1] = allow_manual_entry;
                                            //    errorMessage = "Add Allow Manual Entry yes Or no Only";
                                            //}
                                            ref_ledger_vm.Add(IDVM);

                                        }
                                        else
                                        {
                                            ref_ledger_vm IDVM = new ref_ledger_vm();
                                            IDVM.gl_account_type_name = excelReader.GetString(Array.IndexOf(columnarray, "GeneralLedgerType")).ToLower().Trim();
                                            var general_ledger_type_id = _gl.GetID(IDVM.gl_account_type_name);
                                            if (general_ledger_type_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IDVM.gl_account_type_name;
                                                errorMessage = IDVM.gl_account_type_name + "You have added Incorrect general ledger type ";
                                            }
                                            else
                                            {
                                                IDVM.gl_account_type = general_ledger_type_id;
                                            }


                                            IDVM.gl_ledger_code = excelReader.GetString(Array.IndexOf(columnarray, "GeneralLedgerCode")).Trim();
                                            var head_account = excelReader.GetString(Array.IndexOf(columnarray, "HeadAccount")).ToLower();
                                            if (head_account == "head")
                                            {
                                                IDVM.gl_head_account = 1;
                                            }
                                            else if (head_account == "account")
                                            {
                                                IDVM.gl_head_account = 2;
                                            }
                                            else
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IDVM.gl_ledger_code;
                                                errorMessage = head_account + "Add Correct Spell of head Or account";
                                            }
                                            IDVM.gl_ledger_name = excelReader.GetString(Array.IndexOf(columnarray, "GeneralLedgerName"));
                                            var parent_ledger_code = excelReader.GetString(Array.IndexOf(columnarray, "ParentLedgerCode"));
                                            if (parent_ledger_code != null)
                                            {
                                                var parent_code = _general.ParentCode(parent_ledger_code);
                                                if (parent_code == "")
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = IDVM.gl_ledger_name;
                                                    errorMessage = parent_ledger_code;
                                                }
                                                else
                                                {
                                                    var data = parent_code.Split('-');
                                                    if (data[1] == IDVM.gl_account_type_name)
                                                    {
                                                        IDVM.gl_parent_ledger_code = data[0];
                                                        IDVM.gl_level = (int.Parse(data[3]) + 1);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = IDVM.gl_ledger_name + "diff LG type";
                                                        errorMessage = "Parent Ledger Code Added has Different Gl Account Type!";
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IDVM.gl_ledger_name + "is null";
                                                errorMessage = "You have Added null value!";
                                            }
                                            ref_ledger_vm.Add(IDVM);
                                        }
                                    }
                                }
                                else
                                {
                                    errorMessage = "Check header name!";
                                }
                            }
                            row = row + 1;


                        }
                    }
                    if (ref_ledger_vm.Count == 0)
                    {
                        errorMessage = "File check!";
                    }
                    excelReader.Close();
                    if (errorMessage == "")
                    {
                        var isSucess = _general.AddExcel(ref_ledger_vm);
                        if (isSucess == "Saved")
                        {
                            errorMessage = "success";
                            return Json(errorMessage, JsonRequestBehavior.AllowGet);
                        }
                        else if (isSucess == "Failed")
                        {
                            errorMessage = "Failed";
                        }
                        else
                        {
                            errorList++;
                            errorMessage = "General Ledger Code Already Exist!";
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
        public Boolean ValidateExcelColumns(string[] columnarray)
        {
            if (columnarray.Contains("SrNo") == false)
            {
                return false;
            }
            if (columnarray.Contains("GeneralLedgerType") == false)
            {
                return false;
            }
            if (columnarray.Contains("GeneralLedgerCode") == false)
            {
                return false;
            }
            if (columnarray.Contains("GeneralLedgerName") == false)
            {
                return false;
            }
            
            if (columnarray.Contains("HeadAccount") == false)
            {
                return false;
            }
            if (columnarray.Contains("ParentLedgerCode") == false)
            {
                return false;
            }
            //if (columnarray.Contains("AllowManualEntry") == false)
            //{
            //    return false;
            //}
            return true;
        }

        public ActionResult ExportData()
        {
            
            var query = _general.GetExport();
            var grid = new GridView();
            grid.DataSource = query;
            grid.DataBind();
            if (grid.Rows.Count > 1)
            {
                for (int i = 0; i <= grid.Rows.Count - 1; i++)
                {
                    grid.Rows[i].Cells[0].Text = (i + 1).ToString();
                }
            }
            Response.ClearContent();
            Response.ContentType = "application/vnd.openxmlformats-     officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GeneralLedger.xls");
            //Response.AddHeader("content-disposition", "attachement; filename=GeneralLedger.xlsx");
            //Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index", "ExportData");
        }

        public ActionResult Child(int id)
        {
            ViewBag.accounttype = new SelectList(_gl.GetAll(), "gl_account_type_id", "gl_account_type_description");
            ViewBag.generalledger = new SelectList(_general.GetAll(), "gl_ledger_code", "gl_ledger_name_drop");
            ref_ledger_vm journal_entry = _general.GetChild(id);
            ViewBag.journal_entry = journal_entry;
            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return PartialView("Child_PV", ViewBag);
        }


    }
}
