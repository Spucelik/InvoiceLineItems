using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceLines
{
    public class Employees
    {
        public object[] entities { get; set; }
        public bool isActive { get; set; }
        public string MethodName { get; set; }
        public string text { get; set; }
    }

    class CommonReturnType
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string Employees { get; set; }
        public string IsCheck { get; set; }
    }
}
