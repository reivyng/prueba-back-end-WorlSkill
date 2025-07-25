using Entity.Dtos.PedidoDto;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{

    public interface IPedidoController : IGenericController<PedidoDto, Pedido>
    {
        Task<IActionResult> UpdatePartial(int id, PedidoDto dto);

    }
}
