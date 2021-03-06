
ALTER PROCEDURE [dbo].[spNoticiaGrupoTrabalho]
	@vchAcao VARCHAR(50),
	@intIdNoticia INT = NULL,
	@intIdGrupoTrabalho INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTra.IdNoticia,
			tblTra.IdGrupoTrabalho
		FROM
			tblNoticiaGrupoTrabalho tblTra
		WHERE
		(
			((tblTra.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblTra.IdGrupoTrabalho = @intIdGrupoTrabalho) OR (@intIdGrupoTrabalho IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblNoticiaGrupoTrabalho WHERE IdGrupoTrabalho = @intIdGrupoTrabalho AND IdNoticia = @intIdNoticia)
		BEGIN
	
			INSERT INTO tblNoticiaGrupoTrabalho
						(
							IdNoticia,
							IdGrupoTrabalho
						)
						VALUES
						(
							@intIdNoticia,
							@intIdGrupoTrabalho
						)
		END
					
		SELECT @intIdNoticia AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblNoticiaGrupoTrabalho 
		WHERE
			IdNoticia = @intIdNoticia AND
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdNoticia AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END