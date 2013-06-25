select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblImagem'
select * from tblImagem
go


ALTER PROCEDURE [dbo].[spImagem]
	@vchAcao VARCHAR(50),
	@intIdImagem INT =	NULL,
	@vchLegenda VARCHAR(100) = NULL,
	@bitSelecionada BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblImg.IdImagem,
			tblImg.Legenda,
			ISNULL(tblImg.Selecionada,0) Selecionada
		FROM
			tblImagem tblImg
		WHERE
		(
			((tblImg.IdImagem = @intIdImagem) OR (@intIdImagem IS NULL)) AND
			((tblImg.Legenda LIKE '%' + @vchLegenda + '%') OR (@vchLegenda IS NULL)) AND
			((tblImg.Selecionada = @bitSelecionada) OR (@bitSelecionada IS NULL)) 
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblImagem
					(
						Legenda,
						Selecionada
					)
					VALUES
					(
						@vchLegenda,
						@bitSelecionada
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblImagem
		SET
			Legenda = @vchLegenda,
			Selecionada = @bitSelecionada
			
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
