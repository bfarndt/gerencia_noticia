USE [Noticias]
GO
/****** Object:  StoredProcedure [dbo].[spPermissao]    Script Date: 06/05/2013 23:51:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spPermissao]
	@vchAcao VARCHAR(50),
	@intIdPermissao INT =	NULL,
	@vchDescricao VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblPer.IdPermissao,
			tblPer.Descricao
		FROM
			tblPermissao tblPer
		WHERE
		(
			((tblPer.IdPermissao = @intIdPermissao) OR (@intIdPermissao IS NULL)) AND
			((tblPer.Descricao LIKE '%' + @vchDescricao + '%') OR (@vchDescricao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblPermissao
					(
						Descricao
					)
					VALUES
					(
						@vchDescricao
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblPermissao
		SET
			Descricao = @vchDescricao
			
		WHERE
			IdPermissao = @intIdPermissao
			
		SELECT @intIdPermissao AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblPermissao 
		WHERE
			IdPermissao = @intIdPermissao
			
		SELECT @intIdPermissao AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
