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
    public class InventoryBalanceController : Controller
    {
        private readonly IInventoryBalanceService _inventory;
        private readonly IGenericService _Generic;
        private readonly IItemService _item;
        private readonly IPlantService _plant;
        private readonly IStorageLocation _storageLocation;
        private readonly IBucketService _bucket;
        private readonly IUOMService _Uom;
        private readonly IGeneralLedgerService _generalLedger;
        string[] error = new string[30];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public InventoryBalanceController(IGeneralLedgerService generalLedger, IPlantService plant, IStorageLocation storageLocation,
            IBucketService bucket, IUOMService uom, IInventoryBalanceService inventory, IGenericService generic, IItemService item)
        {
            _inventory = inventory;
            _Generic = generic;
            _item = item;
            _plant = plant;
            _storageLocation = storageLocation;
            _bucket = bucket;
            _Uom = uom;
            _generalLedger = generalLedger;
        }
        public ActionResult Index()
        {

            ViewBag.DataSource = _inventory.GetAll();
            return View();
        }

        public ActionResult Delete(int key)
        {
            var data = _inventory.Delete(key);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public void ExportToExcel(string GridModel)
        {
            ExcelExport exp = new ExcelExport();
            var DataSource = _inventory.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "Inventory Balance.xlsx", ExcelVersion.Excel2010, false, false, "flat-saffron");
        }

        public void ExportToWord(string GridModel)
        {
            WordExport exp = new WordExport();
            var DataSource = _inventory.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "Inventory Balance.docx", false, false, "flat-saffron");
        }

        public void ExportToPdf(string GridModel)
        {
            PdfExport exp = new PdfExport();
            var DataSource = _inventory.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "Inventory Balance.pdf", false, false, "flat-saffron");
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

        //GET: JournalEntry/Details/5
        public ActionResult Details(int id)
        {
            ref_inventory_balance_VM journal_entry = _inventory.GetDetails(id);
            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            var gen = _generalLedger.GetAccountGeneral();
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            var it = _item.GetItemList();
            ViewBag.Item = new SelectList(it, "ITEM_ID", "ITEM_NAME");
            var pl = _plant.GetAll();
            ViewBag.Plant = new SelectList(pl, "PLANT_ID", "PLANT_NAME");
            var sl = _storageLocation.GetAll();
            ViewBag.Sloc = new SelectList(sl, "storage_location_id", "storage_location_name");
            ViewBag.Bucket = _bucket.GetAll();
            var uo = _Uom.GetAll();
            ViewBag.Uom = new SelectList(uo, "UOM_ID", "UOM_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(15), "document_numbring_id", "category");
            return View(journal_entry);
        }

        // GET: JournalEntry/Create
        public ActionResult Create()
        {
            var gen = _Generic.getOffsetAccount("Opening Balance Inventory Offset", 9);
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            var it = _item.GetItemList();
            ViewBag.Item = new SelectList(it, "ITEM_ID", "ITEM_NAME");
            ViewBag.Plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.Bucket = _bucket.GetAll();
            var uo = _Uom.GetAll();
            ViewBag.Uom = new SelectList(uo, "UOM_ID", "UOM_NAME");
            ViewBag.OffsetAccount = _generalLedger.GetAccountGeneralLedger();
            //ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(15), "document_numbring_id", "category");
            return View();
        }

        // POST: JournalEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_inventory_balance_VM vm, FormCollection fc)
        {

            var gen = _Generic.getOffsetAccount("Opening Balance Inventory Offset", 9);
            vm.offset_account_id = gen[0].gl_ledger_id;
            //string products;

            if (ModelState.IsValid)
            {
                var isValid = _inventory.Add(vm);
                if (isValid == "Saved")
                {
                    return RedirectToAction("Index");
                }
                ViewBag.error = isValid;
            }
            
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            var it = _item.GetItemList();
            ViewBag.Item = new SelectList(it, "ITEM_ID", "ITEM_NAME");
            var pl = _plant.GetAll();
            ViewBag.Plant = new SelectList(pl, "PLANT_ID", "PLANT_NAME");
            var sl = _storageLocation.GetAll();
            ViewBag.Sloc = new SelectList(sl, "storage_location_id", "storage_location_name");
            ViewBag.Sloc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.Bucket = _bucket.GetAll();
            var uo = _Uom.GetAll();
            ViewBag.Uom = new SelectList(uo, "UOM_ID", "UOM_NAME"); ;
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(15), "document_numbring_id", "category");
            return View(vm);
        }

        //GET: JournalEntry/Edit/5
        public ActionResult Edit(int id)
        {
            var gen = _generalLedger.GetAccountGeneral();
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            var it = _item.GetItemList();
            ViewBag.Item = new SelectList(it, "ITEM_ID", "ITEM_NAME");
            var pl = _plant.GetAll();
            ViewBag.Plant = new SelectList(pl, "PLANT_ID", "PLANT_NAME");

            ViewBag.Sloc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.Bucket = _bucket.GetAll();
            var uo = _Uom.GetAll();
            ViewBag.Uom = new SelectList(uo, "UOM_ID", "UOM_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(15), "document_numbring_id", "category");
            ref_inventory_balance_VM journal_entry = _inventory.Get(id);

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
        public ActionResult Edit(ref_inventory_balance_VM journal_entry, FormCollection fc)
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

            List<ref_inventory_balance_details> journal_list = new List<ref_inventory_balance_details>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_inventory_balance_details item = new ref_inventory_balance_details();
                item.item_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                item.plant_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
                item.sloc_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[6]);
                item.bucket_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[8]);
                item.batch = emptyStringArray[i].Split(new char[] { ',' })[10];
                item.qty = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[11]);
                item.uom_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[12]);
                item.rate = Double.Parse(emptyStringArray[i].Split(new char[] { ',' })[14]);
                item.value = Double.Parse(emptyStringArray[i].Split(new char[] { ',' })[15]);
                item.line_remarks = emptyStringArray[i].Split(new char[] { ',' })[16];
                if (emptyStringArray[i].Split(new char[] { ',' })[0] == "")
                {
                    item.inventory_balance_detail_id = 0;
                }
                else
                {
                    item.inventory_balance_detail_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                }
                journal_list.Add(item);
            }
            journal_entry.ref_inventory_balance_details = journal_list;
            if (ModelState.IsValid)
            {
                var isValid = _inventory.Update(journal_entry);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            var gen = _generalLedger.GetAccountGeneral();
            ViewBag.GeneralLedger = new SelectList(gen, "gl_ledger_id", "gl_ledger_name");
            var it = _item.GetItemList();
            ViewBag.Item = new SelectList(it, "ITEM_ID", "ITEM_NAME");
            var pl = _plant.GetAll();
            ViewBag.Plant = new SelectList(pl, "PLANT_ID", "PLANT_NAME");
            var sl = _storageLocation.GetAll();
            ViewBag.Sloc = new SelectList(sl, "storage_location_id", "storage_location_name");
            ViewBag.Bucket = _bucket.GetAll();
            var uo = _Uom.GetAll();
            ViewBag.Uom = new SelectList(uo, "UOM_ID", "UOM_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(15), "document_numbring_id", "category");
            return View(journal_entry);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _priceList.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
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
                    List<inventory_balance_VM> inventory_balance_VM = new List<Domain.ViewModel.inventory_balance_VM>();
                    List<inventory_balance_detail_VM> bldetails = new List<Domain.ViewModel.inventory_balance_detail_VM>();
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
                                    var offset_account = _Generic.getOffsetAccount("Opening Balance Inventory Offset", 9);
                                    if (inventory_balance_VM.Count != 0)
                                    {
                                        var zz = inventory_balance_VM.Where(z => z.posting_date == DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "PostingDate"))) && z.header_remarks == excelReader.GetString(Array.IndexOf(columnarray, "HeaderRemarks"))).FirstOrDefault();
                                        if (zz == null)
                                        {
                                            errorMessage = "Add same Header Remarks and Posting Date.";
                                            errorList++;
                                            error[error.Length - 1] = "Add same Header Remarks and Posting Date.";
                                            //var z = new inventory_balance_VM();
                                            //z.offset_account = int.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Offset Account")));
                                            //z.posting_date = DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Posting Date")));
                                            //z.header_remarks = excelReader.GetString(Array.IndexOf(columnarray, "Header Remarks"));
                                            //inventory_balance_VM.Add(z);
                                            //current_id = int.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Offset Account")));
                                        }
                                        else
                                        {
                                            inventory_balance_detail_VM IDVM = new inventory_balance_detail_VM();
                                            var item_code = excelReader.GetString(Array.IndexOf(columnarray, "ItemCode"));
                                            var item_id = _item.GetID(item_code);
                                            //IDVM.item_id = _item.GetAll().Where(x => x.ITEM_CODE == item_code).FirstOrDefault().ITEM_ID;
                                            if (item_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = item_code;
                                                errorMessage = item_code + "not found";
                                            }
                                            else
                                            {
                                                IDVM.item_id = item_id;
                                            }
                                            var plant = excelReader.GetString(Array.IndexOf(columnarray, "Plant"));
                                            var plant_id = _Generic.GetPlantID(plant);
                                            //IDVM.plant_id = _plant.GetAll().Where(x => x.PLANT_NAME == plant).FirstOrDefault().PLANT_ID;
                                            if (plant_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = plant;
                                                errorMessage = plant + "not found";
                                            }
                                            else
                                            {
                                                IDVM.plant_id = plant_id;
                                            }
                                            var sloc = excelReader.GetString(Array.IndexOf(columnarray, "Sloc"));
                                            var sloc_id = _storageLocation.GetID(sloc);
                                            //IDVM.sloc_id = _storageLocation.GetAll().Where(x => x.STORAGE_LOCATION_NAME == sloc).FirstOrDefault().STORAGE_LOCATION_ID;
                                            if (sloc_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = sloc;
                                                errorMessage = sloc + "not found";
                                            }
                                            else
                                            {
                                                IDVM.sloc_id = sloc_id;
                                            }
                                            var bucket = excelReader.GetString(Array.IndexOf(columnarray, "Bucket"));
                                            if (bucket.ToLower() == "quality")
                                            {
                                                errorList++;
                                                error[error.Length - 1] = bucket;
                                                errorMessage = bucket + " Bucket cannot be uploaded.";
                                            }
                                            else
                                            {
                                                var bucket_id = _bucket.GetID(bucket);
                                                //IDVM.bucket_id = _bucket.GetAll().Where(x => x.bucket_name == bucket).FirstOrDefault().bucket_id;
                                                if (bucket_id == 0)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = bucket;
                                                    errorMessage = bucket + "not found";
                                                }
                                                else
                                                {
                                                    IDVM.bucket_id = bucket_id;
                                                }
                                            }
                                            //IDVM.batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                            var item_detail = _item.GetItemsDetail(item_id);
                                            if (item_detail.BATCH_MANAGED == true)
                                            {                                                
                                                var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                                if (batch == null)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = batch;
                                                    errorMessage = item_code + " batch not mentioned";
                                                }
                                                else
                                                {
                                                    IDVM.batch = batch;
                                                }
                                            }
                                            else
                                            {
                                                var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                                if (batch != null)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = batch;
                                                    errorMessage = item_code + " is not batch managed";
                                                }
                                                else
                                                {
                                                    IDVM.batch = "";
                                                }
                                            }
                                            IDVM.qty = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Qty")));
                                            var uom = excelReader.GetString(Array.IndexOf(columnarray, "UoM"));
                                            var uom_id = _Uom.GetID(uom);
                                            //IDVM.uom_id = _Uom.GetAll().Where(x => x.UOM_NAME == uom).FirstOrDefault().UOM_ID;
                                            if (uom_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = uom;
                                                errorMessage = uom + "not found";
                                            }
                                            else
                                            {
                                                IDVM.uom_id = uom_id;
                                            }
                                            IDVM.rate = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Rate")));
                                            if (IDVM.qty == 0)
                                            {
                                                IDVM.qty = 1;
                                            }
                                            IDVM.value = IDVM.qty * IDVM.rate;
                                            IDVM.line_remarks = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));
                                            IDVM.offset_account = offset_account[0].gl_ledger_code;
                                            bldetails.Add(IDVM);
                                        }
                                    }
                                    else
                                    {
                                        inventory_balance_VM IBS = new inventory_balance_VM();
                                        var category_id = _Generic.GetCategoryList(15);
                                        IBS.category_id = category_id[0].document_numbring_id;
                                        IBS.doc_number = _Generic.GetDocumentNumbering(IBS.category_id);
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

                                        inventory_balance_detail_VM IDVM = new inventory_balance_detail_VM();

                                        var item_code = excelReader.GetString(Array.IndexOf(columnarray, "ItemCode"));
                                        var item_id = _item.GetID(item_code);
                                        //IDVM.item_id = _item.GetAll().Where(x => x.ITEM_CODE == item_code).FirstOrDefault().ITEM_ID;
                                        if (item_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = item_code;
                                            errorMessage = item_code + "not found";
                                        }
                                        else
                                        {
                                            IDVM.item_id = item_id;
                                        }
                                        var plant = excelReader.GetString(Array.IndexOf(columnarray, "Plant"));
                                        var plant_id = _Generic.GetPlantID(plant);
                                        //IDVM.plant_id = _plant.GetAll().Where(x => x.PLANT_NAME == plant).FirstOrDefault().PLANT_ID;
                                        if (plant_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = plant;
                                            errorMessage = plant + "not found";
                                        }
                                        else
                                        {
                                            IDVM.plant_id = plant_id;
                                        }
                                        var sloc = excelReader.GetString(Array.IndexOf(columnarray, "Sloc"));
                                        var sloc_id = _storageLocation.GetID(sloc);
                                        //IDVM.sloc_id = _storageLocation.GetAll().Where(x => x.STORAGE_LOCATION_NAME == sloc).FirstOrDefault().STORAGE_LOCATION_ID;
                                        if (sloc_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = sloc;
                                            errorMessage = sloc + "not found";
                                        }
                                        else
                                        {
                                            IDVM.sloc_id = sloc_id;
                                        }
                                        var bucket = excelReader.GetString(Array.IndexOf(columnarray, "Bucket"));
                                        if (bucket.ToLower() == "quality")
                                        {
                                            errorList++;
                                            error[error.Length - 1] = bucket;
                                            errorMessage = bucket + " Bucket cannot be uploaded.";
                                        }
                                        else
                                        {
                                            var bucket_id = _bucket.GetID(bucket);
                                            //IDVM.bucket_id = _bucket.GetAll().Where(x => x.bucket_name == bucket).FirstOrDefault().bucket_id;
                                            if (bucket_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = bucket;
                                                errorMessage = bucket + "not found";
                                            }
                                            else
                                            {
                                                IDVM.bucket_id = bucket_id;
                                            }
                                        }
                                        var item_detail = _item.GetItemsDetail(item_id);
                                        if (item_detail.BATCH_MANAGED == true)
                                        {
                                            var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                            if (batch == null)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = batch;
                                                errorMessage = item_code + " batch not mentioned";
                                            }
                                            else
                                            {
                                                IDVM.batch = batch;
                                            }
                                        }
                                        else
                                        {
                                            var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                            if (batch != null)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = batch;
                                                errorMessage = item_code + " is not batch managed";
                                            }
                                            else
                                            {
                                                IDVM.batch = "";
                                            }
                                        }

                                        IDVM.qty = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Qty")));
                                        var uom = excelReader.GetString(Array.IndexOf(columnarray, "UoM"));
                                        var uom_id = _Uom.GetID(uom);
                                        //IDVM.uom_id = _Uom.GetAll().Where(x => x.UOM_NAME == uom).FirstOrDefault().UOM_ID;
                                        if (uom_id == 0)
                                        {
                                            errorList++;
                                            error[error.Length - 1] = uom;
                                            errorMessage = uom + "not found";
                                        }
                                        else
                                        {
                                            IDVM.uom_id = uom_id;
                                        }
                                        IDVM.rate = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Rate")));
                                        if (IDVM.qty == 0)
                                        {
                                            IDVM.qty = 1;
                                        }
                                        IDVM.value = IDVM.qty * IDVM.rate;
                                        IDVM.line_remarks = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));
                                        IDVM.offset_account = IBS.offset_account;
                                        bldetails.Add(IDVM);
                                        inventory_balance_VM.Add(IBS);
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
                        var isSucess = _inventory.AddExcel(inventory_balance_VM, bldetails);
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
            if (columnarray.Contains("ItemCode") == false)
            {
                return false;
            }
            if (columnarray.Contains("Plant") == false)
            {
                return false;
            }
            if (columnarray.Contains("Sloc") == false)
            {
                return false;
            }
            if (columnarray.Contains("Bucket") == false)
            {
                return false;
            }
            if (columnarray.Contains("Batch") == false)
            {
                return false;
            }
            if (columnarray.Contains("Qty") == false)
            {
                return false;
            }
            if (columnarray.Contains("UoM") == false)
            {
                return false;
            }
            if (columnarray.Contains("Rate") == false)
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