using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
namespace Sciffer.Erp.Service.Interface
{
 public interface IInstructionTypeService : IDisposable
    {
        
        List<ref_instruction_type> GetAll();
        ref_instruction_type Get(int id);
        bool Add(ref_instruction_type Instruction);
        bool Update(Domain.Model.ref_instruction_type Instruction);
        bool Delete(int id);
    }
}
