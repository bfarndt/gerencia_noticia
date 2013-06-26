exec spHistorico 'SELECIONAR',null,6,null,null,null,'1,2,3,4,5,6,8',null
		SELECT
			tblHis.IdHistorico,
			tblHis.IdNoticia,
			tblHis.IdUsuario,
			tblHis.IdStatus,
			tblHis.DataHora,
			tblHis.Descricao
		FROM
			tblHistorico tblHis
		WHERE
		(
			tblHis.IdNoticia = 6) and tblHis.IdStatus in (SELECT ID FROM fn_GetTableFromIntList('1,2,3,4,5,6,8',',')
		))


go
ALTER PROCEDURE [dbo].[spHistorico]
	@vchAcao VARCHAR(50),
	@intIdHistorico INT = NULL,
	@intIdNoticia INT = NULL,
	@intIdUsuario INT = NULL,
	@intIdStatus INT = NULL,
	@datDataHora DATETIME = NULL,
	@vchVariosIdStatus VARCHAR(50) = NULL,
	@vchDescricao VARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
	
		Declare @sql VARCHAR(MAX)

		SELECT
			tblHis.IdHistorico,
			tblHis.IdNoticia,
			tblHis.IdUsuario,
			tblHis.IdStatus,
			tblHis.DataHora,
			tblHis.Descricao
		FROM
			tblHistorico tblHis
		WHERE
		(
			((tblHis.IdHistorico = @intIdHistorico) OR (@intIdHistorico IS NULL)) AND
			((tblHis.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblHis.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblHis.IdStatus in (SELECT ID FROM fn_GetTableFromIntList(@vchVariosIdStatus,',')) OR (@vchVariosIdStatus IS NULL)))
		)
		
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblHistorico
					(
						IdNoticia,
						IdUsuario,
						IdStatus,
						DataHora,
						Descricao
					)
					VALUES
					(
						@intIdNoticia,
						@intIdUsuario,
						@intIdStatus,
						@datDataHora,
						@vchDescricao
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END