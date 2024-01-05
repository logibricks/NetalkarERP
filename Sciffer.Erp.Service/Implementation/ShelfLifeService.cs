using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class ShelfLifeService : IShelfLifeService
    {
        private readonly ScifferContext _sciffercontext;
        public ShelfLifeService(ScifferContext sciffercontext)
        {
            _sciffercontext = sciffercontext;
        }
        public bool Add(ref_shelf_life life)
        {
            try
            {
                _sciffercontext.ref_shelf_life.Add(life);
                _sciffercontext.SaveChanges();
               
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ref_shelf_life Get(int id)
        {
            return _sciffercontext.ref_shelf_life.Where(x => x.shelf_life_id == id).FirstOrDefault();
        }

        public List<ref_shelf_life> GetAll()
        {
            return _sciffercontext.ref_shelf_life.ToList();
        }

        public bool Update(ref_shelf_life life)
        {
            try
            {
                _sciffercontext.Entry(life).State = EntityState.Modified;
                _sciffercontext.SaveChanges();
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }
    }
}
