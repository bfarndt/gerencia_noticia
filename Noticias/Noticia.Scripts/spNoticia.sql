
CREATE PROCEDURE spNoticia
	@vchAcao VARCHAR(50),
	@intIdNoticia INT = NULL,
	@vchTitulo VARCHAR(50) = NULL,
	@vchConteudo VARCHAR(MAX) = NULL,
	@datDataCriacao DATETIME = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(@vchAcao = 'SELECIONAR')
	BEGIN
		SELECT
			tblNot.IdNoticia,
			tblNot.Titulo,
			tblNot.DataCriacao,
			tblNot.Conteudo
		FROM
			tblNoticia tblNot
		WHERE
		(
			(tblNot.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL) AND
			(tblNot.Titulo LIKE  '%' + @vchTitulo + '%') OR (@vchTitulo IS NULL) AND
			(tblNot.DataCriacao = @datDataCriacao) OR (@datDataCriacao IS NULL)
		)
	END
	ELSE IF(@vchAcao = 'INSERIR')
	BEGIN
		INSERT INTO tblNoticia
					(
						Titulo,
						DataCriacao,
						Conteudo
					)
					VALUES
					(
						@vchTitulo,
						GETDATE(),
						@vchConteudo
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(@vchAcao = 'ALTERAR')
	BEGIN
		UPDATE
			tblNoticia
		SET
			Titulo = @vchTitulo,
			Conteudo = @vchConteudo
		WHERE
			IdNoticia = @intIdNoticia
			
		SELECT @intIdNoticia AS Retorno
	END
	ELSE IF(@vchAcao = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblNoticia 
		WHERE 
			IdNoticia = @intIdNoticia
			
		SELECT @intIdNoticia AS Retorno
	END
	ELSE
	BEGIN

		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
