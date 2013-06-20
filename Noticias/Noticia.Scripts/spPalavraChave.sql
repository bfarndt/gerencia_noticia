select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblPalavraChave'
go

ALTER PROCEDURE [dbo].[spPalavraChave]
	@vchAcao VARCHAR(50),
	@intIdPalavraChave INT = NULL,
	@intIdNoticia INT = NULL,
	@vchPalavraChave VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblPal.IdPalavraChave,
			tblPal.IdNoticia,
			tblPal.PalavraChave
		FROM
			tblPalavraChave tblPal
		WHERE
		(
			((tblPal.IdPalavraChave = @intIdPalavraChave) OR (@intIdPalavraChave IS NULL)) AND
			((tblPal.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblPal.PalavraChave LIKE '%' + @vchPalavraChave + '%') OR (@vchPalavraChave IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblPalavraChave WHERE IdNoticia = @intIdNoticia AND PalavraChave = @vchPalavraChave)
		BEGIN
			INSERT INTO tblPalavraChave
						(
							IdNoticia,
							PalavraChave
						)
						VALUES
						(
							@intIdNoticia,
							@vchPalavraChave
						)
						
			SET @intIdPalavraChave = @@IDENTITY
		END
					
		SELECT @intIdNoticia AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblPalavraChave
		SET
			PalavraChave = @vchPalavraChave
		WHERE
			IdPalavraChave = @intIdPalavraChave
			
		SELECT @intIdPalavraChave AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblPalavraChave 
		WHERE
			IdNoticia = @intIdNoticia
			
		SELECT @intIdPalavraChave AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END