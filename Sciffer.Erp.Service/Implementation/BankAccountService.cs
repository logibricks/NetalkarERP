using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class BankAccountService : IBankAccountService
    {
        private readonly ScifferContext _scifferContext;

        public BankAccountService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        
        public bool Add(ref_bank_account_vm BANK)
        {
            try
            {
                int user = 0;
                var bank_account_id = new SqlParameter("@bank_account_id", BANK.bank_account_id==0?-1:BANK.bank_account_id);
                var bank_account_code = new SqlParameter("@bank_account_code", BANK.bank_account_code);
                var bank_id = new SqlParameter("@bank_id", BANK.bank_id);
                var bank_branch = new SqlParameter("@bank_branch", BANK.bank_branch);
                var bank_city = new SqlParameter("@bank_city", BANK.bank_city);
                var bank_state_id = new SqlParameter("@bank_state_id", BANK.bank_state_id);
                var bank_account_type_id = new SqlParameter("@bank_account_type_id", BANK.bank_account_type_id);
                var bank_account_number = new SqlParameter("@bank_account_number", BANK.bank_account_number);
                var bank_ifsc_code = new SqlParameter("@bank_ifsc_code", BANK.bank_ifsc_code==null?string.Empty:BANK.bank_ifsc_code);
                var bank_swift_code = new SqlParameter("@bank_swift_code", BANK.bank_swift_code==null?string.Empty:BANK.bank_swift_code);
                var bank_currency_id = new SqlParameter("@bank_currency_id", BANK.bank_currency_id);
                var gl_account_id = new SqlParameter("@gl_account_id", BANK.gl_account_id);
                var is_blocked = new SqlParameter("@is_blocked", BANK.is_blocked);
                var create_user = new SqlParameter("@create_user", user);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_bank_account @bank_account_id ,@bank_account_code ,@bank_id ,@bank_branch ,@bank_city ,@bank_state_id ,@bank_account_type_id ,@bank_account_number ,@bank_ifsc_code ,@bank_swift_code ,@bank_currency_id ,@gl_account_id ,@is_blocked ,@create_user ",
                    bank_account_id, bank_account_code, bank_id, bank_branch, bank_city, bank_state_id, bank_account_type_id, 
                    bank_account_number, bank_ifsc_code, bank_swift_code, bank_currency_id, gl_account_id, is_blocked, create_user).FirstOrDefault();
                if(val=="Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //ref_bank_account RB = new ref_bank_account();
                //RB.is_blocked = BANK.is_blocked;
                //RB.is_active = true;             
                //RB.bank_account_code = BANK.bank_account_code;
                //RB.bank_id = BANK.bank_id;
                //RB.bank_branch = BANK.bank_branch;
                //RB.bank_city = BANK.bank_city;
                //RB.bank_state_id = BANK.bank_state_id;             
                //RB.bank_account_type_id = BANK.bank_account_type_id;               
                //RB.bank_account_number = BANK.bank_account_number;
                //RB.bank_ifsc_code = BANK.bank_ifsc_code;
                //RB.bank_swift_code = BANK.bank_swift_code;
                //RB.bank_currency_id = BANK.bank_currency_id;              
                //RB.gl_account_id = BANK.gl_account_id;               
                //_scifferContext.ref_bank_account.Add(RB);              
                //_scifferContext.SaveChanges();
            }

            catch (Exception ex)
            {

                return false;
            }

            return true;

        }

        public bool Delete(int id)
        {
            try
            {
                var bankaccount = _scifferContext.ref_bank_account.FirstOrDefault(c => c.bank_account_id == id);
                bankaccount.is_active = false;
                _scifferContext.Entry(bankaccount).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
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

        public ref_bank_account_vm Get(int id)
        {
            ref_bank_account RB = _scifferContext.ref_bank_account.FirstOrDefault(c => c.bank_account_id == id);
            Mapper.CreateMap<ref_bank_account, ref_bank_account_vm>();
            ref_bank_account_vm RBV = Mapper.Map<ref_bank_account, ref_bank_account_vm>(RB);
            RBV.BANK_COUNTRY_ID = RB.REF_STATE.COUNTRY_ID;
            RBV.bank_code = RB.ref_bank.bank_code;
            RBV.bank_name = RB.ref_bank.bank_name;
            return RBV;
        }

        public List<ref_bank_account_vm> GetAll()
        {
            Mapper.CreateMap<ref_bank_account, ref_bank_account_vm>();

            return _scifferContext.ref_bank_account.Project().To<ref_bank_account_vm>().ToList();
        }

        public bool Update(ref_bank_account_vm BANK)
        {
            try
            {

                ref_bank_account RB = new ref_bank_account();
                RB.is_active = true;
                RB.is_blocked = BANK.is_blocked;
                RB.bank_account_id = BANK.bank_account_id;
                RB.bank_account_code = BANK.bank_account_code;
                RB.bank_id = BANK.bank_id;
                RB.is_blocked = BANK.is_blocked;
                RB.bank_branch = BANK.bank_branch;
                RB.bank_city = BANK.bank_city;
                RB.bank_state_id = BANK.bank_state_id;
                RB.bank_account_type_id = BANK.bank_account_type_id;
                RB.bank_account_number = BANK.bank_account_number;
                RB.bank_ifsc_code = BANK.bank_ifsc_code;
                RB.bank_swift_code = BANK.bank_swift_code;
                RB.bank_currency_id = BANK.bank_currency_id;
                RB.gl_account_id = BANK.gl_account_id;
                RB.is_active = true;    
                _scifferContext.Entry(RB).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }

            catch (Exception ex)
            {

                return false;
            }

            return true;

        }

        public List<ref_bank_accountvm> GetBankAccount()
        {
            var query = (from b in _scifferContext.ref_bank_account
                         join b1 in _scifferContext.ref_bank on b.bank_id equals b1.bank_id
                         join a in _scifferContext.REF_ACCOUNT_TYPE on b.bank_account_type_id equals a.ACCOUNT_TYPE_ID
                         join c in _scifferContext.REF_CURRENCY on b.bank_currency_id equals c.CURRENCY_ID
                         join s in _scifferContext.REF_STATE on b.bank_state_id equals s.STATE_ID
                         join c1 in _scifferContext.REF_COUNTRY on s.COUNTRY_ID equals c1.COUNTRY_ID
                         join j in _scifferContext.ref_general_ledger on b.gl_account_id equals j.gl_ledger_id
                         select new ref_bank_accountvm {
                            bank_account_id=b.bank_account_id,
                             bank_id = b.bank_id,
                            bank_currency_id = b.bank_currency_id,
                             bank_account_type_id = b.bank_account_type_id,
                             is_blocked = b.is_blocked,
                             is_active = b.is_active,
                             bank_account_number =b.bank_account_number,
                             bank_state_id = b.bank_state_id,
                             gl_account_id = b.gl_account_id,
                            bank_account_type =a.ACCOUNT_TYPE_NAME,
                                bank_branch=b.bank_branch,
                                bank_city=b.bank_city,
                                bank_code=b1.bank_code,
                                bank_currency=c.CURRENCY_NAME,
                                    bank_ifsc_code=b.bank_ifsc_code,
                                    bank_name=b1.bank_name,
                                    bank_state=s.STATE_NAME,
                                    bank_swift_code=b.bank_swift_code,
                                        gl_account=j.gl_ledger_name,
                                        country_name=c1.COUNTRY_NAME,
                                        BANK_COUNTRY_ID = c1.COUNTRY_ID,
                                        bank_account_code=b.bank_account_code,
                         }).ToList().Where(x=>x.is_active==true).OrderByDescending(a => a.bank_account_id).ToList();
            return query;
        }
    }
}
