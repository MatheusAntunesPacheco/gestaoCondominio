IF OBJECT_ID ('agendamentos') IS NOT NULL
	DROP TABLE dbo.agendamentos
GO

CREATE TABLE dbo.agendamentos
	(
	id     		  		INT IDENTITY PRIMARY KEY NOT NULL,
	txt_cpf       		CHAR (11) NOT NULL,
	id_condominio 		INT NOT NULL,
	id_area_condominio 	INT NOT NULL,
	dt_evento			DATETIME NOT NULL,
	txt_cpf_alteracao 	CHAR (11) NOT NULL,
	dt_alteracao 		DATETIME NOT NULL DEFAULT getdate()
	)
GO

