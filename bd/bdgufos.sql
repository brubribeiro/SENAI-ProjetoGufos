CREATE DATABASE bdgufos;

USE bdgufos;

CREATE TABLE tipousuario(
		IdTipoUsuario INT IDENTITY PRIMARY KEY NOT NULL,
		Titulo VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE usuario(
		IdUsuario INT IDENTITY PRIMARY KEY NOT NULL,
		Nome VARCHAR(255) NOT NULL,
		Email VARCHAR(255) UNIQUE NOT NULL,
		Senha VARCHAR(255) NOT NULL,
		IdTipoUsuario INT FOREIGN KEY REFERENCES tipousuario(IdTipoUsuario),
);

CREATE TABLE localizacao(
		IdLocal INT IDENTITY PRIMARY KEY NOT NULL,
		CNPJ CHAR(14) UNIQUE NOT NULL,
		RazaoSocial VARCHAR(255) UNIQUE NOT NULL,
		Endereco VARCHAR(255) NOT NULL
);

CREATE TABLE categoria(
		IdCategoria INT IDENTITY PRIMARY KEY NOT NULL,
		Titulo VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE evento(
		IdEvento INT IDENTITY PRIMARY KEY NOT NULL,
		Titulo VARCHAR(255) NOT NULL,
		DataEvento DATETIME NOT NULL,
		AcessoLivre BIT DEFAULT (1) NOT NULL,
		IdCategoria INT FOREIGN KEY REFERENCES categoria(IdCategoria),
		IdLocal INT FOREIGN KEY REFERENCES localizacao(IdLocal)
);

CREATE TABLE presenca(
		IdPresenca INT IDENTITY PRIMARY KEY NOT NULL,
		PresencaStatus VARCHAR(255) NOT NULL,
		IdEvento INT FOREIGN KEY REFERENCES evento(IdEvento),
		IdUsuario INT FOREIGN KEY REFERENCES usuario(IdUsuario)
);

INSERT INTO tipousuario (Titulo) VALUES ('Administrador'),('Aluno');

INSERT INTO usuario (Nome,Email,Senha,IdTipoUsuario) VALUES ('Administrador', 'adm@adm.com','123',1), ('Ariel', 'ariel@email.com','123',2);

INSERT INTO localizacao(CNPJ, RazaoSocial, Endereco) VALUES ('3774819008189', 'Escola SENAI de Informática', 'Al. Barão de Limeira,539');

INSERT INTO categoria(Titulo) VALUES ('Desenvolvimento'),('HTML + CSS'),('Marketing');

INSERT INTO evento(Titulo, DataEvento, AcessoLivre, IdCategoria, IdLocal) VALUES ('C#','2019-08-07T18:00:00',0,2,1),('Estrutura Semântica',GETDATE(),1,2,1);

INSERT INTO presenca(PresencaStatus, IdEvento, IdUsuario) VALUES ('Aguardando',1,2),('Confirmado',1,1);