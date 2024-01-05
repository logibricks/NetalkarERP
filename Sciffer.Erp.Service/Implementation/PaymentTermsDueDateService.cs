using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class PaymentTermsDueDateService : IPaymentTermsDueDateService
    {
        private readonly ScifferContext _scifferContext;

        public PaymentTermsDueDateService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_PAYMENT_TERMS_DUE_DATE payment)
        {
            try
            {
                _scifferContext.REF_PAYMENT_TERMS_DUE_DATE.Add(payment);
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
                var payment = _scifferContext.REF_PAYMENT_TERMS_DUE_DATE.FirstOrDefault(c => c.PAYMENT_TERMS_DUE_DATE_ID == id);
                _scifferContext.Entry(payment).State = EntityState.Deleted;
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

        public REF_PAYMENT_TERMS_DUE_DATE Get(int id)
        {
            var payment = _scifferContext.REF_PAYMENT_TERMS_DUE_DATE.FirstOrDefault(c => c.PAYMENT_TERMS_DUE_DATE_ID == id);
            return payment;
        }

        public List<REF_PAYMENT_TERMS_DUE_DATE> GetAll()
        {
            return _scifferContext.REF_PAYMENT_TERMS_DUE_DATE.ToList();
        }

        public bool Update(REF_PAYMENT_TERMS_DUE_DATE payment)
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
