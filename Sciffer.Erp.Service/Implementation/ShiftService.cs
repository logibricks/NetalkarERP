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
    public class ShiftService : IShiftService
    {
        private readonly ScifferContext _scifferContext;
        public ShiftService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public shift Add(shift Shift)
        {
            try
            {
                ref_shifts rs = new ref_shifts();
                rs.from_time = TimeSpan.Parse(Shift.from_time);
                rs.plant_id = Shift.plant_id;
                rs.shift_code = Shift.shift_code;
                rs.shift_id = Shift.shift_id;
                rs.to_time = TimeSpan.Parse(Shift.to_time);
                rs.is_active = true;
                rs.is_blocked = Shift.is_blocked;
                _scifferContext.ref_shifts.Add(rs);
                _scifferContext.SaveChanges();
                Shift.shift_id = _scifferContext.ref_shifts.Max(x => x.shift_id);
                Shift.plant_name = _scifferContext.REF_PLANT.Where(x => x.PLANT_ID == Shift.plant_id).FirstOrDefault().PLANT_NAME;
            }
            catch (Exception ex)
            {
                return Shift;
            }
            return Shift;
        }

        public bool Delete(int? id)
        {
            try
            {
                var rs = _scifferContext.ref_shifts.FirstOrDefault(x => x.shift_id == id);
                rs.is_active = false;
                _scifferContext.Entry(rs).State = EntityState.Modified;                
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
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

        public ref_shifts Get(int? id)
        {
            try
            {
                return _scifferContext.ref_shifts.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_shifts> GetAll()
        {
            try
            {
                var all= _scifferContext.ref_shifts.Where(x=>x.is_active==true).ToList();
                return all;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public shift Update(shift Shift)
        {
            try
            {
                ref_shifts rs = new ref_shifts();
                rs.from_time = TimeSpan.Parse(Shift.from_time);
                rs.plant_id = Shift.plant_id;
                rs.shift_code = Shift.shift_code;
                rs.shift_id = Shift.shift_id;
                rs.to_time = TimeSpan.Parse(Shift.to_time);
                rs.is_active = true;
                rs.is_blocked = Shift.is_blocked;
                _scifferContext.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
                Shift.plant_name = _scifferContext.REF_PLANT.Where(x => x.PLANT_ID == Shift.plant_id).FirstOrDefault().PLANT_NAME;
            }
            catch (Exception ex)
            {
                return Shift;
            }
            return Shift;
        }

        public List<shift> GetShiftList()
        {
            var query = (from s in _scifferContext.ref_shifts.Where(x => x.is_active == true)
                         join p in _scifferContext.REF_PLANT on s.plant_id equals p.PLANT_ID
                         select new
                         {
                             from_time = s.from_time,
                             plant_name = p.PLANT_NAME,
                             shift_code = s.shift_code,
                             shift_desc = s.shift_desc,
                             shift_id = s.shift_id,
                             to_time = s.to_time,
                             plant_id = s.plant_id,
                             is_blocked = s.is_blocked,
                         }).ToList().Select(a => new shift() {
                             from_time = a.from_time.Hours.ToString("00") + ":" + a.from_time.Minutes.ToString("00"),
                             plant_name = a.plant_name,
                             shift_code = a.shift_code,
                             shift_desc = a.plant_name + " - " + a.shift_desc,
                             shift_id = a.shift_id,
                             to_time = a.to_time.Hours.ToString("00")+":"+ a.to_time.Minutes.ToString("00"),
                             plant_id = a.plant_id,
                             is_blocked = a.is_blocked,
                         }).OrderByDescending(a => a.shift_id).ToList();
            return query;
            //throw new NotImplementedException();
        }
    }
}
