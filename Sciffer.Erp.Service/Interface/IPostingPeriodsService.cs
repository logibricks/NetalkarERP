using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
  public interface IPostingPeriodsService:IDisposable
    {   
        
        List<posting_periods> GetPostingPeriods();
        ref_posting_periods_vm Get(int id);       
        bool Add(ref_posting_periods_vm Posting);
        bool Update(ref_posting_periods_vm Posting);
        bool Delete(int id);
        List<posting_periods> GetPostingPeriodsForEdit(int id);
        bool ChangePostingStatus(posting_periods value );
    }
}
