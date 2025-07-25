using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.PizzaDto;
using Entity.Model;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using ValidationException = Utilities.Exceptions.ValidationException;

namespace Business.Implements
{
    public class PizzaBusiness : BaseBusiness<Pizza, PizzaDto>, IPizzaBusiness
    {
        private readonly IPizzaData _pizzaData;
        private readonly IValidator<PizzaDto> _validator; // Add validator field  

        public PizzaBusiness(IPizzaData pizzaData, IMapper mapper, ILogger<PizzaBusiness> logger)
            : base(pizzaData, mapper, logger) // Pass validator to base constructor  
        {
            _pizzaData = pizzaData;
        }

        public async Task<bool> UpdatePartialAsync(PizzaUpdateDto dto)
        {
            if (dto == null || dto.Id == 0)
                return false;

            var pizza = _mapper.Map<Pizza>(dto);

            return await _pizzaData.UpdatePartial(pizza);
        }

        public async Task<bool> ActiveAsync(PizzaActiveDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del ... es inválido");

            var exists = await _pizzaData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("...", dto.Id);

            return await _pizzaData.ActiveAsync(dto.Id, dto.Active);
        }

    }
}
