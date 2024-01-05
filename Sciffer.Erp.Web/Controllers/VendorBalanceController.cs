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
    public class VendorBalanceController : Controller
    {
        private readonly IVendorBalanceService _vendorBalance;
        private readonly IVendorService _vendor;
        private readonly IDocumentNumbringService _DocumentNumbering;
        private readonly IGeneralLedgerService _generalLedger;
        private readonly IAmountTypeService _amount;
        private readonly IGenericService _generic;
        string[] error = new string[30];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public VendorBalanceController(IAmountTypeService amount, IGeneralLedgerService generalLedger, IVendorBalanceService vendorBalance, IVendorService vendor, IDocumentNumbringService documrntNumber, IGenericService gen)
        {
            _DocumentNumbering = documrntNumber;
            _vendorBalance = vendorBalance;
            _vendor = vendor;
            _generalLedger = generalLedger;
            _amount = amount;
            _generic = gen;
        }
        public ActionResult Index()
        {
            var data = _vendorBalance.GetAll();
            ViewBag.DataSource = _vendorBalance.GetAll();

            return View();
        }
        public ActionResult Delete(int key)
        {
            var data = _vendorBalance.Delete(key);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public void ExportToExcel(string GridModel)
        {
            ExcelExport exp = new ExcelExport();
            var DataSource = _vendorBalance.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "VendorBalance.xlsx", ExcelVersion.Excel2010, false, false, "flat-saffron");
        }

        public void ExportToWord(string GridModel)
        {
            WordExport exp = new WordExport();
            var DataSource = _vendorBalance.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "VendorBalance.docx", false, false, "flat-saffron");
        }

        public void ExportToPdf(string GridModel)
        {
            PdfExport exp = new PdfExport();
            var DataSource = _vendorBalance.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "VendorBalance.pdf", false, false, "flat-saffron");
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
            ref_vendor_balance_VM journal_entry = _vendorBalance.GetDetails(id);
            ViewBag.vendor = _vendor.GetVendorList();
            var gen = _generalLedger.GetAccountGeneral();
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            ViewBag.amount = _amount.GetAll();
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(13), "document_numbring_id", "category");
            return View(journal_entry);
        }

        public ActionResult Create()
        {

            var ven = _vendor.GetVendorList();
            ViewBag.Vendor = new SelectList(ven, "VENDOR_ID", "VENDOR_NAME");
            var gen = _generic.getOffsetAccount("Opening Balance Vendor Offset", 9);
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            ViewBag.amount = _amount.GetAll();
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(13), "document_numbring_id", "category");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_vendor_balance_VM vm)
        {
            var offset_account = _generic.getOffsetAccount("Opening Balance Customer Offset", 9);
            vm.offset_account_id = offset_account[0].gl_ledger_id;

            if (ModelState.IsValid)
            {
                var isValid = _vendorBalance.Add(vm);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            var ven = _vendor.GetVendorList();
            ViewBag.vendor = new SelectList(ven, "VENDOR_ID", "VENDOR_NAME");
            var gen = _generalLedger.GetAccountGeneral();
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            ViewBag.amount = _amount.GetAll();
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(13), "document_numbring_id", "category");
            return View(vm);
        }
        public ActionResult Edit(int id)
        {

            ViewBag.vendor = _vendor.GetVendorList();
            ViewBag.DocumentNumber = _DocumentNumbering.GetAll();
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(13), "document_numbring_id", "category");
            ref_vendor_balance_VM journal_entry = _vendorBalance.Get(id);

            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_vendor_balance_VM journal_entry, FormCollection fc)
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

            List<ref_vendor_balance_details> journal_list = new List<ref_vendor_balance_details>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_vendor_balance_details item = new ref_vendor_balance_details();
                item.vendor_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                item.ref1 = emptyStringArray[i].Split(new char[] { ',' })[5];
                item.ref2 = emptyStringArray[i].Split(new char[] { ',' })[6];
                item.ref3 = emptyStringArray[i].Split(new char[] { ',' })[7];
                item.doc_date = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[8]);
                item.due_date = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[9]);
                item.amount = Double.Parse(emptyStringArray[i].Split(new char[] { ',' })[10]);
                item.amount_type_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[11]);
                item.line_remarks = emptyStringArray[i].Split(new char[] { ',' })[13];
                if (emptyStringArray[i].Split(new char[] { ',' })[0] == "")
                {
                    item.vendor_balance_detail_id = 0;
                }
                else
                {
                    item.vendor_balance_detail_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                }
                journal_list.Add(item);
            }
            journal_entry.ref_vendor_balance_details = journal_list;
            if (ModelState.IsValid)
            {
                var isValid = _vendorBalance.Update(journal_entry);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.vendor = _vendor.GetVendorList();
            ViewBag.DocumentNumber = _DocumentNumbering.GetAll();
            ViewBag.category_list = new SelectList(_generic.GetCategoryList(13), "document_numbring_id", "category");
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
                    int current_id = 0;
                    List<vendor_balance_VM> vendor_balance_VM = new List<vendor_balance_VM>();
                    List<vendor_balance_detail_VM> bldetails = new List<vendor_balance_detail_VM>();
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
                                    var offset_account = _generic.getOffsetAccount("Opening Balance Vendor Offset", 9);
                                    if (vendor_balance_VM.Count != 0)
                                    {
                                        var zz = vendor_balance_VM.Where(z => z.posting_date == DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "PostingDate"))) && z.header_remark == excelReader.GetString(Array.IndexOf(columnarray, "HeaderRemarks"))).FirstOrDefault();
                                        if (zz == null)
                                        {
                                            errorMessage = "Add same Header Remarks and Posting Date.";
                                            errorList++;
                                            error[error.Length - 1] = "Add same Header Remarks and Posting Date.";
                                        }
                                        else
                                        {
                                            vendor_balance_detail_VM IDVM = new vendor_balance_detail_VM();
                                            var vendor_code = excelReader.GetString(Array.IndexOf(columnarray, "VendorCode"));
                                            var vendor_id = _generic.GetVendorId(vendor_code);
                                            if (vendor_id == 0)
                                            {
                                                errorMessage = vendor_code + " not found!";
                                                errorList++;
                                                error[error.Length - 1] = vendor_code;
                                            }
                                            else
                                            {
                                                IDVM.vendor_id = vendor_id;
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
                                                errorMessage = amount_type + "not found!";
                                            }
                                            else
                                            {
                                                IDVM.amount_type_id = amount_type_id;
                                            }
                                            IDVM.line_remarks = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));
                                            IDVM.offset_account = zz.offset_account;
                                            var control_dawn_payment_account = excelReader.GetString(Array.IndexOf(columnarray, "Control/DownPaymentAccount"));
                                            if (control_dawn_payment_account == "Down Payment Account")
                                            {
                                                IDVM.control_dawn_payment_account = 2;
                                            }
                                            else
                                            {
                                                IDVM.control_dawn_payment_account = 1;
                                            }
                                            bldetails.Add(IDVM);
                                        }
                                    }
                                    else
                                    {
                                        vendor_balance_VM IBS = new vendor_balance_VM();
                                        var category_id = _generic.GetCategoryList(13);
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
                                        IBS.header_remark = excelReader.GetString(Array.IndexOf(columnarray, "HeaderRemarks"));

                                        vendor_balance_detail_VM IDVM = new vendor_balance_detail_VM();
                                        var vendor_code = excelReader.GetString(Array.IndexOf(columnarray, "VendorCode"));
                                        var vendor_id = _generic.GetVendorId(vendor_code);
                                        if (vendor_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = vendor_code;
                                            errorMessage = vendor_code + "not found!";
                                        }
                                        else
                                        {
                                            IDVM.vendor_id = vendor_id;
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
                                            errorMessage = amount_type + "not found!";
                                        }
                                        else
                                        {
                                            IDVM.amount_type_id = amount_type_id;
                                        }
                                        IDVM.line_remarks = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));
                                        IDVM.offset_account = IBS.offset_account;
                                        var control_dawn_payment_account = excelReader.GetString(Array.IndexOf(columnarray, "Control/DownPaymentAccount"));
                                        if (control_dawn_payment_account == "Down Payment Account")
                                        {
                                            IDVM.control_dawn_payment_account = 2;
                                        }
                                        else if(control_dawn_payment_account == "Control Account")
                                        {
                                            IDVM.control_dawn_payment_account = 1;
                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = amount_type;
                                            errorMessage = "Add Down Payment Account or Control Account!";
                                        }
                                        bldetails.Add(IDVM);
                                        vendor_balance_VM.Add(IBS);
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
                        var isSucess = _vendorBalance.AddExcel(vendor_balance_VM, bldetails);
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
            if (columnarray.Contains("VendorCode") == false)
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
            if (columnarray.Contains("Control/DownPaymentAccount") == false)
            {
                return false;
            }           
            return true;
        }
    }
}