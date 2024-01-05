using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class PriceListService : IPriceListService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public PriceListService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(price_list_vendor_vm item)
        {
            try
            {
                ref_price_list_vendor re = new ref_price_list_vendor();
                re.vendor_id = item.vendor_id;
                re.is_active = true;

                List<ref_price_list_vendor_details> allalternate = new List<ref_price_list_vendor_details>();
                if (item.item_id != null)
                {
                    for (var i = 0; i < item.item_id.Count; i++)
                    {
                        ref_price_list_vendor_details alternate = new ref_price_list_vendor_details();
                        alternate.price_list_id = item.price_list_id;
                        alternate.price_list_detail_id = 0;
                        alternate.price = double.Parse(item.price[i]);
                        alternate.discount = item.discount[i]==""?0:double.Parse(item.discount[i]);
                        alternate.discount_type_id = int.Parse(item.discount_type_id[i]);
                        alternate.price_after_discount = double.Parse(item.price_after_discount[i]);
                        alternate.item_id = int.Parse(item.item_id[i]);
                        alternate.uom_id = int.Parse(item.uom_id[i]);
                        alternate.effective_date = item.effective_date[i];
                        allalternate.Add(alternate);
                    }

                }
                re.ref_price_list_vendor_details = allalternate;
                _scifferContext.ref_price_list_vendor.Add(re);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public string AddExcel(List<ref_price_list_vendor_vm> vm, List<ref_price_list_vendor_detail_vm> vm1)
        {
            var errorMessage = "";
            try
            {
                foreach (var data in vm)
                {
                    ref_price_list_vendor re = new ref_price_list_vendor();
                    var vendor = _scifferContext.ref_price_list_vendor.Where(x => x.vendor_id == data.vendor_id).FirstOrDefault();
                    if (vendor == null)
                    {
                        re.vendor_id = data.vendor_id;
                        re.is_active = true;
                        List<ref_price_list_vendor_details> allalternate = new List<ref_price_list_vendor_details>();
                        foreach (var data1 in vm1)
                        {
                            if (data.vendor_code == data1.vendor_code)
                            {
                                ref_price_list_vendor_details alternate = new ref_price_list_vendor_details();

                                var item_duplicate = _scifferContext.ref_price_list_customer_details.Where(x => x.item_id == data1.item_id).FirstOrDefault();
                                if (item_duplicate == null)
                                {
                                    alternate.item_id = data1.item_id;
                                    alternate.price = data1.price;
                                    alternate.discount = data1.discount;
                                    alternate.discount_type_id = data1.discount_type_id;
                                    alternate.price_after_discount = data1.price_after_discount;
                                    alternate.uom_id = data1.uom_id;
                                    allalternate.Add(alternate);
                                }
                                else
                                {
                                    errorMessage = "Item Code is already exist!";
                                }
                            }

                        }
                        re.ref_price_list_vendor_details = allalternate;
                        _scifferContext.ref_price_list_vendor.Add(re);
                    }
                    else
                    {
                        errorMessage = "Vendor Code already Exist!";
                    }
                }
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return errorMessage = "Failed";
            }

            return errorMessage;
        }

        public bool Delete(int id)
        {
            try
            {
                var re = _scifferContext.ref_price_list_vendor.Where(x => x.price_list_id == id).FirstOrDefault();
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

        public price_list_vendor_vm Get(int id)
        {

            try
            {
                ref_price_list_vendor JR = _scifferContext.ref_price_list_vendor.FirstOrDefault(c => c.price_list_id == id);
                Mapper.CreateMap<ref_price_list_vendor, price_list_vendor_vm>();
                price_list_vendor_vm JRVM = Mapper.Map<ref_price_list_vendor, price_list_vendor_vm>(JR);
                JRVM.ref_price_list_vendor_details = JRVM.ref_price_list_vendor_details.Where(x => x.price_list_id == JR.price_list_id && x.is_active == true).OrderByDescending(x=> x.effective_date).ToList();
                JRVM.vendor_code = JR.REF_VENDOR.VENDOR_CODE;
                JRVM.vendor_name = JR.REF_VENDOR.VENDOR_NAME;

                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<price_list_vendor_vm> GetAll()
        {
            var list = (from ed in _scifferContext.ref_price_list_vendor.Where(x => x.is_active == true)
                        join w in _scifferContext.REF_VENDOR on ed.vendor_id equals w.VENDOR_ID

                        select new price_list_vendor_vm
                        {
                            price_list_id = ed.price_list_id,
                            vendor_id = ed.vendor_id,
                            vendor_name = w.VENDOR_NAME,
                            vendor_code = w.VENDOR_CODE,

                        }).OrderByDescending(a => a.price_list_id).ToList();
            return list;
        }


        public string Update(price_list_vendor_vm item)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("price_list_detail_id", typeof(int));
                dt1.Columns.Add("price", typeof(double));
                dt1.Columns.Add("discount", typeof(double));
                dt1.Columns.Add("discount_type_id", typeof(int));
                dt1.Columns.Add("price_after_discount", typeof(double));
                dt1.Columns.Add("item_id", typeof(int));
                dt1.Columns.Add("uom_id", typeof(int));
                dt1.Columns.Add("effective_date", typeof(DateTime));
                
                if (item.item_id != null)
                {
                    for (var i = 0; i < item.item_id.Count; i++)
                    {
                        dt1.Rows.Add(
                            item.price_list_detail_id[i] == "" ? "-1" : item.price_list_detail_id[i],
                            double.Parse(item.price[i]),
                            item.discount[i] == "" ? 0 : double.Parse(item.discount[i]),
                            int.Parse(item.discount_type_id[i]),
                            double.Parse(item.price_after_discount[i]),
                            int.Parse(item.item_id[i]),
                            int.Parse(item.uom_id[i]),
                            item.effective_date[i]
                           );
                    }
                }

            
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_price_list_detail";
                t1.Value = dt1;

                var price_list_id = new SqlParameter("@price_list_id", item.price_list_id == null ? 0 : item.price_list_id);
                var vendor_id = new SqlParameter("@vendor_id", item.vendor_id == null ? -1 : item.vendor_id);               
                var deleteids = new SqlParameter("@deleteids", item.deleteids == null ? "" : item.deleteids);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_PriceList @price_list_id,@vendor_id,@deleteids,@t1", price_list_id, vendor_id,deleteids, t1).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return ex.Message.ToString();
            }
        }


        //public bool Update(price_list_vendor_vm item)
        //{
        //    try
        //    {

        //        ref_price_list_vendor re = new ref_price_list_vendor();
        //        re.vendor_id = item.vendor_id;
        //        re.is_active = true;
        //        re.price_list_id = item.price_list_id;

        //        //string[] deleteStringArray = new string[0];
        //        //try
        //        //{
        //        //    deleteStringArray = item.deleteids.Split(new char[] { '~' });
        //        //}
        //        //catch
        //        //{

        //        //}
        //        //int pt_detail_id;
        //        //for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
        //        //{
        //        //    if (deleteStringArray[i] != "")
        //        //    {
        //        //        pt_detail_id = int.Parse(deleteStringArray[i]);
        //        //        var pt_detail = _scifferContext.ref_price_list_vendor_details.Find(pt_detail_id);
        //        //        pt_detail.is_active = false;
        //        //        _scifferContext.Entry(pt_detail).State = System.Data.Entity.EntityState.Modified;
        //        //    }
        //        //}


        //        List<ref_price_list_vendor_details> allalternate = new List<ref_price_list_vendor_details>();

        //        if (item.item_id != null)
        //        {
        //            for (var i = 0; i < item.item_id.Count; i++)
        //            {
        //                ref_price_list_vendor_details alternate = new ref_price_list_vendor_details();
        //                alternate.price_list_id = item.price_list_id;
        //                alternate.price_list_detail_id = item.price_list_detail_id[i] == "" ? 0: int.Parse(item.price_list_detail_id[i]);
        //                alternate.price = double.Parse(item.price[i]);
        //                alternate.discount = item.discount[i] == "" ? 0 : double.Parse(item.discount[i]);
        //                alternate.discount_type_id = int.Parse(item.discount_type_id[i]);
        //                alternate.price_after_discount = double.Parse(item.price_after_discount[i]);
        //                alternate.item_id = int.Parse(item.item_id[i]);
        //                alternate.uom_id = int.Parse(item.uom_id[i]);
        //                alternate.effective_date = item.effective_date[i];
        //                allalternate.Add(alternate);
        //            }

        //        }
        //        foreach (var i in allalternate)
        //        {
        //            if (i.price_list_detail_id == 0)
        //            {
        //                _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Added;
        //            }
        //            else
        //            {
        //                _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Modified;
        //            }
        //        }
        //        re.ref_price_list_vendor_details = allalternate;
        //        _scifferContext.Entry(old).State = EntityState.Detached; /
        //       _scifferContext.Entry(re).State = System.Data.Entity.EntityState.Modified;

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
