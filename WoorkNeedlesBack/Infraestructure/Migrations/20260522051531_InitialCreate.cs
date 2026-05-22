using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaInsumo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("CategoriaInsumo_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaProducto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("CategoriaProducto_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    codigohex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Colores_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    codigo = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Paises_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying", nullable: false),
                    descripcion = table.Column<string>(type: "character varying", nullable: false),
                    permisos = table.Column<string>(type: "jsonb", nullable: false, defaultValueSql: "'{}'::jsonb")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tallas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tallas_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idcategoria = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    unidadmedida = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    stockactual = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    stockalerta = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    precio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    proveedor = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Insumos_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_insumo_categoria",
                        column: x => x.idcategoria,
                        principalTable: "CategoriaInsumo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idcategoria = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    material = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    preciobase = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    urlimagen = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    genero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Producto_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_prodcuto_categoria",
                        column: x => x.idcategoria,
                        principalTable: "CategoriaProducto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpais = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying", nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Departamentos_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_depart_paises",
                        column: x => x.idpais,
                        principalTable: "Paises",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "InsumosProducto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idproducto = table.Column<int>(type: "integer", nullable: false),
                    idinsumo = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InsumosProducto_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_insumosp_insumo",
                        column: x => x.idinsumo,
                        principalTable: "Insumos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_insumosp_producto",
                        column: x => x.idproducto,
                        principalTable: "Producto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idproducto = table.Column<int>(type: "integer", nullable: true),
                    idcolor = table.Column<int>(type: "integer", nullable: true),
                    idtalla = table.Column<int>(type: "integer", nullable: true),
                    stock = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Inventario_pkey", x => x.id);
                    table.ForeignKey(
                        name: "Inventario_idcolor_fkey",
                        column: x => x.idcolor,
                        principalTable: "Colores",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "Inventario_idproducto_fkey",
                        column: x => x.idproducto,
                        principalTable: "Producto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "Inventario_idtalla_fkey",
                        column: x => x.idtalla,
                        principalTable: "Tallas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProductoColores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idproducto = table.Column<int>(type: "integer", nullable: true),
                    idcolor = table.Column<int>(type: "integer", nullable: true),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductoColores_pkey", x => x.id);
                    table.ForeignKey(
                        name: "ProductoColores_idcolor_fkey",
                        column: x => x.idcolor,
                        principalTable: "Colores",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ProductoColores_idproducto_fkey",
                        column: x => x.idproducto,
                        principalTable: "Producto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProductoTallas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idproducto = table.Column<int>(type: "integer", nullable: true),
                    idtalla = table.Column<int>(type: "integer", nullable: true),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductoTallas_pkey", x => x.id);
                    table.ForeignKey(
                        name: "ProductoTallas_idproducto_fkey",
                        column: x => x.idproducto,
                        principalTable: "Producto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ProductoTallas_idtalla_fkey",
                        column: x => x.idtalla,
                        principalTable: "Tallas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iddepart = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying", nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Ciudades_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_ciudades_depart",
                        column: x => x.iddepart,
                        principalTable: "Departamentos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpais = table.Column<int>(type: "integer", nullable: false),
                    iddepart = table.Column<int>(type: "integer", nullable: false),
                    idciudad = table.Column<int>(type: "integer", nullable: false),
                    tipocliente = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    tipodocumento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    nombres = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    apellidos = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    razonsocial = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    direccion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    preferencias_compra = table.Column<string>(type: "text", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("clientes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_cliente_ciudad",
                        column: x => x.idciudad,
                        principalTable: "Ciudades",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cliente_depart",
                        column: x => x.iddepart,
                        principalTable: "Departamentos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cliente_paises",
                        column: x => x.idpais,
                        principalTable: "Paises",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idrol = table.Column<int>(type: "integer", nullable: false),
                    idpais = table.Column<int>(type: "integer", nullable: false),
                    idciudad = table.Column<int>(type: "integer", nullable: false),
                    nombres = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    contrasena = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Usuario_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_ciudad",
                        column: x => x.idciudad,
                        principalTable: "Ciudades",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_usuario_pais",
                        column: x => x.idpais,
                        principalTable: "Paises",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_usuario_rol",
                        column: x => x.idrol,
                        principalTable: "Roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idcliente = table.Column<int>(type: "integer", nullable: false),
                    idusuario = table.Column<int>(type: "integer", nullable: false),
                    fechapedido = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechentregaaprox = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaentrega = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'pendiente'::character varying"),
                    direccionentrega = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    observaciones = table.Column<string>(type: "text", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    descuento = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValueSql: "0"),
                    total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fechamodificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pedidos_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedidos_cliente",
                        column: x => x.idcliente,
                        principalTable: "clientes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pedidos_usuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpedido = table.Column<int>(type: "integer", nullable: false),
                    idproducto = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    preciounitario = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    idinventario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DetallePedidos_pkey", x => x.id);
                    table.ForeignKey(
                        name: "DetallePedidos_idinventario_fkey",
                        column: x => x.idinventario,
                        principalTable: "Inventario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_detallep_pedido",
                        column: x => x.idpedido,
                        principalTable: "Pedidos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_detallep_producto",
                        column: x => x.idproducto,
                        principalTable: "Producto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpedido = table.Column<int>(type: "integer", nullable: false),
                    idusuario = table.Column<int>(type: "integer", nullable: false),
                    motivo = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    idcliente = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Devoluciones_pkey", x => x.id);
                    table.ForeignKey(
                        name: "Devoluciones_idcliente_fkey",
                        column: x => x.idcliente,
                        principalTable: "clientes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_devoluciones_pedido",
                        column: x => x.idpedido,
                        principalTable: "Pedidos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_devoluciones_usuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "HistorialPedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpedido = table.Column<int>(type: "integer", nullable: false),
                    idusuario = table.Column<int>(type: "integer", nullable: false),
                    estadoanterior = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    estadoactual = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    observacion = table.Column<string>(type: "text", nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HistorialPedidos_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_historialp_pedido",
                        column: x => x.idpedido,
                        principalTable: "Pedidos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_historialp_usuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpedido = table.Column<int>(type: "integer", nullable: false),
                    idusuario = table.Column<int>(type: "integer", nullable: false),
                    fechapago = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    monto = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    tipopago = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'completado'::character varying"),
                    referencia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    fechacreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pagos_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_pagos_pedido",
                        column: x => x.idpedido,
                        principalTable: "Pedidos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pagos_usuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "CategoriaInsumo_nombre_key",
                table: "CategoriaInsumo",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CategoriaProducto_nombre_key",
                table: "CategoriaProducto",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_iddepart",
                table: "Ciudades",
                column: "iddepart");

            migrationBuilder.CreateIndex(
                name: "clientes_documento_key",
                table: "clientes",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clientes_email_key",
                table: "clientes",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clientes_idciudad",
                table: "clientes",
                column: "idciudad");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_iddepart",
                table: "clientes",
                column: "iddepart");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_idpais",
                table: "clientes",
                column: "idpais");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_idpais",
                table: "Departamentos",
                column: "idpais");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_idinventario",
                table: "DetallePedidos",
                column: "idinventario");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_idpedido",
                table: "DetallePedidos",
                column: "idpedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_idproducto",
                table: "DetallePedidos",
                column: "idproducto");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_idcliente",
                table: "Devoluciones",
                column: "idcliente");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_idpedido",
                table: "Devoluciones",
                column: "idpedido");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_idusuario",
                table: "Devoluciones",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPedidos_idpedido",
                table: "HistorialPedidos",
                column: "idpedido");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPedidos_idusuario",
                table: "HistorialPedidos",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_Insumos_idcategoria",
                table: "Insumos",
                column: "idcategoria");

            migrationBuilder.CreateIndex(
                name: "InsumosProducto_idproducto_idinsumo_key",
                table: "InsumosProducto",
                columns: new[] { "idproducto", "idinsumo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsumosProducto_idinsumo",
                table: "InsumosProducto",
                column: "idinsumo");

            migrationBuilder.CreateIndex(
                name: "Inventario_idproducto_idcolor_idtalla_key",
                table: "Inventario",
                columns: new[] { "idproducto", "idcolor", "idtalla" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_idcolor",
                table: "Inventario",
                column: "idcolor");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_idtalla",
                table: "Inventario",
                column: "idtalla");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_idpedido",
                table: "Pagos",
                column: "idpedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_idusuario",
                table: "Pagos",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_idcliente",
                table: "Pedidos",
                column: "idcliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_idusuario",
                table: "Pedidos",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_idcategoria",
                table: "Producto",
                column: "idcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoColores_idcolor",
                table: "ProductoColores",
                column: "idcolor");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoColores_idproducto",
                table: "ProductoColores",
                column: "idproducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoTallas_idproducto",
                table: "ProductoTallas",
                column: "idproducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoTallas_idtalla",
                table: "ProductoTallas",
                column: "idtalla");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idciudad",
                table: "Usuario",
                column: "idciudad");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idpais",
                table: "Usuario",
                column: "idpais");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idrol",
                table: "Usuario",
                column: "idrol");

            migrationBuilder.CreateIndex(
                name: "Usuario_email_key",
                table: "Usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePedidos");

            migrationBuilder.DropTable(
                name: "Devoluciones");

            migrationBuilder.DropTable(
                name: "HistorialPedidos");

            migrationBuilder.DropTable(
                name: "InsumosProducto");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "ProductoColores");

            migrationBuilder.DropTable(
                name: "ProductoTallas");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Tallas");

            migrationBuilder.DropTable(
                name: "CategoriaInsumo");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "CategoriaProducto");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
