using Entity.Dtos.Base;

namespace Entity.Dtos.PedidoDto
{
    public class PedidoDto : BaseDto
    {
        public int ClienteId { get; set; }
        public int PizzaId { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }

    }
}
