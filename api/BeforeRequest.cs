using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy;

namespace ts.api
{
    public class BeforeRequest
    {
        public static async Task<Response> BeforeRequestHandler(IAccountRepo accountRepo, NancyContext ctx, CancellationToken token)
        {
            // do not check for session key if login
            if (ctx.Request.Url.ToString().Contains("/api/account"))
                return null;

            // check if header contains session key
            var skeys = ctx.Request.Headers["jwt"].ToList();
            if (skeys.Count != 1)
                throw new UserException(strings.InvalidSessionKey);

            // request ctx 
            var userid = await accountRepo.CheckSession(skeys[0]);
            if (ctx.Parameters == null)
                ctx.Parameters = new ExpandoObject();
            ctx.Parameters.UserId = userid;
            return null;
        }
    }
}
