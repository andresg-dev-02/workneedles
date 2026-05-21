using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using AutoMapper;
using Application.DTOs.Client;
using Domain.Specification;

namespace Application.UseCases.Clients
{
    public class CreateCliente(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateClienteDto dto)
        {
            var cliente = Domain.Entities.Cliente.Crear(dto.IdPais, dto.IdDepart, dto.IdCiudad,
                dto.Tipocliente, dto.Tipodocumento, dto.Documento,
                dto.Nombres, dto.Apellidos, dto.Razonsocial,
                dto.Email, dto.Telefono, dto.Direccion, dto.PreferenciasCompra);
            await unitofwork.Clientes.AddAsync(cliente);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllClientes(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ClienteDto>> Execute()
        {
            var options = new QueryOptions<Cliente>()
                .AddInclude("IdpaisNavigation")
                .AddInclude("IddepartNavigation")
                .AddInclude("IdciudadNavigation");
            var clientes = await unitofwork.Clientes.GetAllAsync(options);
            return mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }
    }

    public class GetClienteById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<ClienteDto> Execute(int id)
        {
            var options = new QueryOptions<Cliente>()
                .AddInclude("IdpaisNavigation")
                .AddInclude("IddepartNavigation")
                .AddInclude("IdciudadNavigation");
            var cliente = await unitofwork.Clientes.GetByIdAsync(id, options)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");
            return mapper.Map<ClienteDto>(cliente);
        }
    }

    public class UpdateCliente(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateClienteDto dto)
        {
            var cliente = await unitofwork.Clientes.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");
            cliente.Actualizar(dto.IdPais, dto.IdDepart, dto.IdCiudad,
                dto.Tipocliente, dto.Tipodocumento, dto.Documento,
                dto.Nombres, dto.Apellidos, dto.Razonsocial,
                dto.Email, dto.Telefono, dto.Direccion, dto.PreferenciasCompra);
            unitofwork.Clientes.Update(cliente);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteCliente(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var cliente = await unitofwork.Clientes.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");
            unitofwork.Clientes.Delete(cliente);
            await unitofwork.SaveAsync();
        }
    }
}