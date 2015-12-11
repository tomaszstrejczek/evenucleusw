using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace ts.api
{
    public class Pilots: NancyModule
    {
        public Pilots()
        {
            Get["/pilots/{userid}"] = (dynamic parameters) => GetPilots(parameters.userid);
        }

        public string GetPilots(long userid)
        {
            return "";
        }
    }
}
