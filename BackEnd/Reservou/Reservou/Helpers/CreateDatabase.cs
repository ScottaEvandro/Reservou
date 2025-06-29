namespace Reservou.Helpers;

public class CreateDatabase
{
    private const string CreateUserTable = $@"CREATE TABLE public.usuarios (
	id serial4 NOT NULL,
	nome varchar(150) NOT NULL,
	cpf bpchar(11) NOT NULL,
	email varchar(150) NOT NULL,
	telefone varchar(20) NULL,
	senha varchar(255) NOT NULL,
	tipo_usuario int4 NOT NULL -- 0 - Usário Normal¶1 - Admintrador,
	criado_em timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT usuarios_cpf_key UNIQUE (cpf),
	CONSTRAINT usuarios_email_key UNIQUE (email),
	CONSTRAINT usuarios_pkey PRIMARY KEY (id),
	CONSTRAINT usuarios_tipo_usuario_check CHECK ((tipo_usuario = ANY (ARRAY[0, 1])))
	);
	COMMENT ON COLUMN public.usuarios.tipo_usuario IS '0 - Usário Normal 1 - Admintrador';";

	private const string CreateSpaceTypesTable = $@"CREATE TABLE public.tipos_espacos (
	id serial4 NOT NULL,
	nome varchar(50) NOT NULL,
	criado_em timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT tipos_espacos_nome_key UNIQUE (nome),
	CONSTRAINT tipos_espacos_pkey PRIMARY KEY (id)
	);";

	private const string CreateSpacesTable = $@"CREATE TABLE public.espacos (
	id serial4 NOT NULL,
	nome varchar(100) NOT NULL,
	descricao text NULL,
	capacidade int4 NOT NULL,
	tipo_id int4 NOT NULL,
	valor_reserva numeric(10, 2) NOT NULL,
	duracao_padrao interval NOT NULL,
	tempo_manutencao interval DEFAULT '00:00:00'::interval NOT NULL,
	ativo bool DEFAULT true NOT NULL,
	criado_em timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT espacos_pkey PRIMARY KEY (id),
	CONSTRAINT fk_tipo_espaco FOREIGN KEY (tipo_id) REFERENCES public.tipos_espacos(id)
	);";

	private const string CreateSpaceImagesTable = $@"CREATE TABLE public.espacos_fotos (
	id serial4 NOT NULL,
	espaco_id int4 NOT NULL,
	url text NOT NULL,
	criado_em timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT espacos_fotos_pkey PRIMARY KEY (id),
	CONSTRAINT fk_espaco_foto FOREIGN KEY (espaco_id) REFERENCES public.espacos(id)
	);";

	private const string CreateDisponibilityTable = $@"CREATE TABLE public.disponibilidades (
	id serial4 NOT NULL,
	espaco_id int4 NOT NULL,
	dia_semana int4 NOT NULL,
	hora_inicio time NOT NULL,
	hora_fim time NOT NULL,
	CONSTRAINT chk_hora_valida CHECK ((hora_fim > hora_inicio)),
	CONSTRAINT disponibilidades_dia_semana_check CHECK (((dia_semana >= 0) AND (dia_semana <= 6))),
	CONSTRAINT disponibilidades_pkey PRIMARY KEY (id),
	CONSTRAINT fk_disponibilidade_espaco FOREIGN KEY (espaco_id) REFERENCES public.espacos(id)
	);";

	private const string CreateReservationsTable = $@"CREATE TABLE public.reservas (
	id serial4 NOT NULL,
	usuario_id int4 NOT NULL,
	espaco_id int4 NOT NULL,
	data_inicio timestamp NOT NULL,
	data_fim timestamp NOT NULL,
	status int4 NOT NULL,
	descricao text NULL,
	criado_em timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT reservas_pkey PRIMARY KEY (id),
	CONSTRAINT reservas_status_check CHECK ((status = ANY (ARRAY[0, 1, 2, 3]))),
	CONSTRAINT fk_reserva_espaco FOREIGN KEY (espaco_id) REFERENCES public.espacos(id),
	CONSTRAINT fk_reserva_usuario FOREIGN KEY (usuario_id) REFERENCES public.usuarios(id)
	);";

	private const string CreateFeedbackTable = $@"CREATE TABLE public.feedback (
	id serial4 NOT NULL,
	espaco_id int4 NOT NULL,
	texto text NOT NULL,
	nota int4 NULL,
	criado_em timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT feedback_nota_check CHECK (((nota >= 1) AND (nota <= 5))),
	CONSTRAINT feedback_pkey PRIMARY KEY (id),
	CONSTRAINT fk_feedback_espaco FOREIGN KEY (espaco_id) REFERENCES public.espacos(id)
	);";

}
