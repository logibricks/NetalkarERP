using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class PaymentCycleTypeService : IPaymentCycleTypeService
    {
        private readonly ScifferContext _scifferContext;

        public PaymentCycleTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_PAYMENT_CYCLE_TYPE payment)
        {
            try
            {
                _scifferContext.REF_PAYMENT_CYCLE_TYPE.Add(payment);
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
                var country = _scifferContext.REF_PAYMENT_CYCLE_TYPE.FirstOrDefault(c => c.PAYMENT_CYCLE_TYPE_ID == id);
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

        public REF_PAYMENT_CYCLE_TYPE Get(int id)
        {
            var payment = _scifferContext.REF_PAYMENT_CYCLE_TYPE.FirstOrDefault(c => c.PAYMENT_CYCLE_TYPE_ID == id);
            return payment;
        }

        public List<REF_PAYMENT_CYCLE_TYPE> GetAll()
        {
            return _scifferContext.REF_PAYMENT_CYCLE_TYPE.OrderByDescending(a => a.PAYMENT_CYCLE_TYPE_ID).ToList();
        }

        public bool Update(REF_PAYMENT_CYCLE_TYPE payment)
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
