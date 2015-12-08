using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.data;

namespace ts.services
{
    public class KeyInfoService: IKeyInfoService
    {
        private readonly IKeyInfoRepo _keyInfoRepo;
        public KeyInfoService(IKeyInfoRepo keyInfoRepo)
        {
            _keyInfoRepo = keyInfoRepo;
        }

        public async Task<long> Add(long userid, long keyid, string vcode)
        {
            return await _keyInfoRepo.AddKey(userid, keyid, vcode);
        }

        public async Task Delete(long keyinfoid)
        {
            await _keyInfoRepo.DeleteKey(keyinfoid);
        }
    }
}
