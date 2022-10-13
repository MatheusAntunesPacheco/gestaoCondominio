DROP TABLE usuarios
CREATE TABLE usuarios (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	txt_nome VARCHAR(100) NOT NULL,
	txt_cpf CHAR(11) NOT NULL,
	txt_senha VARCHAR(128) NOT NULL,
	txt_email VARCHAR(150) NOT NULL,
	idc_cadastro_aprovado BIT NOT NULL DEFAULT 0
)

CREATE NONCLUSTERED INDEX usuario_I1
	ON usuarios(txt_cpf)
	INCLUDE (txt_senha);  
GO