using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Nancy;
using ts.data;
using ts.dto;

namespace ts.api
{
    public class Pilots: NancyModule
    {
        private readonly IPilotRepo _pilotRepo;

        public Pilots(IPilotRepo pilotRepo)
        {
            _pilotRepo = pilotRepo;
            Get["/api/pilots", runAsync: true] = async (_, ct) => await GetPilots();
        }

        public async Task<IEnumerable<PilotDto>>  GetPilots()
        {
            long userid = (long)this.Context.Request.Session["UserId"];
            var pilots = await _pilotRepo.GetAll(userid);

            return pilots.Select(x => Mapper.Map<PilotDto>(x));
        }
    }
}
