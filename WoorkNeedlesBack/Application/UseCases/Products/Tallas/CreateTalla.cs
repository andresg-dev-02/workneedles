using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Tallas
{
    public class CreateTalla(IUnitOfWork unitOfWork)
    {
        public async Task Execute(CreateTallaDto tallaCreardto)
        {
            var talla = Talla.Crear(tallaCreardto.Nombre);
            await unitOfWork.Tallas.AddAsync(talla);
            await unitOfWork.SaveAsync();
        }
    }
    public class GetAllTallas(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<IEnumerable<TallaDto>> Execute()
        {
            var tallas = await unitOfWork.Tallas.GetAllAsync();
            return mapper.Map<IEnumerable<TallaDto>>(tallas);
        }
    }

    public class GetTallaById(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<TallaDto> Execute(int id)
        {
            var talla = await unitOfWork.Tallas.GetByIdAsync(id) ?? throw new KeyNotFoundException("Talla no encontrada.");
            return mapper.Map<TallaDto>(talla);
        }
    }

    public class UpdateTalla(IUnitOfWork unitOfWork)
    {
        public async Task Execute(int id, UpdateTallaDto tallaActualizardto)
        {
            var talla = await unitOfWork.Tallas.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Talla no encontrada.");
            talla.Actualizar(tallaActualizardto.Nombre);
            unitOfWork.Tallas.Update(talla);
            await unitOfWork.SaveAsync();
        }
    }

    public class DeleteTalla(IUnitOfWork unitOfWork)
    {
        public async Task Execute(int id)
        {
            var talla = await unitOfWork.Tallas.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Talla no encontrada.");
            unitOfWork.Tallas.Delete(talla);
            await unitOfWork.SaveAsync();
        }
    }
}

