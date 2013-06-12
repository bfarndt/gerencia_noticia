select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblImagemGravacao'
go

CREATE PROCEDURE [dbo].[spImagemGravacao]
	@vchAcao VARCHAR(50),
	@intIdImagem INT = NULL,
	@datDataHoraGravacao DATETIME = NULL,
	@vchLocalGravacao VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblImg.IdImagem,
			tblImg.DataHoraGravacao,
			tblImg.LocalGravacao
		FROM
			tblImagemGravacao tblImg
		WHERE
		(
			((tblImg.IdImagem = @intIdImagem) OR (@intIdImagem IS NULL)) AND
			((tblImg.LocalGravacao LIKE '%' + @vchLocalGravacao + '%') OR (@vchLocalGravacao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblImagemGravacao
					(
						IdImagem,
						DataHoraGravacao,
						LocalGravacao
					)
					VALUES
					(
						@intIdImagem,
						@datDataHoraGravacao,
						@vchLocalGravacao
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblImagemGravacao
		SET
			DataHoraGravacao = @datDataHoraGravacao,
			LocalGravacao = @vchLocalGravacao
		WHERE
			IdImagem = @intIdImagem
			
		SELECT @intIdImagem AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblImagemGravacao 
		WHERE
			IdImagem = @intIdImagem
			
		SELECT @intIdImagem AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END