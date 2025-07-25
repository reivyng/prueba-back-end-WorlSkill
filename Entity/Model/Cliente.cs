using Entity.Model.Base;

namespace Entity.Model
{
    public class Cliente : GenericBase
    {
        public string Telefono { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } // Relación uno-a-muchos

    }
}
