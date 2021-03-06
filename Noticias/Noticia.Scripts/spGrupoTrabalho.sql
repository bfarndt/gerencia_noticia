select * from tblGrupoTrabalho
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblGrupoTrabalho'
GO


select * from tblGrupoTrabalhoUsuario
select * from tblGrupoTrabalho

	

ALTER PROCEDURE [dbo].[spGrupoTrabalho]
	@vchAcao VARCHAR(50),
	@intIdGrupoTrabalho INT = NULL,
	@vchDescricao VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblGru.IdGrupoTrabalho,
			tblGru.Descricao
		FROM
			tblGrupoTrabalho tblGru
		WHERE
		(
			((tblGru.IdGrupoTrabalho = @intIdGrupoTrabalho) OR (@intIdGrupoTrabalho IS NULL)) AND
			((lower(tblGru.Descricao) LIKE '%' + lower(@vchDescricao) + '%') OR (@vchDescricao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblGrupoTrabalho
					(
						Descricao
					)
					VALUES
					(
						@vchDescricao
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblGrupoTrabalho
		SET
			Descricao = @vchDescricao
		WHERE
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdGrupoTrabalho AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblGrupoTrabalho 
		WHERE 
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdGrupoTrabalho AS Retorno
	END
	ELSE
	BEGIN

		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
