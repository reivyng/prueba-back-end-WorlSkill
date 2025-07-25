using Entity.Model.Base;

namespace Entity.Model
{
    public class Pedido : BaseModel
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } // Relación con Cliente

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }     // Relación con Pizza

        public string Estado { get; set; }   // "Pendiente", "Entregado"
        public DateTime Fecha { get; set; }


    }
}
