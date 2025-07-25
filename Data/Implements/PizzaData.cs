using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Model;

namespace Data.Implements
{
    public class PizzaData : BaseModelData<Pizza>, IPizzaData
    {
        public PizzaData(ApplicationDbContext context) : base(context)
        {
        }
            public async Task<bool> UpdatePartial(Pizza pizza)
            {
                var existingPizza = await _dbSet.FindAsync(pizza.Id);
                foreach (var prop in typeof(Pizza).GetProperties().Where(p => p.CanWrite && p.Name != "Id"))
                {
                    var val = prop.GetValue(pizza);
                    if (val != null && (!(val is string s) || !string.IsNullOrWhiteSpace(s)))
                        prop.SetValue(existingPizza, val);
                }

            await _context.SaveChangesAsync();
                return true;
            }

        public async Task<bool> ActiveAsync(int id, bool active)
        {
            var pizza = await _context.Set<Pizza>().FindAsync(id);
            if (pizza == null)
                return false;

            pizza.Active = active;
            _context.Entry(pizza).Property(c => c.Active).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
