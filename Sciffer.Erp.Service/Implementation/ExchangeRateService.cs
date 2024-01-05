using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly ScifferContext _scifferContext;
        public ExchangeRateService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public ref_exchangerate_vm Add(ref_exchangerate_vm exchange)
        {
            try
            {
                ref_exchange_rate er = new ref_exchange_rate();
                er.currency_id1 = exchange.currency1;
                er.currency_id2 = exchange.currency2;
                er.exchange_rate_id = exchange.exchange_rate_id;
                er.from_date = exchange.from_date;               
                er.unit1 = exchange.unit1;
                er.unit2 = exchange.unit2;
                er.is_blocked = exchange.is_blocked;
                _scifferContext.ref_exchange_rate.Add(er);
                _scifferContext.SaveChanges();
                exchange.exchange_rate_id = _scifferContext.ref_exchange_rate.Max(x => x.exchange_rate_id);
                exchange.currency1_name = _scifferContext.REF_CURRENCY.Where(x => x.CURRENCY_ID == exchange.currency1).FirstOrDefault().CURRENCY_NAME;
                exchange.currency2_name = _scifferContext.REF_CURRENCY.Where(x => x.CURRENCY_ID == exchange.currency2).FirstOrDefault().CURRENCY_NAME;
            }
            catch (Exception ex)
            {
                return exchange;
            }
            return exchange;
        }

        public bool Delete(int? id)
        {
            try
            {
                _scifferContext.ref_exchange_rate.Remove(_scifferContext.ref_exchange_rate.Find(id));
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

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

        public ref_exchange_rate Get(int? id)
        {
            try
            {
                return _scifferContext.ref_exchange_rate.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_exchange_rate> GetAll()
        {
            try
            {
                return _scifferContext.ref_exchange_rate.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ref_exchangerate_vm Update(ref_exchangerate_vm exchange)
        {
            try
            {
                ref_exchange_rate er = new ref_exchange_rate();
                er.currency_id1 = exchange.currency1;
                er.currency_id2 = exchange.currency2;
                er.exchange_rate_id = exchange.exchange_rate_id;
                er.from_date = exchange.from_date;
                er.unit1 = exchange.unit1;
                er.unit2 = exchange.unit2;
                er.is_blocked = exchange.is_blocked;
                _scifferContext.Entry(er).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
                exchange.currency1_name = _scifferContext.REF_CURRENCY.Where(x => x.CURRENCY_ID == exchange.currency1).FirstOrDefault().CURRENCY_NAME;
                exchange.currency2_name = _scifferContext.REF_CURRENCY.Where(x => x.CURRENCY_ID == exchange.currency2).FirstOrDefault().CURRENCY_NAME;
            }
            catch (Exception ex)
            {
                return exchange;
            }
            return exchange;
        }

        public List<ref_exchangerate_vm> GetExchanagelist()
        {
            var query = (from exchange in _scifferContext.ref_exchange_rate
                         join u1 in _scifferContext.REF_CURRENCY on exchange.currency_id1 equals u1.CURRENCY_ID
                         join u2 in _scifferContext.REF_CURRENCY on exchange.currency_id2 equals u2.CURRENCY_ID
                         select new ref_exchangerate_vm {
                             exchange_rate_id = exchange.exchange_rate_id,
                             currency1 = u1.CURRENCY_ID,
                             currency2 = u2.CURRENCY_ID,
                             currency1_name = u1.CURRENCY_NAME,
                             currency2_name = u2.CURRENCY_NAME,
                             from_date = exchange.from_date,
                             unit1 = exchange.unit1,
                             unit2 = exchange.unit2,  
                             is_blocked=exchange.is_blocked,                                                         
                             }).OrderByDescending(a => a.exchange_rate_id).ToList();
            return query;
        }

       
    }
}
