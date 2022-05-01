USE BookStoreDB

-------BOOK TABLE-------

CREATE TABLE Books(
BookId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
BookName VARCHAR(MAX) NOT NULL,
AuthorName VARCHAR(250) NOT NULL,
Rating INT,
RatingCount INT,
DiscountPrice INT NOT NULL,
OriginalPrice INT NOT NULL,
Description VARCHAR(MAX) NOT NULL,
BookImage VARCHAR(250),
BookQuantity INT NOT NULL
);
---------STORED PROCEDURES-----------
SELECT * FROM BOOKS
------ADD BOOK------

CREATE PROC spAddBook
(
@BookName VARCHAR(MAX),
@AuthorName VARCHAR(250),
@Rating INT,
@RatingCount INT,
@DiscountPrice INT,
@OriginalPrice INT,
@Description VARCHAR(MAX),
@BookImage VARCHAR(250),
@BookQuantity INT
)
AS
BEGIN
INSERT INTO Books (BookName,AuthorName,Rating,RatingCount,DiscountPrice,OriginalPrice,Description,BookImage,BookQuantity)
VALUES(@BookName,@AuthorName,@Rating,@RatingCount,@DiscountPrice,@OriginalPrice,@Description,@BookImage,@BookQuantity);
END;

--------Update Book Stored Procedure--------

CREATE PROC spUpdateBook
(
@BookId INT,
@BookName VARCHAR(max),
@AuthorName VARCHAR(250),
@Rating INT,
@RatingCount INT,
@DiscountPrice INT,
@OriginalPrice INT,
@Description VARCHAR(max),
@BookImage VARCHAR(250),
@BookQuantity INT
)
AS
BEGIN
UPDATE Books SET 
BookName=@BookName,
AuthorName=@AuthorName,
Rating=@Rating,
RatingCount=@RatingCount,
DiscountPrice=@DiscountPrice,
OriginalPrice=@OriginalPrice,
Description=@Description,
BookImage=@BookImage,
BookQuantity=@BookQuantity
WHERE BookId=@BookId			
END;

--------DELETE BOOK--------

CREATE PROC spDeleteBook
(
@BookId INT
)
AS
BEGIN
DELETE FROM Books WHERE BookId=@BookId
END;

--------GET BY ID------

CREATE PROC spGetBookById
(
@BookId INT
)
AS 
BEGIN
SELECT * FROM Books WHERE BookId=@BookId
END;

-------GET ALL-------

CREATE PROC spGetAllBooks
AS 
BEGIN
SELECT * FROM Books
END;

