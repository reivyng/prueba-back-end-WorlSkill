using Entity.Dtos.PedidoDto;
using Entity.Model;

namespace Business.Interfaces
{
    public interface IPedidoBusiness : IBaseBusiness<Pedido, PedidoDto>
    {
        Task<bool> UpdatePartialAsync(PedidoDto dto);

    }
}
