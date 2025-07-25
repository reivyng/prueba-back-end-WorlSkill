using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Model;

namespace Data.Implements
{
    public class ClienteData : BaseModelData<Cliente>, IClienteData
    {

        public ClienteData(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> UpdatePartial(Cliente cliente)
        {
            var existing = await _dbSet.FindAsync(cliente.Id);
            if (existing == null) return false;

            foreach (var p in typeof(Cliente).GetProperties().Where(x => x.CanWrite && x.Name != "Id"))
            {
                var v = p.GetValue(cliente);
                if (v != null && (!(v is string s) || !string.IsNullOrWhiteSpace(s)))
                    p.SetValue(existing, v);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActiveAsync(int id, bool active)
        {
            var cliente = await _context.Set<Cliente>().FindAsync(id);
            if (cliente == null)
                return false;

            cliente.Active = active;
            _context.Entry(cliente).Property(c => c.Active).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
