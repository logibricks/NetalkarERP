using Excel;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class AssetMasterDataController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IAssetMasterDataService _assetmasterdata;
        string[] error = new string[30];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";

        bool is_based_on_asset_code = false;

        public AssetMasterDataController(IGenericService Generic, IAssetMasterDataService assetmasterdata)
        {
            _Generic = Generic;
            _assetmasterdata = assetmasterdata;
        }



        [HttpPost]
        public JsonResult GetIsBasedOn(bool is_based_on_asset_code_val)
        {
            is_based_on_asset_code = is_based_on_asset_code_val;

            TempData["is_based_on_asset_code"] = is_based_on_asset_code;

            string output = "SuccessFully Received";
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        // GET: AssetMasterData
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _assetmasterdata.GetAll();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.grouplist = _Generic.GetAssetGroup();
            ViewBag.dep = _assetmasterdata.GetDep();
            ViewBag.machineList = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machineList1 = _Generic.GetMachineList(0);
            ViewBag.assetclassList = _Generic.GetAssetClass().Select(a => new { a.asset_class_id, asset_class_code = a.asset_class_code + "/" + a.asset_class_des }).ToList();
            ViewBag.assetgroupList = _Generic.GetAssetGroup().Select(a => new { a.asset_group_id, asset_group_code = a.asset_group_code + "/" + a.asset_group_des }).ToList();
            ViewBag.statusList = _Generic.GetStatusList("ASSETMASDT").Select(a => new { a.status_id, a.status_name }).ToList();
            ViewBag.plantList = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.countryList = _Generic.GetCountryList().Select(a => new { a.COUNTRY_ID, a.COUNTRY_NAME }).ToList();
            ViewBag.priorityList = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.businessunitList = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.vendorList = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.costcenterList = _Generic.GetCostCenter().Select(a => new { a.cost_center_id, cost_center_code = a.cost_center_code + "/" + a.cost_center_description }).ToList();
            return View();
        }



        public ActionResult Save(ref_asset_master_data_vm Assetdata, List<ref_asset_master_data_dep_parameter_vm> DepParaArr)
        {
            var add = _Generic.CheckDuplicate(Assetdata.asset_master_data_code, "", "", "AssetMasterData", Assetdata.asset_master_data_id);
            int add1 = 0;
            if (Assetdata.machine_id != 0)
            {
                add1 = _Generic.CheckDuplicate(Convert.ToString(Assetdata.machine_id), "", "", "AssetMasterData_Dublicate_Machine", Assetdata.machine_id);
            }

            if (add == 0 && add1 == 0)
            {
                var isValid = _assetmasterdata.Add(Assetdata, DepParaArr);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid;
                    return Json(isValid, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var msg = "";
                if (add != 0)
                {
                    msg = Assetdata.asset_master_data_code + " already exists!!";
                }
                else
                {
                    msg = Assetdata.machine_name_text + " already exists!!";
                }

                TempData["doc_num"] = msg;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            ViewBag.machineList = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machineList1 = _Generic.GetMachineList(0);
            ViewBag.assetclassList = _Generic.GetAssetClass().Select(a => new { a.asset_class_id, asset_class_code = a.asset_class_code + "/" + a.asset_class_des }).ToList();
            ViewBag.assetgroupList = _Generic.GetAssetGroup().Select(a => new { a.asset_group_id, asset_group_code = a.asset_group_code + "/" + a.asset_group_des }).ToList();
            ViewBag.statusList = _Generic.GetStatusList("ASSETMASDT").Select(a => new { a.status_id, a.status_name }).ToList();
            ViewBag.plantList = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.countryList = _Generic.GetCountryList().Select(a => new { a.COUNTRY_ID, a.COUNTRY_NAME }).ToList();
            ViewBag.priorityList = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.businessunitList = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.vendorList = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.costcenterList = _Generic.GetCostCenter().Select(a => new { a.cost_center_id, cost_center_code = a.cost_center_code + "/" + a.cost_center_description }).ToList();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_asset_master_data_vm vm = _assetmasterdata.Get((int)id);
            if (vm == null)
            {
                return HttpNotFound();
            }

            ViewBag.machineList = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machineList1 = _Generic.GetMachineList(0);
            ViewBag.assetclassList = _Generic.GetAssetClass().Select(a => new { a.asset_class_id, asset_class_code = a.asset_class_code + "/" + a.asset_class_des }).ToList();
            ViewBag.assetgroupList = _Generic.GetAssetGroup().Select(a => new { a.asset_group_id, asset_group_code = a.asset_group_code + "/" + a.asset_group_des }).ToList();
            ViewBag.statusList = _Generic.GetStatusList("ASSETMASDT").Select(a => new { a.status_id, a.status_name }).ToList();
            ViewBag.plantList = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.countryList = _Generic.GetCountryList().Select(a => new { a.COUNTRY_ID, a.COUNTRY_NAME }).ToList();
            ViewBag.priorityList = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.businessunitList = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.vendorList = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.costcenterList = _Generic.GetCostCenter().Select(a => new { a.cost_center_id, cost_center_code = a.cost_center_code + "/" + a.cost_center_description }).ToList();
            ViewBag.AssetClassDepreciationList = _Generic.GetAssetClassDepreciationList();
            ViewBag.GetDepArea = new SelectList(_assetmasterdata.GetDepArea(), "dep_area_id", "dep_area_code");

            return View(vm);
        }


        [HttpPost]

        public ActionResult UploadFiles()
        {
            string isSucess = null;
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
                    List<ref_asset_master_data_excel_vm> asset_master_data_vm = new List<Domain.ViewModel.ref_asset_master_data_excel_vm>();
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
                                    ref_asset_master_data_excel_vm asset_master_data = new ref_asset_master_data_excel_vm();

                                    is_based_on_asset_code = Convert.ToBoolean(TempData["is_based_on_asset_code"]);
                                    string asset_code = excelReader.GetString(Array.IndexOf(columnarray, "Asset Code")); //Available for Without Code also
                                    string machine_code = is_based_on_asset_code == true ? excelReader.GetString(Array.IndexOf(columnarray, "Machine Code")) : null;
                                    string asset_class = excelReader.GetString(Array.IndexOf(columnarray, "Asset Class"));//Available for Without Code also
                                    string asset_group = excelReader.GetString(Array.IndexOf(columnarray, "Asset Group")); //Available for Without Code also
                                    string status = excelReader.GetString(Array.IndexOf(columnarray, "Status")); //Available for Without Code also
                                    string cost_center = excelReader.GetString(Array.IndexOf(columnarray, "Cost Center")); //Available for Without Code also
                                    string capitalisation_date = excelReader.GetString(Array.IndexOf(columnarray, "Capitalisation Date")); //Available for Without Code also

                                    asset_master_data.is_based_on_machine_code = is_based_on_asset_code;
                                    asset_master_data.asset_code = asset_code;
                                    asset_master_data.machine_code = is_based_on_asset_code == true ? machine_code : null;
                                    asset_master_data.asset_class = asset_class;
                                    asset_master_data.asset_group = asset_group;
                                    asset_master_data.status = status;
                                    asset_master_data.cost_center = cost_center;
                                    asset_master_data.capitalisation_date = capitalisation_date;

                                    //Available for Without Code also

                                    if (is_based_on_asset_code == false)
                                    {
                                        string description = excelReader.GetString(Array.IndexOf(columnarray, "Description")); //Available for Without Code also
                                        string plant = excelReader.GetString(Array.IndexOf(columnarray, "Plant")); //Available for Without Code also
                                        string purchase_order = excelReader.GetString(Array.IndexOf(columnarray, "Purchase Order")); //Available for Without Code also
                                        string manufacturer = excelReader.GetString(Array.IndexOf(columnarray, "Manufacturer")); //Available for Without Code also
                                        string manufacturer_part_number = excelReader.GetString(Array.IndexOf(columnarray, "Manufacturer Part Number")); //Available for Without Code also
                                        string manufacturing_country = excelReader.GetString(Array.IndexOf(columnarray, "Manufacturing Country")); //Available for Without Code also
                                        string priority = excelReader.GetString(Array.IndexOf(columnarray, "Priority")); //Available for Without Code also
                                        string business_unit = excelReader.GetString(Array.IndexOf(columnarray, "Business Unit")); //Available for Without Code also
                                        string asset_tag_no = excelReader.GetString(Array.IndexOf(columnarray, "Asset Tag No")); //Available for Without Code also
                                        string purchasing_vendor_code = excelReader.GetString(Array.IndexOf(columnarray, "Purchasing Vendor Code")); //Available for Without Code also
                                        string model_number = excelReader.GetString(Array.IndexOf(columnarray, "Model Number")); //Available for Without Code also
                                        string manufacturer_serial_number = excelReader.GetString(Array.IndexOf(columnarray, "manufacturer Serial Number")); //Available for Without Code also
                                        string manufacturing_date = excelReader.GetString(Array.IndexOf(columnarray, "Manufacturing Date")); //Available for Without Code also


                                        asset_master_data.description = description;
                                        asset_master_data.plant = plant;
                                        asset_master_data.purchase_order = purchase_order;
                                        asset_master_data.manufacturer = manufacturer;
                                        asset_master_data.manufacturer_part_number = manufacturer_part_number;
                                        asset_master_data.manufacturing_country = manufacturing_country;
                                        asset_master_data.priority = priority;
                                        asset_master_data.business_unit = business_unit;
                                        asset_master_data.asset_tag_no = asset_tag_no;
                                        asset_master_data.purchasing_vendor_code = purchasing_vendor_code;
                                        asset_master_data.model_number = model_number;
                                        asset_master_data.manufacturer_serial_number = manufacturer_serial_number;
                                        asset_master_data.manufacturing_date = manufacturing_date;
                                    }

                                    //

                                    //ref_asset_master_data_excel_vm asset_master_data = new ref_asset_master_data_excel_vm//();
                                    //{


                                    //Without

                                    //};

                                    asset_master_data_vm.Add(asset_master_data);
                                    ////Asset Code

                                    //if (asset_code != null && asset_code != "")
                                    //{

                                    //}else
                                    //{
                                    //    errorList++;
                                    //    error[error.Length - 1] = machine_code;
                                    //    errorMessage = machine_code + " not found";
                                    //}

                                    ////Machine Code
                                    //if(machine_code != "" && machine_code != null)
                                    //{
                                    //    var machine_id = _Generic.GetMachineList(0).Where(x => x.machine_code + "/" + x.machine_name == machine_code).Select(x => x.machine_id).FirstOrDefault();

                                    //    if (machine_id == 0)
                                    //    {
                                    //        errorList++;
                                    //        error[error.Length - 1] = machine_code;
                                    //        errorMessage = machine_code + " not found";
                                    //    }
                                    //    else
                                    //    {
                                    //        asset_master_data.machine_id = machine_id;
                                    //    }

                                    //}else
                                    //{
                                    //    errorList++;
                                    //    error[error.Length - 1] = machine_code;
                                    //    errorMessage = machine_code + "is Empty";
                                    //}


                                    ////Asset Class

                                    //if(asset_class != "" && asset_class != null)
                                    //{
                                    //    var asset_class_id = _Generic.GetAssetClass().Where(x => x.asset_class_code + "/" + x.asset_class_des == asset_class).Select(x => x.asset_class_id).FirstOrDefault();

                                    //    if (asset_class_id == 0)
                                    //    {
                                    //        errorList++;
                                    //        error[error.Length - 1] = asset_class;
                                    //        errorMessage = asset_class + " not found";
                                    //    }
                                    //    else
                                    //    {
                                    //        asset_master_data.asset_class_id = asset_class_id;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    errorList++;
                                    //    error[error.Length - 1] = asset_class;
                                    //    errorMessage = asset_class + "is Empty";
                                    //}

                                    ////Asset Group
                                    //if(asset_group != null  &&  asset_group != "")
                                    //{
                                    //    var asset_group_id = _Generic.GetAssetGroup().Where(x => x.asset_group_code + "/" + x.asset_group_des == asset_group).Select(x => x.asset_group_id).FirstOrDefault();

                                    //    if (asset_group_id == 0)
                                    //    {
                                    //        errorList++;
                                    //        error[error.Length - 1] = asset_group;
                                    //        errorMessage = asset_group + " not found";
                                    //    }
                                    //    else
                                    //    {
                                    //        asset_master_data.asset_group_id = asset_group_id;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    errorList++;
                                    //    error[error.Length - 1] = asset_group;
                                    //    errorMessage = asset_group + "is Empty";
                                    //}



                                    ////Status 

                                    //if(status != null && status != "")
                                    //{
                                    //    var status_id = _Generic.GetStatusList("ASSETMASDT").Where(x => x.status_name == status).Select(x => x.status_id).FirstOrDefault();

                                    //    if (status_id == 0)
                                    //    {
                                    //        errorList++;
                                    //        error[error.Length - 1] = status;
                                    //        errorMessage = status + " not found";
                                    //    }
                                    //    else
                                    //    {
                                    //        asset_master_data.asset_group_id = status_id;
                                    //    }
                                    //}else
                                    //{
                                    //    errorList++;
                                    //    error[error.Length - 1] = status;
                                    //    errorMessage = status + " is Empty";
                                    //}

                                    ////Cost Center

                                    //var cost_center_id = _Generic.GetCostCenter().Where(x => x.cost_center_code + '/' +  x.cost_center_description == cost_center).Select(x => x.cost_center_id).FirstOrDefault();

                                    //if (cost_center_id == 0)
                                    //{
                                    //    errorList++;
                                    //    error[error.Length - 1] = cost_center;
                                    //    errorMessage = cost_center + " not found";
                                    //}
                                    //else
                                    //{
                                    //    asset_master_data.cost_center_id = cost_center_id;
                                    //}

                                    //Capitilisation Date




                                    //END

                                    //    var offset_account = _Generic.getOffsetAccount("Opening Balance Inventory Offset", 9);
                                    //    if (inventory_balance_VM.Count != 0)
                                    //    {
                                    //        var zz = inventory_balance_VM.Where(z => z.posting_date == DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "PostingDate"))) && z.header_remarks == excelReader.GetString(Array.IndexOf(columnarray, "HeaderRemarks"))).FirstOrDefault();
                                    //        if (zz == null)
                                    //        {
                                    //            errorMessage = "Add same Header Remarks and Posting Date.";
                                    //            errorList++;
                                    //            error[error.Length - 1] = "Add same Header Remarks and Posting Date.";
                                    //            //var z = new inventory_balance_VM();
                                    //            //z.offset_account = int.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Offset Account")));
                                    //            //z.posting_date = DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Posting Date")));
                                    //            //z.header_remarks = excelReader.GetString(Array.IndexOf(columnarray, "Header Remarks"));
                                    //            //inventory_balance_VM.Add(z);
                                    //            //current_id = int.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Offset Account")));
                                    //        }
                                    //        else
                                    //        {
                                    //            inventory_balance_detail_VM IDVM = new inventory_balance_detail_VM();
                                    //            var item_code = excelReader.GetString(Array.IndexOf(columnarray, "ItemCode"));
                                    //            var item_id = _item.GetID(item_code);
                                    //            //IDVM.item_id = _item.GetAll().Where(x => x.ITEM_CODE == item_code).FirstOrDefault().ITEM_ID;
                                    //            if (item_id == 0)
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = item_code;
                                    //                errorMessage = item_code + "not found";
                                    //            }
                                    //            else
                                    //            {
                                    //                IDVM.item_id = item_id;
                                    //            }
                                    //            var plant = excelReader.GetString(Array.IndexOf(columnarray, "Plant"));
                                    //            var plant_id = _Generic.GetPlantID(plant);
                                    //            //IDVM.plant_id = _plant.GetAll().Where(x => x.PLANT_NAME == plant).FirstOrDefault().PLANT_ID;
                                    //            if (plant_id == 0)
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = plant;
                                    //                errorMessage = plant + "not found";
                                    //            }
                                    //            else
                                    //            {
                                    //                IDVM.plant_id = plant_id;
                                    //            }
                                    //            var sloc = excelReader.GetString(Array.IndexOf(columnarray, "Sloc"));
                                    //            var sloc_id = _storageLocation.GetID(sloc);
                                    //            //IDVM.sloc_id = _storageLocation.GetAll().Where(x => x.STORAGE_LOCATION_NAME == sloc).FirstOrDefault().STORAGE_LOCATION_ID;
                                    //            if (sloc_id == 0)
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = sloc;
                                    //                errorMessage = sloc + "not found";
                                    //            }
                                    //            else
                                    //            {
                                    //                IDVM.sloc_id = sloc_id;
                                    //            }
                                    //            var bucket = excelReader.GetString(Array.IndexOf(columnarray, "Bucket"));
                                    //            if (bucket.ToLower() == "quality")
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = bucket;
                                    //                errorMessage = bucket + " Bucket cannot be uploaded.";
                                    //            }
                                    //            else
                                    //            {
                                    //                var bucket_id = _bucket.GetID(bucket);
                                    //                //IDVM.bucket_id = _bucket.GetAll().Where(x => x.bucket_name == bucket).FirstOrDefault().bucket_id;
                                    //                if (bucket_id == 0)
                                    //                {
                                    //                    errorList++;
                                    //                    error[error.Length - 1] = bucket;
                                    //                    errorMessage = bucket + "not found";
                                    //                }
                                    //                else
                                    //                {
                                    //                    IDVM.bucket_id = bucket_id;
                                    //                }
                                    //            }
                                    //            //IDVM.batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                    //            var item_detail = _item.GetItemsDetail(item_id);
                                    //            if (item_detail.BATCH_MANAGED == true)
                                    //            {
                                    //                var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                    //                if (batch == null)
                                    //                {
                                    //                    errorList++;
                                    //                    error[error.Length - 1] = batch;
                                    //                    errorMessage = item_code + " batch not mentioned";
                                    //                }
                                    //                else
                                    //                {
                                    //                    IDVM.batch = batch;
                                    //                }
                                    //            }
                                    //            else
                                    //            {
                                    //                var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                    //                if (batch != null)
                                    //                {
                                    //                    errorList++;
                                    //                    error[error.Length - 1] = batch;
                                    //                    errorMessage = item_code + " is not batch managed";
                                    //                }
                                    //                else
                                    //                {
                                    //                    IDVM.batch = "";
                                    //                }
                                    //            }
                                    //            IDVM.qty = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Qty")));
                                    //            var uom = excelReader.GetString(Array.IndexOf(columnarray, "UoM"));
                                    //            var uom_id = _Uom.GetID(uom);
                                    //            //IDVM.uom_id = _Uom.GetAll().Where(x => x.UOM_NAME == uom).FirstOrDefault().UOM_ID;
                                    //            if (uom_id == 0)
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = uom;
                                    //                errorMessage = uom + "not found";
                                    //            }
                                    //            else
                                    //            {
                                    //                IDVM.uom_id = uom_id;
                                    //            }
                                    //            IDVM.rate = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Rate")));
                                    //            if (IDVM.qty == 0)
                                    //            {
                                    //                IDVM.qty = 1;
                                    //            }
                                    //            IDVM.value = IDVM.qty * IDVM.rate;
                                    //            IDVM.line_remarks = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));
                                    //            IDVM.offset_account = offset_account[0].gl_ledger_code;
                                    //            bldetails.Add(IDVM);
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        inventory_balance_VM IBS = new inventory_balance_VM();
                                    //        var category_id = _Generic.GetCategoryList(15);
                                    //        IBS.category_id = category_id[0].document_numbring_id;
                                    //        IBS.doc_number = _Generic.GetDocumentNumbering(IBS.category_id);
                                    //        IBS.offset_account = offset_account[0].gl_ledger_code;
                                    //        var offset_account_id = _generalLedger.GetID(IBS.offset_account);
                                    //        if (offset_account_id == 0)
                                    //        {
                                    //            errorMessage = IBS.offset_account + " notfound!";
                                    //            errorList++;
                                    //            error[error.Length - 1] = IBS.offset_account;
                                    //        }
                                    //        else
                                    //        {
                                    //            IBS.offset_account_id = offset_account_id;
                                    //        }
                                    //        IBS.posting_date = DateTime.Parse(excelReader.GetString(Array.IndexOf(columnarray, "PostingDate")));
                                    //        IBS.header_remarks = excelReader.GetString(Array.IndexOf(columnarray, "HeaderRemarks"));

                                    //        inventory_balance_detail_VM IDVM = new inventory_balance_detail_VM();

                                    //        var item_code = excelReader.GetString(Array.IndexOf(columnarray, "ItemCode"));
                                    //        var item_id = _item.GetID(item_code);
                                    //        //IDVM.item_id = _item.GetAll().Where(x => x.ITEM_CODE == item_code).FirstOrDefault().ITEM_ID;
                                    //        if (item_id == 0)
                                    //        {
                                    //            errorList++;
                                    //            error[error.Length - 1] = item_code;
                                    //            errorMessage = item_code + "not found";
                                    //        }
                                    //        else
                                    //        {
                                    //            IDVM.item_id = item_id;
                                    //        }
                                    //        var plant = excelReader.GetString(Array.IndexOf(columnarray, "Plant"));
                                    //        var plant_id = _Generic.GetPlantID(plant);
                                    //        //IDVM.plant_id = _plant.GetAll().Where(x => x.PLANT_NAME == plant).FirstOrDefault().PLANT_ID;
                                    //        if (plant_id == 0)
                                    //        {
                                    //            errorList++;
                                    //            error[error.Length - 1] = plant;
                                    //            errorMessage = plant + "not found";
                                    //        }
                                    //        else
                                    //        {
                                    //            IDVM.plant_id = plant_id;
                                    //        }
                                    //        var sloc = excelReader.GetString(Array.IndexOf(columnarray, "Sloc"));
                                    //        var sloc_id = _storageLocation.GetID(sloc);
                                    //        //IDVM.sloc_id = _storageLocation.GetAll().Where(x => x.STORAGE_LOCATION_NAME == sloc).FirstOrDefault().STORAGE_LOCATION_ID;
                                    //        if (sloc_id == 0)
                                    //        {
                                    //            errorList++;
                                    //            error[error.Length - 1] = sloc;
                                    //            errorMessage = sloc + "not found";
                                    //        }
                                    //        else
                                    //        {
                                    //            IDVM.sloc_id = sloc_id;
                                    //        }
                                    //        var bucket = excelReader.GetString(Array.IndexOf(columnarray, "Bucket"));
                                    //        if (bucket.ToLower() == "quality")
                                    //        {
                                    //            errorList++;
                                    //            error[error.Length - 1] = bucket;
                                    //            errorMessage = bucket + " Bucket cannot be uploaded.";
                                    //        }
                                    //        else
                                    //        {
                                    //            var bucket_id = _bucket.GetID(bucket);
                                    //            //IDVM.bucket_id = _bucket.GetAll().Where(x => x.bucket_name == bucket).FirstOrDefault().bucket_id;
                                    //            if (bucket_id == 0)
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = bucket;
                                    //                errorMessage = bucket + "not found";
                                    //            }
                                    //            else
                                    //            {
                                    //                IDVM.bucket_id = bucket_id;
                                    //            }
                                    //        }
                                    //        var item_detail = _item.GetItemsDetail(item_id);
                                    //        if (item_detail.BATCH_MANAGED == true)
                                    //        {
                                    //            var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                    //            if (batch == null)
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = batch;
                                    //                errorMessage = item_code + " batch not mentioned";
                                    //            }
                                    //            else
                                    //            {
                                    //                IDVM.batch = batch;
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            var batch = excelReader.GetString(Array.IndexOf(columnarray, "Batch"));
                                    //            if (batch != null)
                                    //            {
                                    //                errorList++;
                                    //                error[error.Length - 1] = batch;
                                    //                errorMessage = item_code + " is not batch managed";
                                    //            }
                                    //            else
                                    //            {
                                    //                IDVM.batch = "";
                                    //            }
                                    //        }

                                    //        IDVM.qty = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Qty")));
                                    //        var uom = excelReader.GetString(Array.IndexOf(columnarray, "UoM"));
                                    //        var uom_id = _Uom.GetID(uom);
                                    //        //IDVM.uom_id = _Uom.GetAll().Where(x => x.UOM_NAME == uom).FirstOrDefault().UOM_ID;
                                    //        if (uom_id == 0)
                                    //        {
                                    //            errorList++;
                                    //            error[error.Length - 1] = uom;
                                    //            errorMessage = uom + "not found";
                                    //        }
                                    //        else
                                    //        {
                                    //            IDVM.uom_id = uom_id;
                                    //        }
                                    //        IDVM.rate = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Rate")));
                                    //        if (IDVM.qty == 0)
                                    //        {
                                    //            IDVM.qty = 1;
                                    //        }
                                    //        IDVM.value = IDVM.qty * IDVM.rate;
                                    //        IDVM.line_remarks = excelReader.GetString(Array.IndexOf(columnarray, "LineRemarks"));
                                    //        IDVM.offset_account = IBS.offset_account;
                                    //        bldetails.Add(IDVM);
                                    //        inventory_balance_VM.Add(IBS);
                                    //    }
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
                        isSucess = _assetmasterdata.AddExcel(asset_master_data_vm, is_based_on_asset_code);
                        if (isSucess.Contains("ERROR"))
                        {
                            errorList++;
                            error[0] = isSucess;
                            errorMessage = isSucess;
                        }

                        if (isSucess == "Saved Sucessfully")
                        {
                            errorMessage = "success";
                            return Json(errorMessage, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            errorMessage = "Failed " + errorMessage;
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
            is_based_on_asset_code = Convert.ToBoolean(TempData["is_based_on_asset_code"]);

            if (columnarray.Contains("SrNo") == false)
            {
                return false;
            }
            if (columnarray.Contains("Asset Code") == false)
            {
                return false;
            }

            if (is_based_on_asset_code == true)
            {
                if (columnarray.Contains("Machine Code") == false)
                {
                    return false;
                }
            }


            if (columnarray.Contains("Asset Class") == false)
            {
                return false;
            }
            if (columnarray.Contains("Asset Group") == false)
            {
                return false;
            }
            if (columnarray.Contains("Status") == false)
            {
                return false;
            }
            if (columnarray.Contains("Cost Center") == false)
            {
                return false;
            }

            if (is_based_on_asset_code == true)
            {
                if (columnarray.Contains("Capitalisation Date") == false)
                {
                    return false;
                }
            }



            if (is_based_on_asset_code == false)
            {

                if (columnarray.Contains("Description") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Plant") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Purchase Order") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Manufacturer") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Manufacturer Part Number") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Manufacturing Country") == false)
                {
                    return false;
                }

                if (columnarray.Contains("Priority") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Business Unit") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Asset Tag No") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Purchasing Vendor Code") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Model Number") == false)
                {
                    return false;
                }
                if (columnarray.Contains("manufacturer Serial Number") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Manufacturing Date") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Cost Center") == false)
                {
                    return false;
                }
                if (columnarray.Contains("Capitalisation Date") == false)
                {
                    return false;
                }

            }

            return true;
        }

        public ActionResult GetDepDetails(int dep_area_id, int asset_master_data_id, string name)
        {
            var data = _assetmasterdata.GetDepDetails(dep_area_id, asset_master_data_id, name);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepreciationAndLedgerDetails(int dep_area_id, int asset_master_data_id, string name)
        {
            var data = _assetmasterdata.GetDepreciationAndLedgerDetails(dep_area_id, asset_master_data_id, name);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}