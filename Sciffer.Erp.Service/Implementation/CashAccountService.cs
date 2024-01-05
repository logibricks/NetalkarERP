using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.SqlClient;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Implementation
{
    public class CashAccountService : ICashAccountService
    {
        private readonly ScifferContext _scifferContext;

        public CashAccountService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
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

        public string Add(ref_cash_account cashaccount)
        {
            try
            {
                int user = 0;
                var entity = new SqlParameter("@entity", "Save");
                var cash_account_id = new SqlParameter("@cash_account_id", cashaccount.cash_account_id == 0 ? -1 : cashaccount.cash_account_id);
                var cash_account_code = new SqlParameter("@cash_account_code", cashaccount.cash_account_code);
                var cash_account_desc = new SqlParameter("@cash_account_desc", cashaccount.cash_account_desc);
                var currency_id = new SqlParameter("@currency_id", cashaccount.currency_id);
                var is_blocked = new SqlParameter("@is_blocked", cashaccount.is_blocked);
                var gl_ledger_id = new SqlParameter("@gl_ledger_id", cashaccount.gl_ledger_id);
                var create_user = new SqlParameter("@create_user", user);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec Save_CashAccountDetails @entity,@cash_account_id,@cash_account_code,@cash_account_desc,@currency_id, @is_blocked,@gl_ledger_id,@create_user",
                    entity, cash_account_id, cash_account_code, cash_account_desc, currency_id, is_blocked, gl_ledger_id, create_user).FirstOrDefault();
                
                return val;                
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ref_cash_account Get(int id)
        {
            return _scifferContext.ref_cash_account.Where(x => x.cash_account_id == id).FirstOrDefault();
        }

        public List<ref_cash_account_VM> getall()
        {
            var getDetails = (from cash in _scifferContext.ref_cash_account
                             join gl in _scifferContext.ref_general_ledger on cash.gl_ledger_id equals gl.gl_ledger_id
                             join curr in _scifferContext.REF_CURRENCY on cash.currency_id equals curr.CURRENCY_ID
                             select new ref_cash_account_VM
                             {
                                 cash_account_code = cash.cash_account_code,
                                 cash_account_desc = cash.cash_account_desc,
                                 cash_account_id = cash.cash_account_id,
                                 currency_id = cash.currency_id,
                                 currency_Name=curr.CURRENCY_NAME,
                                 gl_ledger_id=gl.gl_ledger_id,
                                 gl_name=gl.gl_ledger_name,                                 
                                 is_blocked=cash.is_blocked,                                 
                             }).OrderByDescending(a => a.cash_account_id).ToList();
            return getDetails;
        }
    }
}
