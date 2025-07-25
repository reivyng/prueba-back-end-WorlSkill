using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Model;

namespace Data.Implements
{
    public class PedidoData : BaseModelData<Pedido>, IPedidoData
    {
        public PedidoData(ApplicationDbContext context): base(context)
        {
        }
        public async Task<bool> UpdatePartial(Pedido pedido)
        {
            var existing = await _dbSet.FindAsync(pedido.Id);
            if (existing == null) return false;

            foreach (var c in typeof(Pedido).GetProperties().Where(x => x.CanWrite && x.Name != "Id"))
            {
                var v = c.GetValue(pedido);
                if (v != null && (!(v is string s) || !string.IsNullOrWhiteSpace(s)))
                    c.SetValue(existing, v);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}