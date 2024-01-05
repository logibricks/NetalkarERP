using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_shifts
    {
        [Key]
        public int shift_id { get; set; }
        [Display(Name = "Plant")]
        [Required(ErrorMessage = "plant is required")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name ="Shift Code")]
        [Required(ErrorMessage ="shift code is required")]
        [StringLength(maximumLength:10,MinimumLength =1,ErrorMessage ="code is too long")]
        public string shift_code { get; set; }
        [Display(Name = "From Time")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "from time is required")]
        public string shift_desc { get; set; }
        public TimeSpan from_time { get; set; }
        [Display(Name = "To Time")]
        [Required(ErrorMessage = "to time is required")]
        [DataType(DataType.Time)]
        public TimeSpan to_time { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
    public class shift
    {
        public int shift_id { get; set; }
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        public string shift_code { get; set; }
        public string shift_desc { get; set; }
        public string from_time { get; set; }      
        public string to_time { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
