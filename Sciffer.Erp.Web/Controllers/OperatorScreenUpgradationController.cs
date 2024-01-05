using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class OperatorScreenUpgradationController : Controller
    {
        private readonly IGenericService _Generic;
        public readonly IProcessMasterService _Process;
        public readonly IOperatorScreenUpgradationService _Upgradtion;
        public OperatorScreenUpgradationController(IGenericService gen, IProcessMasterService process, IOperatorScreenUpgradationService upgradation)
        {
            _Generic = gen;
            _Process = process;
            _Upgradtion = upgradation;
        }

        [CustomAuthorizeAttribute("OPSUG")]
        // GET: OperatorScreenUpGradiation
        public ActionResult Index()
        {
            ViewBag.DataSource = _Upgradtion.getall();
            return View();
        }


        [CustomAuthorizeAttribute("OPSUG")]
        public ActionResult Create()
        {
            ViewBag.process_list = new SelectList(_Process.GetAll(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            return View();
        }

        public ActionResult GetMachineListByProcess(int process_id)
        {
            var data = new SelectList(_Upgradtion.GetMachineListByProcess(process_id), "machine_id", "machine_name");
            return Json(data);
        }

        public ActionResult Delete(int id)
        {
            var result = _Upgradtion.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var screen = _Upgradtion.get(id);
            ViewBag.process_list = new SelectList(_Process.GetAll(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            return View(screen);
        }

        public JsonResult UploadFile(FormCollection fc, string filename = "")
        {
            mfg_machine_item_upgradation mfg_machine_item_upgradation = new mfg_machine_item_upgradation();

            var txtResult = "";

            if (fc["ITEM_ID"] == "")
            {
                return Json(new { Status = "ERROR", Message = "Please select Item", data = txtResult, filePath = "" });

            }
            else if (fc["PROCESS_ID"] == "")
            {
                return Json(new { Status = "ERROR", Message = "Please select Process", data = txtResult, filePath = "" });

            }
            else if (fc["MACHINE_ID"] == "")
            {
                return Json(new { Status = "ERROR", Message = "Please select Machine", data = txtResult, filePath = "" });

            }

            try
            {
                var httpRequest = HttpContext.Request;
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        var filePath = HttpContext.Server.MapPath("~/Files/Upgradations/" + postedFile.FileName);

                        IList<string> AllowedFileExtensions = new List<string> { ".pdf" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();

                        if (AllowedFileExtensions.Contains(extension))
                        {
                            mfg_machine_item_upgradation.item_id = int.Parse(fc["ITEM_ID"]);
                            mfg_machine_item_upgradation.process_id = int.Parse(fc["PROCESS_ID"]);
                            mfg_machine_item_upgradation.machine_id = int.Parse(fc["MACHINE_ID"]);
                            mfg_machine_item_upgradation.file_name = postedFile.FileName;
                            mfg_machine_item_upgradation.file_path = "/Files/Upgradations/" + postedFile.FileName;

                            txtResult = _Upgradtion.UploadFile(mfg_machine_item_upgradation);
                            if (txtResult == "Saved")
                            {
                                postedFile.SaveAs(filePath);
                                return Json(new { Status = "Success", Message = "Upload Successfully", data = txtResult, filePath = filePath });
                            }
                        }
                    }
                }

                var res = string.Format("Please Upload a File.");
                return Json(new { Status = "ERROR", Message = res, data = txtResult, filePath = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "ERROR", Message = ex.Message, data = txtResult });
            }
        }

        public JsonResult UploadFile1(FormCollection fc, string filename = "")
        {
            mfg_machine_item_upgradation mfg_machine_item_upgradation = new mfg_machine_item_upgradation();

            var txtResult = "";

            if (fc["ITEM_ID"] == "")
            {
                return Json(new { Status = "ERROR", Message = "Please select Item", data = txtResult, filePath = "" });

            }
            else if (fc["PROCESS_ID"] == "")
            {
                return Json(new { Status = "ERROR", Message = "Please select Process", data = txtResult, filePath = "" });

            }
            else if (fc["MACHINE_ID"] == "")
            {
                return Json(new { Status = "ERROR", Message = "Please select Machine", data = txtResult, filePath = "" });

            }

            try
            {
                var httpRequest = HttpContext.Request;
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        var filePath = HttpContext.Server.MapPath("~/Files/Upgradations/" + postedFile.FileName);

                        IList<string> AllowedFileExtensions = new List<string> { ".pdf" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();

                        if (AllowedFileExtensions.Contains(extension))
                        {
                            mfg_machine_item_upgradation.item_id = int.Parse(fc["ITEM_ID"]);
                            mfg_machine_item_upgradation.process_id = int.Parse(fc["PROCESS_ID"]);
                            mfg_machine_item_upgradation.machine_id = int.Parse(fc["MACHINE_ID"]);
                            mfg_machine_item_upgradation.file_name = postedFile.FileName;
                            mfg_machine_item_upgradation.file_path = "/Files/Upgradations/" + postedFile.FileName;
                            mfg_machine_item_upgradation.mfg_upgradation_id = int.Parse(fc["UPDATE_ID"]);
                            var txtResult1 = _Upgradtion.Update(mfg_machine_item_upgradation);
                            if (txtResult1.mfg_upgradation_id != 0)
                            {
                                postedFile.SaveAs(filePath);
                                return Json(new { Status = "Success", Message = "Upload Successfully", data = txtResult, filePath = filePath });
                            }
                        }
                    }
                }

                var res = string.Format("Please Upload a File.");
                return Json(new { Status = "ERROR", Message = res, data = txtResult, filePath = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "ERROR", Message = ex.Message, data = txtResult });
            }
        }
    }
}