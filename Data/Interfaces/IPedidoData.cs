using Entity.Model;

namespace Data.Interfaces
{
    public interface IPedidoData : IBaseModelData<Pedido>
    {
        Task<bool> UpdatePartial(Pedido pedido);

    }
}
