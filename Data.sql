create database QL_QuanCaFe
go

use QL_QuanCaFe
go

--Food
--Table
--Food Category
--Account
--Bill
--Bill info


create table TableFood
(
	id int identity primary key,
	name nvarchar(100) not null, 
	status nvarchar(100) not null default N'Trống' --Trống || Có người
)
go

create table Account
(
	Id int identity primary key,
	UserName nvarchar(100) not null,
	DisplayName nvarchar(100) not null,
	Password nvarchar(100) not null,
	Type int not null  --1: admin || 0:nhân viên
)
go

create table FoodCategory
(
	id int identity primary key,
	name nvarchar(100) not null
)
go

create table Food
(
	id int identity primary key,
	name nvarchar(100) not null,
	idCategory int not null,
	price float not null default 0

	foreign key (idCategory) references FoodCategory(id)
)
go

create table Bill
(
	id int identity primary key,
	DateCheckIn date not null default GETDATE(),
	DateCheckOut date,
	idTable int not null,
	status int not null default 0 -- 1 là đã thanh toán / 0 là chưa thanh toán

	foreign key (idTable) references TableFood(id)
)
go


create table BillInfo
(
	id int identity primary key,
	idBill int not null,
	idFood int not null,
	count int not null default 0   --đếm số lượng món ăn

	foreign key (idBill) references Bill(id),
	foreign key (idFood) references Food(id)
)
go


-----------------------------------------------------------------------------------
--Nhập thông tin Account
INSERT INTO Account(UserName, DisplayName, Password, Type) VALUES
(N'K8', N'KhiK8', '1', 1),
(N'R9', N'RongK9', '1', 0);

go

--Tạo 1 proc lấy các account 
create proc USP_GetAccountByUserName
@userName nvarchar(100)
as
begin
	select * from Account where UserName = @userName
end
go

--Gọi proc
EXEC USP_GetAccountByUserName @userName = N'K8'
GO

--Tạo proc lấy thông tin Account nếu trùng tên đăng nhập, mk
create proc USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
as
begin
	select * from Account where UserName = @userName and Password = @passWord
end
go


------------------------------------------------------------------------------------

--Nhập thông tin Table
INSERT INTO TableFood(name) VALUES
(N'Bàn 1'),
(N'Bàn 2'),
(N'Bàn 3'),
(N'Bàn 4'),
(N'Bàn 5'),
(N'Bàn 6'),
(N'Bàn 7'),
(N'Bàn 8'),
(N'Bàn 9'),
(N'Bàn 10');
go

INSERT INTO TableFood(name) VALUES
(N'Bàn 11'),
(N'Bàn 12'),
(N'Bàn 13'),
(N'Bàn 14'),
(N'Bàn 15'),
(N'Bàn 16'),
(N'Bàn 17'),
(N'Bàn 18'),
(N'Bàn 19'),
(N'Bàn 20');
go

--Tạo 1 proc chứa danh sách các table
create proc USP_GetTableList
as 
	select * from TableFood
go

exec USP_GetTableList
go

----------------------------------------------------------------------------

--Nhập thông tin category
insert into FoodCategory(name) values
(N'Nước'),
(N'Bánh ngọt'),
(N'Bánh mặn'),
(N'Đồ ăn');
select * from FoodCategory

--Nhập thông tin món 
insert into Food(name, idCategory, price) values
(N'Cafe Sữa', 1, 20000),                 --1
(N'Cafe đen', 1, 15000),				 --2
(N'Trà vải', 1, 25000),					 --3
(N'Bánh bông lan trứng muối', 2, 35000), --4
(N'Bánh socola hạnh nhân', 2, 15000),	 --5
(N'Bánh bao', 3, 25000),				 --6
(N'Bánh xúc xích phô mai', 3, 28000),	 --7	
(N'Cơm chiên hải sản', 4, 60000),		 --8
(N'Phở', 4, 45000),						 --9
(N'Bún bò huế', 4, 45000);				 --10


select * from Food

--Nhập thông tin Bill
insert into Bill(DateCheckIn, DateCheckOut, idTable, status) values
(GETDATE(), null, 1, 0),
(GETDATE(), null, 2, 0),
(GETDATE(), null, 3, 0),
(GETDATE(), GETDATE(), 3, 1);


--Nhập thông tin BillInfo
insert into BillInfo(idBill, idFood, count) values
(1, 1, 1),
(1, 5, 1),
(2, 9, 1),
(2, 2, 1);


insert into BillInfo(idBill, idFood, count) values
(4, 5, 3),
(4, 6, 2),
(4, 2, 1);



--Lấy thông tin trong các bảng để đưa vào Menu, 
select Food.name, BillInfo.count, Food.price, (Food.price*BillInfo.count) as [totalPrice] from Bill, BillInfo, Food
where BillInfo.idBill = Bill.id and Food.id = BillInfo.idFood and Bill.status =0 and Bill.idTable = 3
go

-------------------------------------------------------------------------------------------------------------------------
--Tạo proc để đưa idTable vào Bill
--Đưa vào idtable, dựa vào id thêm vào bill của bàn đó 
create proc USP_InsertBill 
@idTable int
as
begin
	insert into Bill(DateCheckIn, DateCheckOut, idTable, status, discount) values
	(GETDATE(), null, @idTable, 0, 0)
end
go

--Thay đổi proc để làm Btn thêm món 
--Xử lý logic: 
--Phần if: nếu dang có billinfo rồi, sẽ tiếp tục thêm món 
--tạo 2 biến lưu lại idbillinfo và số lượng món hiện tại
--nếu có billinfo sẽ tiến hành tạo biến cộng mới để lưu số lượng hiện tại và số lượng mới thêm vào 
--thực hiện update 
--Phần else: nếu chưa có tiến hành thêm dữ liệu vào billinfo
create proc USP_InsertBillInfo
@idBill int, @idFood int, @count int 
as
begin
	declare @isExistBillInfo int
	declare @foodCount int = 1

	select @isExistBillInfo = id, @foodCount = count from BillInfo where idBill = @idBill and idFood = @idFood

	if(@isExistBillInfo > 0)
	begin
		declare @newCount int = @count + @foodCount  --Xử lí trường hợp âm
		if(@newCount > 0)
			update BillInfo set count = @foodCount + @count  where idBill = @idBill and idFood = @idFood
		else
			delete BillInfo where idBill = @idBill and idFood = @idFood
	end
	else
	begin
		insert BillInfo(idBill, idFood, count) values
			(@idBill, @idFood, @count)
	end
end
go

--Lấy Max id của bill để xác định được Btn thêm món đang insert vào bill nào
--Logic: khi tiến hành thanh toán, bill mới sẽ được tạo nên phải select max để lấy id mới nhất(vì id được tạo kiểu int identity nên sẽ tăng dần)
select max(id) from Bill
go

--Update để đổi status của bill 
update Bill set status = 1, DateCheckOut = GETDATE() where id = 1
go

----------------------------------------------------------------------------------------------
--Tạo trigger xử lý phần LoadTable để thay đổi các status của bàn
--Xử lý logic:
--Tạo ràng buộc thay đổi trạng thái bàn
--Tiến hành khai báo 2 biến idbill và idtable để lưu bàn đang được chọn để ngồi
--Nếu có thì count sẽ > 0 => Cập nhập trạng thái bàn thành có người 
--Trường hợp không có: cập nhập trạng thái bàn trống

create trigger UserTrigger_UpdateBillInfo --Cập nhập trạng thái bàn là "Có người"
on BillInfo for insert, update
as
begin
	declare @idBill int
	select @idBill = idBill from inserted --Lấy ra idBill từ BillInfo
	
	declare @idTable int
	select @idTable = idTable from Bill where Bill.id = @idBill and status = 0
	
	declare @count int 
	select @count = count(*) from BillInfo where idBill = @idBill

	if(@count > 0)
		update TableFood set status = N'Có người' where id = @idTable
	else
		update TableFood set status = N'Trống' where id = @idTable
			
end
go

--Tương tự như trên, tạo 1 cái ràng buộc khác tiện cho việc xử lý:
 --Cập nhập trạng thái bàn là "Trống" sau khi việc thanh toán được hoàn tất
create trigger UserTrigger_UpdateBill
on Bill for update
as
begin
	declare @idBill int
	select @idBill = id from inserted --Lấy ra id từ Bill

	declare @idTable int
	select @idTable = idTable from Bill where Bill.id = @idBill --Lấy idtable nơi mà idbill = idbill được lấy từ inserted
	
	declare @count int = 0
	select @count = count(*) from Bill where idTable = @idTable and status = 0

	if(@count = 0) --Từ ràng buộc trên khi đã trả về trống rồi thì count mặc định sẽ là 0 so sánh với count trong trigger này sẽ trả về bàn trống.
		update TableFood set status = N'Trống' where id = @idTable
	
end
go


---------------------------------------------------------------------------------------
--Xử lí phần Giảm giá
alter table Bill
add discount int
go
update Bill set discount = 0
select * from Bill
go

--Xử lí phần chuyển bàn
--Logic xử lý:
--Truyền vào 2 id bàn(idtable1:bàn đang sử dụng, idtable2: bàn đang muốn chuyển)
--Tạo idbill1, và idbill2

create proc USP_SwitchTable
@idTable1 int, @idTable2 int
as begin
	declare @idFirstBill int 
	declare @idSecondBill int

	declare @isFirstTableEmpty int = 1
	declare @isSecondTableEmpty int = 1

	select @idFirstBill = id from Bill where idTable = @idTable1 and status = 0 --Lấy idbill của table1, truyền vô @idFirstBill (Điều kiện: idtable = @idTable1)
	select @idSecondBill = id from Bill where idTable = @idTable2 and status = 0 --Lấy idbill của table2, truyền vô @idSecondBill (Điều kiện: idtable = @idTable2)
	
	---------------------------------------------------------------------------------------
	--FirstBill
	if(@idFirstBill is null) -- Nếu trường hợp kh có Bill nào, phải insert Bill(Không lấy được idBill từ table 1)
	begin
		insert Bill(DateCheckIn, DateCheckOut, idTable, status) values
		(GETDATE(), null, @idTable1, 0) --Mặc đinh mới tạo bill thì truyền vô idTable1 và status(tình trạng bàn) = 0

		select @idFirstBill = max(id) from Bill where idTable = @idTable1 and status = 0 --Mới tạo xong phải truyền vô idBill1 = Max(idBill) vì idBill kiêu int identity thì mới lấy id mới nhất được
	
		
	end
	select @isFirstTableEmpty = count(*) from BillInfo where idBill = @idFirstBill --Kiểm tra trường hợp Bàn có khách không thông qua việc đếm số lượng xuất hiện 

	--SecondBill
	if(@idSecondBill is null) -- Nếu trường hợp kh có Bill nào, phải insert Bill -- Tương tự với trường hợp FirstBill để chắc chắn có cả 2 idBill
	begin
		insert Bill(DateCheckIn, DateCheckOut, idTable, status) values
		(GETDATE(), null, @idTable2, 0)

		select @idSecondBill = max(id) from Bill where idTable = @idTable2 and status = 0
		
	end
	select @isSecondTableEmpty = count(*) from BillInfo where idBill = @idSecondBill
	---------------------------------------------------------------------------------------

	select id into IDBillInfoTable from BillInfo where idBill = @idSecondBill 
	--Lấy ra id của những bàn nằm trong Bill2 đưa vào table riêng
	
	update BillInfo set idBill = @idSecondBill where idBill = @idFirstBill 
	--Thực hiện cập nhâp chuyển bàn: chuyển BillInfo khi điều kiện  idBill = idBill1(được lấy từ table1), cập nhập idBill = idBill2
	
	update BillInfo set idBill = @idFirstBill where id in (select * from IDBillInfoTable)
	--Sau đó cập nhập lại  idbill2 cho idbill1 với Điều Kiện: idbill = idbill2 nằm trong table riêng
	
	drop table IDBillInfoTable

	if(@isFirstTableEmpty = 0) --Xử lý trường hợp không có khách qua việc select ở trên xong trả về đây
		update TableFood set status = N'Trống' where id = @idTable2
	if(@isSecondTableEmpty = 0)
		update TableFood set status = N'Trống' where id = @idTable1
end
go

-------------------------------------------------------------------------------------------------------------------------------------------------------
--Phần ADMIN
--Thêm 1 cột totalPrice vào Bill
alter table Bill
add finalTotalPrice float
go

alter table Bill
add totalPrice float
go


--Tạo 1 procedure lưu lại thông tin doanh thu của tất cả các ngày
--Dựa vào ngày vào, ngày ra, có thể thống kê và lấy dữ liệu của Bill tương ứng
create proc USP_GetListBillRevenueByDate
@checkIn date, @checkout date 
as
begin
	select Bill.id, TableFood.name as [Tên bàn],  DateCheckIn as [Ngày vào], DateCheckOut as [Ngày ra], Bill.totalPrice as [Tổng tiền], discount as [Giảm giá], Bill.finalTotalPrice as [Thành tiền]
							  from Bill, TableFood
							  where DateCheckIn >= @checkIn and DateCheckOut <= @checkout and TableFood.id = Bill.idTable and Bill.status = 1
end
go

--Tạo 1 procedure cập nhập thông tin cá nhân
--Truyền vào các biến tương ứng 
create proc USP_UpdateAccount
@userName nvarchar(100), @displayName nvarchar(100), @passWord nvarchar(100), @newPassWord nvarchar(100)
as 
begin
	declare @isRightPass int = 0 --Tạo 1 biến xác nhận pass = 0
	select @isRightPass = count(*) from Account where UserName = @userName and  Password = @passWord --Nếu lọc ra được tài khoản bằng với tên đăng nhập, mk => biến +1

	if(@isRightPass = 1)
	begin
		if(@passWord = null or @newPassWord = '') --Kiểm tra pass 
		begin
			update Account set DisplayName = @displayName where UserName = @userName --Cập nhập tên đăng nhập mới
		end
		else --Trường hơp có pass, có thể cập nhập tên mới, mk mới
			update Account set DisplayName = @displayName, Password = @newPassWord where UserName = @userName  
	end
end
go

----------------------------------------------------------------------------------------------
--Phần ADMIN
--Phần insert Food cho Button Thức ăn 
insert into Food(name, idCategory, price) values (N'', 0, 0.0)
--Phần update Food cho Button Sửa
update Food set name = N'', idCategory = 0, price = 0.0 where id = 0
--Phần delete Food cho Button Xóa
delete Food where id= 0
----------------------------------------------------------------------------------------------

--Phần insert FoodCategory cho Button insert Loại Thức ăn
insert into FoodCategory(name) values (N'')
--Phần update FoodCategory cho Button update Loại Thức ăn
update FoodCategory set name = N'' where id = 0
--Phần delete FoodCategory cho Button Xóa Loại Thức ăn
delete FoodCategory where id= 0
----------------------------------------------------------------------------------------------

--Phần insert Table cho Button insert Table
insert into TableFood(name, status) values (N'', N'')
--Phần update Table cho Button update Table
update TableFood set name = N'', status = N'' where id = 0
--Phần delete Table cho Button update Table
delete TableFood where id = 0
go

--Tạo trigger ràng buộc việc thêm bảng 
--Ràng buộc cho việc thêm, cập nhập bàn
create trigger USP_Insert_Update_TableFood
on TableFood for INSERT, UPDATE
AS
BEGIN
	DECLARE @idTable int
	SELECT @idTable = id from inserted
		
	DECLARE @count int = 0
	SELECT @count = count(*) from TableFood where status = N'Có người'

	if(@count > 0)
		update TableFood set status = N'Có người' where id = @idTable
END
drop trigger USP_Insert_Update_TableFood

 


