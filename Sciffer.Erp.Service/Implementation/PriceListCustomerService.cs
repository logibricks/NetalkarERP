using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class PriceListCustomerService : IPriceListCustomerService
    {
        private readonly ScifferContext _scifferContext;
        public PriceListCustomerService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(price_list_customer_vm item)
        {
            try
            {
                ref_price_list_customer re = new ref_price_list_customer();
                re.customer_id = item.customer_id;
                re.is_active = true;
                List<ref_price_list_customer_details> allalternate = new List<ref_price_list_customer_details>();
                if (item.item_id != null)
                {
                    for (var i = 0; i < item.item_id.Count; i++)
                    {
                        ref_price_list_customer_details alternate = new ref_price_list_customer_details();
                        alternate.price_list_id = item.price_list_id;
                        alternate.price_list_detail_id = 0;
                        alternate.price = double.Parse(item.price[i]);
                        alternate.discount = item.discount[i] == "" ? 0 : double.Parse(item.discount[i]);
                        alternate.discount_type_id = int.Parse(item.discount_type_id[i]);
                        alternate.price_after_discount = double.Parse(item.price_after_discount[i]);
                        alternate.item_id = int.Parse(item.item_id[i]);
                        alternate.uom_id = int.Parse(item.uom_id[i]);
                        allalternate.Add(alternate);
                    }

                }
                re.ref_price_list_customer_details = allalternate;
                _scifferContext.ref_price_list_customer.Add(re);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public string AddExcel(List<ref_price_list_customer_vm> vm, List<ref_price_list_customer_detail_vm> vm1)
        {
            var errorMessage = "";
            try
            {
                foreach (var data in vm)
                {
                    ref_price_list_customer re = new ref_price_list_customer();
                    var customer = _scifferContext.ref_price_list_customer.Where(x => x.customer_id == data.customer_id).FirstOrDefault();
                    if (customer == null)
                    {
                        re.customer_id = data.customer_id;
                        re.is_active = true;
                        List<ref_price_list_customer_details> allalternate = new List<ref_price_list_customer_details>();
                        foreach (var data1 in vm1)
                        {
                            if (data.customer_code == data1.customer_code)
                            {
                                ref_price_list_customer_details alternate = new ref_price_list_customer_details();
                                var item_duplicate = _scifferContext.ref_price_list_customer_details.Where(x => x.item_id == data1.item_id).FirstOrDefault();
                                if (item_duplicate == null)
                                {
                                    alternate.item_id = data1.item_id;
                                    alternate.price = data1.price;
                                    alternate.discount = data1.discount;
                                    alternate.discount_type_id = alternate.discount_type_id;
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
                        re.ref_price_list_customer_details = allalternate;
                        _scifferContext.ref_price_list_customer.Add(re);
                    }
                    else
                    {
                        errorMessage = "Customer Code is already exist!";
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
                var re = _scifferContext.ref_price_list_customer.Where(x => x.price_list_id == id).FirstOrDefault();
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

        public price_list_customer_vm Get(int id)
        {
            try
            {

                ref_price_list_customer JR = _scifferContext.ref_price_list_customer.FirstOrDefault(c => c.price_list_id == id);

                Mapper.CreateMap<ref_price_list_customer, price_list_customer_vm>();
                price_list_customer_vm JRVM = Mapper.Map<ref_price_list_customer, price_list_customer_vm>(JR);
                JRVM.ref_price_list_customer_details = JRVM.ref_price_list_customer_details.ToList();
                JRVM.customer_code = JR.REF_CUSTOMER.CUSTOMER_CODE;
                JRVM.customer_name = JR.REF_CUSTOMER.CUSTOMER_NAME;

                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<price_list_customer_vm> GetAll()
        {
            var list = (from ed in _scifferContext.ref_price_list_customer.Where(x => x.is_active == true)
                        join w in _scifferContext.REF_CUSTOMER on ed.customer_id equals w.CUSTOMER_ID

                        select new price_list_customer_vm
                        {
                            price_list_id = ed.price_list_id,
                            customer_id = ed.customer_id,
                            customer_name = w.CUSTOMER_NAME,
                            customer_code = w.CUSTOMER_CODE,

                        }).ToList();
            return list;
        }

        public bool Update(price_list_customer_vm item)
        {
            try
            {

                ref_price_list_customer re = new ref_price_list_customer();

                re.customer_id = item.customer_id;
                re.price_list_id = item.price_list_id;
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
                        var pt_detail = _scifferContext.ref_price_list_customer.Find(pt_detail_id);
                        _scifferContext.Entry(pt_detail).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                List<ref_price_list_customer_details> allalternate = new List<ref_price_list_customer_details>();
                if (item.item_id != null)
                {
                    for (var i = 0; i < item.item_id.Count; i++)
                    {
                        ref_price_list_customer_details alternate = new ref_price_list_customer_details();
                        alternate.price_list_id = item.price_list_id;
                        if (item.price_list_detail_id[i] == "")
                        {
                            alternate.price_list_detail_id = 0;
                        }
                        else
                        {
                            alternate.price_list_detail_id = int.Parse(item.price_list_detail_id[i]);
                        }
                        alternate.price = double.Parse(item.price[i]);
                        alternate.discount = item.discount[i] == "" ? 0 : double.Parse(item.discount[i]);
                        alternate.discount_type_id = int.Parse(item.discount_type_id[i]);
                        alternate.price_after_discount = double.Parse(item.price_after_discount[i]);
                        alternate.item_id = int.Parse(item.item_id[i]);
                        alternate.uom_id = int.Parse(item.uom_id[i]);
                        allalternate.Add(alternate);
                    }
                }

                foreach (var i in allalternate)
                {
                    if (i.price_list_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                re.ref_price_list_customer_details = allalternate;
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
