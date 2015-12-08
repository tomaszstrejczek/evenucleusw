using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ts.domain
{
    public class UserException: Exception
    {
        public string InnerExceptionJson { get; set; }

        public UserException(string msg, Exception inner): base(msg)
        {
            InnerExceptionJson = JsonConvert.SerializeObject(inner.ToString() + "\nstack trace:" + inner.StackTrace.ToString());
        }

        public UserException(string msg): base(msg)
        {
            InnerExceptionJson = "";
        }
    }
}
