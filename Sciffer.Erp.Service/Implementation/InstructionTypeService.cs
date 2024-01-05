using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class InstructionTypeService :IInstructionTypeService
    {
        public readonly ScifferContext _scifferContext;
        public InstructionTypeService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }

       
       public  List<ref_instruction_type> GetAll() {
             
        {
           

            return _scifferContext.ref_instruction_type.ToList().Where(x=>x.is_active==true).ToList();
        }
    }
        public ref_instruction_type Get(int id) {
            ref_instruction_type IT = _scifferContext.ref_instruction_type.FirstOrDefault(c => c.instruction_type_id == id);
            return IT;
        }
        public bool Add(ref_instruction_type Instruction){

            try { 

           
            Instruction.is_active = true;
            _scifferContext.ref_instruction_type.Add(Instruction);
            _scifferContext.SaveChanges();

        }

            catch (Exception e)
            {

                return false;
    }

            return true;


        }
        public bool Update(Domain.Model.ref_instruction_type Instruction)
        {
            try
            {
                Instruction.is_active = true;

                _scifferContext.Entry(Instruction).State = EntityState.Modified;
                _scifferContext.SaveChanges();


            }

            catch (Exception e)
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
        public bool Delete(int id)
        {
            try
            {
                var IT = _scifferContext.ref_instruction_type.FirstOrDefault(c => c.instruction_type_id == id);
                IT.is_active = false;
                _scifferContext.Entry(IT).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

