using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class UploadFilesController : Controller
    {
        // GET: UploadFiles
        [CustomAuthorizeAttribute("UPL")]
        public ActionResult Index(string Status,string text,string error, string errorMessage)
        {
            ViewBag.status = Status;
            ViewBag.text = text;
            ViewBag.error = error;
            ViewBag.errormessage = errorMessage;
            return View();
        }
        
        [HttpPost]
        public ActionResult Upload(FormCollection fc, HttpPostedFileBase file)
        {
            var value = fc.Get("dropdowndata");
            string extension = System.IO.Path.GetExtension(file.FileName);
            string path1 = string.Format("{0}/{1}", Server.MapPath("~/Uploads"), file.FileName);
            if (System.IO.File.Exists(path1))
                System.IO.File.Delete(path1);

            file.SaveAs(path1);
            switch (value)
            {
                case "vendor":
                    return RedirectToAction("UploadFiles", "VendorMaster", new { file = file.FileName });
                case "Customer.xlsx":
                    return RedirectToAction("UploadFiles", "CustomerMaster", new { file = "" });
                    //break;
            }
            
            return RedirectToAction("Index");
        }
        public FileResult downloadFile(string dd )
        {
            if (dd == "vendor")
            {
                return File(Server.MapPath("~/Download/VendorMaster.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VendorMaster.xlsx");

            }
            else if (dd == "customer")
            {
                return File(Server.MapPath("~/Download/CustomerMaster.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerMaster.xlsx");

            }
            else if (dd == "CycleTime")
            {
                return File(Server.MapPath("~/Download/CycleTime.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CycleTime.xlsx");

            }
            else if (dd == "budget")
            {
                return File(Server.MapPath("~/Download/BudgetMaster.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BudgetMaster.xlsx");

            }
            else if (dd == "Item")
            {
                return File(Server.MapPath("~/Download/ItemsMaster.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ItemsMaster.xlsx");

            }
            else if (dd == "GeneralLedger")
            {
                return File(Server.MapPath("~/Download/ChartsOfAccount.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ChartsOfAccount.xlsx");

            }
            else if (dd == "EmployeeBalance")
            {
                return File(Server.MapPath("~/Download/EmployeeBalance.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeBalance.xlsx");

            }
            else if (dd == "CustomerBalance")
            {
                return File(Server.MapPath("~/Download/CustomerBalance.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerBalance.xlsx");

            }
            else if (dd == "GeneralLedgerBalance")
            {
                return File(Server.MapPath("~/Download/GeneralLedgerBalance.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GeneralLedgerBalance.xlsx");

            }
            else if (dd == "InventoryBalance")
            {
                return File(Server.MapPath("~/Download/InventoryBalance.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InventoryBalance.xlsx");

            }
            else if (dd == "VendorBalance")
            {
                return File(Server.MapPath("~/Download/VendorBalance.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VendorBalance.xlsx");

            }
            else if (dd == "PriceListCustomer")
            {
                return File(Server.MapPath("~/Download/PriceListCustomer.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PriceListCustomer.xlsx");

            }
            else if (dd == "PriceListVendor")
            {
                return File(Server.MapPath("~/Download/PriceListVendor.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PriceListVendor.xlsx");

            }
            else if (dd == "GoodsIssue")
            {
                return File(Server.MapPath("~/Download/GoodsIssue.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GoodsIssue.xlsx");
            }
            else if (dd == "AssetMasterDataWithBaseOnMachineCode")
            {
                return File(Server.MapPath("~/Download/AssetMasterDataWithBaseOnMachineCode.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AssetMasterDataWithBaseOnMachineCode.xlsx");

            }else if(dd == "AssetMasterDataWithoutBaseOnMachineCode")
            {
                return File(Server.MapPath("~/Download/AssetMasterDataWithoutBaseOnMachineCode.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AssetMasterDataWithoutBaseOnMachineCode.xlsx");
            }
            else if(dd == "IntialUploadWithWDV_SLM")
            {
                return File(Server.MapPath("~/Download/IntialUploadWithWDV_SLM.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IntialUploadWithWDV_SLM.xlsx");
            }
            else if (dd == "InitialUploadWith_BLOCK")
            {
                return File(Server.MapPath("~/Download/InitialUploadWith_BLOCK.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InitialUploadWith_BLOCK.xlsx");
            }
            else if (dd == "InitialUpload")
            {
                return File(Server.MapPath("~/Download/InitialUpload.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InitialUpload.xlsx");

            }
            else
            {
                return File("Error", "");
            }
        }
    }
}