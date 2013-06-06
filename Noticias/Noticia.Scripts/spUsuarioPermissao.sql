select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblUsuarioPermissao'
go

CREATE PROCEDURE [dbo].[spUsuarioPermissao]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@intIdPermissao INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblUsuPer.IdUsuario,
			tblUsuPer.IdPermissao
		FROM
			tblUsuarioPermissao tblUsuPer
		WHERE
		(
			((tblUsuPer.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblUsuPer.IdPermissao = @intIdPermissao) OR (@intIdPermissao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblUsuarioPermissao
					(
						IdUsuario,
						IdPermissao
					)
					VALUES
					(
						@intIdUsuario,
						@intIdPermissao
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblUsuarioPermissao 
		WHERE
			IdUsuario = @intIdUsuario AND
			IdPermissao = @intIdPermissao
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
