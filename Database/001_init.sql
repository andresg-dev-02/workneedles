--
-- PostgreSQL database dump
--

\restrict NWze6qT3zjZrvYWd8Z2eGtcwJUe5SWY63grFkmWOueixI6HrsnQqndLEHG19Get

-- Dumped from database version 18.3
-- Dumped by pg_dump version 18.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: CategoriaInsumo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CategoriaInsumo" (
    id integer NOT NULL,
    nombre character varying(80) NOT NULL,
    descripcion character varying(200) NOT NULL,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."CategoriaInsumo" OWNER TO postgres;

--
-- Name: CategoriaInsumo_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."CategoriaInsumo_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."CategoriaInsumo_id_seq" OWNER TO postgres;

--
-- Name: CategoriaInsumo_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."CategoriaInsumo_id_seq" OWNED BY public."CategoriaInsumo".id;


--
-- Name: CategoriaProducto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CategoriaProducto" (
    id integer NOT NULL,
    nombre character varying(80) NOT NULL,
    descripcion character varying(200) NOT NULL,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."CategoriaProducto" OWNER TO postgres;

--
-- Name: CategoriaProducto_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."CategoriaProducto_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."CategoriaProducto_id_seq" OWNER TO postgres;

--
-- Name: CategoriaProducto_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."CategoriaProducto_id_seq" OWNED BY public."CategoriaProducto".id;


--
-- Name: Ciudades; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Ciudades" (
    id integer NOT NULL,
    iddepart integer NOT NULL,
    nombre character varying NOT NULL,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."Ciudades" OWNER TO postgres;

--
-- Name: Ciudades_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Ciudades_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Ciudades_id_seq" OWNER TO postgres;

--
-- Name: Ciudades_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Ciudades_id_seq" OWNED BY public."Ciudades".id;


--
-- Name: ColoresProducto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ColoresProducto" (
    id integer NOT NULL,
    idproducto integer NOT NULL,
    color character varying(50) NOT NULL,
    codigohex character varying(7)
);


ALTER TABLE public."ColoresProducto" OWNER TO postgres;

--
-- Name: ColoresProducto_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."ColoresProducto_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."ColoresProducto_id_seq" OWNER TO postgres;

--
-- Name: ColoresProducto_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."ColoresProducto_id_seq" OWNED BY public."ColoresProducto".id;


--
-- Name: Departamentos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Departamentos" (
    id integer NOT NULL,
    idpais integer NOT NULL,
    nombre character varying NOT NULL,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."Departamentos" OWNER TO postgres;

--
-- Name: Departamentos_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Departamentos_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Departamentos_id_seq" OWNER TO postgres;

--
-- Name: Departamentos_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Departamentos_id_seq" OWNED BY public."Departamentos".id;


--
-- Name: DetallePedidos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."DetallePedidos" (
    id integer NOT NULL,
    idpedido integer NOT NULL,
    idproducto integer NOT NULL,
    talla character varying(10),
    color character varying(50),
    cantidad integer NOT NULL,
    preciounitario numeric(10,2) NOT NULL,
    subtotal numeric(10,2) NOT NULL,
    CONSTRAINT "DetallePedidos_cantidad_check" CHECK ((cantidad > 0))
);


ALTER TABLE public."DetallePedidos" OWNER TO postgres;

--
-- Name: DetallePedidos_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."DetallePedidos_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."DetallePedidos_id_seq" OWNER TO postgres;

--
-- Name: DetallePedidos_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."DetallePedidos_id_seq" OWNED BY public."DetallePedidos".id;


--
-- Name: Devoluciones; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Devoluciones" (
    id integer NOT NULL,
    idpedido integer NOT NULL,
    idusuario integer NOT NULL,
    motivo text NOT NULL,
    estado character varying(20),
    fecha timestamp without time zone,
    CONSTRAINT "Devoluciones_estado_check" CHECK (((estado)::text = ANY ((ARRAY['solicitada'::character varying, 'aprobada'::character varying, 'rechazada'::character varying])::text[])))
);


ALTER TABLE public."Devoluciones" OWNER TO postgres;

--
-- Name: Devoluciones_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Devoluciones_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Devoluciones_id_seq" OWNER TO postgres;

--
-- Name: Devoluciones_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Devoluciones_id_seq" OWNED BY public."Devoluciones".id;


--
-- Name: HistorialPedidos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."HistorialPedidos" (
    id integer NOT NULL,
    idpedido integer NOT NULL,
    idusuario integer NOT NULL,
    estadoanterior character varying(20),
    estadoactual character varying(20),
    observacion text,
    fecha timestamp without time zone NOT NULL
);


ALTER TABLE public."HistorialPedidos" OWNER TO postgres;

--
-- Name: HistorialPedidos_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."HistorialPedidos_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."HistorialPedidos_id_seq" OWNER TO postgres;

--
-- Name: HistorialPedidos_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."HistorialPedidos_id_seq" OWNED BY public."HistorialPedidos".id;


--
-- Name: Insumos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Insumos" (
    id integer NOT NULL,
    idcategoria integer NOT NULL,
    nombre character varying(100) NOT NULL,
    descripcion character varying(200) NOT NULL,
    unidadmedida character varying(20) NOT NULL,
    stockactual numeric(10,2) DEFAULT 0 NOT NULL,
    stockalerta numeric(10,2) DEFAULT 0 NOT NULL,
    precio numeric(10,2) NOT NULL,
    proveedor character varying(150),
    activo boolean DEFAULT true,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    fechamodificacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT "Insumos_unidadmedida_check" CHECK (((unidadmedida)::text = ANY ((ARRAY['metros'::character varying, 'kg'::character varying, 'unidades'::character varying, 'litros'::character varying, 'rollos'::character varying])::text[])))
);


ALTER TABLE public."Insumos" OWNER TO postgres;

--
-- Name: InsumosProducto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."InsumosProducto" (
    id integer NOT NULL,
    idproducto integer NOT NULL,
    idinsumo integer NOT NULL,
    cantidad numeric(10,2) NOT NULL
);


ALTER TABLE public."InsumosProducto" OWNER TO postgres;

--
-- Name: InsumosProducto_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."InsumosProducto_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."InsumosProducto_id_seq" OWNER TO postgres;

--
-- Name: InsumosProducto_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."InsumosProducto_id_seq" OWNED BY public."InsumosProducto".id;


--
-- Name: Insumos_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Insumos_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Insumos_id_seq" OWNER TO postgres;

--
-- Name: Insumos_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Insumos_id_seq" OWNED BY public."Insumos".id;


--
-- Name: Pagos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Pagos" (
    id integer NOT NULL,
    idpedido integer NOT NULL,
    idusuario integer NOT NULL,
    fechapago timestamp without time zone DEFAULT now(),
    monto numeric(10,2) NOT NULL,
    tipopago character varying(30) NOT NULL,
    estado character varying(20) DEFAULT 'completado'::character varying NOT NULL,
    referencia character varying(100),
    observaciones text,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT "Pagos_estado_check" CHECK (((estado)::text = ANY ((ARRAY['pendiente'::character varying, 'completado'::character varying, 'reembolsado'::character varying, 'fallido'::character varying])::text[]))),
    CONSTRAINT "Pagos_monto_check" CHECK ((monto > (0)::numeric)),
    CONSTRAINT "Pagos_tipopago_check" CHECK (((tipopago)::text = ANY ((ARRAY['efectivo'::character varying, 'transferencia'::character varying, 'tarjeta_credito'::character varying, 'tarjeta_debito'::character varying, 'credito_empresa'::character varying])::text[])))
);


ALTER TABLE public."Pagos" OWNER TO postgres;

--
-- Name: Pagos_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Pagos_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Pagos_id_seq" OWNER TO postgres;

--
-- Name: Pagos_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Pagos_id_seq" OWNED BY public."Pagos".id;


--
-- Name: Paises; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Paises" (
    id integer NOT NULL,
    nombre character varying(150) NOT NULL,
    codigo character varying(2) NOT NULL,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."Paises" OWNER TO postgres;

--
-- Name: Paises_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Paises_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Paises_id_seq" OWNER TO postgres;

--
-- Name: Paises_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Paises_id_seq" OWNED BY public."Paises".id;


--
-- Name: Pedidos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Pedidos" (
    id integer NOT NULL,
    idcliente integer NOT NULL,
    idusuario integer NOT NULL,
    fechapedido timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    fechentregaaprox date NOT NULL,
    fechaentrega date NOT NULL,
    estado character varying(20) DEFAULT 'pendiente'::character varying NOT NULL,
    direccionentrega character varying(200) NOT NULL,
    observaciones text NOT NULL,
    subtotal numeric(10,2) DEFAULT 0 NOT NULL,
    descuento numeric(10,2) DEFAULT 0,
    total numeric(10,2) DEFAULT 0 NOT NULL,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    fechamodificacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT "Pedidos_estado_check" CHECK (((estado)::text = ANY ((ARRAY['pendiente'::character varying, 'en_preparacion'::character varying, 'enviado'::character varying, 'entregado'::character varying, 'cancelado'::character varying])::text[])))
);


ALTER TABLE public."Pedidos" OWNER TO postgres;

--
-- Name: Pedidos_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Pedidos_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Pedidos_id_seq" OWNER TO postgres;

--
-- Name: Pedidos_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Pedidos_id_seq" OWNED BY public."Pedidos".id;


--
-- Name: Producto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Producto" (
    id integer NOT NULL,
    idcategoria integer NOT NULL,
    nombre character varying(100) NOT NULL,
    descripcion text NOT NULL,
    material character varying(100),
    preciobase numeric(10,2) NOT NULL,
    urlimagen character varying(255),
    activo boolean DEFAULT true,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    fechamodificacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."Producto" OWNER TO postgres;

--
-- Name: Producto_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Producto_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Producto_id_seq" OWNER TO postgres;

--
-- Name: Producto_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Producto_id_seq" OWNED BY public."Producto".id;


--
-- Name: Roles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Roles" (
    id integer NOT NULL,
    nombre character varying NOT NULL,
    descripcion character varying NOT NULL,
    permisos jsonb DEFAULT '{}'::jsonb NOT NULL
);


ALTER TABLE public."Roles" OWNER TO postgres;

--
-- Name: Roles_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Roles_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Roles_id_seq" OWNER TO postgres;

--
-- Name: Roles_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Roles_id_seq" OWNED BY public."Roles".id;


--
-- Name: TallasProducto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TallasProducto" (
    id integer NOT NULL,
    idproducto integer NOT NULL,
    talla character varying(10) NOT NULL,
    stock integer DEFAULT 0 NOT NULL,
    CONSTRAINT "TallasProducto_talla_check" CHECK (((talla)::text = ANY ((ARRAY['XS'::character varying, 'S'::character varying, 'M'::character varying, 'L'::character varying, 'XL'::character varying, 'XXL'::character varying])::text[])))
);


ALTER TABLE public."TallasProducto" OWNER TO postgres;

--
-- Name: TallasProducto_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."TallasProducto_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."TallasProducto_id_seq" OWNER TO postgres;

--
-- Name: TallasProducto_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."TallasProducto_id_seq" OWNED BY public."TallasProducto".id;


--
-- Name: Usuario; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Usuario" (
    id integer NOT NULL,
    idrol integer NOT NULL,
    idpais integer NOT NULL,
    idciudad integer NOT NULL,
    nombres character varying(150) NOT NULL,
    apellidos character varying(150) NOT NULL,
    email character varying(120) NOT NULL,
    contrasena character varying(255) NOT NULL,
    telefono character varying(20) NOT NULL,
    activo boolean DEFAULT true,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    fechamodificacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."Usuario" OWNER TO postgres;

--
-- Name: Usuario_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Usuario_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Usuario_id_seq" OWNER TO postgres;

--
-- Name: Usuario_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Usuario_id_seq" OWNED BY public."Usuario".id;


--
-- Name: clientes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clientes (
    id integer NOT NULL,
    idpais integer NOT NULL,
    iddepart integer NOT NULL,
    idciudad integer NOT NULL,
    tipocliente character varying(20) NOT NULL,
    tipodocumento character varying(30) NOT NULL,
    documento character varying(20) NOT NULL,
    nombres character varying(150),
    apellidos character varying(150),
    razonsocial character varying(150),
    email character varying(120) NOT NULL,
    telefono character varying(20) NOT NULL,
    direccion character varying(200) NOT NULL,
    preferencias_compra text,
    activo boolean DEFAULT true,
    fechacreacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    fechamodificacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT clientes_tipocliente_check CHECK (((tipocliente)::text = ANY ((ARRAY['persona'::character varying, 'empresa'::character varying])::text[]))),
    CONSTRAINT clientes_tipodocumento_check CHECK (((tipodocumento)::text = ANY ((ARRAY['cedula'::character varying, 'nit'::character varying, 'cedula_extranjeria'::character varying, 'pasaporte'::character varying])::text[])))
);


ALTER TABLE public.clientes OWNER TO postgres;

--
-- Name: clientes_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.clientes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.clientes_id_seq OWNER TO postgres;

--
-- Name: clientes_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.clientes_id_seq OWNED BY public.clientes.id;


--
-- Name: CategoriaInsumo id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CategoriaInsumo" ALTER COLUMN id SET DEFAULT nextval('public."CategoriaInsumo_id_seq"'::regclass);


--
-- Name: CategoriaProducto id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CategoriaProducto" ALTER COLUMN id SET DEFAULT nextval('public."CategoriaProducto_id_seq"'::regclass);


--
-- Name: Ciudades id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Ciudades" ALTER COLUMN id SET DEFAULT nextval('public."Ciudades_id_seq"'::regclass);


--
-- Name: ColoresProducto id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ColoresProducto" ALTER COLUMN id SET DEFAULT nextval('public."ColoresProducto_id_seq"'::regclass);


--
-- Name: Departamentos id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Departamentos" ALTER COLUMN id SET DEFAULT nextval('public."Departamentos_id_seq"'::regclass);


--
-- Name: DetallePedidos id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallePedidos" ALTER COLUMN id SET DEFAULT nextval('public."DetallePedidos_id_seq"'::regclass);


--
-- Name: Devoluciones id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Devoluciones" ALTER COLUMN id SET DEFAULT nextval('public."Devoluciones_id_seq"'::regclass);


--
-- Name: HistorialPedidos id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HistorialPedidos" ALTER COLUMN id SET DEFAULT nextval('public."HistorialPedidos_id_seq"'::regclass);


--
-- Name: Insumos id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Insumos" ALTER COLUMN id SET DEFAULT nextval('public."Insumos_id_seq"'::regclass);


--
-- Name: InsumosProducto id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InsumosProducto" ALTER COLUMN id SET DEFAULT nextval('public."InsumosProducto_id_seq"'::regclass);


--
-- Name: Pagos id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pagos" ALTER COLUMN id SET DEFAULT nextval('public."Pagos_id_seq"'::regclass);


--
-- Name: Paises id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Paises" ALTER COLUMN id SET DEFAULT nextval('public."Paises_id_seq"'::regclass);


--
-- Name: Pedidos id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pedidos" ALTER COLUMN id SET DEFAULT nextval('public."Pedidos_id_seq"'::regclass);


--
-- Name: Producto id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Producto" ALTER COLUMN id SET DEFAULT nextval('public."Producto_id_seq"'::regclass);


--
-- Name: Roles id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Roles" ALTER COLUMN id SET DEFAULT nextval('public."Roles_id_seq"'::regclass);


--
-- Name: TallasProducto id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TallasProducto" ALTER COLUMN id SET DEFAULT nextval('public."TallasProducto_id_seq"'::regclass);


--
-- Name: Usuario id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuario" ALTER COLUMN id SET DEFAULT nextval('public."Usuario_id_seq"'::regclass);


--
-- Name: clientes id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientes ALTER COLUMN id SET DEFAULT nextval('public.clientes_id_seq'::regclass);


--
-- Data for Name: CategoriaInsumo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."CategoriaInsumo" (id, nombre, descripcion, fechacreacion) FROM stdin;
\.


--
-- Data for Name: CategoriaProducto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."CategoriaProducto" (id, nombre, descripcion, fechacreacion) FROM stdin;
\.


--
-- Data for Name: Ciudades; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Ciudades" (id, iddepart, nombre, fechacreacion) FROM stdin;
1	1	Bello	2026-05-04 17:51:17.689164
\.


--
-- Data for Name: ColoresProducto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ColoresProducto" (id, idproducto, color, codigohex) FROM stdin;
\.


--
-- Data for Name: Departamentos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Departamentos" (id, idpais, nombre, fechacreacion) FROM stdin;
1	1	Antioquia	2026-05-04 17:48:52.730206
\.


--
-- Data for Name: DetallePedidos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."DetallePedidos" (id, idpedido, idproducto, talla, color, cantidad, preciounitario, subtotal) FROM stdin;
\.


--
-- Data for Name: Devoluciones; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Devoluciones" (id, idpedido, idusuario, motivo, estado, fecha) FROM stdin;
\.


--
-- Data for Name: HistorialPedidos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."HistorialPedidos" (id, idpedido, idusuario, estadoanterior, estadoactual, observacion, fecha) FROM stdin;
\.


--
-- Data for Name: Insumos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Insumos" (id, idcategoria, nombre, descripcion, unidadmedida, stockactual, stockalerta, precio, proveedor, activo, fechacreacion, fechamodificacion) FROM stdin;
\.


--
-- Data for Name: InsumosProducto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."InsumosProducto" (id, idproducto, idinsumo, cantidad) FROM stdin;
\.


--
-- Data for Name: Pagos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Pagos" (id, idpedido, idusuario, fechapago, monto, tipopago, estado, referencia, observaciones, fechacreacion) FROM stdin;
\.


--
-- Data for Name: Paises; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Paises" (id, nombre, codigo, fechacreacion) FROM stdin;
1	Colombia	CO	2026-05-04 17:45:19.873851
\.


--
-- Data for Name: Pedidos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Pedidos" (id, idcliente, idusuario, fechapedido, fechentregaaprox, fechaentrega, estado, direccionentrega, observaciones, subtotal, descuento, total, fechacreacion, fechamodificacion) FROM stdin;
\.


--
-- Data for Name: Producto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Producto" (id, idcategoria, nombre, descripcion, material, preciobase, urlimagen, activo, fechacreacion, fechamodificacion) FROM stdin;
\.


--
-- Data for Name: Roles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Roles" (id, nombre, descripcion, permisos) FROM stdin;
1	Administrador	Acceso total al sistema	{"pagos": ["ver", "crear", "editar", "eliminar"], "pedidos": ["ver", "crear", "editar", "eliminar"], "clientes": ["ver", "crear", "editar", "eliminar"], "reportes": ["ver"], "usuarios": ["ver", "crear", "editar", "eliminar"], "productos": ["ver", "crear", "editar", "eliminar"]}
2	Empleado	Acceso operativo general	{"pagos": ["ver", "crear"], "pedidos": ["ver", "crear", "editar"], "clientes": ["ver", "crear", "editar"], "reportes": ["ver"], "usuarios": [], "productos": ["ver", "crear", "editar"]}
\.


--
-- Data for Name: TallasProducto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TallasProducto" (id, idproducto, talla, stock) FROM stdin;
\.


--
-- Data for Name: Usuario; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Usuario" (id, idrol, idpais, idciudad, nombres, apellidos, email, contrasena, telefono, activo, fechacreacion, fechamodificacion) FROM stdin;
1	1	1	1	Andres Felipe	Gonzalez Pineda	andreszf2022@gmail.com	Iumafis2026*.*-	3134684602	t	2026-05-04 17:52:33.507471	2026-05-04 17:52:33.507471
\.


--
-- Data for Name: clientes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.clientes (id, idpais, iddepart, idciudad, tipocliente, tipodocumento, documento, nombres, apellidos, razonsocial, email, telefono, direccion, preferencias_compra, activo, fechacreacion, fechamodificacion) FROM stdin;
\.


--
-- Name: CategoriaInsumo_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."CategoriaInsumo_id_seq"', 1, false);


--
-- Name: CategoriaProducto_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."CategoriaProducto_id_seq"', 1, false);


--
-- Name: Ciudades_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Ciudades_id_seq"', 1, true);


--
-- Name: ColoresProducto_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."ColoresProducto_id_seq"', 1, false);


--
-- Name: Departamentos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Departamentos_id_seq"', 1, true);


--
-- Name: DetallePedidos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."DetallePedidos_id_seq"', 1, false);


--
-- Name: Devoluciones_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Devoluciones_id_seq"', 1, false);


--
-- Name: HistorialPedidos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."HistorialPedidos_id_seq"', 1, false);


--
-- Name: InsumosProducto_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."InsumosProducto_id_seq"', 1, false);


--
-- Name: Insumos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Insumos_id_seq"', 1, false);


--
-- Name: Pagos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Pagos_id_seq"', 1, false);


--
-- Name: Paises_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Paises_id_seq"', 1, true);


--
-- Name: Pedidos_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Pedidos_id_seq"', 1, false);


--
-- Name: Producto_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Producto_id_seq"', 1, false);


--
-- Name: Roles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Roles_id_seq"', 2, true);


--
-- Name: TallasProducto_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."TallasProducto_id_seq"', 1, false);


--
-- Name: Usuario_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Usuario_id_seq"', 1, true);


--
-- Name: clientes_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.clientes_id_seq', 1, false);


--
-- Name: CategoriaInsumo CategoriaInsumo_nombre_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CategoriaInsumo"
    ADD CONSTRAINT "CategoriaInsumo_nombre_key" UNIQUE (nombre);


--
-- Name: CategoriaInsumo CategoriaInsumo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CategoriaInsumo"
    ADD CONSTRAINT "CategoriaInsumo_pkey" PRIMARY KEY (id);


--
-- Name: CategoriaProducto CategoriaProducto_nombre_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CategoriaProducto"
    ADD CONSTRAINT "CategoriaProducto_nombre_key" UNIQUE (nombre);


--
-- Name: CategoriaProducto CategoriaProducto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CategoriaProducto"
    ADD CONSTRAINT "CategoriaProducto_pkey" PRIMARY KEY (id);


--
-- Name: Ciudades Ciudades_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Ciudades"
    ADD CONSTRAINT "Ciudades_pkey" PRIMARY KEY (id);


--
-- Name: ColoresProducto ColoresProducto_idproducto_color_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ColoresProducto"
    ADD CONSTRAINT "ColoresProducto_idproducto_color_key" UNIQUE (idproducto, color);


--
-- Name: ColoresProducto ColoresProducto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ColoresProducto"
    ADD CONSTRAINT "ColoresProducto_pkey" PRIMARY KEY (id);


--
-- Name: Departamentos Departamentos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Departamentos"
    ADD CONSTRAINT "Departamentos_pkey" PRIMARY KEY (id);


--
-- Name: DetallePedidos DetallePedidos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallePedidos"
    ADD CONSTRAINT "DetallePedidos_pkey" PRIMARY KEY (id);


--
-- Name: Devoluciones Devoluciones_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Devoluciones"
    ADD CONSTRAINT "Devoluciones_pkey" PRIMARY KEY (id);


--
-- Name: HistorialPedidos HistorialPedidos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HistorialPedidos"
    ADD CONSTRAINT "HistorialPedidos_pkey" PRIMARY KEY (id);


--
-- Name: InsumosProducto InsumosProducto_idproducto_idinsumo_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InsumosProducto"
    ADD CONSTRAINT "InsumosProducto_idproducto_idinsumo_key" UNIQUE (idproducto, idinsumo);


--
-- Name: InsumosProducto InsumosProducto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InsumosProducto"
    ADD CONSTRAINT "InsumosProducto_pkey" PRIMARY KEY (id);


--
-- Name: Insumos Insumos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Insumos"
    ADD CONSTRAINT "Insumos_pkey" PRIMARY KEY (id);


--
-- Name: Pagos Pagos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pagos"
    ADD CONSTRAINT "Pagos_pkey" PRIMARY KEY (id);


--
-- Name: Paises Paises_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Paises"
    ADD CONSTRAINT "Paises_pkey" PRIMARY KEY (id);


--
-- Name: Pedidos Pedidos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pedidos"
    ADD CONSTRAINT "Pedidos_pkey" PRIMARY KEY (id);


--
-- Name: Producto Producto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Producto"
    ADD CONSTRAINT "Producto_pkey" PRIMARY KEY (id);


--
-- Name: Roles Roles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Roles"
    ADD CONSTRAINT "Roles_pkey" PRIMARY KEY (id);


--
-- Name: TallasProducto TallasProducto_idproducto_talla_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TallasProducto"
    ADD CONSTRAINT "TallasProducto_idproducto_talla_key" UNIQUE (idproducto, talla);


--
-- Name: TallasProducto TallasProducto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TallasProducto"
    ADD CONSTRAINT "TallasProducto_pkey" PRIMARY KEY (id);


--
-- Name: Usuario Usuario_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuario"
    ADD CONSTRAINT "Usuario_email_key" UNIQUE (email);


--
-- Name: Usuario Usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuario"
    ADD CONSTRAINT "Usuario_pkey" PRIMARY KEY (id);


--
-- Name: clientes clientes_documento_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientes
    ADD CONSTRAINT clientes_documento_key UNIQUE (documento);


--
-- Name: clientes clientes_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientes
    ADD CONSTRAINT clientes_email_key UNIQUE (email);


--
-- Name: clientes clientes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientes
    ADD CONSTRAINT clientes_pkey PRIMARY KEY (id);


--
-- Name: Ciudades fk_ciudades_depart; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Ciudades"
    ADD CONSTRAINT fk_ciudades_depart FOREIGN KEY (iddepart) REFERENCES public."Departamentos"(id);


--
-- Name: clientes fk_cliente_ciudad; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientes
    ADD CONSTRAINT fk_cliente_ciudad FOREIGN KEY (idciudad) REFERENCES public."Ciudades"(id);


--
-- Name: clientes fk_cliente_depart; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientes
    ADD CONSTRAINT fk_cliente_depart FOREIGN KEY (iddepart) REFERENCES public."Departamentos"(id);


--
-- Name: clientes fk_cliente_paises; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientes
    ADD CONSTRAINT fk_cliente_paises FOREIGN KEY (idpais) REFERENCES public."Paises"(id);


--
-- Name: ColoresProducto fk_coloresp_producto; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ColoresProducto"
    ADD CONSTRAINT fk_coloresp_producto FOREIGN KEY (idproducto) REFERENCES public."Producto"(id);


--
-- Name: Departamentos fk_depart_paises; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Departamentos"
    ADD CONSTRAINT fk_depart_paises FOREIGN KEY (idpais) REFERENCES public."Paises"(id);


--
-- Name: DetallePedidos fk_detallep_pedido; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallePedidos"
    ADD CONSTRAINT fk_detallep_pedido FOREIGN KEY (idpedido) REFERENCES public."Pedidos"(id);


--
-- Name: DetallePedidos fk_detallep_producto; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallePedidos"
    ADD CONSTRAINT fk_detallep_producto FOREIGN KEY (idproducto) REFERENCES public."Producto"(id);


--
-- Name: Devoluciones fk_devoluciones_pedido; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Devoluciones"
    ADD CONSTRAINT fk_devoluciones_pedido FOREIGN KEY (idpedido) REFERENCES public."Pedidos"(id);


--
-- Name: Devoluciones fk_devoluciones_usuario; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Devoluciones"
    ADD CONSTRAINT fk_devoluciones_usuario FOREIGN KEY (idusuario) REFERENCES public."Usuario"(id);


--
-- Name: HistorialPedidos fk_historialp_pedido; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HistorialPedidos"
    ADD CONSTRAINT fk_historialp_pedido FOREIGN KEY (idpedido) REFERENCES public."Pedidos"(id);


--
-- Name: HistorialPedidos fk_historialp_usuario; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HistorialPedidos"
    ADD CONSTRAINT fk_historialp_usuario FOREIGN KEY (idusuario) REFERENCES public."Usuario"(id);


--
-- Name: Insumos fk_insumo_categoria; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Insumos"
    ADD CONSTRAINT fk_insumo_categoria FOREIGN KEY (idcategoria) REFERENCES public."CategoriaInsumo"(id);


--
-- Name: InsumosProducto fk_insumosp_insumo; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InsumosProducto"
    ADD CONSTRAINT fk_insumosp_insumo FOREIGN KEY (idinsumo) REFERENCES public."Insumos"(id);


--
-- Name: InsumosProducto fk_insumosp_producto; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InsumosProducto"
    ADD CONSTRAINT fk_insumosp_producto FOREIGN KEY (idproducto) REFERENCES public."Producto"(id);


--
-- Name: Pagos fk_pagos_pedido; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pagos"
    ADD CONSTRAINT fk_pagos_pedido FOREIGN KEY (idpedido) REFERENCES public."Pagos"(id);


--
-- Name: Pagos fk_pagos_usuario; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pagos"
    ADD CONSTRAINT fk_pagos_usuario FOREIGN KEY (idusuario) REFERENCES public."Usuario"(id);


--
-- Name: Pedidos fk_pedidos_cliente; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pedidos"
    ADD CONSTRAINT fk_pedidos_cliente FOREIGN KEY (idcliente) REFERENCES public.clientes(id);


--
-- Name: Pedidos fk_pedidos_usuario; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pedidos"
    ADD CONSTRAINT fk_pedidos_usuario FOREIGN KEY (idusuario) REFERENCES public."Usuario"(id);


--
-- Name: Producto fk_prodcuto_categoria; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Producto"
    ADD CONSTRAINT fk_prodcuto_categoria FOREIGN KEY (idcategoria) REFERENCES public."CategoriaProducto"(id);


--
-- Name: TallasProducto fk_tallasp_producto; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TallasProducto"
    ADD CONSTRAINT fk_tallasp_producto FOREIGN KEY (idproducto) REFERENCES public."Producto"(id);


--
-- Name: Usuario fk_usuario_ciudad; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuario"
    ADD CONSTRAINT fk_usuario_ciudad FOREIGN KEY (idciudad) REFERENCES public."Ciudades"(id);


--
-- Name: Usuario fk_usuario_pais; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuario"
    ADD CONSTRAINT fk_usuario_pais FOREIGN KEY (idpais) REFERENCES public."Paises"(id);


--
-- Name: Usuario fk_usuario_rol; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuario"
    ADD CONSTRAINT fk_usuario_rol FOREIGN KEY (idrol) REFERENCES public."Roles"(id);


--
-- PostgreSQL database dump complete
--

\unrestrict NWze6qT3zjZrvYWd8Z2eGtcwJUe5SWY63grFkmWOueixI6HrsnQqndLEHG19Get

