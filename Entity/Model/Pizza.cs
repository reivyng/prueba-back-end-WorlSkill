using Entity.Model.Base;

namespace Entity.Model
{
    public class Pizza : GenericBase
    {
        public decimal Precio { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } // Relación uno-a-muchos

    }
}
