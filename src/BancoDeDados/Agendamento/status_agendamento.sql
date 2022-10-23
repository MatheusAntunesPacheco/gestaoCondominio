IF OBJECT_ID ('status_agendamento') IS NOT NULL
	DROP TABLE status_agendamento
GO

CREATE TABLE status_agendamento
	(
		id     		  		INT PRIMARY KEY NOT NULL,
		txt_descricao		VARCHAR(20) NOT NULL
	)
GO

INSERT INTO status_agendamento(id, txt_descricao) VALUES (1, 'Agendado')
INSERT INTO status_agendamento(id, txt_descricao) VALUES (2, 'Cancelado')