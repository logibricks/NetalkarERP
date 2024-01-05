using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly ScifferContext _scifferContext;
        public JournalEntryService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(journal_entryVM party)
        {
            try
            {
                journal_entry JE = new journal_entry();
                JE.journal_entry_doc_number = party.journal_entry_doc_number;
                JE.journal_entry_number = party.journal_entry_number;
                JE.journal_entry_date = party.journal_entry_date;
                JE.journal_entry_posting_date = party.journal_entry_posting_date;
                JE.journal_entry_reference = party.journal_entry_reference;
                JE.journal_entry_remarks = party.journal_entry_remarks;
                JE.journal_entry_is_active = true;
                List<journal_entry_detail> jr_list = new List<journal_entry_detail>();
                foreach (var i in party.journal_entry_item)
                {
                    journal_entry_detail jr = new journal_entry_detail();
                    jr.journal_entry_item_is_active = true;
                    jr.journal_sr_no = i.journal_sr_no;
                    jr.journal_entry_item_description = i.journal_entry_item_description;
                    jr.journal_entry_dr = i.journal_entry_dr;
                    jr.journal_entry_cr = i.journal_entry_cr;
                    jr.journal_entry_remarks = i.journal_entry_remarks;
                    jr.party_type_id = i.party_type_id;

                    jr_list.Add(jr);
                }
                JE.journal_entry_item = jr_list;
                _scifferContext.journal_entry.Add(JE);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int? id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[journal_entry] set [journal_entry_is_active] = 0 where journal_entry_id = " + id);
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

        public journal_entryVM Get(int? id)
        {
            try
            {
                journal_entry JR = _scifferContext.journal_entry.FirstOrDefault(c => c.journal_entry_id == id);
                Mapper.CreateMap<journal_entry, journal_entryVM>();
                journal_entryVM JRVM = Mapper.Map<journal_entry, journal_entryVM>(JR);
                JRVM.journal_entry_item = JRVM.journal_entry_item.Where(c => c.journal_entry_item_is_active == true).ToList();
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<journal_entryVM> GetAll()
        {
            try
            {
                Mapper.CreateMap<journal_entry, journal_entryVM>();
                return _scifferContext.journal_entry.Project().To<journal_entryVM>().Where(a => a.journal_entry_is_active == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public List<journal_entryVM> getall()
        {
            var query = (from journal in _scifferContext.journal_entry
                         select new journal_entryVM()
                         {
                             journal_entry_id = journal.journal_entry_id,
                             journal_entry_number = journal.journal_entry_number,
                             journal_entry_doc_number = journal.journal_entry_doc_number,
                             journal_entry_reference = journal.journal_entry_reference,
                             journal_entry_remarks = journal.journal_entry_remarks,
                             journal_entry_date = journal.journal_entry_date,
                             journal_entry_posting_date = journal.journal_entry_posting_date,

                         }).ToList();
            return query; 
        }
        public bool Update(journal_entryVM party)
        {
            try
            {
                journal_entry JE = new journal_entry();
                JE.journal_entry_doc_number = party.journal_entry_doc_number;
                JE.journal_entry_number = party.journal_entry_number;
                JE.journal_entry_date = party.journal_entry_date;
                JE.journal_entry_posting_date = party.journal_entry_posting_date;
                JE.journal_entry_reference = party.journal_entry_reference;
                JE.journal_entry_remarks = party.journal_entry_remarks;
                JE.journal_entry_id = party.journal_entry_id;
                JE.journal_entry_is_active = true;

                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = party.deleteids.Split(new char[] { '~' });
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
                        var pt_detail = _scifferContext.journal_entry_detail.Find(pt_detail_id);
                        pt_detail.journal_entry_item_is_active = false;
                        _scifferContext.Entry(pt_detail).State = EntityState.Modified;
                    }
                }

                List<journal_entry_detail> jr_list = new List<journal_entry_detail>();
                foreach (var i in party.journal_entry_item)
                {
                    journal_entry_detail jr = new journal_entry_detail();
                    jr.journal_entry_item_is_active = true;
                    jr.journal_sr_no = i.journal_sr_no;
                    jr.journal_entry_item_description = i.journal_entry_item_description;
                    jr.journal_entry_dr = i.journal_entry_dr;
                    jr.journal_entry_cr = i.journal_entry_cr;
                    jr.journal_entry_remarks = i.journal_entry_remarks;
                    jr.party_type_id = i.party_type_id;
                    jr.journal_entry_item_id = i.journal_entry_item_id;
                    jr.journal_entry_id = party.journal_entry_id;

                    jr_list.Add(jr);
                }
                JE.journal_entry_item = jr_list;
                foreach (var i in JE.journal_entry_item)
                {
                    if (i.journal_entry_item_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }
                _scifferContext.Entry(JE).State = EntityState.Modified;
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
