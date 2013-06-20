select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'tblImagemGravacao'
go

ALTER PROCEDURE [dbo].[spImagemGravacao]
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
			((tblImg.IdImagem = @intIdImagem) OR (@intIdImagem IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
	
		IF NOT EXISTS(SELECT 1 FROM tblImagemGravacao WHERE IdImagem = @intIdImagem)
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
		END
		ELSE
		BEGIN
			UPDATE
				tblImagemGravacao
			SET
				DataHoraGravacao = @datDataHoraGravacao,
				LocalGravacao = @vchLocalGravacao
			WHERE
				IdImagem = @intIdImagem
		END
					
		SELECT @intIdImagem AS Retorno
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