using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace orocomWS.Utils
{
    public class PaginacionGenerico
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int countBD { get; set; }
        
    }
}