using Excel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class PriceListController : Controller
    {
        private readonly IPriceListService _priceList;
        private readonly IVendorService _vendor;
        private readonly IItemService _item;
        private readonly IGenericService _Generic;
        private readonly IUOMService _uom;
        string[] error = new string[30];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public PriceListController(IPriceListService priceList, IGenericService generic, IUOMService uom)
        {
            _priceList = priceList;
            _Generic = generic;
            _uom = uom;
        }

        [CustomAuthorizeAttribute("PLC")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _priceList.GetAll();
            return View();
        }

        public ActionResult Delete(int key)
        {
            var data = _priceList.Delete(key);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //GET: JournalEntry/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Item = _item.GetItemList();
            ViewBag.Vendor = _vendor.GetVendorList();
            ViewBag.UOM = _uom.GetAll();
            var journal_entry = _priceList.Get(id);
            ViewBag.vendorcodename = journal_entry.vendor_code + "-" + journal_entry.vendor_name;
            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
        }

        // GET: JournalEntry/Create
        [CustomAuthorizeAttribute("PLC")]
        public ActionResult Create()
        {
            ViewBag.Item = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Vendor = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.uom_list = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View();
        }

        // POST: JournalEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(price_list_vendor_vm value)
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
            ViewBag.Vendor = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.uom_list = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View(value);
        }

        //GET: JournalEntry/Edit/5
        [CustomAuthorizeAttribute("PLC")]
        public ActionResult Edit(int id)
        {
            ViewBag.Item = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Vendor = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.uom_list = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            var journal_entry = _priceList.Get(id);
            ViewBag.vendorcodename = journal_entry.vendor_code + "-" + journal_entry.vendor_name;
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
        public ActionResult Edit(price_list_vendor_vm journal_entry, FormCollection fc)
        {
            //string products;
            //products = fc["productdetail"];
            //string[] emptyStringArray = new string[0];
            //try
            //{
            //    emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);
            //}
            //catch
            //{

            //}

            //List<ref_price_list_vendor_details> journal_list = new List<ref_price_list_vendor_details>();
            //for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            //{
            //    ref_price_list_vendor_details item = new ref_price_list_vendor_details();
            //    item.item_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
            //    item.price = Double.Parse(emptyStringArray[i].Split(new char[] { ',' })[6]);
            //    var discount = emptyStringArray[i].Split(new char[] { ',' })[7];
            //    if (discount == "")
            //    {
            //        item.discount = 0;
            //    }
            //    else
            //    {
            //        item.discount = Double.Parse(discount);
            //    }
            //    item.discount_type_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[8]);
            //    item.price_after_discount = Double.Parse(emptyStringArray[i].Split(new char[] { ',' })[10]);
            //    item.uom_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
            //    if (emptyStringArray[i].Split(new char[] { ',' })[0] == "")
            //    {
            //        item.price_list_detail_id = 0;
            //    }
            //    else
            //    {
            //        item.price_list_detail_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
            //    }
            //    journal_list.Add(item);
            //}
            //journal_entry.ref_price_list_vendor_details = journal_list;
            //if (ModelState.IsValid)
            //{
            //    var isValid = _priceList.Update(journal_entry);
            //    if (isValid == true)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //}
            if (ModelState.IsValid)
            {
                var isValid = _priceList.Update(journal_entry);
                if (isValid.Contains("Saved"))
                {
                    TempData["data"] = isValid;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Item = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Vendor = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
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
                    List<ref_price_list_vendor_vm> ref_price_list_vendor_vm = new List<ref_price_list_vendor_vm>();
                    List<ref_price_list_vendor_detail_vm> ref_price_list_vendor_detail_vm = new List<ref_price_list_vendor_detail_vm>();
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
                                    var vendor_code = excelReader.GetString(Array.IndexOf(columnarray, "Vendor Code"));
                                    if (ref_price_list_vendor_vm.Count != 0)
                                    {
                                        var zz = ref_price_list_vendor_vm.Where(z => z.vendor_code == vendor_code).FirstOrDefault();
                                        if (zz == null)
                                        {
                                            var z = new ref_price_list_vendor_vm();
                                            z.vendor_code = excelReader.GetString(Array.IndexOf(columnarray, "Vendor Code"));
                                            var vendor_id = _Generic.GetVendorId(z.vendor_code);
                                            if (vendor_id == 0)
                                            {
                                                errorList++;
                                            }
                                            else
                                            {
                                                z.vendor_id = vendor_id;
                                            }

                                            ref_price_list_vendor_vm.Add(z);
                                            current_id = excelReader.GetString(Array.IndexOf(columnarray, "Vendor Code"));

                                        }
                                        ref_price_list_vendor_detail_vm IDVM = new ref_price_list_vendor_detail_vm();

                                        var ItemCode = excelReader.GetString(Array.IndexOf(columnarray, "Item Code"));
                                        var duplicate = ref_price_list_vendor_detail_vm.Where(x => x.item_code == ItemCode).FirstOrDefault();
                                        if (duplicate == null)
                                        {
                                            IDVM.item_code = ItemCode;
                                            var item_id = _item.GetID(ItemCode);
                                            if (item_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = ItemCode;
                                                errorMessage = ItemCode + "Item Code is Duplicate ";
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

                                            IDVM.vendor_code = zz.vendor_code;
                                            ref_price_list_vendor_detail_vm.Add(IDVM);
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
                                        ref_price_list_vendor_vm IBS = new ref_price_list_vendor_vm();
                                        IBS.vendor_code = excelReader.GetString(Array.IndexOf(columnarray, "Vendor Code"));
                                        var vendor_id = _Generic.GetVendorId(IBS.vendor_code);
                                        if (vendor_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = IBS.vendor_code;
                                            errorMessage = IBS.vendor_code + "not found.";
                                        }
                                        else
                                        {
                                            IBS.vendor_id = vendor_id;
                                        }

                                        ref_price_list_vendor_detail_vm IDVM = new ref_price_list_vendor_detail_vm();
                                        IDVM.item_code = excelReader.GetString(Array.IndexOf(columnarray, "Item Code"));
                                        var item_id = _item.GetID(IDVM.item_code);
                                        if (item_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = IDVM.item_code;
                                            errorMessage = IDVM.item_code + "not found.";
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

                                        IDVM.vendor_code = IBS.vendor_code;
                                        ref_price_list_vendor_detail_vm.Add(IDVM);

                                        ref_price_list_vendor_vm.Add(IBS);

                                        current_id = IBS.vendor_code;
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
                        var isSucess = _priceList.AddExcel(ref_price_list_vendor_vm, ref_price_list_vendor_detail_vm);
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
                        Message = "Check Error!";
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
            if (columnarray.Contains("Vendor Code") == false)
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