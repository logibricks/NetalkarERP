using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{
    public class CustomerBalanceService : ICustomerBalanceService
    {
        private readonly ScifferContext _scifferContext;
        public CustomerBalanceService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool AddExcel(List<customer_balance_VM> item, List<customer_balance_detail_VM> bldetails)
        {
            try
            {
                foreach (var data in item)
                {        
                    DataTable dt = new DataTable();
                    dt.Columns.Add("customer_balance_detail_id", typeof(int));
                    dt.Columns.Add("customer_id", typeof(int));
                    dt.Columns.Add("ref1", typeof(string));
                    dt.Columns.Add("ref2", typeof(string));
                    dt.Columns.Add("ref3", typeof(string));
                    dt.Columns.Add("doc_date", typeof(DateTime));
                    dt.Columns.Add("amount", typeof(double));
                    dt.Columns.Add("line_remark", typeof(string));
                    dt.Columns.Add("is_active", typeof(bool));
                    dt.Columns.Add("due_date", typeof(DateTime));
                    dt.Columns.Add("amount_type_id", typeof(int));
                    foreach (var items in bldetails)
                    {
                        if (data.offset_account == items.offset_account)
                        {                           
                            dt.Rows.Add(items.customer_balance_detail_id == null ? -1 : items.customer_balance_detail_id, items.customer_id, items.ref1,
                                items.ref2, items.ref3, items.doc_date, items.amount, items.line_remark, 1, items.due_date, items.amount_type_id);                            
                        }
                    }
                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_customer_balance_detail";
                    t1.Value = dt;
                    int create_user = 0;
                    var customer_balance_id = new SqlParameter("@customer_balance_id", data.customer_balance_id == null ? -1 : data.customer_balance_id);
                    var offset_account_id = new SqlParameter("@offset_account_id", data.offset_account_id);
                    var posting_date = new SqlParameter("@posting_date", data.posting_date);
                    var header_remarks = new SqlParameter("@header_remarks", data.header_remarks==null?"":data.header_remarks);
                    var is_active = new SqlParameter("@is_active", 1);
                    var doc_number = new SqlParameter("@doc_number", data.doc_number);
                    var category_id = new SqlParameter("@category_id", data.category_id);
                    var user = new SqlParameter("@create_user", create_user);

                    var val = _scifferContext.Database.SqlQuery<string>("exec Save_CustomerBalance @customer_balance_id,@offset_account_id,@posting_date,@header_remarks,@is_active,@doc_number,@category_id,@create_user,@t1",
                        customer_balance_id, offset_account_id, posting_date, header_remarks, is_active, doc_number, category_id, user, t1).FirstOrDefault();

                    if (val == "Saved")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public bool Add(ref_customer_balance_VM item)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("customer_balance_detail_id", typeof(int));
                dt.Columns.Add("customer_id", typeof(int));
                dt.Columns.Add("ref1", typeof(string));
                dt.Columns.Add("ref2", typeof(string));
                dt.Columns.Add("ref3", typeof(string));
                dt.Columns.Add("doc_date", typeof(DateTime));
                dt.Columns.Add("amount", typeof(double));
                dt.Columns.Add("line_remark", typeof(string));
                dt.Columns.Add("is_active", typeof(bool));
                dt.Columns.Add("due_date", typeof(DateTime));
                dt.Columns.Add("amount_type_id", typeof(int));
                for(var i=0; i<item.customer_id.Count; i++)
                {
                    dt.Rows.Add(int.Parse(item.customer_balance_detail_id[i]) == 0 ? -1 : int.Parse(item.customer_balance_detail_id[i]), 
                        int.Parse(item.customer_id[i]),item.ref1[i],item.ref2[i],item.ref3[i],item.doc_date[i],
                        Double.Parse(item.amount[i]),item.line_remarks[i],1,item.due_date[i],int.Parse(item.amount_type_id[i]));
                }
                
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_customer_balance_detail";
                t1.Value = dt;
                int create_user = 0;
                
                var customer_balance_id = new SqlParameter("@customer_balance_id", item.customer_balance_id==null?-1:item.customer_balance_id);
                var offset_account_id = new SqlParameter("@offset_account_id", item.offset_account_id);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var header_remarks = new SqlParameter("@header_remarks", item.header_remarks == null ? "" : item.header_remarks);
                var is_active = new SqlParameter("@is_active", 1);
                var doc_number = new SqlParameter("@doc_number", item.doc_number);                
                var category_id = new SqlParameter("@category_id", item.category_id);
                var user = new SqlParameter("@create_user", create_user);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_CustomerBalance @customer_balance_id,@offset_account_id,@posting_date,@header_remarks,@is_active,@doc_number,@category_id,@create_user,@t1", 
                    customer_balance_id, offset_account_id, posting_date, header_remarks, is_active, doc_number, category_id,user,t1).FirstOrDefault();
                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        
        public bool Delete(int id)
        {
            try
            {
                var re = _scifferContext.ref_customer_balance.Where(x => x.customer_balance_id == id).FirstOrDefault();
                re.is_active = false;
                _scifferContext.Entry(re).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public ref_customer_balance_VM GetDetails(int id)
        {
            try
            {
                ref_customer_balance JR = _scifferContext.ref_customer_balance.FirstOrDefault(c => c.customer_balance_id == id);
                Mapper.CreateMap<ref_customer_balance, ref_customer_balance_VM>();
                ref_customer_balance_VM JRVM = Mapper.Map<ref_customer_balance, ref_customer_balance_VM>(JR);
                JRVM.customer_balance_detail = JRVM.ref_customer_balance_details.Where(a => a.is_active == true).Select(a => new {
                    customer_name = a.REF_CUSTOMER.CUSTOMER_NAME,
                    customer_code = a.REF_CUSTOMER.CUSTOMER_CODE,
                    ref1 = a.ref1,
                    ref2 = a.ref2,
                    ref3 = a.ref3,
                    doc_date = a.doc_date,
                    due_date = a.due_date,
                    amount = a.amount,
                    amount_type = a.ref_amount_type.amount_type,
                    line_remark = a.line_remark
                }).ToList().Select(a => new
                customer_balance_detail
                {
                    customer_name = a.customer_name,
                    customer_code = a.customer_code,
                    ref1 = a.ref1,
                    ref2 = a.ref2,
                    ref3 = a.ref3,
                    doc_date = a.doc_date == null ? "" : a.doc_date.GetValueOrDefault().ToString("dd-MMM-yyyy"),
                    due_date = a.due_date == null ? "" : a.due_date.GetValueOrDefault().ToString("dd-MMM-yyyy"),
                    amount = string.Format("{0:0.00}", a.amount),
                    amount_type = a.amount_type,
                    line_remark = a.line_remark
                }).ToList();
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ref_customer_balance_VM Get(int id)
        {
            try
            {
                ref_customer_balance JR = _scifferContext.ref_customer_balance.FirstOrDefault(c => c.customer_balance_id == id);
                Mapper.CreateMap<ref_customer_balance, ref_customer_balance_VM>();
                ref_customer_balance_VM JRVM = Mapper.Map<ref_customer_balance, ref_customer_balance_VM>(JR);
                JRVM.ref_customer_balance_details = JRVM.ref_customer_balance_details.ToList();
                JRVM.gl_ledger_code = JR.ref_general_ledger.gl_ledger_code;
                JRVM.gl_ledger_name = JR.ref_general_ledger.gl_ledger_name;
                //JRVM.posting_date1 = JRVM.posting_date.Day+"-"+ JRVM.posting_date.Month+"-"+ JRVM.posting_date.Year;
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_customer_balanceVM> GetAll()
        {
            var list = (from ed in _scifferContext.ref_customer_balance.Where(x => x.is_active == true)
                        join r in _scifferContext.ref_general_ledger on ed.offset_account_id equals r.gl_ledger_id
                        join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id into cat1
                        from cat2 in cat1.DefaultIfEmpty()
                        select new ref_customer_balanceVM
                        {
                            header_remarks = ed.header_remarks,
                            offset_account_id = ed.offset_account_id,
                            posting_date = ed.posting_date,
                            customer_balance_id = ed.customer_balance_id,
                            gl_ledger_code = r.gl_ledger_code,
                            gl_ledger_name = r.gl_ledger_name,
                            doc_number = ed.doc_number,
                            category_id = ed.category_id,
                            category_name = cat2.category,
                            create_user = ed.create_user,
                        }).OrderByDescending(a => a.customer_balance_id).ToList();
            return list;
        }

        public bool Update(ref_customer_balance_VM item)
        {
            try
            {

                ref_customer_balance re = new ref_customer_balance();
                re.offset_account_id = item.offset_account_id;
                re.header_remarks = item.header_remarks;
                re.posting_date = item.posting_date;
                re.is_active = true;

                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = item.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int pt_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        pt_detail_id = int.Parse(deleteStringArray[i]);
                        var pt_detail = _scifferContext.ref_customer_balance_details.Find(pt_detail_id);
                        pt_detail.is_active = false;
                        _scifferContext.Entry(pt_detail).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                List<ref_customer_balance_details> allalternate = new List<ref_customer_balance_details>();
                if (item.ref_customer_balance_details != null)
                {
                    foreach (var alternates in item.ref_customer_balance_details)
                    {
                        ref_customer_balance_details alternate = new ref_customer_balance_details();
                        alternate.customer_balance_id = item.customer_balance_id;
                        if (alternates.customer_balance_detail_id == null)
                        {
                            alternates.customer_balance_detail_id = 0;
                        }
                        alternate.customer_balance_id = alternates.customer_balance_id;
                        alternate.customer_id = alternates.customer_id;
                        alternate.ref1 = alternates.ref1;
                        alternate.ref2 = alternates.ref2;
                        alternate.ref3 = alternates.ref3;
                        alternate.doc_date = alternates.doc_date;
                        alternate.due_date = alternates.due_date;
                        alternate.amount = alternates.amount;
                        alternate.amount_type_id = alternates.amount_type_id;
                        alternate.line_remark = alternates.line_remark;
                        alternate.is_active = true;
                        allalternate.Add(alternate);
                    }

                }

                foreach (var i in allalternate)
                {
                    if (i.customer_balance_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                re.ref_customer_balance_details = allalternate;
                _scifferContext.Entry(re).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public bool Add(ref_customer_balance_VM item)
        //{
        //    try
        //    {
        //        ref_customer_balance re = new ref_customer_balance();
        //        re.offset_account_id = item.offset_account_id;
        //        re.header_remarks = item.header_remarks;
        //        re.posting_date = item.posting_date;
        //        re.is_active = true;
        //        List<ref_customer_balance_details> allalternate = new List<ref_customer_balance_details>();
        //        if (item.ref_customer_balance_details != null)
        //        {
        //            foreach (var alternates in item.ref_customer_balance_details)
        //            {
        //                ref_customer_balance_details alternate = new ref_customer_balance_details();
        //                alternate.customer_balance_id = item.customer_balance_id;
        //                if (alternates.customer_balance_detail_id == null)
        //                {
        //                    alternates.customer_balance_detail_id = 0;
        //                }

        //                alternate.customer_balance_id = alternates.customer_balance_id;
        //                alternate.customer_id = alternates.customer_id;
        //                alternate.ref1 = alternates.ref1;
        //                alternate.ref2 = alternates.ref2;
        //                alternate.ref3 = alternates.ref3;
        //                alternate.doc_date = alternates.doc_date;
        //                alternate.due_date = alternates.due_date;
        //                alternate.amount = alternates.amount;
        //                alternate.amount_type_id = alternates.amount_type_id;
        //                alternate.line_remark = alternates.line_remark;
        //                alternate.is_active = true;
        //                allalternate.Add(alternate);
        //            }

        //        }
        //        re.ref_customer_balance_details = allalternate;
        //        _scifferContext.ref_customer_balance.Add(re);
        //        _scifferContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

    }
}
