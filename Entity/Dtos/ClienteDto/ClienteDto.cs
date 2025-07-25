using Entity.Dtos.Base;

namespace Entity.Dtos.ClienteDto
{
    public class ClienteDto : GenericDto
    {
        public bool Active { get; set; }
        public string Telefono { get; set; }


    }
}
