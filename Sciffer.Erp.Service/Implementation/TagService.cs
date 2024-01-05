using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class TagService : ITagService
    {
        private readonly ScifferContext _ScifferContext;
        public TagService(ScifferContext ScifferContext)
        {
            _ScifferContext = ScifferContext;
        }
        public List<inv_item_batch_detail_tag> GetAll()
        {
            return _ScifferContext.inv_item_batch_detail_tag.ToList();
        }
    }
}
