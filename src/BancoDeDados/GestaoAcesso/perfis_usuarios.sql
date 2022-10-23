IF OBJECT_ID ('dbo.perfis_usuarios') IS NOT NULL
	DROP TABLE dbo.perfis_usuarios
GO

CREATE TABLE dbo.perfis_usuarios
	(
		id     		  INT IDENTITY PRIMARY KEY NOT NULL,
		txt_cpf       CHAR (11) NOT NULL,
		id_condominio INT NULL,
		idc_adm       BIT DEFAULT ((0)) NOT NULL,
		txt_cpf_alteracao CHAR (11) NOT NULL,
		dt_alteracao DATETIME NOT NULL DEFAULT getdate(),
		
		FOREIGN KEY (txt_cpf) REFERENCES usuarios(txt_cpf)
	)



