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
        public class MaterialInController : Controller
        {
            private readonly IGenericService _genericService;
            private readonly IPlantService _plantService;
            private readonly IStorageLocation _storageLocation;
            private readonly IBucketService _bucketService;
            private readonly IMaterialOutService _materialoutservice;
            private readonly IItemService _itemService;
            private readonly IBatchService _batchservice;
            private readonly IUOMService _uomService;
            private readonly IMaterialInService _materialinservice;
            private readonly IEmployeeService _employee;

        public MaterialInController(IGenericService GenericService, IPlantService PlantService, IStorageLocation StorageLocation
                , IBucketService BucketService, IMaterialOutService MaterialOutService, IItemService itemService, IBatchService batchservice, IUOMService uomservice, IMaterialInService materialinservice, IEmployeeService employee)
            {
                _genericService = GenericService;
                _plantService = PlantService;
                _storageLocation = StorageLocation;
                _bucketService = BucketService;
                _materialoutservice = MaterialOutService;
                _itemService = itemService;
                _batchservice = batchservice;
                _uomService = uomservice;
               _materialinservice = materialinservice;
                _employee = employee;
        }
        [CustomAuthorizeAttribute("MTRLIN")]
        // GET: InventoryStock
        public ActionResult Index()
            {

                ViewBag.num = TempData["data"];
                ViewBag.DataSource = _materialinservice.GetAll();
                return View();
            }
        [CustomAuthorizeAttribute("MTRLIN")]
        // GET: InventoryStock/Create
        public ActionResult Create()
            {
                ViewBag.error = "";
               
            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(219), "document_numbring_id", "category");
                ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
                ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
                ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
                ViewBag.item_name = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");
                ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
                ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
                ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
                ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
                ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
                return View();
            }

            // POST: InventoryStock/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(inv_material_in_VM inv_Inventory_stock)
            {
                if (ModelState.IsValid)
                {
                    var issaved = _materialinservice.Add(inv_Inventory_stock);
                    if (issaved != "Error")
                    {
                        TempData["data"] = issaved;
                        return RedirectToAction("Index", TempData["data"]);
                    }
                    ViewBag.error = "Something went wrong!!";
                }

                ViewBag.category_list = new SelectList(_genericService.GetCategoryList(219), "document_numbring_id", "category");
                ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
                ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
                ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
                ViewBag.item_name = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");
                ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
                ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
                ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
                ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
                ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
                return View(inv_Inventory_stock);
            }
        [CustomAuthorizeAttribute("MTRLIN")]
        // GET: InventoryStock/Edit/5
        public ActionResult Edit(int? id)

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                inv_material_in_VM inv_Inventory_stock = _materialinservice.Get((int)id);
                if (inv_Inventory_stock == null)
                {
                    return HttpNotFound();
                }
                ViewBag.category_list = new SelectList(_genericService.GetCategoryList(219), "document_numbring_id", "category");
                ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
                ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
                ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
                ViewBag.item_name = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");
                ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
                ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
                 ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
                ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
                ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
                ViewBag.materialoutlist = new SelectList(_materialoutservice.GetAll(), "material_out_id", "document_number");
                ViewBag.cancellationreasonlist = new SelectList(_genericService.GetCANCELLATIONReason("MTRLIN"), "cancellation_reason_id", "cancellation_reason");
                ViewBag.status_list = new SelectList(_genericService.GetStatusList("MTRLIN"), "status_id", "status_name");
            return View(inv_Inventory_stock);
            }

            // POST: InventoryStock/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(inv_material_in_VM inv_Inventory_stock)
            {
                if (ModelState.IsValid)
                {
                    var issaved = _materialinservice.Add(inv_Inventory_stock);
                    if (issaved != "Error")
                    {
                        TempData["data"] = issaved;
                        return RedirectToAction("Index", TempData["data"]);
                    }
                    ViewBag.error = "Something went wrong!!";
                }

                ViewBag.category_list = new SelectList(_genericService.GetCategoryList(219), "document_numbring_id", "category");
                ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
                ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
                ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
                ViewBag.item_name = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");
                ViewBag.vendorlist = new SelectList(_genericService.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
                ViewBag.businesslist = new SelectList(_genericService.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
                ViewBag.employeelist = new SelectList(_employee.GetEmployeeCode(), "employee_id", "employee_name");
                ViewBag.batchlist = new SelectList(_batchservice.GetAll(), "item_batch_id", "batch_number");
                ViewBag.Uomlist = new SelectList(_uomService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.materialoutlist = new SelectList(_materialoutservice.GetAll(), "material_out_id", "document_number");
            return View(inv_Inventory_stock);
            }

        public ActionResult GetMaterialInforVendor(int material_out_id)
        {
            var it = _materialinservice.GetMaterialInforVendor((int)material_out_id);

            return Json(it, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMOList(int vendor_id)
        {
            var it = _materialinservice.GetMOList((int)vendor_id);

            return Json(it, JsonRequestBehavior.AllowGet);
        }

        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var mat_in = _materialinservice.MaterialIn((int)id);
                var mat_in_detail = _materialinservice.GetPurRequisitionDetailReport((int)id);
                //  inv_stock_detail

                DataSet ds = new DataSet("mat_in");

                //var mat = new List<inv_material_in_VM>();
                //mat.Add(mat_in);

                var dt1 = _genericService.ToDataTable(mat_in);
                var dt2 = _genericService.ToDataTable(mat_in_detail);

                dt1.TableName = "inv_material_in";
                dt2.TableName = "inv_material_in_detail";

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);


                rd.Load(Path.Combine(Server.MapPath("~/Reports/MaterialInReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "MaterialInReport.rpt");
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

        public ActionResult Cancel(int material_in_id, int cancellation_reason_id, string cancellation_remarks)
        {
            var issaved = _materialinservice.Cancel(material_in_id, cancellation_reason_id, cancellation_remarks);
            if (issaved.Contains("cancelled"))
            {
                return Json(issaved, JsonRequestBehavior.AllowGet);
            }
            return Json("cancelled", JsonRequestBehavior.AllowGet);

        }

    }
}
