IF OBJECT_ID ('dbo.perfis_usuarios') IS NOT NULL
	DROP TABLE dbo.perfis_usuarios
GO

CREATE TABLE dbo.perfis_usuarios
	(
	if_perfil     INT IDENTITY PRIMARY KEY NOT NULL,
	txt_cpf       CHAR (11) NOT NULL,
	id_condominio INT NULL,
	idc_adm       BIT DEFAULT ((0)) NOT NULL
	)
GO

