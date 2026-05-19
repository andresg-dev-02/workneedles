using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Tallas
{
    public class GetAllTallas(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<IEnumerable<TallaDto>> Execute()
        {
            var tallas = await unitOfWork.Tallas.GetAllAsync();
            return mapper.Map<IEnumerable<TallaDto>>(tallas);
        }
    }
}