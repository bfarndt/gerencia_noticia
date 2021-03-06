exec spUsuario 'SELECIONAR',NULL,'Bento','senha',null,null


select * from tblUsuarioEndereco

ALTER PROCEDURE [dbo].[spUsuario]
	@vchAcao VARCHAR(50),
	@intIdUsuario	INT = NULL,
	@vchLogin	VARCHAR(50) = NULL,
	@vchSenha	VARCHAR(50)= NULL,
	@vchNome	VARCHAR(50) = NULL,
	@intIdTipoUsuario INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblUsu.IdUsuario,
			tblUsu.Login,
			tblUsu.Senha,
			tblUsu.Nome,
			tblUsu.IdTipoUsuario
		FROM
			tblUsuario tblUsu
		WHERE
		(
			((tblUsu.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblUsu.Login = @vchLogin AND tblUsu.Senha = @vchSenha) OR (@vchLogin IS NULL AND @vchSenha IS NULL)) AND
			((tblUsu.Nome LIKE '%' + @vchNome + '%') OR (@vchNome IS NULL)) AND
			((tblUsu.IdTipoUsuario = @intIdTipoUsuario) OR (@intIdTipoUsuario IS NULL))
		)
		
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblUsuario
					(
						Login,
						Senha,
						Nome,
						IdTipoUsuario
					)
					VALUES
					(
						@vchLogin,
						@vchSenha,
						@vchNome,
						@intIdTipoUsuario
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblUsuario
		SET
			Login = @vchLogin,
			Senha = @vchSenha,
			Nome = @vchNome,
			IdTipoUsuario = @intIdTipoUsuario
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblUsuario 
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
