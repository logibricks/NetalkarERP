using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class TaxService : ITaxService
    {

        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _gen;
        public TaxService(ScifferContext scifferContext, IGenericService gen)
        {
            _scifferContext = scifferContext;
            _gen = gen;
        }
        public bool Add(ref_tax_vm tax)
        {

            try
            {
                ref_tax tx = new ref_tax();
                tx.is_blocked = tax.is_blocked;
                tx.tax_code = tax.tax_code;
                tx.tax_id = tax.tax_id;
                tx.tax_name = tax.tax_name;
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = tax.taxdetail.Split(new string[] { "~" }, StringSplitOptions.None);
                }
                catch
                {

                }
                List<ref_tax_detail> tax_detail = new List<ref_tax_detail>();
                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {
                    var code = emptyStringArray[i].Split(new char[] { ',' })[1];
                    ref_tax_detail detail = new ref_tax_detail();
                    detail.tax_charged_on = emptyStringArray[i].Split(new char[] { ',' })[3];
                    detail.tax_element_id = _gen.GetTaxElementId(code);
                    detail.tax_id = tax.tax_id;
                    detail.tax_detail_id = 0;
                    detail.is_active = true;
                    tax_detail.Add(detail);
                }
                tx.ref_tax_detail = tax_detail;
                _scifferContext.ref_tax.Add(tx);
                _scifferContext.SaveChanges();
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
                var tax = _scifferContext.ref_tax.FirstOrDefault(c => c.tax_id == id);
                _scifferContext.Entry(tax).State = EntityState.Deleted;
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

        public ref_tax_vm Get(int id)
        {
            try
            {
                ref_tax JR = _scifferContext.ref_tax.FirstOrDefault(c => c.tax_id == id);
                Mapper.CreateMap<ref_tax, ref_tax_vm>();
                ref_tax_vm JRVM = Mapper.Map<ref_tax, ref_tax_vm>(JR);
                JRVM.ref_tax_detail = JRVM.ref_tax_detail.Where(c => c.is_active == true).ToList();
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_tax_vm> GetAll()
        {
            var query = (from t in _scifferContext.ref_tax
                         select new ref_tax_vm
                         {
                             is_blocked = t.is_blocked,
                             tax_code = t.tax_code,
                             tax_id = t.tax_id,
                             tax_name = t.tax_name,
                             tax_name_code = t.tax_code + " - " + t.tax_name,
                         }).OrderByDescending(a => a.tax_id).ToList();
            return query;
        }

        public bool Update(ref_tax_vm tax)
        {
            try
            {
                ref_tax tx = new ref_tax();
                tx.is_blocked = tax.is_blocked;
                tx.tax_code = tax.tax_code;
                tx.tax_id = tax.tax_id;
                tx.tax_name = tax.tax_name;
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = tax.taxdetail.Split(new string[] { "~" }, StringSplitOptions.None);
                }
                catch
                {

                }
                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = tax.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int tax_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        tax_detail_id = int.Parse(deleteStringArray[i]);
                        var ref_tax_detail = _scifferContext.ref_tax_detail.Find(tax_detail_id);
                        _scifferContext.Entry(ref_tax_detail).State = EntityState.Modified;
                        ref_tax_detail.is_active = false;
                    }
                }
                List<ref_tax_detail> tax_detail = new List<ref_tax_detail>();
                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {
                    var code = emptyStringArray[i].Split(new char[] { ',' })[1];
                    ref_tax_detail detail = new ref_tax_detail();
                    detail.tax_charged_on = emptyStringArray[i].Split(new char[] { ',' })[3];
                    detail.tax_element_id = _gen.GetTaxElementId(code);
                    detail.tax_id = tax.tax_id;
                    detail.tax_detail_id = emptyStringArray[i].Split(new char[] { ',' })[4] == "" ? 0 : int.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
                    detail.is_active = true;
                    tax_detail.Add(detail);
                }
                foreach (var i in tax_detail)
                {
                    if (i.tax_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }
                _scifferContext.Entry(tx).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
