using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class DocumentNumbringService : IDocumentNumbringService
    {
        private readonly ScifferContext _scifferContext;

        public DocumentNumbringService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public document_numbring Add(document_numbring doc)
        {
            try
            {
                ref_document_numbring d = new ref_document_numbring();
                d.module_id = doc.module_id;
                d.category = doc.category;
                d.current_number = doc.current_number;
                d.document_numbring_id = doc.document_numbring_id;
                d.prefix_sufix_id = doc.prefix_sufix_id;
                d.prefix_sufix = doc.prefix_sufix;
                d.financial_year_id = doc.financial_year_id;
                d.from_number = doc.from_number;
                d.module_form_id = doc.module_form_id;
                d.module_id = doc.module_id;
                d.set_default = doc.set_default;
                d.to_number = doc.to_number;
                d.is_blocked = doc.is_blocked;
                d.plant_id = doc.plant_id;
                if (doc.set_default == true)
                {
                    List<ref_document_numbring> rd = new List<ref_document_numbring>();
                    rd = _scifferContext.ref_document_numbring.Where(x => x.module_form_id == doc.module_form_id && x.financial_year_id==doc.financial_year_id).ToList();
                    foreach (var i in rd)
                    {
                        i.set_default = false;
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }
                _scifferContext.ref_document_numbring.Add(d);
                _scifferContext.SaveChanges();
                doc.document_numbring_id = _scifferContext.ref_document_numbring.Max(x => x.document_numbring_id);
                doc.module_name = _scifferContext.ref_module.Where(x => x.module_id == doc.module_id).FirstOrDefault().module_name;
                doc.form_name = _scifferContext.ref_module_form.Where(x => x.module_form_id == doc.module_form_id).FirstOrDefault().module_form_name;
                doc.financial_year = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_ID == doc.financial_year_id).FirstOrDefault().FINANCIAL_YEAR_NAME;
                doc.prefix_sufix_name = doc.prefix_sufix_id == 1 ? "Prefix" : "Suffix";
            }
            catch (Exception EX)
            {
                return doc;
            }
            return doc;
        }

        public bool Delete(int id)
        {
            try
            {
                var doc = _scifferContext.ref_document_numbring.FirstOrDefault(c => c.document_numbring_id == id);
                _scifferContext.Entry(doc).State = EntityState.Deleted;
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

        public ref_document_numbring Get(int id)
        {
            var doc = _scifferContext.ref_document_numbring.FirstOrDefault(c => c.document_numbring_id == id);
            return doc;
        }

        public List<ref_document_numbring> GetAll()
        {
            return _scifferContext.ref_document_numbring.ToList();
        }

        public document_numbring Update(document_numbring doc)
        {
            try
            {
                ref_document_numbring d = new ref_document_numbring();
                d.module_id = doc.module_id;
                d.category = doc.category;
                d.current_number = doc.current_number;
                d.document_numbring_id = doc.document_numbring_id;
                d.prefix_sufix_id = doc.prefix_sufix_id;
                d.prefix_sufix = doc.prefix_sufix;
                d.financial_year_id = doc.financial_year_id;
                d.from_number = doc.from_number;
                d.module_form_id = doc.module_form_id;
                d.module_id = doc.module_id;
                d.set_default = doc.set_default;
                d.to_number = doc.to_number;
                d.is_blocked = doc.is_blocked;
                d.plant_id = doc.plant_id;
                if (doc.set_default == true)
                {
                    List<ref_document_numbring> rd = new List<ref_document_numbring>();
                    rd = _scifferContext.ref_document_numbring.Where(x => x.module_form_id == doc.module_form_id && x.document_numbring_id != doc.document_numbring_id && x.financial_year_id == doc.financial_year_id && x.plant_id == doc.plant_id).ToList();
                    foreach (var i in rd)
                    {
                        i.set_default = false;
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }
                _scifferContext.Entry(d).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                doc.prefix_sufix_name = doc.prefix_sufix_id == 1 ? "Prefix" : "Suffix";
                doc.module_name = _scifferContext.ref_module.Where(x => x.module_id == doc.module_id).FirstOrDefault().module_name;
                doc.form_name = _scifferContext.ref_module_form.Where(x => x.module_form_id == doc.module_form_id).FirstOrDefault().module_form_name;
                doc.financial_year = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_ID == doc.financial_year_id).FirstOrDefault().FINANCIAL_YEAR_NAME;
                
            }
            catch (Exception ex)
            {
                return doc;
            }
            return doc;
        }

        public List<document_numbring> GetDocumentNumbering()
        {
            var query = (from d in _scifferContext.ref_document_numbring
                         join f in _scifferContext.REF_FINANCIAL_YEAR on d.financial_year_id equals f.FINANCIAL_YEAR_ID
                         join m in _scifferContext.ref_module on d.module_id equals m.module_id
                         join m1 in _scifferContext.ref_module_form on d.module_form_id equals m1.module_form_id
                         join plnt in _scifferContext.REF_PLANT on d.plant_id equals plnt.PLANT_ID into plant1 from pl in plant1.DefaultIfEmpty() 
                         select new document_numbring
                         {
                        category=d.category,
                        current_number=d.current_number,
                        document_numbring_id=d.document_numbring_id,
                        prefix_sufix_id=d.prefix_sufix_id,
                        prefix_sufix=d.prefix_sufix,
                        financial_year=f.FINANCIAL_YEAR_NAME,
                        form_name=m1.module_form_name,
                        from_number=d.from_number,
                        module_name=m.module_name,
                        to_number=d.to_number,                       
                        financial_year_id=d.financial_year_id,
                        module_form_id=d.module_form_id,
                        module_id=d.module_id,
                        prefix_sufix_name = d.prefix_sufix_id == 1 ? "Prefix" : "Suffix",
                        is_blocked=d.is_blocked,
                        set_default=d.set_default,
                        plant_id = d.plant_id,
                        plant_name = pl.PLANT_CODE + "-" + pl.PLANT_NAME
        }).OrderByDescending(a => a.document_numbring_id).ToList();
            return query;
        }

        public bool checksetdefault(int id,int financial_year_id)
        {
            var chk = _scifferContext.ref_document_numbring.Count(x => x.module_form_id == id && x.financial_year_id==financial_year_id);
            if (chk == 0)
                return true;
            else
                return false;
        }

    }
}
