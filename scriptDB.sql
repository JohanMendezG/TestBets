
USE BetPlay
GO

IF OBJECT_ID('Bets') IS NOT NULL
	DROP TABLE Bets

IF OBJECT_ID('Roulettes') IS NOT NULL
	DROP TABLE Roulettes

IF OBJECT_ID('Users') IS NOT NULL
	DROP TABLE Users

CREATE TABLE Users(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(15) NOT NULL,
[Money] DECIMAL(10,2) NOT NULL
)

CREATE TABLE Roulettes(
Id INT PRIMARY KEY IDENTITY,
[State] BIT NOT NULL DEFAULT 'FALSE',
DateOpen VARCHAR(50) NULL,
DateClose VARCHAR(50) NULL
)

CREATE TABLE Bets(
Id INT PRIMARY KEY IDENTITY,
DateBet VARCHAR(50) NOT NULL,
RouletteId INT NOT NULL,
UserId INT NOT NULL,
BetNumber BIT NOT NULL,
Number INT NULL,
Color INT NULL,
[Money] DECIMAL(10,2) NOT NULL,
FOREIGN KEY(RouletteId) REFERENCES Roulettes(Id),
FOREIGN KEY(UserId) REFERENCES Users(Id),
)
GO

INSERT INTO [BetPlay].[dbo].[Users]([Name],Money) VALUES('User1',5000),('User2',3000),('User3',4000)



