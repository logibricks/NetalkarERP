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
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly ScifferContext _scifferContext;

        public DocumentTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_document_type Document)
        {
            try
            {
                _scifferContext.ref_document_type.Add(Document);
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
                var document = _scifferContext.ref_document_type.FirstOrDefault(a => a.document_type_id == id);
                document.is_active = false;
                _scifferContext.Entry(document).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
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

        public ref_document_type Get(int id)
        {
            try
            {
                return _scifferContext.ref_document_type.FirstOrDefault(a => a.document_type_id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_document_type> GetAll()
        {
            try
            {
                return _scifferContext.ref_document_type.Where(a=>a.is_active==true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(ref_document_type Document)
        {
            try
            {
                _scifferContext.Entry(Document).State = System.Data.Entity.EntityState.Modified;
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
