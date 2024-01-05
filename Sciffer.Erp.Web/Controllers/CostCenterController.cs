using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CostCenterController : Controller
    {
        private readonly ICostCenterService _coscenterservice;
        private readonly IGenericService _Generic;
        public CostCenterController(ICostCenterService CostCenter, IGenericService Generic)
        {
            _coscenterservice = CostCenter;
            _Generic = Generic;

        }
        // GET: CostCenter
        [CustomAuthorizeAttribute("CSTCEN")]
        public ActionResult Index()
        {
            //var parentid = _coscenterservice.GetCostCenter();
            //ViewBag.DataSource = _coscenterservice.GetAll();
            var parentid = _coscenterservice.GetParent();
            var res = _coscenterservice.GetTreeVeiwList(parentid);
            ViewBag.DataSource = res;
            ViewBag.parentlist = new SelectList(parentid, "cost_center_id", "cost_center_code");
            return View(res);
        }

      
       
        //delete
        public ActionResult InLineDelete(int key)
        {
            var delete = _coscenterservice.Delete(key);
            return Json(delete, JsonRequestBehavior.AllowGet);
        }       
        public ActionResult InlineInsert(ref_cost_center value)
        {

            var add = _Generic.CheckDuplicate(value.cost_center_code.ToString(), value.cost_center_description+","+value.cost_center_level,"", "costcenter", value.cost_center_id);
            if (add == 0)
            {
                if (value.cost_center_id == 0)
                {
                    //var data1 = _coscenterservice.Add(value);
                    ViewBag.parentlist = new SelectList(_coscenterservice.GetAll(), "cost_center_id", "cost_center_code");
                    return Json( JsonRequestBehavior.AllowGet);
                }
                else
                {
                   // var data1 = _coscenterservice.Update(value);
                    ViewBag.parentlist = new SelectList(_coscenterservice.GetAll(), "cost_center_id", "cost_center_code");
                    return Json( JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }

        [CustomAuthorizeAttribute("CSTCEN")]
        public ActionResult Create()
        {
            ViewBag.parentlist = new SelectList(_coscenterservice.GetDropdownParent(), "cost_center_code", "cost_center_description");
           
            return PartialView("Create_PV", ViewBag);
        }

        [HttpPost]
        public ActionResult Create(ref_cost_center_vm value)
        {
            var cost_center_level = _coscenterservice.GetLevel(value.parent_code);
            value.cost_center_level = int.Parse(cost_center_level.ToString())+1;
            var isValid = _coscenterservice.Add(value);
            if (isValid == true)
            {
                return RedirectToAction("Index");
            }
             
            ViewBag.parentlist = new SelectList(_coscenterservice.GetDropdownParent(), "cost_center_code", "cost_center_description");
            return View();
        }

        [CustomAuthorizeAttribute("CSTCEN")]
        public ActionResult Edit(int id)
        {
            ref_cost_center costCenter = _coscenterservice.Get(id);

            ViewBag.costCenter = costCenter;

            if (costCenter == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentlist = new SelectList(_coscenterservice.GetDropdownParent(), "cost_center_code", "cost_center_description");
            return PartialView("Edit_PV", ViewBag);
        }
        [HttpPost]
        public ActionResult Edit(ref_cost_center_vm value)
        {
            if (ModelState.IsValid)
            {
                var cost_center_level = _coscenterservice.GetLevel(value.parent_code);
                value.cost_center_level = int.Parse(cost_center_level.ToString()) + 1;
                var isValid = _coscenterservice.Update(value);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.parentlist = new SelectList(_coscenterservice.GetDropdownParent(), "cost_center_code", "cost_center_description");
            return View();
        }
        public ActionResult Child(int id)
        {
            ViewBag.parentlist = new SelectList(_coscenterservice.GetDropdownParent(), "cost_center_code", "cost_center_description");
            ref_cost_center_vm costCenter = _coscenterservice.GetChild(id);
            ViewBag.costCenter = costCenter;
            if (costCenter == null)
            {
                return HttpNotFound();
            }
            return PartialView("Child_PV", ViewBag);
        }
        public ActionResult ExportData()
        {

            var query = _coscenterservice.GetExport();
            var grid = new GridView();
            grid.DataSource = query;
            grid.DataBind();
            if (grid.Rows.Count > 1)
            {
                for (int i = 0; i <= grid.Rows.Count - 1; i++)
                {
                    grid.Rows[i].Cells[0].Text = (i + 1).ToString();
                }
            }
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=CostCenter.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index", "ExportData");
        }
    }
}