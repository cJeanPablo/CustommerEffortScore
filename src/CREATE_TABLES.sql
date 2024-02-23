
CREATE TABLE TipoPergunta(
    Id bigint IDENTITY(1,1),
    Tipo varchar(20),
    PRIMARY KEY (Id)
)

create table Pergunta(
    Id bigint IDENTITY (1,1),
    IdTipoPergunta bigint,
    TxtPergunta varchar(300) NOT NULL,
    Ordem int not null,
    Ativo int not null,
    PRIMARY KEY (Id),
    FOREIGN KEY(IdTipoPergunta) REFERENCES TipoPergunta(Id)
)
