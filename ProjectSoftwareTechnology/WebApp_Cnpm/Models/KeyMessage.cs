using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models
{
    public class KeyMessage
    {
        public String message { get; set; }
        public String author { get; set; }
    }

    public class KeyAmountElement
    {
        public int amountElement { get; set; }
        public String callGetMessage { get; set; }
    }
}
