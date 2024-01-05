using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBucketService
    {
        List<ref_bucket> GetAll();
        ref_bucket Get(int id);
        bool Add(ref_bucket vm);
        bool Update(ref_bucket vm);
        bool Delete(int id);
        int GetID(string st);
    }
}
