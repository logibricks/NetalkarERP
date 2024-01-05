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
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly ScifferContext _scifferContext;

        public PaymentTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_PAYMENT_TYPE Add(REF_PAYMENT_TYPE payment)
        {
            try
            {
                payment.is_active = true;
                _scifferContext.REF_PAYMENT_TYPE.Add(payment);
                _scifferContext.SaveChanges();
                payment.PAYMENT_TYPE_ID = _scifferContext.REF_PAYMENT_TYPE.Max(x => x.PAYMENT_TYPE_ID);
            }
            catch (Exception e)
            {
                return payment;
            }
            return payment;
        }

        public bool Delete(int id)
        {
            try
            {
                var payment = _scifferContext.REF_PAYMENT_TYPE.FirstOrDefault(c => c.PAYMENT_TYPE_ID == id);
                payment.is_active = false;
                _scifferContext.Entry(payment).State = EntityState.Modified;
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

        public REF_PAYMENT_TYPE Get(int id)
        {
            var payment = _scifferContext.REF_PAYMENT_TYPE.FirstOrDefault(c => c.PAYMENT_TYPE_ID == id);
            return payment;
        }

        public List<REF_PAYMENT_TYPE> GetAll()
        {
            return _scifferContext.REF_PAYMENT_TYPE.Where(x=>x.is_active==true).OrderByDescending(a => a.PAYMENT_TYPE_ID).ToList();
        }

        public REF_PAYMENT_TYPE Update(REF_PAYMENT_TYPE payment)
        {
            try
            {
                payment.is_active = true;
                _scifferContext.Entry(payment).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return payment;
            }
            return payment;
        }
    }
}
