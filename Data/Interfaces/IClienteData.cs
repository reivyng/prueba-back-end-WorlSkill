using Entity.Model;

namespace Data.Interfaces
{
    public interface IClienteData : IBaseModelData<Cliente>
    {
        Task<bool> UpdatePartial(Cliente cliente);
        Task<bool> ActiveAsync(int id, bool active);


    }
}
