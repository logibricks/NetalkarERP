using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Sciffer.Erp.Service.Implementation
{
    public class PlantService : IPlantService
    {
        private readonly ScifferContext _scifferContext;

        public PlantService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(REF_PLANT_VM plant)
        {
            try
            {

                REF_PLANT RP = new REF_PLANT();
                RP.is_active = true;
                RP.is_blocked = plant.is_blocked;
                RP.PLANT_ID = plant.PLANT_ID;
                RP.PLANT_NAME = plant.PLANT_NAME;
                RP.PLANT_CODE = plant.PLANT_CODE;
                RP.PLANT_ADDRESS = plant.PLANT_ADDRESS;
                RP.PLANT_CITY = plant.PLANT_CITY;
                RP.PLANT_STATE = plant.PLANT_STATE;
                RP.PLANT_TELEPHONE = plant.PLANT_TELEPHONE;
                RP.PLANT_MOBILE = plant.PLANT_MOBILE;
                RP.PLANT_EMAIL = plant.PLANT_EMAIL;
                RP.excise_number = plant.excise_number;
                RP.excise_range = plant.excise_range;
                RP.excise_division = plant.excise_division;
                RP.service_tax_number = plant.excise_commisionerate;
                RP.cst_number = plant.cst_number;
                RP.gst_number = plant.gst_number;
                RP.vat_number = plant.vat_number;
                RP.pincode = plant.pincode;
                RP.telephone_cdoe = plant.telephone_cdoe;
                RP.service_tax_number = plant.service_tax_number;
                _scifferContext.REF_PLANT.Add(RP);
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
                var plant = _scifferContext.REF_PLANT.FirstOrDefault(c => c.PLANT_ID == id);
                plant.is_active = false;
                _scifferContext.Entry(plant).State = EntityState.Modified;
                _scifferContext.SaveChanges();
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

        public REF_PLANT_VM Get(int id)
        {
            REF_PLANT plant = _scifferContext.REF_PLANT.FirstOrDefault(c => c.PLANT_ID == id);
            Mapper.CreateMap<REF_PLANT, REF_PLANT_VM>();
            REF_PLANT_VM plantvm = Mapper.Map<REF_PLANT, REF_PLANT_VM>(plant);

            var REGISTER = _scifferContext.REF_STATE.Where(s => s.STATE_ID == plant.PLANT_STATE)
             .Select(s => new
             {
                 COUNTRY_ID = s.COUNTRY_ID
             }).Single();

            plantvm.PLANT_COUNTRY = REGISTER.COUNTRY_ID;
            return plantvm;
        }

        public List<REF_PLANT_VM> GetAll()
        {
            Mapper.CreateMap<REF_PLANT, REF_PLANT_VM>();

            return _scifferContext.REF_PLANT.Where(x => x.is_active == true).Project().To<REF_PLANT_VM>().ToList();
        }

        public bool Update(REF_PLANT_VM plant)
        {
            try
            {

                REF_PLANT RP = new REF_PLANT();
                RP.PLANT_ID = plant.PLANT_ID;
                RP.PLANT_NAME = plant.PLANT_NAME;
                RP.PLANT_CODE = plant.PLANT_CODE;
                RP.PLANT_ADDRESS = plant.PLANT_ADDRESS;
                RP.PLANT_CITY = plant.PLANT_CITY;
                RP.PLANT_STATE = plant.PLANT_STATE;
                RP.PLANT_TELEPHONE = plant.PLANT_TELEPHONE;
                RP.PLANT_MOBILE = plant.PLANT_MOBILE;
                RP.PLANT_EMAIL = plant.PLANT_EMAIL;
                RP.excise_number = plant.excise_number;
                RP.excise_range = plant.excise_range;
                RP.excise_division = plant.excise_division;
                RP.service_tax_number = plant.excise_commisionerate;
                RP.cst_number = plant.cst_number;
                RP.gst_number = plant.gst_number;
                RP.vat_number = plant.vat_number;
                RP.service_tax_number = plant.service_tax_number;
                RP.is_blocked = plant.is_blocked;
                RP.pincode = plant.pincode;
                RP.telephone_cdoe = plant.telephone_cdoe;
                RP.is_active = true;       
                _scifferContext.Entry(RP).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public List<plant_vm> GetPlantList()
        {
            var query = (from p in _scifferContext.REF_PLANT
                         join s in _scifferContext.REF_STATE on p.PLANT_STATE equals s.STATE_ID
                         join c in _scifferContext.REF_COUNTRY on s.COUNTRY_ID equals c.COUNTRY_ID
                         select new plant_vm
                         {
                             PLANT_ADDRESS = p.PLANT_ADDRESS,
                             PLANT_CITY = p.PLANT_CITY,
                             PLANT_CODE = p.PLANT_CODE +" - " + p.PLANT_NAME,
                             PLANT_COUNTRY = c.COUNTRY_NAME,
                             PLANT_EMAIL = p.PLANT_EMAIL,
                             PLANT_ID = p.PLANT_ID,
                             PLANT_MOBILE = p.PLANT_MOBILE,
                             PLANT_NAME = p.PLANT_NAME,
                             PLANT_STATE = s.STATE_NAME,
                             PLANT_TELEPHONE = p.PLANT_TELEPHONE,
                             excise_number = p.excise_number,
                             excise_range = p.excise_range,
                             excise_division = p.excise_division,
                             service_tax_number = p.service_tax_number,
                             excise_commisionerate = p.excise_commisionerate,
                             cst_number = p.cst_number,
                             gst_number = p.gst_number,
                             vat_number = p.vat_number,
                             telephone_cdoe = p.telephone_cdoe,
                             pincode = p.pincode
                         }).OrderByDescending(a => a.PLANT_ID).ToList();
            return query;
        }
        public List<REF_PLANT_VM> GetPlant()
        {
            var query = (from ed in _scifferContext.REF_PLANT.Where(x => x.is_active == true)
                         select new REF_PLANT_VM
                         {
                             PLANT_ID = ed.PLANT_ID,
                             PLANT_NAME = ed.PLANT_CODE + "-" + ed.PLANT_NAME,
                         }).ToList();
            return query;
        }

    }
}