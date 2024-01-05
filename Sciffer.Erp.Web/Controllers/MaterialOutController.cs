using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class MaterialOutController : Controller
    {
        private readonly IGenericService _genericService;
        private readonly IPlantService _plantService;
        private readonly IStorageLocation _storageLocation;
        private readonly IBucketService _bucketService;
        private readonly IMaterialOutService _materialoutservice;
        private readonly IItemService _itemService;
        private readonly IBatchService _batchservice;
        private readonly IUOMService _uomService;
        private readonly IEmployeeService _employee;



        public MaterialOutController(IGenericService GenericService, IPlantService PlantService, IStorageLocation StorageLocation
            , IBucketService BucketService, IMaterialOutService MaterialOutService, IItemService itemService, IBatchService batchservice,IUOMService uomservice, IEmployeeService employee)
        {
            _genericService = GenericService;
            _plantService = PlantService;
            _storageLocation = StorageLocation;
            _bucketService = BucketService;
            _materialoutservice = MaterialOutService;
            _itemService = itemService;
            _batchservice = batchservice;
            _uomService = uomservice;
            _employee = employee;
        }
        [CustomAuthorizeAttribute("MTRLO")]
        // GET: InventoryStock
        public ActionResult Index()
        {

            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _materialoutservice.GetAll();
            return View();
        }
        [CustomAuthorizeAttribute("MTRLO")]
        // GET: InventoryStock/Create
        public ActionResult Create()
        {

            var user_id = (int)Session["User_Id"];

            ViewBag.EmployeeId = _genericService.GetEmployeeIdFromUser(user_id);
            ViewBag.error = "";
            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(218), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.item_name = new SelectList(_genericService.GetItemListByType(), "ITEM_ID", "ITEM_NAME");
            ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
            ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
            ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.hsn_list = new SelectList(_genericService.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.tax_list = new SelectList(_genericService.GetTaxList(), "tax_id", "tax_name");
            return View();
        }

        // POST: InventoryStock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(inv_material_out_VM inv_Inventory_stock)
        {
            if (ModelState.IsValid)
            {
                var issaved = _materialoutservice.Add(inv_Inventory_stock);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong!!";
            }

            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(218), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.item_name = new SelectList(_genericService.GetItemListByType(), "ITEM_ID", "ITEM_NAME");
            ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
            ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
            ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.hsn_list = new SelectList(_genericService.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.tax_list = new SelectList(_genericService.GetTaxList(), "tax_id", "tax_name");
            return View(inv_Inventory_stock);
        }
        [CustomAuthorizeAttribute("MTRLO")]
        // GET: InventoryStock/Edit/5
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_material_out_VM inv_Inventory_stock = _materialoutservice.Get((int)id);
            if (inv_Inventory_stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(218), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.item_name = new SelectList(_genericService.GetItemListByType(), "ITEM_ID", "ITEM_NAME");
            ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
            ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
            ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.hsn_list = new SelectList(_genericService.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.tax_list = new SelectList(_genericService.GetTaxList(), "tax_id", "tax_name");
            ViewBag.cancellationreasonlist = new SelectList(_genericService.GetCANCELLATIONReason("MTRLO"), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_genericService.GetStatusList("MTRLO"), "status_id", "status_name");
            return View(inv_Inventory_stock);
        }

        // POST: InventoryStock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(inv_material_out_VM inv_Inventory_stock)
        {
            if (ModelState.IsValid)
            {
                var issaved = _materialoutservice.Add(inv_Inventory_stock);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong!!";
            }

            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(218), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.item_name = new SelectList(_genericService.GetItemListByType(), "ITEM_ID", "ITEM_NAME");
            ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
            ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
            ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
            return View(inv_Inventory_stock);
        }


        public CrystalReportPdfResult Pdf(int id, int type)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                double qty = 0;
                var mat_out = _materialoutservice.MaterialOut((int)id);
                var mat_out_detail = _materialoutservice.GetPurRequisitionDetailReport((int)id);
                DataSet ds = new DataSet("pr");
                foreach (var i in mat_out_detail)
                {
                    qty = qty + i.quantity;
                }
                //var mat = new List<inv_material_out_VM>();
                //mat.Add(mat_out);
                var dt1 = _genericService.ToDataTable(mat_out);
                var dt2 = _genericService.ToDataTable(mat_out_detail);

                dt1.TableName = "inv_material_out";
                dt2.TableName = "inv_material_out_detail";
                dt1.Rows[0]["total_quantity"] = qty;
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);

              
                if (type == 1)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/MaterialOutReport.rpt")));
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/MaterialOutReporta5.rpt")));
                }
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = "";
                if (type == 1)
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "MaterialOutReport.rpt");
                }
                else
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "MaterialOutReporta5.rpt");
                }
                return new CrystalReportPdfResult(reportPath, rd);
            }
           catch(Exception ex)
            {
                return null;
            }
            finally
            {
                rd.Close();
                rd.Clone();
                rd.Dispose();
                rd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }


        public ActionResult Cancel(int material_out_id, int cancellation_reason_id, string cancellation_remarks)
        {
            var issaved = _materialoutservice.Cancel(material_out_id, cancellation_reason_id, cancellation_remarks);
            if (issaved.Contains("cancelled"))
            {
                return Json(issaved, JsonRequestBehavior.AllowGet);
            }
            return Json("cancelled", JsonRequestBehavior.AllowGet);

        }
    }
}
