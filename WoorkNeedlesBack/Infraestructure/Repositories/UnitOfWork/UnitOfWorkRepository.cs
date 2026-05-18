using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports.Output.UnitOfWork;
using Domain.Ports.Output;
using Infraestructure.Data;
using Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infraestructure.Repositories.UnitOfWork
{
    public class UnitOfWorkRepository(WoorkNeedlesContext context, IMapper mapper) : IUnitOfWork
    {
        public IGenericRepository<Domain.Entities.Usuario> Usuarios { get; } =
            new GenericRepository<Domain.Entities.Usuario>(context);

        public IGenericRepository<Domain.Entities.CategoriaProducto> Categorias { get; } =
            new GenericRepository<Domain.Entities.CategoriaProducto>(context);

        public IGenericRepository<Domain.Entities.Producto> Productos { get; } =
            new GenericRepository<Domain.Entities.Producto>(context);

        public async Task SaveAsync() => await context.SaveChangesAsync();

        public void Dispose() => context.Dispose();
    }
}