using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Web;
using System.IO;
using Excel;
using System.Collections.Generic;
using System.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class GoodsIssueController : Controller
    {
        private readonly IGoodsIssueService _goodsIssueService;
        private readonly IItemService _itemService;
        private readonly IUOMService _UOMService;
        private readonly IGeneralLedgerService _generalLedgerService;
        private readonly ICategoryService _categoryService;
        private readonly IPlantService _plantService;
        private readonly IStorageLocation _storageLocationService;
        private readonly IReasonDeterminationService _reasonDeterminationService;
        private readonly IGenericService _Generic;
        private readonly IBatchService _batchService;
        private readonly IBucketService _bucketService;
        private readonly IMachineMasterService _machineService;
        private readonly IMaterialRequisionNoteService _materialReqService;
        private readonly IGoodsReceiptService _goodsReceipt;
        private readonly IItemService _item;
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
        public GoodsIssueController(IItemService item,IGoodsReceiptService goodsReceipt, IMaterialRequisionNoteService materialReqService, IMachineMasterService machineService, IBucketService bucket, IBatchService batch, IGenericService gen, IGoodsIssueService GoodsIssueService, IItemService ItemService, IUOMService UOMService, IGeneralLedgerService GeneralLedgerService, ICategoryService CategoryService, IPlantService PlantService, IStorageLocation StorageLocationService, IReasonDeterminationService ReasonDeterminationService)
        {
            _goodsIssueService = GoodsIssueService;
            _itemService = ItemService;
            _UOMService = UOMService;
            _generalLedgerService = GeneralLedgerService;
            _categoryService = CategoryService;
            _plantService = PlantService;
            _storageLocationService = StorageLocationService;
            _reasonDeterminationService = ReasonDeterminationService;
            _Generic = gen;
            _batchService = batch;
            _bucketService = bucket;
            _machineService = machineService;
            _materialReqService = materialReqService;
            _goodsReceipt = goodsReceipt;
            _item = item;
        }
        [CustomAuthorizeAttribute("GI")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _goodsIssueService.getall();
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS_ISSUE_VM gOODS_ISSUE_VM = _goodsIssueService.Get((int)id);
            if (gOODS_ISSUE_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.reasonlist = new SelectList(_reasonDeterminationService.GetReasonList(2), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.storagelist = new SelectList(_storageLocationService.GetAll(), "storage_location_id", "STORAGE_LOCATION_NAME");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            //ViewBag.categorylist = new SelectList(_categoryService.GetAll(), "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = _generalLedgerService.GetAll();
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(81), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(_UOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.machineList = new SelectList(_machineService.GetAll(), "machine_id", "machine_name");
            ViewBag.mrnList = new SelectList(_materialReqService.GetMRnList(), "material_requision_note_id", "mrn_date_number");
            return View(gOODS_ISSUE_VM);
        }
        [CustomAuthorizeAttribute("GI")]
        public ActionResult Create()
        {
            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var r = _reasonDeterminationService.GetReasonList(2);
            var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.error = "";
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.storagelist = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "STORAGE_LOCATION_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(81), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            ViewBag.machineList = new SelectList(_machineService.GetAll(), "machine_id", "machine_name");
            ViewBag.mrnList = new SelectList(_materialReqService.GetMRnList(), "material_requision_note_id", "mrn_date_number");
            return View();
        }

        // POST:
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GOODS_ISSUE_VM gOODS_ISSUE_VM)
        {
            if (ModelState.IsValid)
            {
                var issaved = _goodsIssueService.Add(gOODS_ISSUE_VM);
                if (issaved.Contains("Saved"))
                {
                    //var sp = issaved.Split('~')[1];
                    TempData["data"] = issaved;
                    return RedirectToAction("Index");
                }
                ViewBag.error = issaved;
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            var it = _itemService.GetItemList();
            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var p = _plantService.GetAll();
            var s = _storageLocationService.GetAll();
            var r = _reasonDeterminationService.GetReasonList(2);
            var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.storagelist = new SelectList(s, "storage_location_id", "STORAGE_LOCATION_ID");
            ViewBag.plantlist = new SelectList(p, "PLANT_ID", "PLANT_NAME");
            // ViewBag.categorylist = new SelectList(c, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_generalLedgerService.GetAll(), "gl_ledger_id", "gl_ledger_code");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(81), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            ViewBag.machineList = new SelectList(_machineService.GetAll(), "machine_id", "machine_name");
            ViewBag.mrnList = new SelectList(_materialReqService.GetMRnList(), "material_requision_note_id", "mrn_date_number");
            return View(gOODS_ISSUE_VM);
        }
        [CustomAuthorizeAttribute("GI")]
        // GET:
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS_ISSUE_VM gOODS_ISSUE_VM = _goodsIssueService.Get((int)id);
            if (gOODS_ISSUE_VM == null)
            {
                return HttpNotFound();
            }

            var it = _itemService.GetAll();
            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var p = _plantService.GetAll();
            var s = _storageLocationService.GetAll();
            var r = _reasonDeterminationService.GetReasonList(2);
            var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.storagelist = new SelectList(s, "storage_location_id", "STORAGE_LOCATION_NAME");
            ViewBag.plantlist = new SelectList(p, "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(81), "document_numbring_id", "category");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            ViewBag.machineList = new SelectList(_machineService.GetAll(), "machine_id", "machine_name");
            ViewBag.mrnList = new SelectList(_materialReqService.GetMRnList(), "material_requision_note_id", "mrn_date_number");
            return View(gOODS_ISSUE_VM);
        }



        // POST: 
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GOODS_ISSUE_VM gOODS_ISSUE_VM, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                var isedited = _goodsIssueService.Update(gOODS_ISSUE_VM);
                if (isedited == true)
                    return RedirectToAction("Index");
            }

            var it = _itemService.GetAll();
            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var p = _plantService.GetAll();
            var s = _storageLocationService.GetAll();
            var r = _reasonDeterminationService.GetReasonList(2);
            var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.storagelist = new SelectList(s, "STORAGE_LOCATION_ID", "STORAGE_LOCATION_NAME");
            ViewBag.plantlist = new SelectList(p, "PLANT_ID", "PLANT_NAME");
            // ViewBag.categorylist = new SelectList(c, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = _generalLedgerService.GetAll();
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(81), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            ViewBag.machineList = new SelectList(_machineService.GetAll(), "machine_id", "machine_name");
            ViewBag.mrnList = new SelectList(_materialReqService.GetMRnList(), "material_requision_note_id", "mrn_date_number");
            return View(gOODS_ISSUE_VM);
        }

        // GET:
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS_ISSUE_VM gOODS_ISSUE = _goodsIssueService.Get((int)id);
            if (gOODS_ISSUE == null)
            {
                return HttpNotFound();
            }
            return View(gOODS_ISSUE);
        }

        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _goodsIssueService.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            GOODS_ISSUE_VM gOODS_ISSUE = _goodsIssueService.Get((int)id);
            if (gOODS_ISSUE == null)
            {
                return HttpNotFound();
            }
            return View(gOODS_ISSUE);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _goodsIssueService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetTagMRNProductForGoodsIssue(int id)
        {
            var vm = _goodsIssueService.GetTagMRNProductForGoodsIssue(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult GetNonTagMRNProductForGoodsIssue(int id)
        {
            var vm = _goodsIssueService.GetNonTagMRNProductForGoodsIssue(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult TagItemsForGoodsIssue(int sloc_id, int plant_id, int item_id)
        {
            var vm = _goodsIssueService.GetTagItemsForGoodsIssue(sloc_id, plant_id, item_id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult NonTagItemsForGoodsIssue(int sloc_id, int plant_id, int item_id)
        {
            var vm = _goodsIssueService.GetNonTagItemsForGoodsIssue(sloc_id, plant_id, item_id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
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
                    int contcol = 0, custcol = 0, glcol = 0, prmcol = 0, stccol = 0;
                    string uploadtype = Request.Params[0];
                    List<GOODS_ISSUE_VM> GOODS_ISSUE_VM = new List<GOODS_ISSUE_VM>();
                    List<goods_issue_detail_VM> goods_issue_detail_VM = new List<goods_issue_detail_VM>();
                    List<goods_issue_batch_VM> goods_issue_batch_VM = new List<goods_issue_batch_VM>();
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

                            if (sheet.TableName == "goods_issue")
                            {

                                string[] HeaderColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    HeaderColumnArray[custcol] = ary1.ToString();
                                    custcol = custcol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateHeaderExcelColumns(HeaderColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(HeaderColumnArray, "SrNo")].ToString().Trim();
                                            if (sr_no != "")
                                            {
                                                //if (GOODS_ISSUE_VM.Count == 0)
                                                //{
                                                GOODS_ISSUE_VM IDVM = new GOODS_ISSUE_VM();
                                                var header_reference = a[Array.IndexOf(HeaderColumnArray, "HeaderReferenece")].ToString().Trim();



                                                if (header_reference == "")
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = header_reference.ToString();
                                                    errorMessage = "Add Header Reference.";
                                                }
                                                else
                                                {
                                                    IDVM.header_reference = header_reference;
                                                }
                                                IDVM.document_code = a[Array.IndexOf(HeaderColumnArray, "DocumentCode")].ToString().Trim();

                                                var document_number = a[Array.IndexOf(HeaderColumnArray, "DocumentNumber")].ToString().Trim();
                                                if (IDVM.document_code == "GI")
                                                {
                                                    IDVM.document_id = 0;
                                                }
                                                else
                                                {
                                                    var document_id = _Generic.getDocumentId(IDVM.document_code, document_number);
                                                    if (document_id == 0)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = document_id.ToString();
                                                        errorMessage = "Document Number not exist.";
                                                    }
                                                    else
                                                    {
                                                        IDVM.document_id = document_id;
                                                    }
                                                }

                                                var posting_date = a[Array.IndexOf(HeaderColumnArray, "PostingDate")].ToString().Trim();
                                                if (posting_date == "")
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = posting_date.ToString();
                                                    errorMessage = "Add posting date.";
                                                }
                                                else
                                                {
                                                    IDVM.posting_date = DateTime.Parse(posting_date);
                                                }

                                                var document_date = a[Array.IndexOf(HeaderColumnArray, "DocumentDate")].ToString().Trim();
                                                if (document_date == "")
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = document_date.ToString();
                                                    errorMessage = "Add document date.";
                                                }
                                                else
                                                {
                                                    IDVM.document_date = DateTime.Parse(document_date);
                                                }

                                                var plant = a[Array.IndexOf(HeaderColumnArray, "Plant")].ToString().Trim();
                                                var plant_id = _Generic.GetPlantID(plant);
                                                if (plant_id == 0)
                                                {

                                                    errorList++;
                                                    error[error.Length - 1] = plant;
                                                    errorMessage = "Plant not exist!";
                                                }
                                                else
                                                {
                                                    IDVM.plant_id = plant_id;
                                                    IDVM.document_numbring_id = _Generic.GetCategoryByModuleFormAndPlant(81, IDVM.plant_id);
                                                }
                                                
                                                IDVM.remarks = a[Array.IndexOf(HeaderColumnArray, "Remarks")].ToString().Trim();
                                                IDVM.ref1 = a[Array.IndexOf(HeaderColumnArray, "Reference")].ToString().Trim();

                                                GOODS_ISSUE_VM.Add(IDVM);
                                                //}
                                            }
                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Check Headers Name!";
                                            errorMessage = "Check Headers Name!";
                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "goods_issue_detail" && errorMessage == "")
                            {
                                string[] DetailColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    DetailColumnArray[contcol] = ary1.ToString();
                                    contcol = contcol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateDetailExcelColumns(DetailColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(DetailColumnArray, "SrNo")].ToString().Trim();
                                            if (sr_no != "")
                                            {
                                                //if (goods_issue_detail_VM.Count == 0)
                                                //{
                                                goods_issue_detail_VM CE = new goods_issue_detail_VM();
                                                var header_reference = a[Array.IndexOf(DetailColumnArray, "HeaderReferenece")].ToString().Trim();
                                                var FoundHeaderReference = GOODS_ISSUE_VM.Where(x => x.header_reference == header_reference).FirstOrDefault();
                                                if (FoundHeaderReference != null)
                                                {
                                                    CE.header_referenec = header_reference;
                                                    var item_code = a[Array.IndexOf(DetailColumnArray, "ItemCode")].ToString().Trim();
                                                    if (item_code == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = item_code;
                                                        errorMessage = "Item Code is blank!";
                                                    }
                                                    else
                                                    {
                                                        var item_id = _Generic.GetItemId(item_code);
                                                        if (item_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_code;
                                                            errorMessage = item_code + " ItemCode not found!";
                                                        }
                                                        else
                                                        {
                                                            CE.item_id = item_id;
                                                        }
                                                    }
                                                    var UoM = a[Array.IndexOf(DetailColumnArray, "UoM")].ToString().Trim();
                                                    if (UoM == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = UoM;
                                                        errorMessage = "UoM is blank!";
                                                    }
                                                    else
                                                    {
                                                        var uom_id = _Generic.GetUoMId(UoM);
                                                        if (uom_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = UoM;
                                                            errorMessage = UoM + " UoM not found!";
                                                        }
                                                        else
                                                        {
                                                            CE.uom_id = uom_id;
                                                        }
                                                    }
                                                    var StorageLocation = a[Array.IndexOf(DetailColumnArray, "StorageLocation")].ToString().Trim();
                                                    if (StorageLocation == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = StorageLocation;
                                                        errorMessage = "StorageLocation is blank!";
                                                    }
                                                    else
                                                    {
                                                        var sloc_id = _Generic.GetSlocId(StorageLocation);
                                                        if (sloc_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = StorageLocation;
                                                            errorMessage = StorageLocation + " StorageLocation not found!";
                                                        }
                                                        else
                                                        {
                                                            CE.sloc_id = sloc_id;
                                                        }
                                                    }
                                                    CE.bucket_id = 2;
                                                    var balance_qty = _Generic.getBatchQuantityUsingItemSlocPlant(CE.sloc_id, FoundHeaderReference.plant_id, CE.item_id, 2);

                                                    var Quantity = a[Array.IndexOf(DetailColumnArray, "Quantity")].ToString().Trim();

                                                    if (Quantity == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = Quantity;
                                                        errorMessage = "Quantity is blank!";
                                                    }
                                                    else
                                                    {
                                                        //if (Double.Parse(Quantity) <= balance_qty)
                                                        //{
                                                            CE.quantity = Double.Parse(Quantity);
                                                        //}
                                                        //else
                                                        //{
                                                        //    errorList++;
                                                        //    error[error.Length - 1] = Quantity;
                                                        //    errorMessage = item_code + " Quantity is Greater than Stock!";
                                                        //}
                                                    }
                                                    var rate = _goodsReceipt.GetMAP(CE.item_id, FoundHeaderReference.plant_id);
                                                    CE.rate = rate;
                                                    CE.value = CE.rate * CE.quantity;
                                                    var document_detail_id = 0;
                                                    if (FoundHeaderReference.document_code == "GI")
                                                    {
                                                        CE.document_detail_id = document_detail_id;
                                                    }
                                                    else
                                                    {
                                                        document_detail_id = _Generic.getDocumentDetailId(FoundHeaderReference.document_code, CE.item_id);
                                                        CE.document_detail_id = document_detail_id;
                                                    }

                                                    goods_issue_detail_VM.Add(CE);
                                                }
                                                else
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = header_reference;
                                                    errorMessage = header_reference + " Header reference not found!";
                                                }
                                                //}

                                            }

                                        }
                                        else
                                        {
                                            errorList++;
                                            errorMessage = "Check Headers Name.";
                                            error[error.Length - 1] = "Check Headers Name.";
                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "goods_issue_batch" && errorMessage == "")
                            {
                                string[] BatchColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    BatchColumnArray[glcol] = ary1.ToString();
                                    glcol = glcol + 1;
                                }
                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateBatchExcelColumns(BatchColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(BatchColumnArray, "SrNo")].ToString().Trim();
                                            if (sr_no != "")
                                            {
                                                if (goods_issue_batch_VM.Count == 0)
                                                {
                                                    goods_issue_batch_VM GE1 = new goods_issue_batch_VM();
                                                    var Item_Code = a[Array.IndexOf(BatchColumnArray, "ItemCode")].ToString().Trim();
                                                    var item_id = _Generic.GetItemId(Item_Code);
                                                    var Item = goods_issue_detail_VM.Where(x => x.item_id == item_id).FirstOrDefault();
                                                    if (Item == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = Item_Code + " not found";
                                                        errorMessage = Item_Code + " ItemCode not found from sheet 3!";
                                                    }
                                                    else
                                                    {
                                                        GE1.item_id = item_id;
                                                        var Quantity = a[Array.IndexOf(BatchColumnArray, "Quantity")].ToString().Trim();
                                                        if (Quantity == null)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = Quantity + " not found";
                                                            errorMessage = Quantity + " Quantity not found!";
                                                        }
                                                        else
                                                        {
                                                            GE1.quantity = Double.Parse(Quantity);
                                                        }
                                                        var item_detail = _item.GetItemsDetail(GE1.item_id);
                                                        if (item_detail.BATCH_MANAGED == true)
                                                        {
                                                            var batch1 = a[Array.IndexOf(BatchColumnArray, "BatchNumber")].ToString().Trim();
                                                            if (batch1 == null)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = batch1;
                                                                errorMessage = Item_Code + " batch not mentioned";
                                                            }
                                                            else
                                                            {
                                                                GE1.batch_number = batch1;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            var batch1 = a[Array.IndexOf(BatchColumnArray, "BatchNumber")].ToString().Trim();
                                                            if (batch1 != "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = batch1;
                                                                errorMessage = Item_Code + " is not batch managed";
                                                            }
                                                            else
                                                            {
                                                                GE1.batch_number = "";
                                                                GE1.batch_id = 0;
                                                            }
                                                        }

                                                    }
                                                    goods_issue_batch_VM.Add(GE1);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    excelReader.Close();

                    if (errorMessage == "")
                    {
                        var isSucess = _goodsIssueService.AddExcel(GOODS_ISSUE_VM, goods_issue_detail_VM, goods_issue_batch_VM);
                        if (isSucess == "Saved")
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
            return Json(errorList + " - " + errorMessage, JsonRequestBehavior.AllowGet);
        }
        public Boolean ValidateHeaderExcelColumns(string[] HeaderColumnArray)
        {
            if (HeaderColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("HeaderReferenece") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("DocumentCode") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("DocumentNumber") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("PostingDate") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("DocumentDate") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("Plant") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("Remarks") == false)
            {
                return false;
            }
            if (HeaderColumnArray.Contains("Reference") == false)
            {
                return false;
            }
            return true;
        }

        public Boolean ValidateDetailExcelColumns(string[] DetailColumnArray)
        {
            if (DetailColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (DetailColumnArray.Contains("HeaderReferenece") == false)
            {
                return false;
            }
            if (DetailColumnArray.Contains("ItemCode") == false)
            {
                return false;
            }
            if (DetailColumnArray.Contains("UoM") == false)
            {
                return false;
            }
            if (DetailColumnArray.Contains("StorageLocation") == false)
            {
                return false;
            }
            if (DetailColumnArray.Contains("Quantity") == false)
            {
                return false;
            }
            if (DetailColumnArray.Contains("Reason") == false)
            {
                return false;
            }
            return true;
        }

        public Boolean ValidateBatchExcelColumns(string[] BatchColumnArray)
        {
            if (BatchColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (BatchColumnArray.Contains("ItemCode") == false)
            {
                return false;
            }
            if (BatchColumnArray.Contains("Quantity") == false)
            {
                return false;
            }
            if (BatchColumnArray.Contains("BatchNumber") == false)
            {
                return false;
            }
            return true;
        }

        public ActionResult GetChilItem(string entity, int item_id)
        {
            var data = _goodsIssueService.GetChilItem(entity, item_id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
