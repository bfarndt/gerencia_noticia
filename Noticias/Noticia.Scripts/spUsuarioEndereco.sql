select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblUsuarioEndereco'
go

ALTER PROCEDURE [dbo].[spUsuarioEndereco]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@vchEmail VARCHAR(50) = NULL,
	@vchTelefone VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblEnd.IdUsuario,
			tblEnd.Email,
			tblEnd.Telefone
		FROM
			tblUsuarioEndereco tblEnd
		WHERE
		(
			((tblEnd.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblEnd.Email LIKE '%' + @vchEmail + '%') OR (@vchEmail IS NULL)) AND
			((tblEnd.Telefone LIKE '%' + @vchTelefone + '%') OR (@vchTelefone IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblUsuarioEndereco
					(
						IdUsuario,
						Email,
						Telefone
					)
					VALUES
					(
						@intIdUsuario,
						@vchEmail,
						@vchTelefone
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblUsuarioEndereco
		SET
			Email = @vchEmail,
			Telefone = @vchTelefone
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblUsuarioEndereco 
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
