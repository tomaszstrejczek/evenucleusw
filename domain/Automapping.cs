using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using ts.dto;

namespace ts.domain
{
    public static class Automapping
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<KeyInfo, KeyInfoDto>()
                    .ForMember(d => d.Pilots, opt => opt.Ignore())
                    .ForMember(d => d.Corporations, opt => opt.Ignore());
                cfg.CreateMap<Skill, SkillDto>();
                cfg.CreateMap<SkillInQueue, SkillInQueueDto>()
                    .ForMember(d => d.Length, m => m.MapFrom(s => TimeSpanFormatter.Format(s.Length)));
                cfg.CreateMap<Pilot, PilotDto>();
                cfg.CreateMap<Corporation, CorporationDto>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
