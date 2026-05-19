using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports.Output.UnitOfWork;
using Domain.Ports.Output;
using Infraestructure.Data;
using Infraestructure.Repositories.Generic;
using Application.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infraestructure.Repositories.UnitOfWork
{
    public class UnitOfWorkRepository(WoorkNeedlesContext context, IMapper mapper) : IUnitOfWork
    {
        public IGenericRepository<Domain.Entities.Usuario> Usuarios { get; } = 
            new GenericRepository<Domain.Entities.Usuario, Infraestructure.Persistence.Models.Usuario>(context, mapper);

        public IGenericRepository<Domain.Entities.CategoriaProducto> Categorias { get; } =
            new GenericRepository<Domain.Entities.CategoriaProducto,
                                Infraestructure.Persistence.Models.CategoriaProducto>(context, mapper);

        public IGenericRepository<Domain.Entities.Producto> Productos { get; } =
            new GenericRepository<Domain.Entities.Producto,
                                Infraestructure.Persistence.Models.Producto>(context, mapper);

        public IGenericRepository<Domain.Entities.Colore> Colores { get; } =
            new GenericRepository<Domain.Entities.Colore,
                                Infraestructure.Persistence.Models.Colore>(context, mapper);

        public IGenericRepository<Domain.Entities.Talla> Tallas { get; } =
            new GenericRepository<Domain.Entities.Talla,
                                Infraestructure.Persistence.Models.Talla>(context, mapper);

        public IGenericRepository<Domain.Entities.ProductoColore> ProductoColores { get; } =
            new GenericRepository<Domain.Entities.ProductoColore,
                                Infraestructure.Persistence.Models.ProductoColore>(context, mapper);

        public IGenericRepository<Domain.Entities.ProductoTalla> ProductoTallas { get; } =
            new GenericRepository<Domain.Entities.ProductoTalla,
                                Infraestructure.Persistence.Models.ProductoTalla>(context, mapper);

        public async Task SaveAsync() => await context.SaveChangesAsync();
        public void Dispose() => context.Dispose();
    }
}