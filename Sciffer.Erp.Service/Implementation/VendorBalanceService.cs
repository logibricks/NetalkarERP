using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using System.Linq;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class VendorBalanceService : IVendorBalanceService
    {
        private readonly ScifferContext _scifferContext;
        public VendorBalanceService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool AddExcel(List<vendor_balance_VM> item, List<vendor_balance_detail_VM> bldetails)
        {
            try
            {
                foreach (var data in item)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("vendor_balance_detail_id", typeof(int));
                    dt.Columns.Add("vendor_id", typeof(int));
                    dt.Columns.Add("ref1", typeof(string));
                    dt.Columns.Add("ref2", typeof(string));
                    dt.Columns.Add("ref3", typeof(string));
                    dt.Columns.Add("doc_date", typeof(DateTime));
                    dt.Columns.Add("amount", typeof(double));
                    dt.Columns.Add("line_remarks", typeof(string));
                    dt.Columns.Add("is_active", typeof(bool));
                    dt.Columns.Add("due_date", typeof(DateTime));
                    dt.Columns.Add("amount_type_id", typeof(int));
                    dt.Columns.Add("control_dawn_payment_account", typeof(int));
                    foreach (var items in bldetails)
                    {
                        if (data.offset_account == items.offset_account)
                        {
                            dt.Rows.Add(items.vendor_balance_detail_id == null ? -1 : items.vendor_balance_detail_id, items.vendor_id, items.ref1,
                                items.ref2, items.ref3, items.doc_date, items.amount, items.line_remarks, 1, items.due_date, items.amount_type_id, items.control_dawn_payment_account);
                        }
                    }
                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_vendor_balance_detail";
                    t1.Value = dt;
                    int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    var vendor_balance_id = new SqlParameter("@vendor_balance_id", data.vendor_balance_id == null ? -1 : data.vendor_balance_id);
                    var offset_account_id = new SqlParameter("@offset_account_id", data.offset_account_id);
                    var posting_date = new SqlParameter("@posting_date", data.posting_date);
                    var header_remark = new SqlParameter("@header_remark", data.header_remark==null?"":data.header_remark);
                    var is_active = new SqlParameter("@is_active", 1);
                    var doc_number = new SqlParameter("@doc_number", data.doc_number);
                    var category_id = new SqlParameter("@category_id", data.category_id);
                    var user = new SqlParameter("@create_user", create_user);

                    var val = _scifferContext.Database.SqlQuery<string>("exec Save_VendorBalance @vendor_balance_id,@offset_account_id,@posting_date,@header_remark,@is_active,@doc_number,@category_id,@create_user,@t1",
                        vendor_balance_id, offset_account_id, posting_date, header_remark, is_active, doc_number, category_id, user, t1).FirstOrDefault();

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
        public bool Add(ref_vendor_balance_VM item)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("vendor_balance_detail_id", typeof(int));
                dt.Columns.Add("vendor_id", typeof(int));
                dt.Columns.Add("ref1", typeof(string));
                dt.Columns.Add("ref2", typeof(string));
                dt.Columns.Add("ref3", typeof(string));
                dt.Columns.Add("doc_date", typeof(DateTime));
                dt.Columns.Add("amount", typeof(double));
                dt.Columns.Add("line_remark", typeof(string));
                dt.Columns.Add("is_active", typeof(bool));
                dt.Columns.Add("due_date", typeof(DateTime));
                dt.Columns.Add("amount_type_id", typeof(int));
                for (var i = 0; i < item.vendor_id.Count; i++)
                {
                    dt.Rows.Add(int.Parse(item.vendor_balance_detail_id[i]) == 0 ? -1 : int.Parse(item.vendor_balance_detail_id[i]),
                        int.Parse(item.vendor_id[i]), item.ref1[i], item.ref2[i], item.ref3[i], item.doc_date[i],
                        Double.Parse(item.amount[i]), item.line_remarks[i], 1, item.due_date[i], int.Parse(item.amount_type_id[i]));
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_vendor_balance_detail";
                t1.Value = dt;
                int create_user = 0;
                var vendor_balance_id = new SqlParameter("@vendor_balance_id", item.vendor_balance_id == null ? -1 : item.vendor_balance_id);
                var offset_account_id = new SqlParameter("@offset_account_id", item.offset_account_id);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var header_remark = new SqlParameter("@header_remarks", item.header_remark == null ? "" : item.header_remark);
                var is_active = new SqlParameter("@is_active", 1);
                var doc_number = new SqlParameter("@doc_number", item.doc_number);
                var category_id = new SqlParameter("@category_id", item.category_id);
                var user = new SqlParameter("@create_user", create_user);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_VendorBalance @vendor_balance_id,@offset_account_id,@posting_date,@header_remarks,@is_active,@doc_number,@category_id,@create_user,@t1",
                    vendor_balance_id, offset_account_id, posting_date, header_remark, is_active, doc_number, category_id, user, t1).FirstOrDefault();
                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public bool Add(ref_vendor_balance_VM item)
        //{
        //    try
        //    {
        //        ref_vendor_balance re = new ref_vendor_balance();
        //        re.offset_account_id = item.offset_account_id;
        //        re.header_remark = item.header_remark;
        //        re.posting_date = item.posting_date;
        //        re.is_active = true;
        //        List<ref_vendor_balance_details> allalternate = new List<ref_vendor_balance_details>();
        //        if (item.ref_vendor_balance_details != null)
        //        {
        //            foreach (var alternates in item.ref_vendor_balance_details)
        //            {
        //                ref_vendor_balance_details alternate = new ref_vendor_balance_details();
        //                alternate.vendor_balance_id = item.vendor_balance_id;
        //                if (alternates.vendor_balance_detail_id == null)
        //                {
        //                    alternates.vendor_balance_detail_id = 0;
        //                }

        //                alternate.vendor_balance_id = alternates.vendor_balance_id;
        //                alternate.amount = alternates.amount;
        //                alternate.amount_type_id = alternates.amount_type_id;
        //                alternate.doc_date = alternates.doc_date;
        //                alternate.due_date = alternates.due_date;
        //                alternate.line_remarks = alternates.line_remarks;
        //                alternate.ref1 = alternates.ref1;
        //                alternate.ref2 = alternates.ref2;
        //                alternate.ref3 = alternates.ref3;
        //                alternate.vendor_id = alternates.vendor_id;
        //                alternate.is_active = true;
        //                allalternate.Add(alternate);
        //            }

        //        }
        //        re.ref_vendor_balance_details = allalternate;
        //        _scifferContext.ref_vendor_balance.Add(re);
        //        _scifferContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        public bool Delete(int id)
        {
            try
            {
                var re = _scifferContext.ref_vendor_balance.Where(x => x.vendor_balance_id == id).FirstOrDefault();
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
        public ref_vendor_balance_VM GetDetails(int id)
        {
            try
            {
                ref_vendor_balance JR = _scifferContext.ref_vendor_balance.FirstOrDefault(c => c.vendor_balance_id == id);
                Mapper.CreateMap<ref_vendor_balance, ref_vendor_balance_VM>();
                ref_vendor_balance_VM JRVM = Mapper.Map<ref_vendor_balance, ref_vendor_balance_VM>(JR);
                JRVM.vendor_balance_detail = JRVM.ref_vendor_balance_details.Where(a => a.is_active == true).Select(a => new {
                    vendor = a.REF_VENDOR.VENDOR_CODE,
                    ref1 = a.ref1,
                    ref2 = a.ref2,
                    ref3 = a.ref3,
                    doc_date = a.doc_date,
                    due_date = a.due_date,
                    amount = a.amount,
                    amount_type = a.ref_amount_type.amount_type,
                    line_remark = a.line_remarks
                }).ToList().Select(a => new
                vendor_balance_detail
                {
                    vendor = a.vendor,
                    ref1 = a.ref1,
                    ref2 = a.ref2,
                    ref3 = a.ref3,
                    doc_date = a.doc_date == null ? "" : a.doc_date.ToString("dd-MMM-yyyy"),
                    due_date = a.due_date == null ? "" : a.due_date.ToString("dd-MMM-yyyy"),
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
        public ref_vendor_balance_VM Get(int id)
        {
            try
            {
                ref_vendor_balance JR = _scifferContext.ref_vendor_balance.FirstOrDefault(c => c.vendor_balance_id == id);
                Mapper.CreateMap<ref_vendor_balance, ref_vendor_balance_VM>();
                ref_vendor_balance_VM JRVM = Mapper.Map<ref_vendor_balance, ref_vendor_balance_VM>(JR);
                JRVM.ref_vendor_balance_details = JRVM.ref_vendor_balance_details.ToList();
                JRVM.gl_ledger_code = JR.ref_general_ledger.gl_ledger_code;
                JRVM.gl_ledger_name = JR.ref_general_ledger.gl_ledger_name;
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_vendor_balanceVM> GetAll()
        {
            var list = (from ed in _scifferContext.ref_vendor_balance.Where(x => x.is_active == true)
                        join r in _scifferContext.ref_general_ledger on ed.offset_account_id equals r.gl_ledger_id
                        join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
                        select new ref_vendor_balanceVM
                        {
                            header_remark = ed.header_remark,
                            offset_account_id = ed.offset_account_id,
                            posting_date = ed.posting_date,
                            vendor_balance_id = ed.vendor_balance_id,
                            gl_ledger_code = r.gl_ledger_code,
                            gl_ledger_name = r.gl_ledger_name,
                            doc_number = ed.doc_number,
                            category_id = ed.category_id,
                            category_name = cat.category,

                        }).OrderByDescending(a => a.vendor_balance_id).ToList();
            return list;
        }

        public bool Update(ref_vendor_balance_VM item)
        {
            try
            {
                ref_vendor_balance re = new ref_vendor_balance();
                re.offset_account_id = item.offset_account_id;
                re.header_remark = item.header_remark;
                re.posting_date = item.posting_date;
                re.vendor_balance_id = item.vendor_balance_id;
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
                        var pt_detail = _scifferContext.ref_vendor_balance_details.Find(pt_detail_id);
                        pt_detail.is_active = false;
                        _scifferContext.Entry(pt_detail).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                List<ref_vendor_balance_details> allalternate = new List<ref_vendor_balance_details>();
                if (item.ref_vendor_balance_details != null)
                {
                    foreach (var alternates in item.ref_vendor_balance_details)
                    {
                        ref_vendor_balance_details alternate = new ref_vendor_balance_details();
                        alternate.vendor_balance_id = item.vendor_balance_id;
                        if (alternates.vendor_balance_detail_id == null)
                        {
                            alternates.vendor_balance_detail_id = 0;
                        }
                        alternate.vendor_balance_id = alternates.vendor_balance_id;
                        alternate.amount = alternates.amount;
                        alternate.amount_type_id = alternates.amount_type_id;
                        alternate.doc_date = alternates.doc_date;
                        alternate.due_date = alternates.due_date;
                        alternate.line_remarks = alternates.line_remarks;
                        alternate.ref1 = alternates.ref1;
                        alternate.ref2 = alternates.ref2;
                        alternate.ref3 = alternates.ref3;
                        alternate.vendor_id = alternates.vendor_id;
                        alternate.is_active = true;
                        allalternate.Add(alternate);
                    }

                }

                foreach (var i in allalternate)
                {
                    if (i.vendor_balance_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                re.ref_vendor_balance_details = allalternate;
                _scifferContext.Entry(re).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
