using AutoMapper;
using Desafio.BackEnd.Dtos;
using Desafio.BackEnd.Entities;

namespace Desafio.BackEnd.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<EventDto, Event>().ReverseMap();
            CreateMap<PanelistDto, Panelist>().ReverseMap();
        }
    }
}
