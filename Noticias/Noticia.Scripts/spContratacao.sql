select * from tblContratacao
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblContratacao'
go

ALTER PROCEDURE [dbo].[spContratacao]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@datDataHora DATETIME = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblCon.IdUsuario,
			tblCon.DataHora
		FROM
			tblContratacao tblCon
		WHERE
		(
			((tblCon.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblContratacao
					(
						IdUsuario,
						DataHora
					)
					VALUES
					(
						@intIdUsuario,
						@datDataHora
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblContratacao
		SET
			DataHora = @datDataHora
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblContratacao 
		WHERE 
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN

		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
