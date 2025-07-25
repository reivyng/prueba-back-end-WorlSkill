using Entity.Dtos.PizzaDto;
using Entity.Model;

namespace Business.Interfaces
{
    public interface IPizzaBusiness : IBaseBusiness<Pizza, PizzaDto>
    {
       
        Task<bool> UpdatePartialAsync(PizzaUpdateDto dto);
        Task<bool> ActiveAsync(PizzaActiveDto dto);

    }
}
