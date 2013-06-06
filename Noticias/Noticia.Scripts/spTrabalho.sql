select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblTrabalho'
go

CREATE PROCEDURE [dbo].[spTrabalho]
	@vchAcao VARCHAR(50),
	@intIdTrabalho INT = NULL,
	@intIdTipoUsuario INT = NULL,
	@decValorHoraTrabalhada DECIMAL(10,2) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTra.IdTrabalho,
			tblTra.IdTipoUsuario,
			tblTra.ValorHoraTrabalhada
		FROM
			tblTrabalho tblTra
		WHERE
		(
			((tblTra.IdTrabalho = @intIdTrabalho) OR (@intIdTrabalho IS NULL)) AND
			((tblTra.IdTipoUsuario = @intIdTipoUsuario) OR (@intIdTipoUsuario IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblTrabalho
					(
						IdTipoUsuario,
						ValorHoraTrabalhada
					)
					VALUES
					(
						@intIdTipoUsuario,
						@decValorHoraTrabalhada
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblTrabalho
		SET
			IdTipoUsuario = @intIdTipoUsuario,
			ValorHoraTrabalhada = @decValorHoraTrabalhada
		WHERE
			IdTrabalho = @intIdTrabalho
			
		SELECT @intIdTrabalho AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblTrabalho 
		WHERE
			IdTrabalho = @intIdTrabalho
			
		SELECT @intIdTrabalho AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
