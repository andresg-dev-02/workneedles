using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Colores
{
    public class CreateColor(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateColorDto createColorDto)
        {
            var color = Colore.Crear(createColorDto.Nombre, createColorDto.Codigohex);
            await unitofwork.Colores.AddAsync(color);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllColores(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ColorDto>> Execute()
        {
            var colores = await unitofwork.Colores.GetAllAsync();
            return mapper.Map<IEnumerable<ColorDto>>(colores);
        }
    }

    public class GetColoreById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<ColorDto> Execute(int id)
        {
            var color = await unitofwork.Colores.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Color no encontrado.");
            return mapper.Map<ColorDto>(color);
        }
    }

    public class UpdateColore(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateColorDto colorActualizardto)
        {
            var color = await unitofwork.Colores.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Color no encontrado.");
            color.Actualizar(colorActualizardto.Nombre, colorActualizardto.Codigohex);
            unitofwork.Colores.Update(color);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteColore(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var color = await unitofwork.Colores.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Color no encontrado.");
            unitofwork.Colores.Delete(color);
            await unitofwork.SaveAsync();
        }
    }
}