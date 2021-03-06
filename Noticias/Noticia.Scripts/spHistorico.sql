
ALTER
 PROCEDURE [dbo].[spHistorico]
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
		
		set @sql =
		'
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
			((tblHis.IdStatus in ('+ @vchVariosIdStatus +')) OR (@vchVariosIdStatus IS NULL))
		)
		';
		
		Exec(@sql)
		
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