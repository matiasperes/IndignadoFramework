
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/17/2012 08:29:20
-- Generated from EDMX file: D:\bdi_docs\Documents\Visual Studio 2010\Projects\IndignadoFramework\IndignadoFramework\IndignadoFramework.Model\ModeloIndignadoFramework.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [IndignadoFramework];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsuarioConvocatoria_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioConvocatoria] DROP CONSTRAINT [FK_UsuarioConvocatoria_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioConvocatoria_Convocatoria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioConvocatoria] DROP CONSTRAINT [FK_UsuarioConvocatoria_Convocatoria];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioCategoriaTematica_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioCategoriaTematica] DROP CONSTRAINT [FK_UsuarioCategoriaTematica_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioCategoriaTematica_CategoriaTematica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioCategoriaTematica] DROP CONSTRAINT [FK_UsuarioCategoriaTematica_CategoriaTematica];
GO
IF OBJECT_ID(N'[dbo].[FK_ConvocatoriaMovimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConvocatoriaSet] DROP CONSTRAINT [FK_ConvocatoriaMovimiento];
GO
IF OBJECT_ID(N'[dbo].[FK_ConvocatoriaCategoriaTematica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConvocatoriaSet] DROP CONSTRAINT [FK_ConvocatoriaCategoriaTematica];
GO
IF OBJECT_ID(N'[dbo].[FK_ContenidoCategoriaTematica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContenidoSet] DROP CONSTRAINT [FK_ContenidoCategoriaTematica];
GO
IF OBJECT_ID(N'[dbo].[FK_ContenidoMegusta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MegustaSet] DROP CONSTRAINT [FK_ContenidoMegusta];
GO
IF OBJECT_ID(N'[dbo].[FK_EspecificacionUsuarioMovimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EspecificacionUsuarioSet] DROP CONSTRAINT [FK_EspecificacionUsuarioMovimiento];
GO
IF OBJECT_ID(N'[dbo].[FK_MovimientoFuenteWEB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FuenteWEBSet] DROP CONSTRAINT [FK_MovimientoFuenteWEB];
GO
IF OBJECT_ID(N'[dbo].[FK_EspecificacionUsuarioContenido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContenidoSet] DROP CONSTRAINT [FK_EspecificacionUsuarioContenido];
GO
IF OBJECT_ID(N'[dbo].[FK_EspecificacionUsuarioMegusta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MegustaSet] DROP CONSTRAINT [FK_EspecificacionUsuarioMegusta];
GO
IF OBJECT_ID(N'[dbo].[FK_Inadecuado_EspecificacionUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inadecuado] DROP CONSTRAINT [FK_Inadecuado_EspecificacionUsuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Inadecuado_Contenido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inadecuado] DROP CONSTRAINT [FK_Inadecuado_Contenido];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MovimientoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MovimientoSet];
GO
IF OBJECT_ID(N'[dbo].[ConvocatoriaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConvocatoriaSet];
GO
IF OBJECT_ID(N'[dbo].[EspecificacionUsuarioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EspecificacionUsuarioSet];
GO
IF OBJECT_ID(N'[dbo].[CategoriaTematicaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoriaTematicaSet];
GO
IF OBJECT_ID(N'[dbo].[ContenidoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContenidoSet];
GO
IF OBJECT_ID(N'[dbo].[FuenteWEBSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FuenteWEBSet];
GO
IF OBJECT_ID(N'[dbo].[MegustaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MegustaSet];
GO
IF OBJECT_ID(N'[dbo].[MensajeChatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MensajeChatSet];
GO
IF OBJECT_ID(N'[dbo].[UsuarioConvocatoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioConvocatoria];
GO
IF OBJECT_ID(N'[dbo].[UsuarioCategoriaTematica]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioCategoriaTematica];
GO
IF OBJECT_ID(N'[dbo].[Inadecuado]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Inadecuado];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MovimientoSet'
CREATE TABLE [dbo].[MovimientoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [UbicacionLatitud] float  NULL,
    [UbicacionLongitud] float  NULL,
    [Descripcion] nvarchar(max)  NULL,
    [Logo] nvarchar(max)  NULL,
    [Estilo] nvarchar(max)  NULL,
    [M] int  NOT NULL,
    [N] int  NOT NULL,
    [X] int  NOT NULL,
    [Z] int  NOT NULL
);
GO

-- Creating table 'ConvocatoriaSet'
CREATE TABLE [dbo].[ConvocatoriaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Inicio] datetime  NOT NULL,
    [UbicacionLatitud] float  NOT NULL,
    [UbicacionLongitud] float  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Quorum] int  NOT NULL,
    [Titulo] nvarchar(50)  NOT NULL,
    [MovimientoId] int  NOT NULL,
    [CategoriaTematicaId] int  NOT NULL,
    [CantUsuariosConfirmados] int  NOT NULL,
    [Suspendida] bit  NOT NULL
);
GO

-- Creating table 'EspecificacionUsuarioSet'
CREATE TABLE [dbo].[EspecificacionUsuarioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UbicacionLatitud] float  NOT NULL,
    [UbicacionLongitud] float  NOT NULL,
    [Membership] nvarchar(100)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [MovimientoId] int  NOT NULL,
    [BajasContenido] int  NOT NULL
);
GO

-- Creating table 'CategoriaTematicaSet'
CREATE TABLE [dbo].[CategoriaTematicaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ContenidoSet'
CREATE TABLE [dbo].[ContenidoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ubicacion] nvarchar(max)  NOT NULL,
    [Tipo] nvarchar(max)  NOT NULL,
    [FechaPosteado] datetime  NOT NULL,
    [CategoriaTematicaId] int  NOT NULL,
    [Inadecuado] int  NOT NULL,
    [EspecificacionUsuarioId] int  NOT NULL,
    [Titulo] nvarchar(50)  NOT NULL,
    [CantMeGusta] int  NOT NULL,
    [Habilitado] bit  NOT NULL
);
GO

-- Creating table 'FuenteWEBSet'
CREATE TABLE [dbo].[FuenteWEBSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Tipo] nvarchar(50)  NOT NULL,
    [MovimientoId] int  NOT NULL,
    [UrlDll] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MegustaSet'
CREATE TABLE [dbo].[MegustaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [ContenidoId] int  NOT NULL,
    [EspecificacionUsuarioId] int  NOT NULL
);
GO

-- Creating table 'MensajeChatSet'
CREATE TABLE [dbo].[MensajeChatSet] (
    [idRoom] int  NOT NULL,
    [idMensaje] nvarchar(200)  NOT NULL,
    [fecha] datetime  NULL,
    [mensaje] nvarchar(max)  NULL,
    [usuarioId] int  NULL,
    [usuarioNombre] nvarchar(max)  NULL
);
GO

-- Creating table 'UsuarioConvocatoria'
CREATE TABLE [dbo].[UsuarioConvocatoria] (
    [UsuariosConfirmados_Id] int  NOT NULL,
    [AsisteAConvocatorias_Id] int  NOT NULL
);
GO

-- Creating table 'UsuarioCategoriaTematica'
CREATE TABLE [dbo].[UsuarioCategoriaTematica] (
    [Usuarios_Id] int  NOT NULL,
    [CategoriasTematicas_Id] int  NOT NULL
);
GO

-- Creating table 'Inadecuado'
CREATE TABLE [dbo].[Inadecuado] (
    [Inadecuados_Id] int  NOT NULL,
    [Contenido_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MovimientoSet'
ALTER TABLE [dbo].[MovimientoSet]
ADD CONSTRAINT [PK_MovimientoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConvocatoriaSet'
ALTER TABLE [dbo].[ConvocatoriaSet]
ADD CONSTRAINT [PK_ConvocatoriaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EspecificacionUsuarioSet'
ALTER TABLE [dbo].[EspecificacionUsuarioSet]
ADD CONSTRAINT [PK_EspecificacionUsuarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategoriaTematicaSet'
ALTER TABLE [dbo].[CategoriaTematicaSet]
ADD CONSTRAINT [PK_CategoriaTematicaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContenidoSet'
ALTER TABLE [dbo].[ContenidoSet]
ADD CONSTRAINT [PK_ContenidoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FuenteWEBSet'
ALTER TABLE [dbo].[FuenteWEBSet]
ADD CONSTRAINT [PK_FuenteWEBSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MegustaSet'
ALTER TABLE [dbo].[MegustaSet]
ADD CONSTRAINT [PK_MegustaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [idRoom], [idMensaje] in table 'MensajeChatSet'
ALTER TABLE [dbo].[MensajeChatSet]
ADD CONSTRAINT [PK_MensajeChatSet]
    PRIMARY KEY CLUSTERED ([idRoom], [idMensaje] ASC);
GO

-- Creating primary key on [UsuariosConfirmados_Id], [AsisteAConvocatorias_Id] in table 'UsuarioConvocatoria'
ALTER TABLE [dbo].[UsuarioConvocatoria]
ADD CONSTRAINT [PK_UsuarioConvocatoria]
    PRIMARY KEY NONCLUSTERED ([UsuariosConfirmados_Id], [AsisteAConvocatorias_Id] ASC);
GO

-- Creating primary key on [Usuarios_Id], [CategoriasTematicas_Id] in table 'UsuarioCategoriaTematica'
ALTER TABLE [dbo].[UsuarioCategoriaTematica]
ADD CONSTRAINT [PK_UsuarioCategoriaTematica]
    PRIMARY KEY NONCLUSTERED ([Usuarios_Id], [CategoriasTematicas_Id] ASC);
GO

-- Creating primary key on [Inadecuados_Id], [Contenido_Id] in table 'Inadecuado'
ALTER TABLE [dbo].[Inadecuado]
ADD CONSTRAINT [PK_Inadecuado]
    PRIMARY KEY NONCLUSTERED ([Inadecuados_Id], [Contenido_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsuariosConfirmados_Id] in table 'UsuarioConvocatoria'
ALTER TABLE [dbo].[UsuarioConvocatoria]
ADD CONSTRAINT [FK_UsuarioConvocatoria_Usuario]
    FOREIGN KEY ([UsuariosConfirmados_Id])
    REFERENCES [dbo].[EspecificacionUsuarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AsisteAConvocatorias_Id] in table 'UsuarioConvocatoria'
ALTER TABLE [dbo].[UsuarioConvocatoria]
ADD CONSTRAINT [FK_UsuarioConvocatoria_Convocatoria]
    FOREIGN KEY ([AsisteAConvocatorias_Id])
    REFERENCES [dbo].[ConvocatoriaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioConvocatoria_Convocatoria'
CREATE INDEX [IX_FK_UsuarioConvocatoria_Convocatoria]
ON [dbo].[UsuarioConvocatoria]
    ([AsisteAConvocatorias_Id]);
GO

-- Creating foreign key on [Usuarios_Id] in table 'UsuarioCategoriaTematica'
ALTER TABLE [dbo].[UsuarioCategoriaTematica]
ADD CONSTRAINT [FK_UsuarioCategoriaTematica_Usuario]
    FOREIGN KEY ([Usuarios_Id])
    REFERENCES [dbo].[EspecificacionUsuarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CategoriasTematicas_Id] in table 'UsuarioCategoriaTematica'
ALTER TABLE [dbo].[UsuarioCategoriaTematica]
ADD CONSTRAINT [FK_UsuarioCategoriaTematica_CategoriaTematica]
    FOREIGN KEY ([CategoriasTematicas_Id])
    REFERENCES [dbo].[CategoriaTematicaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioCategoriaTematica_CategoriaTematica'
CREATE INDEX [IX_FK_UsuarioCategoriaTematica_CategoriaTematica]
ON [dbo].[UsuarioCategoriaTematica]
    ([CategoriasTematicas_Id]);
GO

-- Creating foreign key on [MovimientoId] in table 'ConvocatoriaSet'
ALTER TABLE [dbo].[ConvocatoriaSet]
ADD CONSTRAINT [FK_ConvocatoriaMovimiento]
    FOREIGN KEY ([MovimientoId])
    REFERENCES [dbo].[MovimientoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConvocatoriaMovimiento'
CREATE INDEX [IX_FK_ConvocatoriaMovimiento]
ON [dbo].[ConvocatoriaSet]
    ([MovimientoId]);
GO

-- Creating foreign key on [CategoriaTematicaId] in table 'ConvocatoriaSet'
ALTER TABLE [dbo].[ConvocatoriaSet]
ADD CONSTRAINT [FK_ConvocatoriaCategoriaTematica]
    FOREIGN KEY ([CategoriaTematicaId])
    REFERENCES [dbo].[CategoriaTematicaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConvocatoriaCategoriaTematica'
CREATE INDEX [IX_FK_ConvocatoriaCategoriaTematica]
ON [dbo].[ConvocatoriaSet]
    ([CategoriaTematicaId]);
GO

-- Creating foreign key on [CategoriaTematicaId] in table 'ContenidoSet'
ALTER TABLE [dbo].[ContenidoSet]
ADD CONSTRAINT [FK_ContenidoCategoriaTematica]
    FOREIGN KEY ([CategoriaTematicaId])
    REFERENCES [dbo].[CategoriaTematicaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContenidoCategoriaTematica'
CREATE INDEX [IX_FK_ContenidoCategoriaTematica]
ON [dbo].[ContenidoSet]
    ([CategoriaTematicaId]);
GO

-- Creating foreign key on [ContenidoId] in table 'MegustaSet'
ALTER TABLE [dbo].[MegustaSet]
ADD CONSTRAINT [FK_ContenidoMegusta]
    FOREIGN KEY ([ContenidoId])
    REFERENCES [dbo].[ContenidoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContenidoMegusta'
CREATE INDEX [IX_FK_ContenidoMegusta]
ON [dbo].[MegustaSet]
    ([ContenidoId]);
GO

-- Creating foreign key on [MovimientoId] in table 'EspecificacionUsuarioSet'
ALTER TABLE [dbo].[EspecificacionUsuarioSet]
ADD CONSTRAINT [FK_EspecificacionUsuarioMovimiento]
    FOREIGN KEY ([MovimientoId])
    REFERENCES [dbo].[MovimientoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EspecificacionUsuarioMovimiento'
CREATE INDEX [IX_FK_EspecificacionUsuarioMovimiento]
ON [dbo].[EspecificacionUsuarioSet]
    ([MovimientoId]);
GO

-- Creating foreign key on [MovimientoId] in table 'FuenteWEBSet'
ALTER TABLE [dbo].[FuenteWEBSet]
ADD CONSTRAINT [FK_MovimientoFuenteWEB]
    FOREIGN KEY ([MovimientoId])
    REFERENCES [dbo].[MovimientoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MovimientoFuenteWEB'
CREATE INDEX [IX_FK_MovimientoFuenteWEB]
ON [dbo].[FuenteWEBSet]
    ([MovimientoId]);
GO

-- Creating foreign key on [EspecificacionUsuarioId] in table 'ContenidoSet'
ALTER TABLE [dbo].[ContenidoSet]
ADD CONSTRAINT [FK_EspecificacionUsuarioContenido]
    FOREIGN KEY ([EspecificacionUsuarioId])
    REFERENCES [dbo].[EspecificacionUsuarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EspecificacionUsuarioContenido'
CREATE INDEX [IX_FK_EspecificacionUsuarioContenido]
ON [dbo].[ContenidoSet]
    ([EspecificacionUsuarioId]);
GO

-- Creating foreign key on [EspecificacionUsuarioId] in table 'MegustaSet'
ALTER TABLE [dbo].[MegustaSet]
ADD CONSTRAINT [FK_EspecificacionUsuarioMegusta]
    FOREIGN KEY ([EspecificacionUsuarioId])
    REFERENCES [dbo].[EspecificacionUsuarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EspecificacionUsuarioMegusta'
CREATE INDEX [IX_FK_EspecificacionUsuarioMegusta]
ON [dbo].[MegustaSet]
    ([EspecificacionUsuarioId]);
GO

-- Creating foreign key on [Inadecuados_Id] in table 'Inadecuado'
ALTER TABLE [dbo].[Inadecuado]
ADD CONSTRAINT [FK_Inadecuado_EspecificacionUsuario]
    FOREIGN KEY ([Inadecuados_Id])
    REFERENCES [dbo].[EspecificacionUsuarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Contenido_Id] in table 'Inadecuado'
ALTER TABLE [dbo].[Inadecuado]
ADD CONSTRAINT [FK_Inadecuado_Contenido]
    FOREIGN KEY ([Contenido_Id])
    REFERENCES [dbo].[ContenidoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Inadecuado_Contenido'
CREATE INDEX [IX_FK_Inadecuado_Contenido]
ON [dbo].[Inadecuado]
    ([Contenido_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------