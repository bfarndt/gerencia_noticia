select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblNoticiaImagem'
select * from tblNoticiaImagem

CREATE PROCEDURE [dbo].[spNoticiaImagem]
	@vchAcao VARCHAR(50),
	@intIdNoticia INT = NULL,
	@intIdImagem INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblNotImg.IdNoticia,
			tblNotImg.IdImagem
		FROM
			tblNoticiaImagem tblNotImg
		WHERE
		(
			((tblNotImg.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblNotImg.IdImagem = @intIdImagem) OR (@intIdImagem IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblNoticiaImagem
					(
						IdNoticia,
						IdImagem
					)
					VALUES
					(
						@intIdNoticia,
						@intIdImagem
					)
					
		SELECT @intIdNoticia AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblNoticiaImagem 
		WHERE
			IdNoticia = @intIdNoticia AND
			IdImagem = @intIdImagem
			
		SELECT @intIdNoticia AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
