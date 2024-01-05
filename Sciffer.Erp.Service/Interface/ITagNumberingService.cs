using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITagNumberingService:IDisposable
    {
        List<mfg_tag_numbering_VM> GetAll();
        List<mfg_tag_numbering_VM> GetTagNumbering();
        mfg_tag_numbering_VM Get(int id);
        mfg_tag_numbering_VM Add(mfg_tag_numbering_VM tag);
        mfg_tag_numbering_VM Update(mfg_tag_numbering_VM tag);
        bool Delete(int id);
    }
}
