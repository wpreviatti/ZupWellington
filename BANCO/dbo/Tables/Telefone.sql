CREATE TABLE [dbo].[Telefone] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [NumeroTelefone]       NVARCHAR (MAX) NOT NULL,
    [TipoTelefone]         NVARCHAR (MAX) NULL,
    [ClienteNumeroChapaId] INT            NULL,
    CONSTRAINT [PK_Telefone] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Telefone_Cliente_ClienteNumeroChapaId] FOREIGN KEY ([ClienteNumeroChapaId]) REFERENCES [dbo].[Cliente] ([NumeroChapaId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Telefone_ClienteNumeroChapaId]
    ON [dbo].[Telefone]([ClienteNumeroChapaId] ASC);

