USE BookStoreDB

--------CART TABLE----------

CREATE TABLE Cart
(
	CartId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	OrderQuantity INT DEFAULT 1,
	UserID INT FOREIGN KEY REFERENCES UserTable(UserId) ,
	BookId INT FOREIGN KEY REFERENCES Books(BookId) 
);


SELECT * FROM Cart

---------STORED PROCEDURES-----------

-----ADD-----

CREATE PROC spAddCart
(
@OrderQuantity INT,
@UserId INT,
@BookId INT
)
AS
BEGIN 
	IF(EXISTS(SELECT * FROM Books WHERE BookId=@BookId))
		BEGIN
			INSERT INTO Carts(OrderQuantity,UserId,BookId)
			VALUES(@OrderQuantity,@UserId,@BookId)
		END
	ELSE
		BEGIN
			SELECT 1
		END
END;


------GET-----

CREATE PROC spGetCartByUser
(
	@UserId INT
)
AS
BEGIN
	SELECT CartId,OrderQuantity,UserId,c.BookId,BookName,AuthorName,
	DiscountPrice,OriginalPrice,BookImage FROM Cart c join Books b ON c.BookId=b.BookId 
	WHERE UserId=@UserId;
END;

-----UPDATE-----

CREATE PROC spUpdateCart
(
	@OrderQuantity INT,
	@BookId INT,
	@UserId INT,
	@CartId INT
)
AS
BEGIN
UPDATE Cart SET BookId=@BookId,
				UserId=@UserId,
				OrderQuantity=@OrderQuantity
				WHERE CartId=@CartId;
END;

-----DELETE-----

CREATE PROC spDeleteCart
(
	@CartId INT,
	@BookId INT
)
AS
BEGIN
DELETE Carts WHERE
		CartId=@CartId and BookId=@BookId;
END;

SELECT * FROM Carts