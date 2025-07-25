using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.ClienteDto;
using Entity.Model;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using ValidationException = Utilities.Exceptions.ValidationException;

namespace Business.Implements
{
    public class ClienteBusiness : BaseBusiness<Cliente, ClienteDto>, IClienteBusiness
    {
        private readonly IClienteData _clienteData;
        private readonly IValidator<ClienteDto> _validator;

        public ClienteBusiness(IClienteData clienteData, IMapper mapper, ILogger<ClienteBusiness> logger)
            : base(clienteData, mapper, logger)
        {
            _clienteData = clienteData;
        }

        public async Task<bool> UpdatePartialAsync(ClienteUpdateDto dto)
        {
            if (dto == null || dto.Id == 0)
                return false;

            var cliente = _mapper.Map<Cliente>(dto);

            return await _clienteData.UpdatePartial(cliente);
        }

        public async Task<bool> ActiveAsync(ClienteActiveDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del ... es inválido");

            var exists = await _clienteData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("...", dto.Id);

            return await _clienteData.ActiveAsync(dto.Id, dto.Active);
        }
    }
}
