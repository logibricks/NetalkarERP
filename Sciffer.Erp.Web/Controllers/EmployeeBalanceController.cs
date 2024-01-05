using Excel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class EmployeeBalanceController : Controller
    {
        private readonly IEmployeeBalanceService _employeeBalance;
        private readonly IEmployeeService _employee;
        private readonly IDocumentNumbringService _documentNumber;
        private readonly IGeneralLedgerService _generalLedger;
        private readonly IAmountTypeService _amount;
        private readonly IGenericService _generic;
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";

        public EmployeeBalanceController(IAmountTypeService amount, IGeneralLedgerService generalLedger, IEmployeeBalanceService employeeBalance, IEmployeeService employee, IDocumentNumbringService documentNumber, IGenericService gen)
        {
            _generic = gen;
            _employeeBalance = employeeBalance;
            _employee = employee;
            _documentNumber = documentNumber;
            _generalLedger = generalLedger;
            _amount = amount;
        }

        public ActionResult Index()
        {
            var data = _employeeBalance.GetAll();
            ViewBag.DataSource = _employeeBalance.GetAll();
            return View();
        }
        public ActionResult Delete(int key)
        {
            var data = _employeeBalance.Delete(key);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public void ExportToExcel(string GridModel)
        {
            ExcelExport exp = new ExcelExport();
            var DataSource = _employeeBalance.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "EmployeeBalance.xlsx", ExcelVersion.Excel2010, false, false, "flat-saffron");
        }

        public void ExportToWord(string GridModel)
        {
            WordExport exp = new WordExport();
            var DataSource = _employeeBalance.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "EmployeeBalance.docx", false, false, "flat-saffron");
        }

        public void ExportToPdf(string GridModel)
        {
            PdfExport exp = new PdfExport();
            var DataSource = _employeeBalance.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "EmployeeBalance.pdf", false, false, "flat-saffron");
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
        public ActionResult Details(int id)
        {
            ref_employee_balance_VM journal_entry = _employeeBalance.GetDetails(id);
            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            ViewBag.glcodelist = new SelectList(_generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            var emp = _employee.GetEmployeeList();
            ViewBag.employee = new SelectList(emp, "employee_id", "employee_name");
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(17), "document_numbring_id", "category");
            var gen = _generic.getOffsetAccount("Opening Balance Employee Offset", 9);
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            ViewBag.amount = _amount.GetAll();
            return View(journal_entry);
        }

        public ActionResult Create()
        {
            var gen = _generic.getOffsetAccount("Opening Balance Employee Offset", 9);
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            var emp = _employee.GetEmployeeList();
            ViewBag.employee = new SelectList(emp, "employee_id", "employee_name");
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(17), "document_numbring_id", "category");
            ViewBag.amount = _amount.GetAll();
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_employee_balance_VM vm)
        {
            var offset_account = _generic.getOffsetAccount("Opening Balance Employee Offset", 9);
            vm.offset_account_id = offset_account[0].gl_ledger_id;
            if (ModelState.IsValid)
            {
                var isValid = _employeeBalance.Add(vm);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.employee = _employee.GetEmployeeList();
            ViewBag.offsetAccount = _generalLedger.GetAccountGeneralLedger();
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(17), "document_numbring_id", "category");
            ViewBag.amount = _amount.GetAll();
            return View(vm);
        }
        public ActionResult Edit(int id)
        {

            ViewBag.employee = _employee.GetEmployeeList();
            ViewBag.DocumentNumber = _documentNumber.GetAll();
            ViewBag.amount = _amount.GetAll();
            ref_employee_balance_VM journal_entry = _employeeBalance.Get(id);

            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_employee_balance_VM journal_entry, FormCollection fc)
        {
            string products;
            products = fc["productdetail"];
            string[] emptyStringArray = new string[0];
            try
            {
                emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }

            List<ref_employee_balance_detail> journal_list = new List<ref_employee_balance_detail>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_employee_balance_detail item = new ref_employee_balance_detail();
                item.employee_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                item.ref1 = emptyStringArray[i].Split(new char[] { ',' })[5];
                item.ref2 = emptyStringArray[i].Split(new char[] { ',' })[6];
                item.ref3 = emptyStringArray[i].Split(new char[] { ',' })[7];
                item.doc_date = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[8]);
                item.due_date = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[9]);
                item.amount = Double.Parse(emptyStringArray[i].Split(new char[] { ',' })[10]);
                item.amount_type_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[11]);
                item.line_remark = emptyStringArray[i].Split(new char[] { ',' })[13];
                if (emptyStringArray[i].Split(new char[] { ',' })[0] == "")
                {
                    item.employee_balance_detail_id = 0;
                }
                else
                {
                    item.employee_balance_detail_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                }
                journal_list.Add(item);
            }
            journal_entry.ref_employee_balance_detail = journal_list;
            if (ModelState.IsValid)
            {
                var isValid = _employeeBalance.Update(journal_entry);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.employee = _employee.GetEmployeeList();
            ViewBag.DocumentNumber = _documentNumber.GetAll();
            ViewBag.amount = _amount.GetAll();
            return View(journal_entry);
        }

        [HttpPost]

        public ActionResult UploadFiles()
        {
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
                    List<employee_balance_VM> employee_balance_VM = new List<employee_balance_VM>();
                    List<employee_balance_details_VM> bldetails = new List<employee_balance_details_VM>();
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
                                    var offset_account = _generic.getOffsetAccount("Opening Balance Employee Offset", 9);
                                    if (employee_balance_VM.Count != 0)
                                    {
                                        var zz = employee_balance_VM.Where(z => z.posting_date == DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "PostingDate"))) && z.header_remarks == excelReader.GetString(Array.IndexOf(columnarray, "HeaderRemarks"))).FirstOrDefault();
                                        if (zz == null)
                                        {
                                            errorMessage = "Add same Header Remarks and Posting Date.";
                                            errorList++;
                                            error[error.Length - 1] = "Add same Header Remarks and Posting Date.";
                                        }
                                        else
                                        {
                                            employee_balance_details_VM IDVM = new employee_balance_details_VM();
                                            var employee_code = excelReader.GetString(Array.IndexOf(columnarray, "EmployeeCode"));
                                            var employee_id = _generic.GetEmployeeID(employee_code);
                                            if (employee_id == 0)
                                            {
                                                errorMessage = employee_code + "not found!";
                                                errorList++;
                                                error[error.Length - 1] = employee_code;
                                            }
                                            else
                                            {
                                                IDVM.employee_id = employee_id;
                                            }
                                            IDVM.ref1 = excelReader.GetString(Array.IndexOf(columnarray, "Ref1"));
                                            IDVM.ref2 = excelReader.GetString(Array.IndexOf(columnarray, "Ref2"));
                                            IDVM.ref3 = excelReader.GetString(Array.IndexOf(columnarray, "Ref3"));
                                            var doc_date = excelReader.GetString(Array.IndexOf(columnarray, "DocumentDate"));
                                            if (doc_date == null)
                                            {
                                                IDVM.doc_date = zz.posting_date;
                                            }
                                            else
                                            {
                                                IDVM.doc_date = DateTime.Parse(doc_date);
                                            }
                                            var due_date = excelReader.GetString(Array.IndexOf(columnarray, "DueDate"));
                                            if (due_date == null)
                                            {
                                                IDVM.due_date = zz.posting_date;
                                            }
                                            else
                                            {
                                                IDVM.due_date = DateTime.Parse(due_date);
                                            }

                                            IDVM.amount = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Amount")));

                                            var amount_type = excelReader.GetString(Array.IndexOf(columnarray, "AmountType"));
                                            var amount_type_id = _amount.GetID(amount_type);
                                            if (amount_type_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = amount_type;
                                                errorMessage = amount_type + " Not found!";
                                            }
                                            else
                                            {
                                                IDVM.amount_type_id = amount_type_id;
                                            }
                                            IDVM.line_remark = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));
                                            IDVM.offset_account = offset_account[0].gl_ledger_code;
                                            bldetails.Add(IDVM);
                                        }
                                    }
                                    else
                                    {
                                        employee_balance_VM IBS = new employee_balance_VM();
                                        var category_id = _generic.GetCategoryList(17);
                                        IBS.category_id = category_id[0].document_numbring_id;
                                        IBS.doc_number = _generic.GetDocumentNumbering(IBS.category_id);
                                        IBS.offset_account = offset_account[0].gl_ledger_code;
                                        var offset_account_id = _generalLedger.GetID(IBS.offset_account);
                                        if (offset_account_id == 0)
                                        {
                                            errorMessage = IBS.offset_account + " notfound!";
                                            errorList++;
                                            error[error.Length - 1] = IBS.offset_account;
                                        }
                                        else
                                        {
                                            IBS.offset_account_id = offset_account_id;
                                        }
                                        IBS.posting_date = DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "PostingDate")));
                                        IBS.header_remarks = excelReader.GetString(Array.IndexOf(columnarray, "HeaderRemarks"));

                                        employee_balance_details_VM IDVM = new employee_balance_details_VM();
                                        var employee_code = excelReader.GetString(Array.IndexOf(columnarray, "EmployeeCode"));
                                        var employee_id = _generic.GetEmployeeID(employee_code);
                                        if (employee_id == 0)
                                        {
                                            error[error.Length - 1] = employee_code;
                                            errorMessage = employee_code + " not found";
                                            errorList++;
                                        }
                                        else
                                        {
                                            IDVM.employee_id = employee_id;
                                        }


                                        IDVM.ref1 = excelReader.GetString(Array.IndexOf(columnarray, "Ref1"));
                                        IDVM.ref2 = excelReader.GetString(Array.IndexOf(columnarray, "Ref2"));
                                        IDVM.ref3 = excelReader.GetString(Array.IndexOf(columnarray, "Ref3"));
                                        var doc_date = excelReader.GetString(Array.IndexOf(columnarray, "DocumentDate"));
                                        if (doc_date == null)
                                        {
                                            IDVM.doc_date = IBS.posting_date;
                                        }
                                        else
                                        {
                                            IDVM.doc_date = DateTime.Parse(doc_date);
                                        }
                                        var due_date = excelReader.GetString(Array.IndexOf(columnarray, "DueDate"));
                                        if (due_date == null)
                                        {
                                            IDVM.due_date = IBS.posting_date;
                                        }
                                        else
                                        {
                                            IDVM.due_date = DateTime.Parse(due_date);
                                        }
                                        IDVM.amount = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Amount")));

                                        var amount_type = excelReader.GetString(Array.IndexOf(columnarray, "AmountType"));
                                        var amount_type_id = _amount.GetID(amount_type);
                                        if (amount_type_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = amount_type;
                                            errorMessage = amount_type + " not found!";
                                        }
                                        else
                                        {
                                            IDVM.amount_type_id = amount_type_id;
                                        }

                                        IDVM.line_remark = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));

                                        IDVM.offset_account = IBS.offset_account;
                                        bldetails.Add(IDVM);
                                        employee_balance_VM.Add(IBS);
                                    }

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
                        var isSucess = _employeeBalance.AddExcel(employee_balance_VM, bldetails);
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
            if (columnarray.Contains("SrNo") == false)
            {
                return false;
            }
            //if (columnarray.Contains("Offset Account") == false)
            //{
            //    return false;
            //}
            if (columnarray.Contains("PostingDate") == false)
            {
                return false;
            }
            if (columnarray.Contains("HeaderRemarks") == false)
            {
                return false;
            }
            if (columnarray.Contains("EmployeeCode") == false)
            {
                return false;
            }

            if (columnarray.Contains("Ref1") == false)
            {
                return false;
            }
            if (columnarray.Contains("Ref2") == false)
            {
                return false;
            }
            if (columnarray.Contains("Ref3") == false)
            {
                return false;
            }
            if (columnarray.Contains("DocumentDate") == false)
            {
                return false;
            }
            if (columnarray.Contains("DueDate") == false)
            {
                return false;
            }
            if (columnarray.Contains("Amount") == false)
            {
                return false;
            }
            if (columnarray.Contains("AmountType") == false)
            {
                return false;
            }
            if (columnarray.Contains("LineRemarks") == false)
            {
                return false;
            }
            return true;
        }
    }
}