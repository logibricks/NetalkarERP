using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBatchNumberingService
    {
        List<batch_numbering_VM> GetAll();
        batch_numbering_VM Add(batch_numbering_VM value);
        batch_numbering_VM Update(batch_numbering_VM value);
        bool Delete(int key);
        ref_batch_numbering Get(int id);
        ref_batch_numbering GetCategory(int item_category, int plant_id);
    }
}
