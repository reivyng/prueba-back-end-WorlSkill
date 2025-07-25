using AutoMapper;
using Entity.Dtos.PizzaDto;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    public class PizzaProfile :Profile
    {
        public PizzaProfile()
        {
            CreateMap<Pizza, PizzaDto>().ReverseMap();
            CreateMap<Pizza, PizzaUpdateDto>().ReverseMap();
        }
    }
}
