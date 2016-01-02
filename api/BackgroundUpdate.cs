using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using ts.services;

namespace ts.api
{
    public class BackgroundUpdate: NancyModule
    {
        private readonly IBackgroundUpdate _backgroundUpdate;

        public BackgroundUpdate(IBackgroundUpdate backgroundUpdate)
        {
            _backgroundUpdate = backgroundUpdate;
            Post["/backgroundupdate", runAsync: true] = async (_, ct) => await DoUpdate();
        }

        public async Task<string> DoUpdate()
        {
            long userid = (long) this.Context.Request.Session["UserId"];
            await _backgroundUpdate.Update(userid);

            return "ok";
        }
    }
}
