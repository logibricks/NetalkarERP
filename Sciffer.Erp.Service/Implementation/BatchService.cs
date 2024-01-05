using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class BatchService : IBatchService
    {
        private readonly ScifferContext _scifferContext;

        public BatchService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(inv_item_batch_VM batch)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public inv_item_batch_VM Get(int id)
        {
            try
            {
                inv_item_batch JR = _scifferContext.inv_item_batch.FirstOrDefault(c => c.item_batch_id == id);
                Mapper.CreateMap<inv_item_batch, inv_item_batch_VM>();
                inv_item_batch_VM JRVM = Mapper.Map<inv_item_batch, inv_item_batch_VM>(JR);
                JRVM.inv_item_batch_detail = JRVM.inv_item_batch_detail.ToList();
                //JRVM.gl_ledger_code = JR.ref_general_ledger.gl_ledger_code;
                //JRVM.gl_ledger_name = JR.ref_general_ledger.gl_ledger_name;
                //JRVM.posting_date1 = JRVM.posting_date.Day+"-"+ JRVM.posting_date.Month+"-"+ JRVM.posting_date.Year;
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<inv_item_batch_VM> GetAll()
        {
            var query = (from ed in _scifferContext.inv_item_batch
                         //join item in _scifferContext.REF_ITEM on ed.item_id equals item.ITEM_ID
                         select new inv_item_batch_VM
                         {
                             item_batch_id = ed.item_batch_id,
                             batch_number = ed.batch_number,
                             batch_yes_no =ed.batch_yes_no,
                             batch_manual_yes_no = ed.batch_manual_yes_no,
                             document_code = ed.document_code,
                             document_id = ed.document_id,
                             document_detail_id = ed.document_detail_id,
                             item_id = ed.item_id,
                             expirary_date = ed.expirary_date,
                             //item_name = item.ITEM_NAME,

                         }).OrderByDescending(a => a.item_batch_id).ToList();
            return query;
        }

        //public List<BATCH> GetBatchUsingPlantItem(int plant_id,int item_id)
        //{
        //   // var ent = new SqlParameter("@entity", entity.Trim());
        //    var plant = new SqlParameter("@plant_id", plant_id);
        //    var item = new SqlParameter("@item_id", item_id);
        //    var val = _scifferContext.Database.SqlQuery<BATCH>(
        //    "exec GetBatchFromItemPlant @plant_id,@item_id", plant, item).ToList();
        //    return val;

        //}
        public List<inv_item_batch_VM> GetBatchNumberUsingPlant(int item_id, int plant_id)
        {
            var batchNumber = (from ed in _scifferContext.inv_item_batch.Where(x => x.item_id == item_id)
                               //join bd in _scifferContext.inv_item_batch_detail.Where(x=>x.plant_id==plant_id && x.bucket_id==1) on ed.item_batch_id equals bd.item_batch_id
                               select new inv_item_batch_VM
                               {
                                   item_batch_id = ed.item_batch_id,
                                   batch_number = ed.batch_number,
                               }).ToList();
            return batchNumber;
        }
        public List<inv_item_batch_VM> GetBatchNumber(int item_id)
        {
            var batchNumber = (from ed in _scifferContext.inv_item_batch.Where(x => x.item_id == item_id)
                               //join bd in _scifferContext.inv_item_batch_detail.Where(x=>x.plant_id==plant_id) on ed.item_batch_id equals bd.item_batch_id
                               select new inv_item_batch_VM {
                                   item_batch_id = ed.item_batch_id,
                                   batch_number = ed.batch_number,
                               }).ToList();
            return batchNumber;
        }
        public bool Update(inv_item_batch_VM batch)
        {
            throw new NotImplementedException();
        }
    }
}
