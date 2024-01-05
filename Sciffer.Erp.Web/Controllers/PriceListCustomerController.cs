using Excel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class PriceListCustomerController : Controller
    {
        private readonly IPriceListCustomerService _priceList;
        private readonly ICustomerService _customer;
        private readonly IItemService _item;
        private readonly IGenericService _Generic;
        private readonly IUOMService _uom;
        string[] error = new string[30];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public PriceListCustomerController(IUOMService uom, IPriceListCustomerService priceList, ICustomerService customer, IItemService item, IGenericService generic)
        {
            _priceList = priceList;
            _customer = customer;
            _item = item;
            _Generic = generic;
            _uom = uom;
        }

        [CustomAuthorizeAttribute("PRLC")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _priceList.GetAll();
            return View();
        }


        public ActionResult Delete(int key)
        {
            var data = _priceList.Delete(key);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            ViewBag.Item = _item.GetItemList();
            ViewBag.Customer = _customer.GetCustomerList();
            ViewBag.UOM = _uom.GetAll();
            var journal_entry = _priceList.Get(id);
            ViewBag.customercodename = journal_entry.customer_code + "-" + journal_entry.customer_name;
            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
        }

        // GET: JournalEntry/Create
        [CustomAuthorizeAttribute("PRLC")]
        public ActionResult Create()
        {
            ViewBag.Item = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Customer = new SelectList(_Generic.GetCustomerforsearchdropdown(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.uom_list = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View();
        }

        // POST: JournalEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(price_list_customer_vm value)
        {
            if (ModelState.IsValid)
            {
                var isValid = _priceList.Add(value);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Item = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Customer = new SelectList(_Generic.GetCustomerforsearchdropdown(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.uom_list = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View(value);
        }

        //GET: JournalEntry/Edit/5
        [CustomAuthorizeAttribute("PRLC")]
        public ActionResult Edit(int id)
        {

            ViewBag.Item = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Customer = new SelectList(_Generic.GetCustomerforsearchdropdown(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.uom_list = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            var journal_entry = _priceList.Get(id);
            ViewBag.customercodename = journal_entry.customer_code + "-" + journal_entry.customer_name;
            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
        }

        // POST: JournalEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(price_list_customer_vm journal_entry)
        {
            if (ModelState.IsValid)
            {
                var isValid = _priceList.Update(journal_entry);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Item = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Customer = new SelectList(_Generic.GetCustomerforsearchdropdown(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.uom_list = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
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
                    string current_id = "";
                    List<ref_price_list_customer_vm> ref_price_list_customer_vm = new List<ref_price_list_customer_vm>();
                    List<ref_price_list_customer_detail_vm> ref_price_list_customer_detail_vm = new List<ref_price_list_customer_detail_vm>();
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
                                string sr_no = excelReader.GetString(Array.IndexOf(columnarray, "Sr No"));
                                if (sr_no != null)
                                {
                                    var customer_code = excelReader.GetString(Array.IndexOf(columnarray, "Customer Code"));
                                    if (ref_price_list_customer_vm.Count != 0)
                                    {
                                        var zz = ref_price_list_customer_vm.Where(z => z.customer_code == customer_code).FirstOrDefault();
                                        if (zz == null)
                                        {
                                            var z = new ref_price_list_customer_vm();
                                            z.customer_code = excelReader.GetString(Array.IndexOf(columnarray, "Customer Code"));
                                            var customer_id = _Generic.GetCustomerId(z.customer_code);
                                            if (customer_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = customer_code;
                                                errorMessage = customer_code + "not found";
                                            }
                                            else
                                            {
                                                z.customer_id = customer_id;
                                            }

                                            ref_price_list_customer_vm.Add(z);
                                            current_id = excelReader.GetString(Array.IndexOf(columnarray, "Customer Code"));

                                        }
                                        ref_price_list_customer_detail_vm IDVM = new ref_price_list_customer_detail_vm();

                                        var ItemCode = excelReader.GetString(Array.IndexOf(columnarray, "Item Code"));
                                        var duplicate = ref_price_list_customer_detail_vm.Where(x => x.item_code == ItemCode).FirstOrDefault();
                                        if (duplicate == null)
                                        {
                                            IDVM.item_code = ItemCode;
                                            var item_id = _item.GetID(ItemCode);
                                            if (item_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = ItemCode;
                                                errorMessage = ItemCode + "not found";
                                            }
                                            else
                                            {
                                                IDVM.item_id = item_id;
                                            }
                                            var uom_id = _Generic.GetUOMIDByitemid(IDVM.item_id);
                                            IDVM.uom_id = uom_id;
                                            IDVM.price = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Price")));
                                            var discount = excelReader.GetString(Array.IndexOf(columnarray, "Discount"));
                                            if (discount == null)
                                            {
                                                IDVM.discount = 0;
                                            }
                                            else
                                            {
                                                IDVM.discount = Double.Parse(discount);
                                            }
                                            var y = (IDVM.price * IDVM.discount) / 100;
                                            IDVM.price_after_discount = (IDVM.price - y);

                                            IDVM.customer_code = zz.customer_code;
                                            ref_price_list_customer_detail_vm.Add(IDVM);
                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Item Code is Duplicate ";
                                            errorMessage = "Item Code is Duplicate ";
                                        }

                                    }
                                    else
                                    {
                                        ref_price_list_customer_vm IBS = new ref_price_list_customer_vm();
                                        IBS.customer_code = excelReader.GetString(Array.IndexOf(columnarray, "Customer Code"));
                                        var customer_id = _Generic.GetCustomerId(IBS.customer_code);
                                        if (customer_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = IBS.customer_code;
                                            errorMessage = IBS.customer_code + "not found!";
                                        }
                                        else
                                        {
                                            IBS.customer_id = customer_id;
                                        }

                                        ref_price_list_customer_detail_vm IDVM = new ref_price_list_customer_detail_vm();
                                        IDVM.item_code = excelReader.GetString(Array.IndexOf(columnarray, "Item Code"));
                                        var item_id = _item.GetID(IDVM.item_code);
                                        if (item_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = IDVM.item_code;
                                            errorMessage = IDVM.item_code + "not found!";
                                        }
                                        else
                                        {
                                            IDVM.item_id = item_id;
                                        }
                                        var uom_id = _Generic.GetUOMIDByitemid(IDVM.item_id);
                                        IDVM.uom_id = uom_id;
                                        IDVM.price = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Price")));
                                        var discount = excelReader.GetString(Array.IndexOf(columnarray, "Discount"));
                                        if (discount == null)
                                        {
                                            IDVM.discount = 0;
                                        }
                                        else
                                        {
                                            IDVM.discount = Double.Parse(discount);
                                        }
                                        var y = (IDVM.price * IDVM.discount) / 100;
                                        IDVM.price_after_discount = (IDVM.price - y);

                                        IDVM.customer_code = IBS.customer_code;
                                        ref_price_list_customer_detail_vm.Add(IDVM);

                                        ref_price_list_customer_vm.Add(IBS);

                                        current_id = IBS.customer_code;
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
                        var isSucess = _priceList.AddExcel(ref_price_list_customer_vm, ref_price_list_customer_detail_vm);
                        if (isSucess == "")
                        {
                            Message = "Success";
                        }
                        else if (isSucess == "Failed")
                        {
                            Message = "Failed";
                        }
                        else
                        {
                            Message = isSucess;
                        }
                    }
                    else
                    {
                        Message = "Check Error";
                    }
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
            if (columnarray.Contains("Sr No") == false)
            {
                return false;
            }
            if (columnarray.Contains("Customer Code") == false)
            {
                return false;
            }
            if (columnarray.Contains("Item Code") == false)
            {
                return false;
            }
            if (columnarray.Contains("Price") == false)
            {
                return false;
            }
            if (columnarray.Contains("Discount") == false)
            {
                return false;
            }
            return true;
        }
    }
}