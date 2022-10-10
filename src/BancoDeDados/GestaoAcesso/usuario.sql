CREATE TABLE usuario (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	txt_nome VARCHAR(100) NOT NULL,
	txt_cpf CHAR(11) NOT NULL,
	txt_senha VARCHAR(128) NOT NULL,
	idc_administrador BIT NOT NULL,
	dt_criacao DATETIME DEFAULT getdate(),
	dt_ultima_alteracao_senha DATETIME NOT NULL
	idc_cadastro_aprovado BIT NOT NULL DEFAULT 0
)

CREATE NONCLUSTERED INDEX usuario_I1
	ON usuario(txt_cpf)
	INCLUDE (txt_senha);  
GO