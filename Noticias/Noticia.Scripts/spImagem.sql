select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblImagem'
select * from tblImagem
go
tblNoticiaImagem


CREATE PROCEDURE [dbo].[spImagem]
	@vchAcao VARCHAR(50),
	@intIdImagem INT =	NULL,
	@vchLegenda VARCHAR(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblImg.IdImagem,
			tblImg.Legenda
		FROM
			tblImagem tblImg
		WHERE
		(
			((tblImg.IdImagem = @intIdImagem) OR (@intIdImagem IS NULL)) AND
			((tblImg.Legenda LIKE '%' + @vchLegenda + '%') OR (@vchLegenda IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblImagem
					(
						Legenda
					)
					VALUES
					(
						@vchLegenda
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblImagem
		SET
			Legenda = @vchLegenda
			
		WHERE
			IdImagem = @intIdImagem
			
		SELECT @intIdImagem AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblImagem 
		WHERE
			IdImagem = @intIdImagem
			
		SELECT @intIdImagem AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
