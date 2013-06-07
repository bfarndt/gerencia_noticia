select * from tblDiaSemana
go

CREATE PROCEDURE [dbo].[spDiaSemana]
	@vchAcao VARCHAR(50),
	@intIdDia INT =	NULL,
	@vchDescricao VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblDia.IdDia,
			tblDia.Descricao
		FROM
			tblDiaSemana tblDia
		WHERE
		(
			((tblDia.IdDia = @intIdDia) OR (@intIdDia IS NULL)) AND
			((tblDia.Descricao LIKE '%' + @vchDescricao + '%') OR (@vchDescricao IS NULL))
		)
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
