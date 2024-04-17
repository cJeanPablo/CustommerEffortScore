CREATE DATABASE CES
GO

USE CES
GO

CREATE TABLE dbo.TipoPergunta(
    Id int IDENTITY(1,1),
    Tipo varchar(20),
    Ativo bit not null,
    PRIMARY KEY (Id)
)
GO

create table dbo.Pergunta(
    Id int IDENTITY (1,1),
    IdTipoPergunta int,
    TxtPergunta varchar(300) NOT NULL,
    Ordem int not null,
    NotaMaxima int null,
    Ativo bit not null,
    PRIMARY KEY (Id),
    FOREIGN KEY(IdTipoPergunta) REFERENCES TipoPergunta(Id)
)
GO

USE CES
GO

INSERT INTO [dbo].[TipoPergunta]
(
    Tipo,
    ATIVO
)
VALUES
(
'Nota',
1
)
INSERT INTO [dbo].[TipoPergunta]
(
    Tipo,
    ATIVO
)
VALUES
(
    'Normal',
    1
)

INSERT INTO [dbo].[Pergunta]
(
    TxtPergunta,
    IdTipoPergunta,
    NotaMaxima,
    Ativo,
    Ordem
)
VALUES(
    'Qual a nota que você daria ao comprar em nosso site?',
    (select top 1 id from TipoPergunta where Tipo = 'Nota'),
    1,
    5,
    0
)


INSERT INTO [dbo].[Pergunta]
(
    TxtPergunta,
    IdTipoPergunta,
    Ativo,
    Ordem
)
VALUES(
    'Tem alguma sugestão de melhoria?',
    (select top 1 id from TipoPergunta where Tipo = 'Normal'),
    1,
    1
)

select * from Pergunta
select * from TipoPergunta