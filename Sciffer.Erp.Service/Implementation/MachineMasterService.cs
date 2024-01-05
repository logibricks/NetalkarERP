using AutoMapper;
using AutoMapper.QueryableExtensions;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class MachineMasterService : IMachineMasterService
    {
        private readonly ScifferContext _scifferContext;

        public MachineMasterService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(ref_machine_master_VM mach)
        {
            try
            {
                ref_machine rm = new ref_machine();
                
               // rm.machine_id = mach.machine_id;
                 rm.machine_category_id = mach.machine_category_id;

                rm.machine_code = mach.machine_code;
                rm.machine_name = mach.machine_name;
                rm.machine_parent_code = mach.machine_parent_code;
                rm.status_id = mach.status_id;
                rm.plant_id = mach.plant_id;
                rm.length_uom_id = mach.length_uom_id;
                rm.breadth_uom_id = mach.breadth_uom_id;
                rm.weight_uom_id = mach.weight_uom_id;
                rm.height_uom_id = mach.height_uom_id;
                rm.location = mach.location;
                rm.acquisition_value = mach.acquisition_value;
                rm.currency_id = mach.currency_id;
                rm.purchase_order_id = mach.purchase_order_id;
                rm.manufacturer = mach.manufacturer;
                rm.manufacturer_part_no = mach.manufacturer_part_no;
                rm.manufacturing_country_id = mach.manufacturing_country_id;
                rm.priority_id = mach.priority_id;
                rm.acquisition_date = mach.acquisition_date;
                rm.purchasing_vendor = mach.purchasing_vendor;
                rm.model_no = mach.model_no;
                rm.manufacturing_serial_number = mach.manufacturing_serial_number;
                rm.manufacturing_date = mach.manufacturing_date;
                rm.is_blocked = mach.is_blocked;
                rm.business_area = mach.business_area;
                rm.cost_center = mach.cost_center;
                rm.asset_code_id = mach.asset_code_id;
                rm.asset_tag_no = mach.asset_tag_no;
                rm.warranty_applicable = mach.warranty_applicable;
                rm.warranty_start_date = mach.warranty_start_date;
                rm.warranty_end_date = mach.warranty_end_date;
                rm.guarantee_applicable = mach.guarantee_applicable;
                rm.guarantee_start_date = mach.guarantee_start_date;
                rm.guarantee_end_date = mach.guarantee_end_date;
                rm.remarks = mach.remarks;
                
                rm.attachement = mach.attachement;
                rm.amc_vendor = mach.amc_vendor;
                rm.repairs_vendor = mach.repairs_vendor;
                rm.is_active = true;

                rm.length = mach.length;
                rm.breadth = mach.breadth;
                rm.height = mach.height;
                rm.weight = mach.weight;



                List<ref_machine_details> rmd = new List<ref_machine_details>();

                foreach (var I in mach.ref_machine_details)
                {
                    ref_machine_details rd = new ref_machine_details();

                    rd.machine_detail_id = I.machine_detail_id;
                    rd.item_id = I.item_id;
                    rd.initial_received_qty = I.initial_received_qty;
                    rd.suggested_qty = I.suggested_qty;
                    rd.current_qty = I.current_qty;
                    rd.sr_no = I.sr_no;
                    rd.is_active = true;
                    rmd.Add(rd);


    }
                rm.ref_machine_details = rmd;
                _scifferContext.ref_machine.Add(rm);

                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[ref_machine_master] set [IS_ACTIVE] = 0 where machine_id = " + id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #region dispoable methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion

        public ref_machine_master_VM Get(int id)
        {
            ref_machine rm = _scifferContext.ref_machine.FirstOrDefault(c => c.machine_id == id);
            Mapper.CreateMap<ref_machine, ref_machine_master_VM>();
            ref_machine_master_VM mmv = Mapper.Map<ref_machine, ref_machine_master_VM>(rm);
            mmv.ref_machine_details = mmv.ref_machine_details.Where(c => c.is_active == true).ToList();
            return mmv;
        }
        public List<ref_machine_master_VM> GetAll()
        {
            Mapper.CreateMap<ref_machine, ref_machine_master_VM>().ForMember(dest => dest.attachement, opt => opt.Ignore());
            return _scifferContext.ref_machine.Project().To<ref_machine_master_VM>().Where(c => c.is_active == true).ToList();

        }

        public List<ref_machine_master_VM> getall()
        {
            var query = (from mach in _scifferContext.ref_machine
                         join cat in _scifferContext.ref_machine_category on mach.machine_category_id equals cat.machine_category_id
                         join status in _scifferContext.ref_status on mach.status_id equals status.status_id into status1
                         from status2 in status1.DefaultIfEmpty()
                         join plantname in _scifferContext.REF_PLANT on mach.plant_id equals plantname.PLANT_ID into plantname1
                         from plantname2 in plantname1.DefaultIfEmpty()
                         join lenghtuom in _scifferContext.REF_UOM on mach.length_uom_id equals lenghtuom.UOM_ID into lenghtuom1
                         from lenghtuom2 in lenghtuom1.DefaultIfEmpty()
                         join breadthuom in _scifferContext.REF_UOM on mach.breadth_uom_id equals breadthuom.UOM_ID into breadthuom1
                         from breadthuom2 in breadthuom1.DefaultIfEmpty()
                         join weightuom in _scifferContext.REF_UOM on mach.weight_uom_id equals weightuom.UOM_ID into weightuom1
                         from weightuom2 in weightuom1.DefaultIfEmpty()
                         join heightuom in _scifferContext.REF_UOM on mach.height_uom_id equals heightuom.UOM_ID into heightuom1
                         from heightuom2 in heightuom1.DefaultIfEmpty()
                         join mci in _scifferContext.REF_COUNTRY on mach.manufacturing_country_id equals mci.COUNTRY_ID into mci1
                         from mci2 in mci1.DefaultIfEmpty()
                         join prior in _scifferContext.REF_PRIORITY on mach.priority_id equals prior.PRIORITY_ID into prior1
                         from prior2 in prior1.DefaultIfEmpty()
                         join pvendor in _scifferContext.REF_VENDOR on mach.purchasing_vendor equals pvendor.VENDOR_ID into pvendor1
                         from pvendor2 in pvendor1.DefaultIfEmpty()
                         join barea in _scifferContext.REF_BUSINESS_UNIT on mach.business_area equals barea.BUSINESS_UNIT_ID into barea1
                         from barea2 in barea1.DefaultIfEmpty()
                         join center in _scifferContext.ref_cost_center on mach.cost_center equals center.cost_center_id into center1
                         from center2 in center1.DefaultIfEmpty()
                         join amc in _scifferContext.REF_VENDOR on mach.amc_vendor equals amc.VENDOR_ID into amc1
                         from amc2 in amc1.DefaultIfEmpty()
                         join repair in _scifferContext.REF_VENDOR on mach.repairs_vendor equals repair.VENDOR_ID into repair1
                         from repair2 in repair1.DefaultIfEmpty()
                         join curr in _scifferContext.REF_CURRENCY on mach.currency_id equals curr.CURRENCY_ID into curr1
                         from curr2 in curr1.DefaultIfEmpty()
                         select new ref_machine_master_VM()
                         {
                             machine_id = mach.machine_id,
                             machine_code = mach.machine_code,
                             machine_name = mach.machine_name,
                             machine_category_code = cat.machine_category_code,
                             machine_category_name = cat.machine_category_description,
                             machine_parent_code1 = mach.machine_code,
                             machine_parent_name = mach.machine_name,
                             status_name = status2 == null?"":status2.status_name,
                             plant_name = plantname2 == null?"":plantname2.PLANT_NAME,
                             location = mach.location,
                             length = mach.length,
                             length_uom_name = lenghtuom2 == null?"":lenghtuom2.UOM_NAME,
                             breadth = mach.breadth,
                             breadth_uom_name = breadthuom2 == null?"":breadthuom2.UOM_NAME,
                             height = mach.height,
                             height_uom_name = heightuom2 == null ? "" : heightuom2.UOM_NAME,
                             weight = mach.weight,
                             weight_uom_name = weightuom2 == null ?"":weightuom2.UOM_NAME,
                             acquisition_value = mach.acquisition_value,
                             currency_name = curr2 == null ?"":curr2.CURRENCY_NAME,
                             acquisition_date = mach.acquisition_date,
                             purchase_order_id = mach.purchase_order_id,
                             purchasing_vendor_name = pvendor2 == null ? "" : pvendor2.VENDOR_NAME,
                             manufacturer = mach.manufacturer,
                             model_no = mach.model_no,
                             manufacturer_part_no = mach.manufacturer_part_no,
                             manufacturing_serial_number = mach.manufacturing_serial_number,
                             manufacturing_country_name = mci2 == null ?"": mci2.COUNTRY_NAME,
                             manufacturing_date = mach.manufacturing_date,
                             priority_name = prior2 == null ? "" : prior2.PRIORITY_NAME,
                             is_blocked = mach.is_blocked,
                             business_area_code =barea2.BUSINESS_UNIT_NAME,
                             business_area_name = barea2.BUSINESS_UNIT_DESCRIPTION,
                             cost_center_code = center2.cost_center_code,
                             cost_center_name = center2.cost_center_description,
                             asset_code_id = mach.asset_code_id,
                             asset_tag_no = mach.asset_tag_no,
                             warranty_applicable = mach.warranty_applicable,
                             warranty_start_date = mach.warranty_start_date,
                             warranty_end_date = mach.warranty_end_date,
                             guarantee_applicable = mach.guarantee_applicable,
                             guarantee_start_date = mach.guarantee_start_date,
                             guarantee_end_date = mach.guarantee_end_date,
                             amc_vendor_name =  amc2.VENDOR_NAME,
                             amc_vendor_code = amc2.VENDOR_CODE,
                             repairs_vendor_name = repair2.VENDOR_NAME,
                             repairs_vendor_code = repair2.VENDOR_CODE,
                             remarks = mach.remarks,
                             attachement = mach.attachement,
                         }).OrderByDescending(a => a.machine_id).ToList();
            return query;
        }



        public bool Update(ref_machine_master_VM mach)
        {
            try
            {
                ref_machine rm = new ref_machine();
               

                rm.machine_id = mach.machine_id;
                rm.machine_category_id = mach.machine_category_id;

                rm.machine_code = mach.machine_code;
                rm.machine_name = mach.machine_name;
                rm.machine_parent_code = mach.machine_parent_code;
                rm.status_id = mach.status_id;
                rm.plant_id = mach.plant_id;
                rm.length_uom_id = mach.length_uom_id;
                rm.breadth_uom_id = mach.breadth_uom_id;
                rm.weight_uom_id = mach.weight_uom_id;
                rm.height_uom_id = mach.height_uom_id;
                rm.location = mach.location;
                rm.acquisition_value = mach.acquisition_value;
                rm.currency_id = mach.currency_id;
                rm.purchase_order_id = mach.purchase_order_id;
                rm.manufacturer = mach.manufacturer;
                rm.manufacturer_part_no = mach.manufacturer_part_no;
                rm.manufacturing_country_id = mach.manufacturing_country_id;
                rm.priority_id = mach.priority_id;
                rm.acquisition_date = mach.acquisition_date;
                rm.purchasing_vendor = mach.purchasing_vendor;
                rm.model_no = mach.model_no;
                rm.manufacturing_serial_number = mach.manufacturing_serial_number;
                rm.manufacturing_date = mach.manufacturing_date;
                rm.is_blocked = mach.is_blocked;
                rm.business_area = mach.business_area;
                rm.cost_center = mach.cost_center;
                rm.asset_code_id = mach.asset_code_id;
                rm.asset_tag_no = mach.asset_tag_no;
                rm.warranty_applicable = mach.warranty_applicable;
                rm.warranty_start_date = mach.warranty_start_date;
                rm.warranty_end_date = mach.warranty_end_date;
                rm.guarantee_applicable = mach.guarantee_applicable;
                rm.guarantee_start_date = mach.guarantee_start_date;
                rm.guarantee_end_date = mach.guarantee_end_date;
                rm.remarks = mach.remarks;
                if (mach.attachement != null)
                    rm.attachement = mach.attachement;
                rm.amc_vendor = mach.amc_vendor;
                rm.repairs_vendor = mach.repairs_vendor;
                rm.is_active = true;

                rm.length = mach.length;
                rm.breadth = mach.breadth;
                rm.height = mach.height;
                rm.weight = mach.weight;

                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = mach.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int machine_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        machine_detail_id = int.Parse(deleteStringArray[i]);
                        var machine_detail = _scifferContext.ref_machine_details.Find(machine_detail_id);
                        // int a = 0;
                        _scifferContext.Entry(machine_detail).State = EntityState.Modified;
                        machine_detail.is_active = false;
                    }
                }
                List<ref_machine_details> rmd = new List<ref_machine_details>();

                foreach (var I in mach.ref_machine_details)
                {
                    ref_machine_details rd = new ref_machine_details();

                    
                    rd.machine_detail_id = I.machine_detail_id;
                    rd.item_id = I.item_id;
                    rd.initial_received_qty = I.initial_received_qty;
                    rd.suggested_qty = I.suggested_qty;
                    rd.current_qty = I.current_qty;
                    rd.machine_id = mach.machine_id;
                    rd.sr_no = I.sr_no;
                    rd.is_active = true;
                    rmd.Add(rd);

    }
                rm.ref_machine_details = rmd;
                foreach (var i in rm.ref_machine_details)
                {
                    if (i.machine_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }
                _scifferContext.Entry(rm).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
