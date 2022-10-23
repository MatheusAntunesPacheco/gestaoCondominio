IF OBJECT_ID ('dbo.usuarios') IS NOT NULL
	DROP TABLE dbo.usuarios
GO

CREATE TABLE usuarios (
	txt_cpf CHAR(11) NOT NULL PRIMARY KEY,
	txt_nome VARCHAR(100) NOT NULL,
	txt_senha VARCHAR(128) NOT NULL,
	txt_email VARCHAR(150) NOT NULL
)