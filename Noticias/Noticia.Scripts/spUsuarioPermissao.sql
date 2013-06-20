select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblUsuarioPermissao'
go

ALTER PROCEDURE [dbo].[spUsuarioPermissao]
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
		IF NOT EXISTS(SELECT 1 FROM tblUsuarioPermissao WHERE IdUsuario = @intIdUsuario AND IdPermissao = @intIdPermissao)
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
		END
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
	ELSE IF(upper(@vchAcao) = 'DELETAR_POR_USUARIO')
	BEGIN
		DELETE FROM 
			tblUsuarioPermissao 
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END	
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END