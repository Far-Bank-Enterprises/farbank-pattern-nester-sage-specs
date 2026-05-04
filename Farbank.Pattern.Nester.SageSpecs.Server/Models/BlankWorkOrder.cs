using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farbank.Pattern.Nester.SageSpecs.Models
{
    public class BlankWorkOrder
    {
        public string Number { get; set; }
        public string Item { get; set; }
        public string Mandrel { get; set; }
        public int Quantity { get; set; }
        public bool AmShift { get; set; }
    }
}
