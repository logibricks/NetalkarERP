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
    public class BucketService : IBucketService
    {
        private readonly ScifferContext _scifferContext;
        public BucketService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_bucket vm)
        {
            vm.is_active = true;
            _scifferContext.ref_bucket.Add(vm);
            _scifferContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var ed = _scifferContext.ref_bucket.Where(x => x.bucket_id == id).FirstOrDefault();
            ed.is_active = false;
            return true;
        }

        public ref_bucket Get(int id)
        {
            return _scifferContext.ref_bucket.Where(x => x.bucket_id == id && x.is_active == true).FirstOrDefault();
        }

        public List<ref_bucket> GetAll()
        {
            return _scifferContext.ref_bucket.ToList();
        }

        public int GetID(string st)
        {
            var X = _scifferContext.ref_bucket.Where(x => x.bucket_name == st).FirstOrDefault();
            var id = X == null ? "0" : X.bucket_id.ToString();
            return int.Parse(id);
        }

        public bool Update(ref_bucket vm)
        {
            vm.is_active = true;
            _scifferContext.Entry(vm).State = System.Data.Entity.EntityState.Modified;
            _scifferContext.SaveChanges();
            return true;
        }
    }
}
