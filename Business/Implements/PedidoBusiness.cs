using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.PedidoDto;
using Entity.Model;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public class PedidoBusiness : BaseBusiness<Pedido, PedidoDto>, IPedidoBusiness
    {
        private readonly IPedidoData _pivoteData;
        private readonly IValidator<PedidoDto> _validator;

        public PedidoBusiness(IPedidoData pivoteData,  IMapper mapper, ILogger<PedidoBusiness> logger)
            : base(pivoteData , mapper, logger)
        {
            _pivoteData = pivoteData;
        }

        public async Task<bool> UpdatePartialAsync(PedidoDto dto)
        {
            if (dto == null || dto.Id == 0)
                return false;

            var pivote = _mapper.Map<Pedido>(dto);

            return await _pivoteData.UpdatePartial(pivote);
        }
    }
}
