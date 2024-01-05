using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ScifferContext _scifferContext;

        public CreditCardService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }


        public bool Add(REF_CREDIT_CARD_VM CreditCard)
        {
            try
            {

                ref_credit_card RCC = new ref_credit_card();
                RCC.country_id = CreditCard.country_id;
                RCC.is_active = true;
                RCC.credit_card_code = CreditCard.credit_card_code;
                RCC.bank_id = CreditCard.bank_id;                              
                RCC.credit_card_number = CreditCard.credit_card_number;
                RCC.currency_id = CreditCard.currency_id;               
                RCC.gl_ledger_id = CreditCard.gl_ledger_id;
                RCC.is_blocked = CreditCard.is_blocked;
                _scifferContext.ref_credit_card.Add(RCC);
                _scifferContext.SaveChanges();
            }

            catch (Exception e)
            {

                return false;
            }

            return true;

        }

        public bool Delete(int id)
        {
            try {
                var credit = _scifferContext.ref_credit_card.Where(x => x.credit_card_id == id).FirstOrDefault();
                credit.is_active = false;
                _scifferContext.Entry(credit).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                
            }
            catch (Exception e)
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

        public REF_CREDIT_CARD_VM Get(int id)
        {

            ref_credit_card RCC=_scifferContext.ref_credit_card.FirstOrDefault(c => c.credit_card_id == id);
            Mapper.CreateMap<ref_credit_card, REF_CREDIT_CARD_VM>();
            REF_CREDIT_CARD_VM RCCV = Mapper.Map<ref_credit_card, REF_CREDIT_CARD_VM>(RCC);
            RCCV.bank_name = RCC.ref_bank.bank_name;
            RCCV.bank_code = RCC.ref_bank.bank_code;
            RCCV.gl_ledger_code = RCC.REF_GENERAL_LEDGER.gl_ledger_code;
            RCCV.gl_ledger_name = RCC.REF_GENERAL_LEDGER.gl_ledger_name;         
            return RCCV;
        }

        public List<REF_CREDIT_CARD_VM> GetAll()
        {
            var query = (from cr in _scifferContext.ref_credit_card.Where(x=>x.is_active==true)
                         join b in _scifferContext.ref_bank on cr.bank_id equals b.bank_id
                         join c in _scifferContext.REF_CURRENCY on cr.currency_id equals c.CURRENCY_ID
                         join c1 in _scifferContext.REF_COUNTRY on cr.country_id equals c1.COUNTRY_ID
                         join j in _scifferContext.ref_general_ledger on cr.gl_ledger_id equals j.gl_ledger_id
                         select new REF_CREDIT_CARD_VM
                         {
                             bank_name =b.bank_name,                           
                             country_name = c1.COUNTRY_NAME,
                             credit_card_code = cr.credit_card_code,
                             credit_card_id = cr.credit_card_id,
                             credit_card_number = cr.credit_card_number,
                             Currency = c.CURRENCY_NAME,                           
                             gl_ledger_code = j.gl_ledger_name ,
                             is_blocked = cr.is_blocked,

                         }).OrderByDescending(a => a.credit_card_id).ToList();
            return query;
        }

        public bool Update(REF_CREDIT_CARD_VM CreditCard)
        {
            try
            {

                ref_credit_card RCC = new ref_credit_card();
                RCC.is_active = true;
                RCC.country_id = CreditCard.country_id;
                RCC.credit_card_id = CreditCard.credit_card_id;
                RCC.credit_card_code = CreditCard.credit_card_code;
                RCC.bank_id = CreditCard.bank_id;                
                RCC.credit_card_number = CreditCard.credit_card_number;
                RCC.currency_id = CreditCard.currency_id;
                RCC.gl_ledger_id = CreditCard.gl_ledger_id;
                RCC.is_blocked = CreditCard.is_blocked;
                _scifferContext.Entry(RCC).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }

            catch (Exception e)
            {

                return false;
            }

            return true;
        }
    }
}
