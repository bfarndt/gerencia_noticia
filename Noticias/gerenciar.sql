USE [Noticias]
GO
/****** Object:  Table [dbo].[tblDiaSemana]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDiaSemana](
	[IdDia] [int] NOT NULL,
	[Descricao] [varchar](20) NULL,
 CONSTRAINT [PK_tblDiaSemana] PRIMARY KEY CLUSTERED 
(
	[IdDia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblGrupoTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblGrupoTrabalho](
	[IdGrupoTrabalho] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL,
 CONSTRAINT [PK_tblGrupoTrabalho] PRIMARY KEY CLUSTERED 
(
	[IdGrupoTrabalho] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblImagem]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblImagem](
	[IdImagem] [int] IDENTITY(1,1) NOT NULL,
	[Legenda] [varchar](100) NULL,
	[Selecionada] [varchar](100) NULL,
 CONSTRAINT [PK_tblImagem] PRIMARY KEY CLUSTERED 
(
	[IdImagem] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetTableFromIntList]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GetTableFromIntList]
(@strIntList VARCHAR(MAX),
 @strDelimiter VARCHAR(10)
)
 
RETURNS @tblList TABLE (ID INT NOT NULL)
 
AS
 
BEGIN
 
DECLARE    @iStartPos INT,@iEndPos INT,@strValue VARCHAR(15)
SET @iStartPos = 1
SET @strIntList = @strIntList + @strDelimiter
SET @iEndPos = CHARINDEX(@strDelimiter, @strIntList)
 
WHILE @iEndPos > 0
 
BEGIN
 
    SET @strValue = SUBSTRING(@strIntList, @iStartPos, @iEndPos - @iStartPos)
    INSERT @tblList (ID) VALUES(CONVERT(INT, @strValue))
    SET @iStartPos = @iEndPos + 1
    SET @iEndPos = CHARINDEX(@strDelimiter, @strIntList, @iStartPos)
END
RETURN
END
GO
/****** Object:  Table [dbo].[tblTipoUsuario]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTipoUsuario](
	[IdTipoUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL,
 CONSTRAINT [PK_tblTipoUsuario] PRIMARY KEY CLUSTERED 
(
	[IdTipoUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblStatusNoticia]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStatusNoticia](
	[IdStatus] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL,
 CONSTRAINT [PK_tblStatusNoticia] PRIMARY KEY CLUSTERED 
(
	[IdStatus] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPermissao]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPermissao](
	[IdPermissao] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL,
 CONSTRAINT [PK_tblPermissao] PRIMARY KEY CLUSTERED 
(
	[IdPermissao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblNoticia]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblNoticia](
	[IdNoticia] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NULL,
	[Conteudo] [varchar](max) NULL,
 CONSTRAINT [PK_tblNoticia] PRIMARY KEY CLUSTERED 
(
	[IdNoticia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUsuario]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NULL,
	[Senha] [varchar](50) NULL,
	[Nome] [varchar](50) NULL,
	[IdTipoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_tblUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTrabalho](
	[IdTrabalho] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoUsuario] [int] NOT NULL,
	[ValorHoraTrabalhada] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblTrabalho] PRIMARY KEY CLUSTERED 
(
	[IdTrabalho] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblImagemGravacao]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblImagemGravacao](
	[IdImagem] [int] NOT NULL,
	[DataHoraGravacao] [datetime] NULL,
	[LocalGravacao] [varchar](50) NULL,
 CONSTRAINT [PK_tblImagemGravacao] PRIMARY KEY CLUSTERED 
(
	[IdImagem] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblImagemArquivo]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblImagemArquivo](
	[IdImagem] [int] NOT NULL,
	[Imagem] [varbinary](max) NULL,
	[Extensao] [varchar](10) NULL,
	[Tamanho] [nchar](10) NULL,
	[Formato] [varchar](50) NULL,
 CONSTRAINT [PK_tblImagemArquivo] PRIMARY KEY CLUSTERED 
(
	[IdImagem] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPalavraChave]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPalavraChave](
	[IdPalavraChave] [int] IDENTITY(1,1) NOT NULL,
	[IdNoticia] [int] NOT NULL,
	[PalavraChave] [varchar](50) NULL,
 CONSTRAINT [PK_tblPalavraChave] PRIMARY KEY CLUSTERED 
(
	[IdPalavraChave] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblNoticiaImagem]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNoticiaImagem](
	[IdNoticia] [int] NOT NULL,
	[IdImagem] [int] NOT NULL,
 CONSTRAINT [PK_tblNoticiaImagem] PRIMARY KEY CLUSTERED 
(
	[IdNoticia] ASC,
	[IdImagem] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNoticiaGrupoTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNoticiaGrupoTrabalho](
	[IdNoticia] [int] NOT NULL,
	[IdGrupoTrabalho] [int] NOT NULL,
 CONSTRAINT [PK_tblNoticiaGrupoTrabalho] PRIMARY KEY CLUSTERED 
(
	[IdNoticia] ASC,
	[IdGrupoTrabalho] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spDiaSemana]    Script Date: 06/21/2013 14:53:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[spGrupoTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGrupoTrabalho]
	@vchAcao VARCHAR(50),
	@intIdGrupoTrabalho INT = NULL,
	@vchDescricao VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblGru.IdGrupoTrabalho,
			tblGru.Descricao
		FROM
			tblGrupoTrabalho tblGru
		WHERE
		(
			((tblGru.IdGrupoTrabalho = @intIdGrupoTrabalho) OR (@intIdGrupoTrabalho IS NULL)) AND
			((lower(tblGru.Descricao) LIKE '%' + lower(@vchDescricao) + '%') OR (@vchDescricao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblGrupoTrabalho
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
			tblGrupoTrabalho
		SET
			Descricao = @vchDescricao
		WHERE
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdGrupoTrabalho AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblGrupoTrabalho 
		WHERE 
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdGrupoTrabalho AS Retorno
	END
	ELSE
	BEGIN

		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spImagem]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spImagem]
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
			tblImg.Selecionada
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
GO
/****** Object:  StoredProcedure [dbo].[spTipoUsuario]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTipoUsuario]
	@vchAcao VARCHAR(50),
	@intIdTipoUsuario INT =	NULL,
	@vchDescricao VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTipUsu.IdTipoUsuario,
			tblTipUsu.Descricao
		FROM
			tblTipoUsuario tblTipUsu
		WHERE
		(
			((tblTipUsu.IdTipoUsuario = @intIdTipoUsuario) OR (@intIdTipoUsuario IS NULL)) AND
			((tblTipUsu.Descricao LIKE '%' + @vchDescricao + '%') OR (@vchDescricao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblTipoUsuario
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
			tblTipoUsuario
		SET
			Descricao = @vchDescricao
			
		WHERE
			IdTipoUsuario = @intIdTipoUsuario
			
		SELECT @intIdTipoUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblTipoUsuario 
		WHERE
			IdTipoUsuario = @intIdTipoUsuario
			
		SELECT @intIdTipoUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spStatusNoticia]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spStatusNoticia]
	@vchAcao VARCHAR(50),
	@intIdStatus INT =	NULL,
	@vchDescricao VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTipUsu.IdStatus,
			tblTipUsu.Descricao
		FROM
			tblStatusNoticia tblTipUsu
		WHERE
		(
			((tblTipUsu.IdStatus = @intIdStatus) OR (@intIdStatus IS NULL)) AND
			((tblTipUsu.Descricao LIKE '%' + @vchDescricao + '%') OR (@vchDescricao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblStatusNoticia
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
			tblStatusNoticia
		SET
			Descricao = @vchDescricao
			
		WHERE
			IdStatus = @intIdStatus
			
		SELECT @intIdStatus AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblStatusNoticia 
		WHERE
			IdStatus = @intIdStatus
			
		SELECT @intIdStatus AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spPermissao]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPermissao]
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
GO
/****** Object:  StoredProcedure [dbo].[spNoticia]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spNoticia]
	@vchAcao VARCHAR(50),
	@intIdNoticia INT = NULL,
	@vchTitulo VARCHAR(50) = NULL,
	@vchConteudo VARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblNot.IdNoticia,
			tblNot.Titulo,
			tblNot.Conteudo
		FROM
			tblNoticia tblNot
		WHERE
		(
			((tblNot.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblNot.Titulo LIKE  '%' + @vchTitulo + '%') OR (@vchTitulo IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblNoticia
					(
						Titulo,
						Conteudo
					)
					VALUES
					(
						@vchTitulo,
						@vchConteudo
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
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
	ELSE IF(upper(@vchAcao) = 'DELETAR')
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
/****** Object:  Table [dbo].[tblUsuarioPermissao]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsuarioPermissao](
	[IdUsuario] [int] NOT NULL,
	[IdPermissao] [int] NOT NULL,
 CONSTRAINT [PK_tblUsuarioPermissao] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdPermissao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsuarioEndereco]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsuarioEndereco](
	[IdUsuario] [int] NOT NULL,
	[Email] [varchar](50) NULL,
	[Telefone] [varchar](50) NULL,
 CONSTRAINT [PK_tblUsuarioEndereco] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[spUsuario]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUsuario]
	@vchAcao VARCHAR(50),
	@intIdUsuario	INT = NULL,
	@vchLogin	VARCHAR(50) = NULL,
	@vchSenha	VARCHAR(50)= NULL,
	@vchNome	VARCHAR(50) = NULL,
	@intIdTipoUsuario INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblUsu.IdUsuario,
			tblUsu.Login,
			tblUsu.Senha,
			tblUsu.Nome,
			tblUsu.IdTipoUsuario
		FROM
			tblUsuario tblUsu
		WHERE
		(
			((tblUsu.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblUsu.Login = @vchLogin AND tblUsu.Senha = @vchSenha) OR (@vchLogin IS NULL AND @vchSenha IS NULL)) AND
			((tblUsu.Nome LIKE '%' + @vchNome + '%') OR (@vchNome IS NULL)) AND
			((tblUsu.IdTipoUsuario = @intIdTipoUsuario) OR (@intIdTipoUsuario IS NULL))
		)
		
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblUsuario
					(
						Login,
						Senha,
						Nome,
						IdTipoUsuario
					)
					VALUES
					(
						@vchLogin,
						@vchSenha,
						@vchNome,
						@intIdTipoUsuario
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblUsuario
		SET
			Login = @vchLogin,
			Senha = @vchSenha,
			Nome = @vchNome,
			IdTipoUsuario = @intIdTipoUsuario
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblUsuario 
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTrabalho]
	@vchAcao VARCHAR(50),
	@intIdTrabalho INT = NULL,
	@intIdTipoUsuario INT = NULL,
	@decValorHoraTrabalhada DECIMAL(10,2) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTra.IdTrabalho,
			tblTra.IdTipoUsuario,
			tblTra.ValorHoraTrabalhada
		FROM
			tblTrabalho tblTra
		WHERE
		(
			((tblTra.IdTrabalho = @intIdTrabalho) OR (@intIdTrabalho IS NULL)) AND
			((tblTra.IdTipoUsuario = @intIdTipoUsuario) OR (@intIdTipoUsuario IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblTrabalho
					(
						IdTipoUsuario,
						ValorHoraTrabalhada
					)
					VALUES
					(
						@intIdTipoUsuario,
						@decValorHoraTrabalhada
					)
					
		SELECT @@IDENTITY AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblTrabalho
		SET
			IdTipoUsuario = @intIdTipoUsuario,
			ValorHoraTrabalhada = @decValorHoraTrabalhada
		WHERE
			IdTrabalho = @intIdTrabalho
			
		SELECT @intIdTrabalho AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblTrabalho 
		WHERE
			IdTrabalho = @intIdTrabalho
			
		SELECT @intIdTrabalho AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spImagemGravacao]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[spImagemArquivo]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spImagemArquivo]
	@vchAcao VARCHAR(50),
	@intIdImagem INT = NULL,
	@binImagem VARBINARY(MAX) = NULL,
	@vchExtensao VARCHAR(10) = NULL,
	@vchTamanho NCHAR(10) = NULL,
	@vchFormato VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblImg.IdImagem,
			tblImg.Imagem,
			tblImg.Extensao,
			tblImg.Tamanho,
			tblImg.Formato
		FROM
			tblImagemArquivo tblImg
		WHERE
		(
			((tblImg.IdImagem = @intIdImagem) OR (@intIdImagem IS NULL)) AND
			((tblImg.Extensao LIKE '%' + @vchExtensao + '%') OR (@vchExtensao IS NULL)) AND
			((tblImg.Formato LIKE '%' + @vchFormato + '%') OR (@vchFormato IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblImagemArquivo
					(
						IdImagem,
						Imagem,
						Extensao,
						Tamanho,
						Formato
					)
					VALUES
					(
						@intIdImagem,
						@binImagem,
						@vchExtensao,
						@vchTamanho,
						@vchFormato
					)
					
		SELECT @intIdImagem AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblImagemArquivo
		SET
			Formato = @vchFormato
		WHERE
			IdImagem = @intIdImagem
			
		SELECT @intIdImagem AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblImagemArquivo 
		WHERE
			IdImagem = @intIdImagem
			
		SELECT @intIdImagem AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spPalavraChave]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPalavraChave]
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
GO
/****** Object:  StoredProcedure [dbo].[spNoticiaImagem]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
		IF NOT EXISTS(SELECT 1 FROM tblNoticiaImagem WHERE IdNoticia = @intIdNoticia AND IdImagem = @intIdImagem)
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
		END
					
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
GO
/****** Object:  StoredProcedure [dbo].[spNoticiaGrupoTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spNoticiaGrupoTrabalho]
	@vchAcao VARCHAR(50),
	@intIdNoticia INT = NULL,
	@intIdGrupoTrabalho INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTra.IdNoticia,
			tblTra.IdGrupoTrabalho
		FROM
			tblNoticiaGrupoTrabalho tblTra
		WHERE
		(
			((tblTra.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblTra.IdGrupoTrabalho = @intIdGrupoTrabalho) OR (@intIdGrupoTrabalho IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblNoticiaGrupoTrabalho WHERE IdGrupoTrabalho = @intIdGrupoTrabalho AND IdNoticia = @intIdNoticia)
		BEGIN
	
			INSERT INTO tblNoticiaGrupoTrabalho
						(
							IdNoticia,
							IdGrupoTrabalho
						)
						VALUES
						(
							@intIdNoticia,
							@intIdGrupoTrabalho
						)
		END
					
		SELECT @intIdNoticia AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblNoticiaGrupoTrabalho 
		WHERE
			IdNoticia = @intIdNoticia AND
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdNoticia AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  Table [dbo].[tblHistorico]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblHistorico](
	[IdHistorico] [int] IDENTITY(1,1) NOT NULL,
	[IdNoticia] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
	[DataHora] [datetime] NULL,
	[Descricao] [varchar](max) NULL,
 CONSTRAINT [PK_tblHistorico] PRIMARY KEY CLUSTERED 
(
	[IdHistorico] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblGrupoTrabalhoUsuario]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGrupoTrabalhoUsuario](
	[IdGrupoTrabalho] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_tblGrupoTrabalhoUsuario] PRIMARY KEY CLUSTERED 
(
	[IdGrupoTrabalho] ASC,
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDiasTrabalhados]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDiasTrabalhados](
	[IdUsuario] [int] NOT NULL,
	[IdDia] [int] NOT NULL,
 CONSTRAINT [PK_tblDiasTrabalhados_1] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdDia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblContratacao]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblContratacao](
	[IdUsuario] [int] NOT NULL,
	[DataHora] [datetime] NULL,
 CONSTRAINT [PK_tblContratacao] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spUsuarioPermissao]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUsuarioPermissao]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@intIdPermissao INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblUsuPer.IdUsuario,
			tblUsuPer.IdPermissao
		FROM
			tblUsuarioPermissao tblUsuPer
		WHERE
		(
			((tblUsuPer.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblUsuPer.IdPermissao = @intIdPermissao) OR (@intIdPermissao IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblUsuarioPermissao WHERE IdUsuario = @intIdUsuario AND IdPermissao = @intIdPermissao)
		BEGIN
			INSERT INTO tblUsuarioPermissao
						(
							IdUsuario,
							IdPermissao
						)
						VALUES
						(
							@intIdUsuario,
							@intIdPermissao
						)
		END
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblUsuarioPermissao 
		WHERE
			IdUsuario = @intIdUsuario AND
			IdPermissao = @intIdPermissao
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR_POR_USUARIO')
	BEGIN
		DELETE FROM 
			tblUsuarioPermissao 
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END	
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spUsuarioEndereco]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUsuarioEndereco]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@vchEmail VARCHAR(50) = NULL,
	@vchTelefone VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblEnd.IdUsuario,
			tblEnd.Email,
			tblEnd.Telefone
		FROM
			tblUsuarioEndereco tblEnd
		WHERE
		(
			((tblEnd.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblEnd.Email LIKE '%' + @vchEmail + '%') OR (@vchEmail IS NULL)) AND
			((tblEnd.Telefone LIKE '%' + @vchTelefone + '%') OR (@vchTelefone IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblUsuarioEndereco
					(
						IdUsuario,
						Email,
						Telefone
					)
					VALUES
					(
						@intIdUsuario,
						@vchEmail,
						@vchTelefone
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblUsuarioEndereco
		SET
			Email = @vchEmail,
			Telefone = @vchTelefone
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblUsuarioEndereco 
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spHistorico]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 PROCEDURE [dbo].[spHistorico]
	@vchAcao VARCHAR(50),
	@intIdHistorico INT = NULL,
	@intIdNoticia INT = NULL,
	@intIdUsuario INT = NULL,
	@intIdStatus INT = NULL,
	@datDataHora DATETIME = NULL,
	@vchVariosIdStatus VARCHAR(50) = NULL,
	@vchDescricao VARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
	
		Declare @sql VARCHAR(MAX)

		SELECT
			tblHis.IdHistorico,
			tblHis.IdNoticia,
			tblHis.IdUsuario,
			tblHis.IdStatus,
			tblHis.DataHora,
			tblHis.Descricao
		FROM
			tblHistorico tblHis
		WHERE
		(
			((tblHis.IdHistorico = @intIdHistorico) OR (@intIdHistorico IS NULL)) AND
			((tblHis.IdNoticia = @intIdNoticia) OR (@intIdNoticia IS NULL)) AND
			((tblHis.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblHis.IdStatus in (SELECT ID FROM fn_GetTableFromIntList(@vchVariosIdStatus,',')) OR (@vchVariosIdStatus IS NULL)))
		)
		
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblHistorico
					(
						IdNoticia,
						IdUsuario,
						IdStatus,
						DataHora,
						Descricao
					)
					VALUES
					(
						@intIdNoticia,
						@intIdUsuario,
						@intIdStatus,
						@datDataHora,
						@vchDescricao
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spGrupoTrabalhoUsuario]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGrupoTrabalhoUsuario]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@intIdGrupoTrabalho INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTra.IdUsuario,
			tblTra.IdGrupoTrabalho
		FROM
			tblGrupoTrabalhoUsuario tblTra
		WHERE
		(
			((tblTra.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblTra.IdGrupoTrabalho = @intIdGrupoTrabalho) OR (@intIdGrupoTrabalho IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblGrupoTrabalhoUsuario WHERE IdUsuario = @intIdUsuario AND IdGrupoTrabalho = @intIdGrupoTrabalho)
		BEGIN
	
			INSERT INTO tblGrupoTrabalhoUsuario
						(
							IdUsuario,
							IdGrupoTrabalho
						)
						VALUES
						(
							@intIdUsuario,
							@intIdGrupoTrabalho
						)
		END
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblGrupoTrabalhoUsuario 
		WHERE
			IdUsuario = @intIdUsuario AND
			IdGrupoTrabalho = @intIdGrupoTrabalho
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR_POR_USUARIO')
	BEGIN
		DELETE FROM 
			tblGrupoTrabalhoUsuario 
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spDiasTrabalhados]    Script Date: 06/21/2013 14:53:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDiasTrabalhados]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@intIdDia INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblTra.IdUsuario,
			tblTra.IdDia
		FROM
			tblDiasTrabalhados tblTra
		WHERE
		(
			((tblTra.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL)) AND
			((tblTra.IdDia = @intIdDia) OR (@intIdDia IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblDiasTrabalhados WHERE IdUsuario = @intIdUsuario AND IdDia = @intIdDia)
		BEGIN
			INSERT INTO tblDiasTrabalhados
						(
							IdUsuario,
							IdDia
						)
						VALUES
						(
							@intIdUsuario,
							@intIdDia
						)
		END
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblDiasTrabalhados 
		WHERE
			IdUsuario = @intIdUsuario AND
			IdDia = @intIdDia
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN
		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spContratacao]    Script Date: 06/21/2013 14:53:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spContratacao]
	@vchAcao VARCHAR(50),
	@intIdUsuario INT = NULL,
	@datDataHora DATETIME = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(upper(@vchAcao) = 'SELECIONAR')
	BEGIN
		SELECT
			tblCon.IdUsuario,
			tblCon.DataHora
		FROM
			tblContratacao tblCon
		WHERE
		(
			((tblCon.IdUsuario = @intIdUsuario) OR (@intIdUsuario IS NULL))
		)
	END
	ELSE IF(upper(@vchAcao) = 'INSERIR')
	BEGIN
		INSERT INTO tblContratacao
					(
						IdUsuario,
						DataHora
					)
					VALUES
					(
						@intIdUsuario,
						@datDataHora
					)
					
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'ALTERAR')
	BEGIN
		UPDATE
			tblContratacao
		SET
			DataHora = @datDataHora
		WHERE
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE IF(upper(@vchAcao) = 'DELETAR')
	BEGIN
		DELETE FROM 
			tblContratacao 
		WHERE 
			IdUsuario = @intIdUsuario
			
		SELECT @intIdUsuario AS Retorno
	END
	ELSE
	BEGIN

		SELECT 'AÇÃO INEXISTENTE' AS Retorno
	END
END
GO
/****** Object:  ForeignKey [FK_tblContratacao_tblUsuario]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblContratacao]  WITH CHECK ADD  CONSTRAINT [FK_tblContratacao_tblUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[tblUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[tblContratacao] CHECK CONSTRAINT [FK_tblContratacao_tblUsuario]
GO
/****** Object:  ForeignKey [FK_tblDiasTrabalhados_tblDiaSemana1]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblDiasTrabalhados]  WITH CHECK ADD  CONSTRAINT [FK_tblDiasTrabalhados_tblDiaSemana1] FOREIGN KEY([IdDia])
REFERENCES [dbo].[tblDiaSemana] ([IdDia])
GO
ALTER TABLE [dbo].[tblDiasTrabalhados] CHECK CONSTRAINT [FK_tblDiasTrabalhados_tblDiaSemana1]
GO
/****** Object:  ForeignKey [FK_tblDiasTrabalhados_tblUsuario]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblDiasTrabalhados]  WITH CHECK ADD  CONSTRAINT [FK_tblDiasTrabalhados_tblUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[tblUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[tblDiasTrabalhados] CHECK CONSTRAINT [FK_tblDiasTrabalhados_tblUsuario]
GO
/****** Object:  ForeignKey [FK_tblGrupoTrabalhoUsuario_tblGrupoTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblGrupoTrabalhoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tblGrupoTrabalhoUsuario_tblGrupoTrabalho] FOREIGN KEY([IdGrupoTrabalho])
REFERENCES [dbo].[tblGrupoTrabalho] ([IdGrupoTrabalho])
GO
ALTER TABLE [dbo].[tblGrupoTrabalhoUsuario] CHECK CONSTRAINT [FK_tblGrupoTrabalhoUsuario_tblGrupoTrabalho]
GO
/****** Object:  ForeignKey [FK_tblGrupoTrabalhoUsuario_tblUsuario]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblGrupoTrabalhoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tblGrupoTrabalhoUsuario_tblUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[tblUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[tblGrupoTrabalhoUsuario] CHECK CONSTRAINT [FK_tblGrupoTrabalhoUsuario_tblUsuario]
GO
/****** Object:  ForeignKey [FK_tblHistorico_tblNoticia]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblHistorico]  WITH CHECK ADD  CONSTRAINT [FK_tblHistorico_tblNoticia] FOREIGN KEY([IdNoticia])
REFERENCES [dbo].[tblNoticia] ([IdNoticia])
GO
ALTER TABLE [dbo].[tblHistorico] CHECK CONSTRAINT [FK_tblHistorico_tblNoticia]
GO
/****** Object:  ForeignKey [FK_tblHistorico_tblStatusNoticia]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblHistorico]  WITH CHECK ADD  CONSTRAINT [FK_tblHistorico_tblStatusNoticia] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[tblStatusNoticia] ([IdStatus])
GO
ALTER TABLE [dbo].[tblHistorico] CHECK CONSTRAINT [FK_tblHistorico_tblStatusNoticia]
GO
/****** Object:  ForeignKey [FK_tblHistorico_tblUsuario1]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblHistorico]  WITH CHECK ADD  CONSTRAINT [FK_tblHistorico_tblUsuario1] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[tblUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[tblHistorico] CHECK CONSTRAINT [FK_tblHistorico_tblUsuario1]
GO
/****** Object:  ForeignKey [FK_tblImagemArquivo_tblImagem]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblImagemArquivo]  WITH CHECK ADD  CONSTRAINT [FK_tblImagemArquivo_tblImagem] FOREIGN KEY([IdImagem])
REFERENCES [dbo].[tblImagem] ([IdImagem])
GO
ALTER TABLE [dbo].[tblImagemArquivo] CHECK CONSTRAINT [FK_tblImagemArquivo_tblImagem]
GO
/****** Object:  ForeignKey [FK_tblImagemGravacao_tblImagem]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblImagemGravacao]  WITH CHECK ADD  CONSTRAINT [FK_tblImagemGravacao_tblImagem] FOREIGN KEY([IdImagem])
REFERENCES [dbo].[tblImagem] ([IdImagem])
GO
ALTER TABLE [dbo].[tblImagemGravacao] CHECK CONSTRAINT [FK_tblImagemGravacao_tblImagem]
GO
/****** Object:  ForeignKey [FK_tblNoticiaGrupoTrabalho_tblGrupoTrabalho]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblNoticiaGrupoTrabalho]  WITH CHECK ADD  CONSTRAINT [FK_tblNoticiaGrupoTrabalho_tblGrupoTrabalho] FOREIGN KEY([IdGrupoTrabalho])
REFERENCES [dbo].[tblGrupoTrabalho] ([IdGrupoTrabalho])
GO
ALTER TABLE [dbo].[tblNoticiaGrupoTrabalho] CHECK CONSTRAINT [FK_tblNoticiaGrupoTrabalho_tblGrupoTrabalho]
GO
/****** Object:  ForeignKey [FK_tblNoticiaGrupoTrabalho_tblNoticia]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblNoticiaGrupoTrabalho]  WITH CHECK ADD  CONSTRAINT [FK_tblNoticiaGrupoTrabalho_tblNoticia] FOREIGN KEY([IdNoticia])
REFERENCES [dbo].[tblNoticia] ([IdNoticia])
GO
ALTER TABLE [dbo].[tblNoticiaGrupoTrabalho] CHECK CONSTRAINT [FK_tblNoticiaGrupoTrabalho_tblNoticia]
GO
/****** Object:  ForeignKey [FK_tblNoticiaImagem_tblImagem]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblNoticiaImagem]  WITH CHECK ADD  CONSTRAINT [FK_tblNoticiaImagem_tblImagem] FOREIGN KEY([IdImagem])
REFERENCES [dbo].[tblImagem] ([IdImagem])
GO
ALTER TABLE [dbo].[tblNoticiaImagem] CHECK CONSTRAINT [FK_tblNoticiaImagem_tblImagem]
GO
/****** Object:  ForeignKey [FK_tblNoticiaImagem_tblNoticia]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblNoticiaImagem]  WITH CHECK ADD  CONSTRAINT [FK_tblNoticiaImagem_tblNoticia] FOREIGN KEY([IdNoticia])
REFERENCES [dbo].[tblNoticia] ([IdNoticia])
GO
ALTER TABLE [dbo].[tblNoticiaImagem] CHECK CONSTRAINT [FK_tblNoticiaImagem_tblNoticia]
GO
/****** Object:  ForeignKey [FK_tblPalavraChave_tblNoticia]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblPalavraChave]  WITH CHECK ADD  CONSTRAINT [FK_tblPalavraChave_tblNoticia] FOREIGN KEY([IdNoticia])
REFERENCES [dbo].[tblNoticia] ([IdNoticia])
GO
ALTER TABLE [dbo].[tblPalavraChave] CHECK CONSTRAINT [FK_tblPalavraChave_tblNoticia]
GO
/****** Object:  ForeignKey [FK_tblTrabalho_tblTipoUsuario]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblTrabalho]  WITH CHECK ADD  CONSTRAINT [FK_tblTrabalho_tblTipoUsuario] FOREIGN KEY([IdTipoUsuario])
REFERENCES [dbo].[tblTipoUsuario] ([IdTipoUsuario])
GO
ALTER TABLE [dbo].[tblTrabalho] CHECK CONSTRAINT [FK_tblTrabalho_tblTipoUsuario]
GO
/****** Object:  ForeignKey [FK_tblUsuario_tblTipoUsuario]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuario_tblTipoUsuario] FOREIGN KEY([IdTipoUsuario])
REFERENCES [dbo].[tblTipoUsuario] ([IdTipoUsuario])
GO
ALTER TABLE [dbo].[tblUsuario] CHECK CONSTRAINT [FK_tblUsuario_tblTipoUsuario]
GO
/****** Object:  ForeignKey [FK_tblUsuarioEndereco_tblUsuario]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblUsuarioEndereco]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuarioEndereco_tblUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[tblUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[tblUsuarioEndereco] CHECK CONSTRAINT [FK_tblUsuarioEndereco_tblUsuario]
GO
/****** Object:  ForeignKey [FK_tblUsuarioPermissao_tblPermissao]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblUsuarioPermissao]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuarioPermissao_tblPermissao] FOREIGN KEY([IdPermissao])
REFERENCES [dbo].[tblPermissao] ([IdPermissao])
GO
ALTER TABLE [dbo].[tblUsuarioPermissao] CHECK CONSTRAINT [FK_tblUsuarioPermissao_tblPermissao]
GO
/****** Object:  ForeignKey [FK_tblUsuarioPermissao_tblUsuario]    Script Date: 06/21/2013 14:53:49 ******/
ALTER TABLE [dbo].[tblUsuarioPermissao]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuarioPermissao_tblUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[tblUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[tblUsuarioPermissao] CHECK CONSTRAINT [FK_tblUsuarioPermissao_tblUsuario]
GO
