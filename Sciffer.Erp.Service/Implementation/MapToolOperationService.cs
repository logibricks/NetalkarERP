using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Service.Implementation
{
    public class MapToolOperationService : IMapToolOperationService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericservice;
        public MapToolOperationService(ScifferContext scifferContext, IGenericService genericservice)
        {
            _scifferContext = scifferContext;
            _genericservice = genericservice;
        }

        public string Add(ref_tool_operation_map_vm vm)
        {
            try
            {

                if(vm.tool_operation_map_id == 0|| vm.tool_operation_map_id == null)
                {
                    ref_tool_operation_map rtom = new ref_tool_operation_map();
                    rtom.process_id = vm.process_id;
                    rtom.item_id = vm.crankshaft_id;
                    rtom.tool_id = vm.tool_id;
                    rtom.tool_usage_type_id = vm.tool_usage_type_id;
                    rtom.tool_category_id = vm.tool_category_id;
                    rtom.is_active = true;
                    rtom.created_by = 1;
                    rtom.modify_ts = DateTime.Now;
                    rtom.modified_by = 1;
                    rtom.create_ts = DateTime.Now;
                    rtom.is_blocked = vm.is_blocked;
                    _scifferContext.ref_tool_operation_map.Add(rtom);
                    _scifferContext.SaveChanges();
                    return "Saved";

                }
                else
                {
                    var rtom = _scifferContext.ref_tool_operation_map.Where(x => x.tool_operation_map_id == vm.tool_operation_map_id && x.is_active == true).FirstOrDefault();

                    //ref_tool_operation_map rtom = new ref_tool_operation_map();
                    rtom.process_id = vm.process_id;
                    rtom.item_id = vm.crankshaft_id;
                    rtom.tool_id = vm.tool_id;
                    rtom.tool_usage_type_id = vm.tool_usage_type_id;
                    rtom.tool_category_id = vm.tool_category_id;
                    rtom.is_active = true;
                    rtom.created_by = 1;
                    rtom.modify_ts = DateTime.Now;
                    rtom.modified_by = 1;
                    rtom.create_ts = DateTime.Now;
                    rtom.is_blocked = vm.is_blocked;
                    

                    //_scifferContext.ref_tool_operation_map.Add(rtom);
                    _scifferContext.Entry(rtom).State = System.Data.Entity.EntityState.Modified;
                    _scifferContext.SaveChanges();
                    return "Update";


                    //ref_tool_operation_map rtom = new ref_tool_operation_map();
                    //rtom.process_id = vm.process_id;
                    //rtom.item_id = vm.crankshaft_id;
                    //rtom.tool_id = vm.tool_id;
                    //rtom.tool_usage_type_id = vm.tool_usage_type_id;
                    //rtom.tool_category_id = vm.tool_category_id;
                    //rtom.is_active = true;
                    //rtom.created_by = 1;
                    //rtom.modify_ts = DateTime.Now;
                    //rtom.modified_by = 1;
                    //rtom.create_ts = DateTime.Now;

                    //_scifferContext.Entry(rtom).State = EntityState.Modified;
                    //_scifferContext.SaveChanges();
                    //vm.tool_operation_map_id = _scifferContext.ref_tool_operation_map.Where(x => x.tool_operation_map_id == vm.tool_operation_map_id).FirstOrDefault().tool_operation_map_id;
                    //return "Update";
                }

            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ref_tool_operation_map_vm Get(int? id)
        {
            ref_tool_operation_map rm = _scifferContext.ref_tool_operation_map.FirstOrDefault(c => c.tool_operation_map_id == id);
            Mapper.CreateMap<ref_tool_operation_map, ref_tool_operation_map_vm>();
            ref_tool_operation_map_vm mmv = Mapper.Map<ref_tool_operation_map, ref_tool_operation_map_vm>(rm);
            return mmv;
            //throw new NotImplementedException();
        }

        public List<ref_tool_operation_map_vm> GetAll()
        {
            var result = (from rtom in _scifferContext.ref_tool_operation_map.Where(x => x.is_active == true)
                          join rmp in _scifferContext.ref_mfg_process on rtom.process_id equals rmp.process_id
                          join ri in _scifferContext.REF_ITEM on rtom.item_id equals ri.ITEM_ID
                          join rtut in _scifferContext.ref_tool_usage_type on rtom.tool_usage_type_id equals rtut.tool_usage_type_id
                          join rtc in _scifferContext.ref_tool_category on rtom.tool_category_id equals rtc.tool_category_id
                          select new
                          {
                              tool_opeation_map_id=rtom.tool_operation_map_id,
                              process_id = rmp.process_id,
                              process_code = rmp.process_code,
                              process_description = rmp.process_description,
                              ITEM_CATEGORY_ID = ri.ITEM_ID,
                              ITEM_CODE_CRANKSHAFT = ri.ITEM_CODE,
                              ITEM_NAME_CRANKSHAFT = ri.ITEM_NAME,
                              ITEM_ID = ri.ITEM_ID,
                              ITEM_CODE = ri.ITEM_CODE,
                              ITEM_NAME = ri.ITEM_NAME,
                              tool_usage_type_id = rtut.tool_usage_type_id,
                              tool_usage_type_name = rtut.tool_usage_type_name,
                              tool_category_id = rtc.tool_category_id,
                              tool_category_name = rtc.tool_category_name,
                              is_blocked=rtom.is_blocked,

                          }).Select(x => new ref_tool_operation_map_vm
                          {
                              tool_operation_map_id=x.tool_opeation_map_id,
                              process_id = x.process_id,
                              process_code = x.process_code,
                              process_description = x.process_description,
                              crankshaft_id = x.ITEM_CATEGORY_ID,
                              ITEM_CODE_CRANKSHAFT = x.ITEM_CODE_CRANKSHAFT,
                              ITEM_NAME_CRANKSHAFT = x.ITEM_NAME_CRANKSHAFT,
                              tool_id = x.ITEM_ID,
                              ITEM_CODE = x.ITEM_CODE,
                              item_name = x.ITEM_NAME,
                              tool_usage_type_id = x.tool_usage_type_id,
                              tool_usage_type_name = x.tool_usage_type_name,
                              tool_category_id = x.tool_category_id,
                              tool_category_name = x.tool_category_name,
                              is_blocked=x.is_blocked
                          }).OrderByDescending(x => x.tool_operation_map_id).ToList();
                return result;
        }

        public string Update(ref_tool_operation_map_vm vm)
        {
            throw new NotImplementedException();
        }

        public List<ref_tool_operation_map_vm> GetItemCrankshaftList()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.ITEM_CATEGORY_ID==3)
                         select new ref_tool_operation_map_vm
                         {
                             crankshaft_id = i.ITEM_ID,
                             item_name = i.ITEM_CODE + "-" + i.ITEM_NAME,
                         }).ToList();
            return query;
        }

        public List<ref_tool_operation_map_vm> GetItemToolList()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true)
                         select new ref_tool_operation_map_vm
                         {
                             tool_id = i.ITEM_ID,
                             item_name = i.ITEM_CODE + "-" + i.ITEM_NAME,
                         }).ToList();
            return query;
        }

        public List<ref_tool_operation_map_vm> GetToolUsagetypeList()
        {
            var query = (from i in _scifferContext.ref_tool_usage_type
                         select new ref_tool_operation_map_vm
                         {
                             tool_usage_type_id = i.tool_usage_type_id,
                             tool_usage_type_name = i.tool_usage_type_name,
                         }).ToList();
            return query;
        }

        public List<ref_tool_operation_map_vm> GetToolCatagoryList()
        {
            var query = (from i in _scifferContext.ref_tool_category
                         select new ref_tool_operation_map_vm
                         {
                             tool_category_id = i.tool_category_id,
                             tool_category_name = i.tool_category_name,
                         }).ToList();
            return query;
        }
    }
}
