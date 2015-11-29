using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy;

namespace ts.api
{
    public class BeforRequest
    {
        public static async Task<Response> BeforRequestHandler(IAccountRepo accountRepo, NancyContext ctx, CancellationToken token)
        {
            // do not check for session key if login
            if (ctx.Request.Url.ToString().Contains("/api/account"))
                return null;

            // check if header contains session key
            var skeys = ctx.Request.Headers["jwt"].ToList();
            if (skeys.Count != 1)
                throw new UserException(strings.InvalidUserPassword);

            // request ctx 
            ctx.Parameters.UserId = await accountRepo.CheckSession(skeys[0]);
            return null;
        }
    }
}
