using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_STORAGE_LOCATION
    {
        [Key]
        public int storage_location_id { get; set; }
        [Required]
        [Display(Name = "Sloc Code")]
        public string storage_location_name { get; set; }
        [Display(Name = "Sloc Description")]
        [Required(ErrorMessage ="Description is required")]
        public string description { get; set; }
        
        [Display(Name = "Blocked")]
        public bool is_blocked { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id ")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public bool? is_active { get; set; }
    }
    public class storage_vm
    {
        public int storage_location_id { get; set; }
        public string storage_location_name { get; set; }
        public string plant_name { get; set; }
        public int plant_id { get; set; }
        public string description { get; set; }
        public bool is_blocked { get; set; }
        public int state_id { get; set; }
    }
}
