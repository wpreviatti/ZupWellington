CREATE TABLE [dbo].[Cliente] (
    [NumeroChapaId]          INT            IDENTITY (1, 1) NOT NULL,
    [Nome]                   NVARCHAR (MAX) NOT NULL,
    [Sobrenome]              NVARCHAR (MAX) NOT NULL,
    [Mail]                   NVARCHAR (MAX) NOT NULL,
    [NomeLiderNumeroChapaId] INT            NULL,
    [Senha]                  NVARCHAR (MAX) NOT NULL,
    [DtCadastro]             DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([NumeroChapaId] ASC),
    CONSTRAINT [FK_Cliente_Cliente_NomeLiderNumeroChapaId] FOREIGN KEY ([NomeLiderNumeroChapaId]) REFERENCES [dbo].[Cliente] ([NumeroChapaId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Cliente_NomeLiderNumeroChapaId]
    ON [dbo].[Cliente]([NomeLiderNumeroChapaId] ASC);

