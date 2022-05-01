USE BookStoreDB

-------ADDRESS TYPE TABLE------

CREATE TABLE AddressType
(
	TypeId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TypeName VARCHAR(MAX) NOT NULL
);

INSERT INTO AddressType
VALUES('Home'),('Office'),('Other');

SELECT * FROM AddressType

------Addresses Table-------

CREATE TABLE Addresses
(
	AddressId INT IDENTITY(1,1) PRIMARY KEY,
	Address VARCHAR(MAX) NOT NULL,
	City VARCHAR(MAX) NOT NULL,
	State VARCHAR(MAX) NOT NULL,
	TypeId INT NOT NULL 
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId),
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES UsersTable(UserId),
);

------ADD-------

CREATE PROC spAddAddress
(
	@Address VARCHAR(MAX),
	@City VARCHAR(MAX) ,
	@State VARCHAR(MAX),
	@TypeId INT,
	@UserId INT
)
AS
BEGIN
	IF EXISTS(SELECT * FROM AddressType WHERE TypeId=@TypeId)
		BEGIN
			INSERT INTO Addresses
			VALUES(@Address, @City, @State, @TypeId, @UserId);
		END
	Else
		BEGIN
			SELECT 2
		END
END;

--------GET--------

CREATE PROC spGetAllAddresses
(
	@UserId INT
)
AS
BEGIN
	SELECT Address, City, State, a.UserId, b.TypeId
	FROM Addresses a
    INNER JOIN AddressType b on b.TypeId = a.TypeId 
	WHERE 
	UserId = @UserId;
END;

-------UPDATE------

CREATE PROC spUpdateAddress
(
	@AddressId INT,
	@Address VARCHAR(MAX),
	@City VARCHAR(MAX),
	@State VARCHAR(MAX),
	@TypeId INT,
	@UserId INT
)
AS
BEGIN
	IF EXISTS(SELECT * FROM AddressType WHERE TypeId = @TypeId)
		BEGIN
			UPDATE Addresses SET
			Address = @Address, 
			City = @City,
			State = @State, 
			TypeId = @TypeId,
			UserId = @UserId
			WHERE
				AddressId = @AddressId
		END
	Else
		BEGIN
			SELECT 2
		END
END;

-------Delete Address Stored Procedure-------

CREATE PROC spDeleteAddress
(
	@AddressId INT,
	@UserId INT
)
AS
BEGIN
	DELETE Addresses
	WHERE 
		AddressId=@AddressId and UserId=@UserId;
END;
