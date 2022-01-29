CREATE DATABASE CoffeManagement
GO

USE CoffeManagement
GO

--Account 
--TableFood
--Categories 
--Bill
--BillInfo
--Products
-- Link drive Database để không bị đổi font chữ :  
--https://docs.google.com/document/d/1sEJvUo57OFM4bOd0RBOrI1ay8k0oMyl2MdQMGqV3Whk/edit?usp=sharing 


CREATE TABLE Account 
(
     UserName nvarchar(100) PRIMARY KEY, -- Tên đăng nh?p không đư?c trùng
     DisplayName nvarchar (100) NOT NULL DEFAULT N'NhanVien', --Tên hi?n th?
     PassWord nvarchar (1000) NOT NULL DEFAULT 0,
     Type INT NOT NULL DEFAULT 0  -- 1:qu?n l?, 0: nhân viên
)

GO
CREATE TABLE TableFood 
(
     TableID int IDENTITY PRIMARY KEY,
	 Tablename nvarchar(100) NOT NULL DEFAULT N'Bàn chưa có tên',
     status nvarchar (100) NOT NULL DEFAULT N'Bàn tr?ng'    --Bàn tr?ng hay đ? có ngư?i
)

GO
CREATE TABLE Categories 
(
	 CategoryID int IDENTITY PRIMARY KEY ,
	 CategoryName nvarchar (50) NOT NULL DEFAULT N'Chưa đ?t tên'
)
GO
CREATE TABLE Products (
	 ProductID int IDENTITY PRIMARY KEY ,
	 ProductName nvarchar (50) NOT NULL DEFAULT N'Chưa đ?t tên',
	 Price float NOT NULL DEFAULT 0,
	 IDCategory int NOT NULL 

	 FOREIGN KEY (IDCategory) REFERENCES dbo.Categories (CategoryID)
)

GO
CREATE TABLE Bill 
(
	 BillID int IDENTITY PRIMARY KEY ,
	 DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	 DateCheckOut DATE ,
	 IDTable int NOT NULL,
	 status int NOT NULL DEFAULT 0   --1: đ? thanh toán , 0: chưa thanh toán
	 FOREIGN KEY (IDTable) REFERENCES dbo.TableFood (TableID)
)
GO
CREATE TABLE BillInfo 
(
     ID int IDENTITY PRIMARY KEY,
	 IDBill int NOT NULL ,
	 IDProduct int NOT NULL,
	 count int NOT NULL DEFAULT 0
	 FOREIGN KEY (IDBill) REFERENCES dbo.Bill (BillID),
	 FOREIGN KEY (IDProduct) REFERENCES dbo.Products (ProductID)

)

GO
insert into Account (UserName, DisplayName, PassWord, Type)
values 
(N'VanAnh1789', N'Nguy?n Vân Anh',N'adminva1506', 1),
(N'NgocThuy1791', N'Tr?n Ng?c Thu?',N'hellont1006',0),
(N'ThanhTin1790', N'Lê Thành Tín',N'hellott0106', 0)
GO
-- Thêm bàn
insert into TableFood (Tablename, status)
values
( N'Bàn 1', N'Bàn tr?ng'),
( N'Bàn 2', N'Bàn tr?ng'),
( N'Bàn 3', N'Bàn tr?ng'),
( N'Bàn 4', N'Bàn tr?ng'),
( N'Bàn 5', N'Bàn tr?ng'),
( N'Bàn 6', N'Bàn tr?ng'),
( N'Bàn 7', N'Bàn tr?ng'),
( N'Bàn 8', N'Bàn tr?ng'),
( N'Bàn 9', N'Bàn tr?ng'),
( N'Bàn 10', N'Bàn tr?ng'),
( N'Bàn 11', N'Bàn tr?ng'),
( N'Bàn 12', N'Bàn tr?ng'),
( N'Bàn 13', N'Bàn tr?ng'),
( N'Bàn 14', N'Bàn tr?ng'),
( N'Bàn 15', N'Bàn tr?ng')
GO
-- Thêm category
insert into Categories (CategoryName)
values
(N'Cafe'),
(N'Sinh t?'),
(N'Các lo?i trà'),
(N'Th?c u?ng khác')
GO
--Thêm món vào product
insert into Products ( ProductName, price, IDCategory)
values
(N'B?c x?u nóng', 30000, 1),
(N'Cafe đen đá', 25000, 1),
(N'Cafe s?a đá', 30000, 1),
(N'Cappuccino', 35000, 1),
(N'Late', 35000, 1),
(N'Sinh t? bơ', 30000, 2),
(N'Sinh t? d?a', 30000, 2),
(N'Sinh t? dâu', 30000, 2),
(N'Sinh t? m?ng c?u', 25000, 2),
(N'Sinh t? vi?t qu?t', 30000, 2),
(N'Trà đào', 20000, 3),
(N'Trà th?ch đào', 20000, 3),
(N'Trà th?ch v?i', 20000, 3),
(N'Trà s?a socola', 25000, 3),
(N'Trà s?a truy?n th?ng', 20000, 3),
(N'Sô-Cô-La', 30000, 4),
(N'Soda dâu', 25000, 4),
(N'Soda vi?t qu?t', 25000, 4),
(N'Matcha đá xay', 25000, 4),
(N'Chanh dây đá xay', 25000, 4)

GO
-- Store Proc Hi?n Th? Bàn
CREATE PROC GetTableList
as select * from dbo.TableFood
go

GO
-- Thêm Bill
create PROC InsertBill
@IDTable int
as
begin
   insert into Bill( DateCheckIn, DateCheckOut, IDTable, status)
   values
      ( GETDATE(),null, @IDTable, 0)
END

GO
-- BillInfo
CREATE PROC InsertBillInfo
@IDBill int, @IDProduct int, @count int
as
begin
    DECLARE @isExitsBillInfo int
	DECLARE @ProductCount int =1

	select @isExitsBillInfo = ID, @ProductCount = b.count 
	from dbo.BillInfo as b
	where IDBill = @IDBill AND IDProduct = @IDProduct

	IF( @isExitsBillInfo >0)
	begin
	   DECLARE @newCount int = @ProductCount + @count
	   if (@newCount > 0)
	       UPDATE dbo.BillInfo set count= @ProductCount + @count where IDProduct = @IDProduct
	   else 
	       DELETE dbo.BillInfo where IDBill = @IDBill AND IDProduct = @IDProduct
	end
	ELSE
	begin
	   insert into BillInfo(IDBill, IDProduct, count)
       values
        (@IDBill , @IDProduct, @count)
	end
END

GO
-- C?p nh?t
CREATE TRIGGER UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
    declare @IDBill int
	SELECT @IDBill = IDBill FROM Inserted
	declare @IDTable int
	SELECT @IDTable = IDTable FROM dbo.Bill Where BillID= @IDBill AND status = 0

	DECLARE @count int
    SELECT @count = COUNT(*) FROM dbo.BillInfo WHERE IDBill = @IDBill
	
	IF(@count > 0)
	BEGIN
	   UPDATE dbo.TableFood SET status = N'Có ngư?i' WHERE TableID = @IDTable 
	END
	ELSE
	BEGIN
	   UPDATE dbo.TableFood SET status= N'Bàn tr?ng' WHERE TableID = @IDTable
	END
END

GO
--C?p nh?t Bill
CREATE TRIGGER UpdateBill
ON dbo.Bill FOR UPDATE 
AS
BEGIN
    declare @IDBill int
	SELECT @IDBill = BillID FROM Inserted
	declare @IDTable int
	SELECT @IDTable = IDTable FROM dbo.Bill Where BillID= @IDBill
	declare @count int = 0
	SELECT @count = COUNT (*) FROM dbo.Bill WHERE IDTable = @IDTable AND status = 0
	if (@count = 0)
	    UPDATE dbo.TableFood SET status = N'Bàn tr?ng' WHERE TableID = @IDTable
END

GO
--Chuy?n bàn
CREATE PROC ChangeTableFood
@IDTable1 int, @IDTable2 int
as
begin
    DECLARE @IDOneBill int
	DECLARE @IDTwoBill int

	DECLARE @ISOneTable int = 0
	DECLARE @ISTwoTable int = 0

	select @IDTwoBill = BillID from dbo.Bill where IDTable = @IDTable2 AND status = 0
	select @IDOneBill = BillID from dbo.Bill where IDTable = @IDTable1 AND status = 0

	IF (@IDOneBill is NULL)
	BEGIN
	    insert into Bill (DateCheckIn, DateCheckOut, IDTable, status)
        values
            ( GETDATE(), NULL , @IDTable1, 0)
		SELECT @IDOneBill = MAX( BillID) FROM dbo.Bill where IDTable = @IDTable1 AND status = 0

	END

	Select @ISOneTable = COUNT(*) FROM dbo.BillInfo WHERE IDBill = @IDOneBill

	IF (@IDTwoBill is NULL)
	BEGIN
	    insert into Bill (DateCheckIn, DateCheckOut, IDTable, status)
        values
            ( GETDATE(), NULL , @IDTable2, 0)
		SELECT @IDTwoBill = MAX( BillID) FROM dbo.Bill where IDTable = @IDTable2 AND status = 0
	END

	Select @ISTwoTable = COUNT(*) FROM dbo.BillInfo WHERE IDBill = @IDTwoBill

    select ID INTO IDBillInfoTable From dbo.BillInfo where IDBill =  @IDTwoBill

	UPDATE dbo.BillInfo SET IDBill = @IDTwoBill WHERE IDBill =  @IDOneBill
	UPDATE dbo.BillInfo SET IDBill = @IDOneBill WHERE ID IN (SELECT * FROM IDBillInfoTable)

	drop table IDBillInfoTable

	IF ( @ISOneTable = 0)
	   UPDATE dbo.TableFood SET status = N'Bàn tr?ng' WHERE TableID = @IDTable2
	IF ( @ISTwoTable = 0)
	   UPDATE dbo.TableFood SET status = N'Bàn tr?ng' WHERE TableID = @IDTable1
end

GO
--Thêm c?t t?ng ti?n
ALTER TABLE dbo.Bill ADD totalPrice FLOAT

GO
--Hi?n th? danh sách bill
CREATE PROC GetListBillByDate
@checkIn date , @checkOut date
AS
BEGIN
    SELECT t.Tablename AS [Tên bàn], b.totalPrice AS [T?ng ti?n], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra]
	FROM dbo.Bill as b, dbo.TableFood as t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut<= @checkOut AND b.status = 1
	AND t.TableID = b.IDTable
END

GO
-- Xoá BillInfo
CREATE TRIGGER DeleteBillInfo
ON dbo.BillInfo FOR DELETE
AS
BEGIN
    DECLARE @IDBillInfo int
	DECLARE @IDBill int
	SELECT @IDBillInfo = ID, @IDBill = Deleted.IDBill FROM Deleted

   	DECLARE @IDTable int
	SELECT @IDTable = IDTable FROM dbo.Bill WHERE BillID = @IDBill

	DECLARE @count int = 0
	SELECT @count = COUNT(*) FROM dbo.BillInfo AS bi, dbo.Bill AS b WHERE b.BillID = bi.IDBill AND b.BillID = @IDBill AND b.status= 0
	if(@count = 0)
	   UPDATE dbo.TableFood SET status= N'Bàn tr?ng' WHERE TableID = @IDTable
END

GO
-- T?M KI?M TÊN MÓN
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưà???á??????????è???é????? ????í????ó??????????ù???ú?????????? Ă ĐÊÔƠƯÀ???Á??????????È???É?????????Í ????Ó??????????Ù???Ú??????????' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO

