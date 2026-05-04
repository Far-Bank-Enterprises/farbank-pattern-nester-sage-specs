using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Farbank.Pattern.Nester.SageSpecs.Models
{
    [Table("BlankWorkOrderSettings", Schema = "sagespecs")]
    public class BlankWorkOrderSetting
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int WarrantyCapacity { get; set; }
        [Required]
        public int BlankCapacity { get; set; }
        [Required]
        public int BlankWorkOrderMaxQty { get; set; }
        [Required]
        public int BlankWorkOrderMinQty { get; set; }
        [Required]
        public int RepairWorkOrderMaxQty { get; set; }
        [Required]
        public int RepairWorkOrderMinQty { get; set; }
        [Required]
        public bool PriorShiftMandrels { get; set; }
    }
}

