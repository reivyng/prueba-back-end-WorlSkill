using AutoMapper;
using Entity.Dtos.ClienteDto;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Cliente, ClienteUpdateDto>().ReverseMap();
        }
    }
}
