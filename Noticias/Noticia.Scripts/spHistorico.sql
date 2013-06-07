select * from tblHistorico
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblHistorico'
go

CREATE PROCEDURE [dbo].[spHistorico]
	@vchAcao VARCHAR(50),
	@intIdHistorico INT = NULL,
	@intIdNoticia INT = NULL,
	@intIdUsuario INT = NULL,
	@intIdStatus INT = NULL,
	@datDataHora DATETIME = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblHis.IdHistorico,
			tblHis.IdNoticia,
			tblHis.IdUsuario,
			tblHis.IdStatus,
			tblHis.DataHora
		FROM
			tblHistorico tblHis
		WHERE
		(
			((tblHis.IdHistorico = @intIdHistorico) OR (@intIdHistorico IS NULL)) AND
			((tblHis.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblHis.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblHis.IdStatus = @intIdStatus) OR (@intIdStatus IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblHistorico
					(
						IdNoticia,
						IdUsuario,
						IdStatus,
						DataHora
					)
					VALUES
					(
						@intIdNoticia,
						@intIdUsuario,
						@intIdStatus,
						@datDataHora
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
