using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sciffer.Erp.Service.Implementation
{
    public class TagNumberingService : ITagNumberingService
    {
        private readonly ScifferContext _scifferContext;

        public TagNumberingService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public mfg_tag_numbering_VM Add(mfg_tag_numbering_VM tag)
        {
            try
            {
                mfg_tag_numbering rc = new mfg_tag_numbering();
                rc.tag_numbering_id = tag.tag_numbering_id;
                rc.from_number = tag.from_number;
                rc.to_number = tag.to_number;
                rc.current_number = tag.current_number;
                rc.year = tag.year;
                rc.month = tag.month;
                rc.prefix = tag.prefix;
                rc.is_blocked = tag.is_blocked;
                rc.is_active = true;
                rc.machine_id = tag.machine_id;
                rc.item_id = tag.ITEM_ID;

                _scifferContext.mfg_tag_numbering.Add(rc);
                _scifferContext.SaveChanges();

                tag.tag_numbering_id = _scifferContext.mfg_tag_numbering.Max(x => x.tag_numbering_id);
                tag.ITEM_NAME = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == tag.ITEM_ID).FirstOrDefault().ITEM_NAME;
                tag.machine_code = _scifferContext.ref_machine.Where(x => x.machine_id == tag.machine_id).FirstOrDefault().machine_name;

            }
            catch (Exception e)
            {
                return tag;
            }
            return tag;
        }

        public bool Delete(int id)
        {
            try
            {
                var tag = _scifferContext.mfg_tag_numbering.FirstOrDefault(c => c.tag_numbering_id == id);
                tag.is_active = false;
                _scifferContext.Entry(tag).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
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

        public mfg_tag_numbering_VM Get(int id)
        {
            var tag = _scifferContext.mfg_tag_numbering.FirstOrDefault(c => c.tag_numbering_id == id);
            Mapper.CreateMap<mfg_tag_numbering, mfg_tag_numbering_VM>();
            mfg_tag_numbering_VM VM = Mapper.Map<mfg_tag_numbering, mfg_tag_numbering_VM>(tag);
            return VM;
        }

        public List<mfg_tag_numbering_VM> GetAll()
        {
            var query = (from tg in _scifferContext.mfg_tag_numbering
                         where tg.is_active == true
                         join ri in _scifferContext.REF_ITEM on tg.item_id equals ri.ITEM_ID
                         join rm in _scifferContext.ref_machine on tg.machine_id equals rm.machine_id
                         orderby tg.tag_numbering_id descending
                         select new
                         {
                             tag_numbering_id = tg.tag_numbering_id,
                             from_number = tg.from_number,
                             to_number = tg.to_number,
                             current_number = tg.current_number,
                             year = tg.year,
                             month = tg.month,
                             prefix = tg.prefix,
                             is_active = tg.is_active,
                             is_blocked = tg.is_blocked,
                             machine_id = tg.machine_id,
                             machine_code = rm.machine_name,
                             ITEM_ID = tg.item_id,
                             ITEM_NAME = ri.ITEM_NAME
                         }).ToList()
                         .Select(x => new mfg_tag_numbering_VM
                         {
                             tag_numbering_id = x.tag_numbering_id,
                             from_number = x.from_number,
                             to_number = x.to_number,
                             current_number = x.current_number,
                             year = x.year,
                             month = x.month,
                             prefix = x.prefix,
                             is_active = x.is_active,
                             is_blocked = x.is_blocked,
                             machine_id = x.machine_id,
                             machine_code = x.machine_code,
                             ITEM_ID = x.ITEM_ID,
                             ITEM_NAME = x.ITEM_NAME
                         }).ToList();
            return query;
        }

        public List<mfg_tag_numbering_VM> GetTagNumbering()
        {
            var query = (from tg in _scifferContext.mfg_tag_numbering
                         where tg.is_active == true
                         join ri in _scifferContext.REF_ITEM on tg.item_id equals ri.ITEM_ID
                         join rm in _scifferContext.ref_machine on tg.machine_id equals rm.machine_id
                         orderby tg.tag_numbering_id descending
                         select new
                         {
                             tag_numbering_id = tg.tag_numbering_id,
                             from_number = tg.from_number,
                             to_number = tg.to_number,
                             current_number = tg.current_number,
                             year = tg.year,
                             month = tg.month,
                             prefix = tg.prefix,
                             is_active = tg.is_active,
                             is_blocked = tg.is_blocked,
                             machine_id = tg.machine_id,
                             machine_code = rm.machine_code,
                             ITEM_ID = tg.item_id,
                             ITEM_NAME = ri.ITEM_NAME
                         }).ToList()
                         .Select(x => new mfg_tag_numbering_VM
                         {
                             tag_numbering_id = x.tag_numbering_id,
                             from_number = x.from_number,
                             to_number = x.to_number,
                             current_number = x.current_number,
                             year = x.year,
                             month = x.month,
                             prefix = x.prefix,
                             is_active = x.is_active,
                             is_blocked = x.is_blocked,
                             machine_id = x.machine_id,
                             machine_code = x.machine_code,
                             ITEM_ID = x.ITEM_ID,
                             ITEM_NAME = x.ITEM_NAME
                         }).ToList();
            return query;
        }



        public mfg_tag_numbering_VM Update(mfg_tag_numbering_VM tag)
        {
            try
            {
                mfg_tag_numbering rc = _scifferContext.mfg_tag_numbering.Where(x => x.tag_numbering_id == tag.tag_numbering_id).FirstOrDefault();
                rc.from_number = tag.from_number;
                rc.to_number = tag.to_number;
                rc.current_number = tag.current_number;
                rc.year = tag.year;
                rc.month = tag.month;
                rc.prefix = tag.prefix;
                rc.is_blocked = tag.is_blocked;
                rc.is_active = true;
                rc.item_id = tag.ITEM_ID;
                rc.machine_id = tag.machine_id;

                _scifferContext.Entry(rc).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                tag.ITEM_NAME = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == tag.ITEM_ID).FirstOrDefault().ITEM_NAME;
                tag.machine_code = _scifferContext.ref_machine.Where(x => x.machine_id == tag.machine_id).FirstOrDefault().machine_name;

            }
            catch (Exception e)
            {
                return tag;
            }
            return tag;
        }

    }
}
