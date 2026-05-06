using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Models
{

    public class ODataResult<T>
    {
        public string odatacontext { get; set; }
        public List<T> value { get; set; }
    }

}
