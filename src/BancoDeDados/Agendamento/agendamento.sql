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
		id_status			INT NOT NULL,
		dt_alteracao 		DATETIME NOT NULL DEFAULT getdate(),
		txt_cpf_alteracao	CHAR(11) NOT NULL,

		FOREIGN KEY (id_status) REFERENCES status_agendamento(id)
	)
GO

CREATE TRIGGER agendamentos_TRI ON agendamentos
    FOR INSERT
    AS
    BEGIN
       INSERT INTO auditoria_agendamentos (id_agendamento, id_status, dt_evento, txt_cpf_alteracao, dt_alteracao)
       		SELECT i.id, i.id_status, i.dt_evento, i.txt_cpf_alteracao, i.dt_alteracao
          FROM INSERTED AS i
    END
GO

CREATE TRIGGER agendamentos_TRU ON agendamentos
	FOR UPDATE
	AS
	BEGIN
		INSERT INTO auditoria_agendamentos (id_agendamento, id_status, dt_evento, txt_cpf_alteracao, dt_alteracao)
       		SELECT i.id, i.id_status, i.dt_evento, i.txt_cpf_alteracao, i.dt_alteracao
          FROM INSERTED AS i
	END
GO