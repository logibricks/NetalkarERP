using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class BankService : IBankService
    {
        private readonly ScifferContext _scifferContext;

        public BankService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public ref_bank Add(ref_bank bank)
        {
            try
            {
                bank.is_active = true;
                _scifferContext.ref_bank.Add(bank);
                _scifferContext.SaveChanges();
                bank.bank_id = _scifferContext.ref_bank.Max(x => x.bank_id);
            }
            catch (Exception ex)
            {
                return bank;
            }
            return bank;
        }

        public bool Delete(int id)
        {
            try
            {
                var batch = _scifferContext.ref_bank.FirstOrDefault(c => c.bank_id == id);
                _scifferContext.Entry(batch).State = EntityState.Modified;
                batch.is_active = false;
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

        public ref_bank Get(int id)
        {
            var bank = _scifferContext.ref_bank.FirstOrDefault(c => c.bank_id == id);
            return bank;
        }

        public List<ref_bank> GetAll()
        {
            return _scifferContext.ref_bank.Where(x => x.is_active == true).OrderByDescending(a=>a.bank_id).ToList();
        }

        public ref_bank Update(ref_bank bank)
        {
            try
            {
                bank.is_active = true;
                _scifferContext.Entry(bank).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return bank;
            }
            return bank;
        }
    }
}
