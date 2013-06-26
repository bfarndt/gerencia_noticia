select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblImagemArquivo'
go

select * from tblImagemArquivo 
select * from tblImagem
exec spImagemArquivo 'SELECIONAR',null,null,null,null,null,null,0

ALTER PROCEDURE [dbo].[spImagemArquivo]
	@vchAcao VARCHAR(50),
	@intIdImagemArquivo INT = NULL,
	@intIdImagem INT = NULL,
	@binImagem VARBINARY(MAX) = NULL,
	@vchExtensao VARCHAR(10) = NULL,
	@vchTamanho NCHAR(10) = NULL,
	@vchFormato VARCHAR(50) = NULL,
	@vchNomeArquivo VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblImgArq.IdImagemArquivo,
			tblImgArq.IdImagem,
			tblImgArq.Extensao,
			tblImgArq.Tamanho,
			tblImgArq.Formato,
			tblImgArq.NomeArquivo
		FROM
			tblImagemArquivo tblImgArq
		JOIN
			tblImagem tblImg ON tblImgArq.IdImagem = tblImg.IdImagem
		WHERE
		(
			((tblImgArq.IdImagem = @intIdImagem) OR (@intIdImagem IS NULL)) AND
			((tblImgArq.Extensao LIKE '%' + @vchExtensao + '%') OR (@vchExtensao IS NULL)) AND
			((tblImgArq.Formato LIKE '%' + @vchFormato + '%') OR (@vchFormato IS NULL)) AND
			((tblImgArq.NomeArquivo LIKE '%' + @vchNomeArquivo + '%') OR (@vchNomeArquivo IS NULL)) 
			
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblImagemArquivo WHERE IdImagem = @intIdImagem)
		BEGIN
			INSERT INTO tblImagemArquivo
						(
							IdImagem,
							Imagem,
							Extensao,
							Tamanho,
							Formato,
							NomeArquivo
						)
						VALUES
						(
							@intIdImagem,
							@binImagem,
							@vchExtensao,
							@vchTamanho,
							@vchFormato,
							@vchNomeArquivo
						)
						
			
		END
		
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblImagemArquivo
		SET
			Formato = @vchFormato
		WHERE
			IdImagemArquivo = @intIdImagemArquivo
			
		SELECT @intIdImagem AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblImagemArquivo 
		WHERE
			IdImagemArquivo = @intIdImagemArquivo
			
		SELECT @intIdImagemArquivo AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'IMAGEM')
	BEGIN
		SELECT
			tblImg.Imagem
		FROM
			tblImagemArquivo tblImg
		WHERE
			tblImg.IdImagem = @intIdImagem
	END	
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END



