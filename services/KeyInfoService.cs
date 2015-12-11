using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.data;
using ts.dto;
using AutoMapper;

namespace ts.services
{
    public class KeyInfoService: IKeyInfoService
    {
        private readonly IKeyInfoRepo _keyInfoRepo;
        private readonly IPilotRepo _pilotRepo;
        private readonly ICorporationRepo _corporationRepo;

        public KeyInfoService(IKeyInfoRepo keyInfoRepo, IPilotRepo pilotRepo, ICorporationRepo corporationRepo)
        {
            _keyInfoRepo = keyInfoRepo;
            _pilotRepo = pilotRepo;
            _corporationRepo = corporationRepo;
        }

        public async Task<long> Add(long userid, long keyid, string vcode)
        {
            var keyinfoid = await _keyInfoRepo.AddKey(userid, keyid, vcode);
            await _pilotRepo.SimpleUpdateFromKey(userid, keyinfoid, keyid, vcode);

            return keyinfoid;
        }

        public async Task Delete(long keyinfoid)
        {
            await _keyInfoRepo.DeleteKey(keyinfoid);
        }

        public async Task<List<KeyInfoDto>> Get(long userid)
        {
            var keys = await _keyInfoRepo.GetKeys(userid);
            var pilots = await _pilotRepo.GetAll(userid);
            var corpos = await _corporationRepo.GetAll(userid);

            var result =
                keys.GroupBy(x => x.KeyInfoId).Select(group => group.First()).Select(Mapper.Map<KeyInfoDto>).ToList();
            foreach (var r in result)
            {
                r.Pilots = pilots.Where(p => p.KeyInfoId == r.KeyInfoId).Select(Mapper.Map<PilotDto>).ToList();
                r.Corporations =
                    corpos.Where(c => c.KeyInfoId == r.KeyInfoId).Select(Mapper.Map<CorporationDto>).ToList();
            }

            return result;
        }
    }
}
