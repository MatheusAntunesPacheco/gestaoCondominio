IF OBJECT_ID ('auditoria_agendamentos') IS NOT NULL
	DROP TABLE auditoria_agendamentos
GO

CREATE TABLE auditoria_agendamentos
	(
		id     		  		INT IDENTITY PRIMARY KEY NOT NULL,
		id_agendamento		INT NOT NULL,
		id_status			INT NOT NULL,
		dt_evento			DATETIME NOT NULL,
		txt_cpf_alteracao 	CHAR (11) NOT NULL,
		dt_alteracao 		DATETIME NOT NULL DEFAULT getdate(),

		FOREIGN KEY (id_agendamento) REFERENCES agendamentos(id),
		FOREIGN KEY (id_status) REFERENCES status_agendamento(id)
	)
GO