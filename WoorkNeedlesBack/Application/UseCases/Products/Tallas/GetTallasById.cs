using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Tallas
{
    public class GetTallaById(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<TallaDto> Execute(int id)
        {
            var talla = await unitOfWork.Tallas.GetByIdAsync(id) ?? throw new KeyNotFoundException("Talla no encontrada.");
            return mapper.Map<TallaDto>(talla);
        }
    }

    public class CreateTalla(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<TallaDto> Execute(CreateTallaDto dto)
        {
            var talla = mapper.Map<Domain.Entities.Talla>(dto);
            await unitOfWork.Tallas.AddAsync(talla);
            await unitOfWork.SaveAsync();
            return mapper.Map<TallaDto>(talla);
        }
    }
}