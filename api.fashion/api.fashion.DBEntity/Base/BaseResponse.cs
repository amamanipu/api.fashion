using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class BaseResponse
    {
        public bool issuccess { get; set; }
        public string errorcode { get; set; }
        public string errormessage { get; set; }
        public object data { get; set; }
    }
}
