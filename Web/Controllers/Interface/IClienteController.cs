using Entity.Dtos.ClienteDto;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IClienteController : IGenericController<ClienteDto, Cliente>
    {
        Task<IActionResult> UpdatePartial(int id,  ClienteUpdateDto dto);
        Task<IActionResult> DeleteLogic(int id);
    }
}


