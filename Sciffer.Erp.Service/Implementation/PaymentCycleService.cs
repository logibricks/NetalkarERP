using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class PaymentCycleService : IPaymentCycleService
    {

        private readonly ScifferContext _scifferContext;

        public PaymentCycleService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_PAYMENT_CYCLE payment)
        {
            try
            {
                _scifferContext.REF_PAYMENT_CYCLE.Add(payment);
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var country = _scifferContext.REF_PAYMENT_CYCLE.FirstOrDefault(c => c.PAYMENT_CYCLE_ID == id);
                _scifferContext.Entry(country).State = EntityState.Deleted;
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

        public REF_PAYMENT_CYCLE Get(int id)
        {
            var payment = _scifferContext.REF_PAYMENT_CYCLE.FirstOrDefault(c => c.PAYMENT_CYCLE_ID == id);
            return payment;
        }

        public List<REF_PAYMENT_CYCLE> GetAll()
        {
            return _scifferContext.REF_PAYMENT_CYCLE.OrderByDescending(a => a.PAYMENT_CYCLE_ID).ToList();
        }

        public bool Update(REF_PAYMENT_CYCLE payment)
        {
            try
            {
                _scifferContext.Entry(payment).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
