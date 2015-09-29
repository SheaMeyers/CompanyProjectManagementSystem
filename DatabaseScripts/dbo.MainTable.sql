CREATE TABLE [dbo].[MainTable] (
    [abcId]        NCHAR(100) NOT NULL,
    [title]        NCHAR(100) NULL,
    [description]  NCHAR(100) NULL,
    [vendor]       NCHAR(100) NULL,
    [listPrice]    NCHAR(100) NULL,
    [Cost]         NCHAR(100) NULL,
    [Status]       NCHAR(100) NULL,
    [Location]     NCHAR(100) NULL,
    [dateCreated]  NCHAR(100) NULL,
    [dateRecieved] NCHAR(100) NULL,
    PRIMARY KEY CLUSTERED ([abcId] ASC)
);