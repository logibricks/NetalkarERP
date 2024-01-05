using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Sciffer.Erp.Domain.Model
{
    public class ref_fin_template
    {
        [Key]
        public int template_id { get; set; }
        public string template_name { get; set; }
        public int created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public bool is_blocked { get; set; }
        public bool t_format { get; set; }
        public virtual ICollection<ref_fin_template_detail> ref_fin_template_detail { get; set; }

    }
    
    public class exportdata1
    {
        public int SrNo { get; set; }
        public int? BsAndPl { get; set; }
        public int? BsPlSide { get; set; }
        public int? GroupNo { get; set; }
        public string GroupName { get; set; }
        public string parent_id { get; set; }
        public int? GroupLevel { get; set; }
        public bool? MainHeading { get; set; }
    }
    public class Node1
    {
        public string group_name;
        public int? parent_id;
        public int? template_detail_id;
}
}
