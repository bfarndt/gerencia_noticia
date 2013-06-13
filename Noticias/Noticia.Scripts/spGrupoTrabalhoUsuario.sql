select * from tblGrupoTrabalhoUsuario
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblGrupoTrabalhoUsuario'
go

CREATE PROCEDURE [dbo].[spGrupoTrabalhoUsuario]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@intIdGrupoTrabalho INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTra.IdUsuario,
			tblTra.IdGrupoTrabalho
		FROM
			tblGrupoTrabalhoUsuario tblTra
		WHERE
		(
			((tblTra.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblTra.IdGrupoTrabalho = @intIdGrupoTrabalho) OR (@intIdGrupoTrabalho IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblGrupoTrabalhoUsuario
					(
						IdUsuario,
						IdGrupoTrabalho
					)
					VALUES
					(
						@intIdUsuario,
						@intIdGrupoTrabalho
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblGrupoTrabalhoUsuario 
		WHERE
			IdUsuario = @intIdUsuario AND
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
