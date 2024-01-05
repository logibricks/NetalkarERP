using AutoMapper;
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

namespace Sciffer.Erp.Service.Implementation
{
   public  class ProcessMasterService: IProcessMasterService
    {
        private readonly ScifferContext _scifferContext;

        public ProcessMasterService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_mfg_process_vm Add(ref_mfg_process_vm process)
        {
            try
            {
                ref_mfg_process rc = new ref_mfg_process();
                rc.process_id = process.process_id;
                rc.process_code = process.process_code;
                rc.process_description = process.process_description;
                rc.is_blocked = process.is_blocked;
                rc.is_active = true;

                _scifferContext.ref_mfg_process.Add(rc);
                _scifferContext.SaveChanges();
                process.process_id = _scifferContext.ref_mfg_process.Max(x => x.process_id);
                //    machine.Machine_id = _scifferContext.REF_MACHINE.Max(x => x.Machine_id);
                //    machine.Machine_name = _scifferContext.REF_MACHINE.Where(x => x.COUNTRY_ID == currency.CURRENCY_COUNTRY_ID).FirstOrDefault().COUNTRY_NAME;
            }
            catch (Exception e)
            {
                return process;
            }
            return process;
        }

        public bool Delete(int id)
        {
            try
            {
                var process = _scifferContext.ref_mfg_process.FirstOrDefault(c => c.process_id == id);
                //var country = currency.CURRENCY_COUNTRY_ID;
                //if (country != 0)
                //{
                process.is_active = false;
                _scifferContext.Entry(process).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                //}
            }
            catch (Exception e)
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

        public ref_mfg_process_vm Get(int id)
        {
            var process = _scifferContext.ref_mfg_process.FirstOrDefault(c => c.process_id == id);
            Mapper.CreateMap<ref_mfg_process, ref_mfg_process_vm>();
            ref_mfg_process_vm VM = Mapper.Map<ref_mfg_process, ref_mfg_process_vm>(process);
            return VM;
        }

        public List<ref_mfg_process> GetAll()
        {
            var query = _scifferContext.ref_mfg_process.Where(x => x.is_active == true).OrderByDescending(x=>x.process_id).ToList();
            return query;
        }



        public ref_mfg_process_vm Update(ref_mfg_process_vm process)
        {
            try
            {
                ref_mfg_process rc = _scifferContext.ref_mfg_process.Where(x => x.process_id == process.process_id).FirstOrDefault();
                rc.process_code = process.process_code;
                rc.process_description = process.process_description;
                rc.is_blocked = process.is_blocked;
                rc.is_active = true;
                _scifferContext.Entry(rc).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                process.process_description = _scifferContext.ref_mfg_process.Where(x => x.process_id == process.process_id).FirstOrDefault().process_description;
                //currency.CountryName = _scifferContext.REF_COUNTRY.Where(x => x.COUNTRY_ID == currency.CURRENCY_COUNTRY_ID).FirstOrDefault().COUNTRY_NAME;
            }
            catch (Exception e)
            {
                return process;
            }
            return process;
        }

        public List<ref_mfg_process> GetMachine()
        {
            var query = _scifferContext.ref_mfg_process.ToList();
            return query;
        }

    }
}
