CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [name] VARCHAR(50) NOT NULL, 
    [player1] XML NOT NULL, 
    [player2] NCHAR(10) NOT NULL, 
    [game] NCHAR(10) NOT NULL
)
