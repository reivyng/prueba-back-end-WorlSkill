using Entity.Dtos.ClienteDto;
using Entity.Model;

namespace Business.Interfaces
{
    public interface IClienteBusiness : IBaseBusiness<Cliente, ClienteDto>
    {
       
        Task<bool> UpdatePartialAsync(ClienteUpdateDto dto);
        Task<bool> ActiveAsync(ClienteActiveDto dto);



    }
}
