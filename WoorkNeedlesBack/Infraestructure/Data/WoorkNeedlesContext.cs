using System;
using System.Collections.Generic;
using Infraestructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data;

public partial class WoorkNeedlesContext : DbContext
{
    public WoorkNeedlesContext()
    {
    }

    public WoorkNeedlesContext(DbContextOptions<WoorkNeedlesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaInsumo> CategoriaInsumos { get; set; }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Colore> Colores { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Devolucione> Devoluciones { get; set; }

    public virtual DbSet<HistorialPedido> HistorialPedidos { get; set; }

    public virtual DbSet<Insumo> Insumos { get; set; }

    public virtual DbSet<InsumosProducto> InsumosProductos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Paise> Paises { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoColore> ProductoColores { get; set; }

    public virtual DbSet<ProductoTalla> ProductoTallas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Talla> Tallas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=aws-1-us-east-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.wzcolhczbqulloczkzjm;Password=andrewsmsSoft7_.05;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<CategoriaInsumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CategoriaInsumo_pkey");

            entity.ToTable("CategoriaInsumo");

            entity.HasIndex(e => e.Nombre, "CategoriaInsumo_nombre_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CategoriaProducto_pkey");

            entity.ToTable("CategoriaProducto");

            entity.HasIndex(e => e.Nombre, "CategoriaProducto_nombre_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ciudades_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Iddepart).HasColumnName("iddepart");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");

            entity.HasOne(d => d.IddepartNavigation).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.Iddepart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ciudades_depart");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.Documento, "clientes_documento_key").IsUnique();

            entity.HasIndex(e => e.Email, "clientes_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(150)
                .HasColumnName("apellidos");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .HasColumnName("documento");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Idciudad).HasColumnName("idciudad");
            entity.Property(e => e.Iddepart).HasColumnName("iddepart");
            entity.Property(e => e.Idpais).HasColumnName("idpais");
            entity.Property(e => e.Nombres)
                .HasMaxLength(150)
                .HasColumnName("nombres");
            entity.Property(e => e.PreferenciasCompra).HasColumnName("preferencias_compra");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(150)
                .HasColumnName("razonsocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.Tipocliente)
                .HasMaxLength(20)
                .HasColumnName("tipocliente");
            entity.Property(e => e.Tipodocumento)
                .HasMaxLength(30)
                .HasColumnName("tipodocumento");

            entity.HasOne(d => d.IdciudadNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Idciudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente_ciudad");

            entity.HasOne(d => d.IddepartNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Iddepart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente_depart");

            entity.HasOne(d => d.IdpaisNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Idpais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente_paises");
        });

        modelBuilder.Entity<Colore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Colores_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigohex)
                .HasMaxLength(7)
                .HasColumnName("codigohex");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Departamentos_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Idpais).HasColumnName("idpais");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdpaisNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.Idpais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_depart_paises");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DetallePedidos_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Idinventario).HasColumnName("idinventario");
            entity.Property(e => e.Idpedido).HasColumnName("idpedido");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Preciounitario)
                .HasPrecision(10, 2)
                .HasColumnName("preciounitario");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdinventarioNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.Idinventario)
                .HasConstraintName("DetallePedidos_idinventario_fkey");

            entity.HasOne(d => d.IdpedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.Idpedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detallep_pedido");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detallep_producto");
        });

        modelBuilder.Entity<Devolucione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Devoluciones_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Idpedido).HasColumnName("idpedido");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Motivo).HasColumnName("motivo");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.Idcliente)
                .HasConstraintName("Devoluciones_idcliente_fkey");

            entity.HasOne(d => d.IdpedidoNavigation).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.Idpedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_devoluciones_pedido");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_devoluciones_usuario");
        });

        modelBuilder.Entity<HistorialPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HistorialPedidos_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estadoactual)
                .HasMaxLength(20)
                .HasColumnName("estadoactual");
            entity.Property(e => e.Estadoanterior)
                .HasMaxLength(20)
                .HasColumnName("estadoanterior");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Idpedido).HasColumnName("idpedido");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Observacion).HasColumnName("observacion");

            entity.HasOne(d => d.IdpedidoNavigation).WithMany(p => p.HistorialPedidos)
                .HasForeignKey(d => d.Idpedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_historialp_pedido");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.HistorialPedidos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_historialp_usuario");
        });

        modelBuilder.Entity<Insumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Insumos_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(150)
                .HasColumnName("proveedor");
            entity.Property(e => e.Stockactual)
                .HasPrecision(10, 2)
                .HasColumnName("stockactual");
            entity.Property(e => e.Stockalerta)
                .HasPrecision(10, 2)
                .HasColumnName("stockalerta");
            entity.Property(e => e.Unidadmedida)
                .HasMaxLength(20)
                .HasColumnName("unidadmedida");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Insumos)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_insumo_categoria");
        });

        modelBuilder.Entity<InsumosProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InsumosProducto_pkey");

            entity.ToTable("InsumosProducto");

            entity.HasIndex(e => new { e.Idproducto, e.Idinsumo }, "InsumosProducto_idproducto_idinsumo_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasPrecision(10, 2)
                .HasColumnName("cantidad");
            entity.Property(e => e.Idinsumo).HasColumnName("idinsumo");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");

            entity.HasOne(d => d.IdinsumoNavigation).WithMany(p => p.InsumosProductos)
                .HasForeignKey(d => d.Idinsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_insumosp_insumo");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.InsumosProductos)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_insumosp_producto");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Inventario_pkey");

            entity.ToTable("Inventario");

            entity.HasIndex(e => new { e.Idproducto, e.Idcolor, e.Idtalla }, "Inventario_idproducto_idcolor_idtalla_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Idcolor).HasColumnName("idcolor");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Idtalla).HasColumnName("idtalla");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");

            entity.HasOne(d => d.IdcolorNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Idcolor)
                .HasConstraintName("Inventario_idcolor_fkey");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("Inventario_idproducto_fkey");

            entity.HasOne(d => d.IdtallaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Idtalla)
                .HasConstraintName("Inventario_idtalla_fkey");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pagos_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'completado'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechapago)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechapago");
            entity.Property(e => e.Idpedido).HasColumnName("idpedido");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .HasColumnName("referencia");
            entity.Property(e => e.Tipopago)
                .HasMaxLength(30)
                .HasColumnName("tipopago");

            entity.HasOne(d => d.IdpedidoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.Idpedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pagos_pedido");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pagos_usuario");
        });

        modelBuilder.Entity<Paise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Paises_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(2)
                .HasColumnName("codigo");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pedidos_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descuento)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("descuento");
            entity.Property(e => e.Direccionentrega)
                .HasMaxLength(200)
                .HasColumnName("direccionentrega");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechaentrega).HasColumnName("fechaentrega");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Fechapedido)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechapedido");
            entity.Property(e => e.Fechentregaaprox).HasColumnName("fechentregaaprox");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Tokenconfirmacion)
                .HasMaxLength(50)
                .HasColumnName("tokenconfirmacion");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Idcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pedidos_cliente");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pedidos_usuario");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Producto_pkey");

            entity.ToTable("Producto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Genero)
                .HasMaxLength(10)
                .HasColumnName("genero");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Material)
                .HasMaxLength(100)
                .HasColumnName("material");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Preciobase)
                .HasPrecision(10, 2)
                .HasColumnName("preciobase");
            entity.Property(e => e.Urlimagen)
                .HasMaxLength(255)
                .HasColumnName("urlimagen");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_prodcuto_categoria");
        });

        modelBuilder.Entity<ProductoColore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductoColores_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Idcolor).HasColumnName("idcolor");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");

            entity.HasOne(d => d.IdcolorNavigation).WithMany(p => p.ProductoColores)
                .HasForeignKey(d => d.Idcolor)
                .HasConstraintName("ProductoColores_idcolor_fkey");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.ProductoColores)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("ProductoColores_idproducto_fkey");
        });

        modelBuilder.Entity<ProductoTalla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductoTallas_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Idtalla).HasColumnName("idtalla");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.ProductoTallas)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("ProductoTallas_idproducto_fkey");

            entity.HasOne(d => d.IdtallaNavigation).WithMany(p => p.ProductoTallas)
                .HasForeignKey(d => d.Idtalla)
                .HasConstraintName("ProductoTallas_idtalla_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Permisos)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("permisos");
        });

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tallas_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Usuario_pkey");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "Usuario_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(150)
                .HasColumnName("apellidos");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Fechamodificacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamodificacion");
            entity.Property(e => e.Idciudad).HasColumnName("idciudad");
            entity.Property(e => e.Idpais).HasColumnName("idpais");
            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Nombres)
                .HasMaxLength(150)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdciudadNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idciudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_ciudad");

            entity.HasOne(d => d.IdpaisNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idpais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_pais");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idrol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
