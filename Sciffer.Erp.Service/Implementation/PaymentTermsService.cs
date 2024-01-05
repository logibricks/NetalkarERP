using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class PaymentTermsService : IPaymentTermsService
    {

        private readonly ScifferContext _scifferContext;

        public PaymentTermsService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public payment_terms_vm Add(payment_terms_vm payment)
        {
            try
            {
                REF_PAYMENT_TERMS pt = new REF_PAYMENT_TERMS();
                pt.is_blocked = payment.is_blocked;
                pt.payment_terms_code = payment.payment_terms_code;
                pt.payment_terms_days = payment.payment_terms_days;
                pt.payment_terms_description = payment.payment_terms_description;
                pt.payment_terms_due_date_id = payment.payment_terms_due_date_id;
                pt.payment_terms_id = payment.payment_terms_id;
                pt.is_active = true;
                _scifferContext.REF_PAYMENT_TERMS.Add(pt);
                _scifferContext.SaveChanges();
                payment.payment_terms_id = _scifferContext.REF_PAYMENT_TERMS.Max(x => x.payment_terms_id);
                payment.payment_terms_due_date = _scifferContext.REF_PAYMENT_TERMS_DUE_DATE.Where(x => x.PAYMENT_TERMS_DUE_DATE_ID == payment.payment_terms_due_date_id).FirstOrDefault().PAYMENT_TERMS_DUE_DATE_NAME;
            }
            catch (Exception)
            {
                return payment;
            }
            return payment;
        }

        public bool Delete(int id)
        {
            try
            {
                var payment = _scifferContext.REF_PAYMENT_TERMS.FirstOrDefault(c => c.payment_terms_id == id);
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

        public REF_PAYMENT_TERMS Get(int id)
        {
            var payment = _scifferContext.REF_PAYMENT_TERMS.FirstOrDefault(c => c.payment_terms_id == id);
            return payment;
        }

        public List<REF_PAYMENT_TERMS> GetAll()
        {
            return _scifferContext.REF_PAYMENT_TERMS.ToList();
        }

        public payment_terms_vm Update(payment_terms_vm payment)
        {
            try
            {
                REF_PAYMENT_TERMS pt = new REF_PAYMENT_TERMS();
                pt.is_blocked = payment.is_blocked;
                pt.payment_terms_code = payment.payment_terms_code;
                pt.payment_terms_days = payment.payment_terms_days;
                pt.payment_terms_description = payment.payment_terms_description;
                pt.payment_terms_due_date_id = payment.payment_terms_due_date_id;
                pt.payment_terms_id = payment.payment_terms_id;
                pt.is_active = true;
                _scifferContext.Entry(pt).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                payment.payment_terms_due_date = _scifferContext.REF_PAYMENT_TERMS_DUE_DATE.Where(x => x.PAYMENT_TERMS_DUE_DATE_ID == payment.payment_terms_due_date_id).FirstOrDefault().PAYMENT_TERMS_DUE_DATE_NAME;

            }
            catch (Exception)
            {
                return payment;
            }
            return payment;
        }

        public List<payment_terms_vm> GetPaymentTerms()
        {
            var query = (from p in _scifferContext.REF_PAYMENT_TERMS.Where(x => x.is_active == true)
                         join d in _scifferContext.REF_PAYMENT_TERMS_DUE_DATE on p.payment_terms_due_date_id equals d.PAYMENT_TERMS_DUE_DATE_ID
                         select new payment_terms_vm
                         {
                             payment_terms_days = p.payment_terms_days,
                             payment_terms_code = p.payment_terms_code,
                             payment_terms_description = p.payment_terms_description,
                             payment_terms_due_date = d.PAYMENT_TERMS_DUE_DATE_NAME,
                             payment_terms_id = p.payment_terms_id,
                             is_blocked = p.is_blocked,
                             payment_terms_due_date_id = p.payment_terms_due_date_id,
                         }).OrderByDescending(a => a.payment_terms_id).ToList();
            return query;
        }
    }
}
