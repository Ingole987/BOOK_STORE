CREATE DATABASE BookStoreDB

USE BookStoreDB

----------------------------------------------------------------
CREATE TABLE UserTable
(
UserID INT  PRIMARY KEY  IDENTITY not null,
FullName VARCHAR (50) not null,
EmailID VARCHAR (50) UNIQUE not null,
Password VARCHAR (50) UNIQUE not null,
PhoneNumber INT UNIQUE not null
)

SELECT * FROM UserTable