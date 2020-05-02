USE [C:\USERS\PIOTREK\SOURCE\REPOS\REJESTROSOBZAGINIONYCH\REJESTROSOBZAGINIONYCH\APP_DATA\BAZAOSOB.MDF]
GO

/****** Object: Table [dbo].[MissingPeople] Script Date: 02.05.2020 22:55:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MissingPeople] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [Imie]                NCHAR (50)      NOT NULL,
    [Nazwisko]            NCHAR (50)      NOT NULL,
    [MiejsceZamieszkania] NCHAR (100)     NOT NULL,
    [MiejsceZaginiecia]   NCHAR (100)     NOT NULL,
    [Wiek]                INT             NOT NULL,
    [Plec]                NCHAR (1)       NOT NULL,
    [Opis]                NCHAR (1000)    NOT NULL,
    [Zdjecie]             VARBINARY (MAX) NOT NULL
);


