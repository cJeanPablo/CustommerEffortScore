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
