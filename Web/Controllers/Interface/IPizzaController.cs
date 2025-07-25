using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Entity.Dtos.PizzaDto;

namespace Web.Controllers.Interface
{
    public interface IPizzaController : IGenericController<PizzaDto, Pizza>
    {
        Task<IActionResult> UpdatePartial(int id, PizzaUpdateDto dto);
        Task<IActionResult> ActiveAsync(int id);
    }
}


