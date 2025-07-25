using Entity.Model;

namespace Data.Interfaces
{
    public interface IPizzaData : IBaseModelData<Pizza>
    {
        Task<bool> UpdatePartial(Pizza pizza);
        Task<bool> ActiveAsync(int id, bool active);

    }
}
