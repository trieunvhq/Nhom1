-- tên Database: Quản lý quán ăn nhanh
-- Các bảng sử dụng:
-- 1.	Nhân viên (Mã nhân viên, Họ tên, Ngày sinh, Số điện thoại)
-- 2.	Món ăn (Mã món, Tên món, Đơn giá)
-- 3.	Order (Mã order, Tên order, thời gian Order, trạng thái thanh toán, Ghi chú)
-- 4.	Đơn hàng (Mã_order, Mã món, Số lượng, Đơn giá, Ghi chú)
-- 5.	Thanh toán (Mã nhân viên, Mã order, Tổng tiền, Giảm giá, Thời gian thanh toán)

CREATE DATABASE QuanLyQuanAnNhanh
GO
USE QuanLyQuanAnNhanh
GO
CREATE TABLE NHANVIEN
(
	maNV INT IDENTITY PRIMARY KEY,
	hoTenNV NVARCHAR(100) NOT NULL DEFAULT N'No Name',
	ngaySinhNV DATE NOT NULL DEFAULT ('1/1/1900'),
	soDTNV NVARCHAR(50)
)
GO

CREATE TABLE MONAN
(
	maMon INT IDENTITY PRIMARY KEY,
	tenMOn NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên món',
	donGia DECIMAL NOT NULL DEFAULT 0
)
GO

CREATE TABLE tabORDER
(
	maOrder INT IDENTITY PRIMARY KEY,
	tenOrder NVARCHAR(100) NOT NULL DEFAULT N'Chưa có Order',
	timeOrder DATETIME NOT NULL DEFAULT GETDATE(),
	status NVARCHAR(50) NOT NULL DEFAULT N'Chưa thanh toán',
	ghiChu NVARCHAR(150)
)
GO

CREATE TABLE DONHANG
(
	maOrder INT FOREIGN KEY REFERENCES dbo.tabORDER(maOrder),
	maMon INT FOREIGN KEY REFERENCES dbo.MONAN(maMon),
	soLuong INT NOT NULL DEFAULT 1,
	donGia DECIMAL NOT NULL DEFAULT 0,
	thanhTien DECIMAL NOT NULL DEFAULT 0,
	ghiChu NVARCHAR(150),
	CONSTRAINT khoaChinhDH PRIMARY KEY (maOrder, maMon) -- Tạo khóa chính nhiều trường
)
GO

CREATE TABLE THANHTOAN
(
	maOrder INT FOREIGN KEY REFERENCES dbo.tabORDER(maOrder),
	maNV INT NOT NULL FOREIGN KEY REFERENCES dbo.NHANVIEN(maNV),
	giamGia INT NOT NULL DEFAULT 0,
	tongTien DECIMAL NOT NULL DEFAULT 0,
	timeThanhToan DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT khoaChinhTT PRIMARY KEY (maOrder)
)
GO

CREATE TABLE ACOUNT
(
	ID INT IDENTITY PRIMARY KEY,
	maNV INT NOT NULL FOREIGN KEY REFERENCES dbo.NHANVIEN(maNV),
	UserName NVARCHAR(100) NOT NULL DEFAULT N'Chưa nhập tên sử dụng',
	Pasword NVARCHAR(1000) NOT NULL DEFAULT N'123456',
	typeAcount NVARCHAR(50) NOT NULL DEFAULT N'Tài khoản thường' -- typeAcount = 1: Admin; typeAcount =0: staff
)
GO

---------------------------------------------
--DBCC CHECKIDENT ('[tabORDER]', RESEED, 0)  -- reset giá trị của identity về 0

DECLARE @i INT =1
WHILE @i <=500
BEGIN
	INSERT dbo.tabORDER ( tenOrder) VALUES  ( N'Order ' + CAST(@i AS NVARCHAR(100)))
	SET @i = @i + 1
END

GO
----------------------------------------------
INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Văn Triệu', -- hoTenNV - nvarchar(100)
          ('12/12/1989'), -- ngaySinhNV - date
          N'094 860 93 68'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Đinh Đình Hoàng', -- hoTenNV - nvarchar(100)
          ('8/17/1999'), -- ngaySinhNV - date
          N'098 860 93 58'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Bùi Văn Tiến', -- hoTenNV - nvarchar(100)
          ('7/27/1990'), -- ngaySinhNV - date
          N'098 860 93 58'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Uông Sĩ Phượng', -- hoTenNV - nvarchar(100)
          ('6/6/1980'), -- ngaySinhNV - date
          N'098 218 46 81'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Thị Mận', -- hoTenNV - nvarchar(100)
          ('12/22/1989'), -- ngaySinhNV - date
          N'098 218 46 82'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Nha Bắc', -- hoTenNV - nvarchar(100)
          ('8/9/1999'), -- ngaySinhNV - date
          N'098 218 46 83'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Bùi Thúy Vân', -- hoTenNV - nvarchar(100)
          ('5/27/1990'), -- ngaySinhNV - date
          N'098 218 46 84'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Đoàn Thanh Toàn', -- hoTenNV - nvarchar(100)
          ('6/16/2000'), -- ngaySinhNV - date
          N'097 235 73 19'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Thị Vân', -- hoTenNV - nvarchar(100)
          ('12/22/1989'), -- ngaySinhNV - date
          N'097 235 73 11'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Đinh Văn Hoàng', -- hoTenNV - nvarchar(100)
          ('12/27/1989'), -- ngaySinhNV - date
          N'097 235 73 12'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Hồ Phương', -- hoTenNV - nvarchar(100)
          ('7/7/1990'), -- ngaySinhNV - date
          N'08 8888 9008'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Mai Văn Thái', -- hoTenNV - nvarchar(100)
          ('6/16/1990'), -- ngaySinhNV - date
          N'08 8888 9001'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Thị Mận', -- hoTenNV - nvarchar(100)
          ('12/12/1989'), -- ngaySinhNV - date
          N'098 300 22 91'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Giang Ngọc Dân', -- hoTenNV - nvarchar(100)
          ('8/9/1999'), -- ngaySinhNV - date
          N'098 300 22 92'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Bạch Văn Tuấn', -- hoTenNV - nvarchar(100)
          ('5/17/1990'), -- ngaySinhNV - date
          N'098 300 22 93'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Đỗ Minh Trí', -- hoTenNV - nvarchar(100)
          ('6/26/2000'), -- ngaySinhNV - date
          N'098 300 22 94'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Thị Vân', -- hoTenNV - nvarchar(100)
          ('12/12/1989'), -- ngaySinhNV - date
          N'098 300 22 95'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Đào Tiến Dần', -- hoTenNV - nvarchar(100)
          ('8/17/1999'), -- ngaySinhNV - date
          N'098 300 22 96'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Đinh Đình Hà', -- hoTenNV - nvarchar(100)
          ('7/27/1990'), -- ngaySinhNV - date
          N'098 300 22 97'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Thế Linh', -- hoTenNV - nvarchar(100)
          ('6/6/1980'), -- ngaySinhNV - date
          N'098 300 22 98'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Đình Vũ', -- hoTenNV - nvarchar(100)
          ('12/22/1989'), -- ngaySinhNV - date
          N'098 300 22 99'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Ngô Hữu Lộc', -- hoTenNV - nvarchar(100)
          ('8/9/1999'), -- ngaySinhNV - date
          N'098 300 22 10'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Thế Hanh', -- hoTenNV - nvarchar(100)
          ('5/27/1990'), -- ngaySinhNV - date
          N'098 300 22 20'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Bùi Thu Trang', -- hoTenNV - nvarchar(100)
          ('6/16/2000'), -- ngaySinhNV - date
          N'098 300 22 30'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Phạm Văn Vân', -- hoTenNV - nvarchar(100)
          ('12/22/1989'), -- ngaySinhNV - date
          N'098 680 78 26'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Ngọc Khánh', -- hoTenNV - nvarchar(100)
          ('12/27/1989'), -- ngaySinhNV - date
          N'098 7803075'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Phạm Duy Trường', -- hoTenNV - nvarchar(100)
          ('7/7/1990'), -- ngaySinhNV - date
          N'096 802 66 35'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Đào Văn Nhung', -- hoTenNV - nvarchar(100)
          ('6/16/1990'), -- ngaySinhNV - date
          N'098 920 72 82'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Thị Đào', -- hoTenNV - nvarchar(100)
          ('12/12/1989'), -- ngaySinhNV - date
          N'097 988 44 84'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Trần Văn C', -- hoTenNV - nvarchar(100)
          ('8/9/1999'), -- ngaySinhNV - date
          N'098 393 38 18'  -- soDTNV - nvarchar(50)
        )

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Bạch Văn Tuấn', -- hoTenNV - nvarchar(100)
          ('8/9/1989'), -- ngaySinhNV - date
          N'091 246 55 65'  -- soDTNV - nvarchar(50)
		)

INSERT INTO dbo.NHANVIEN
        ( hoTenNV, ngaySinhNV, soDTNV )
VALUES  ( N'Nguyễn Văn A', -- hoTenNV - nvarchar(100)
          ('6/26/2000'), -- ngaySinhNV - date
          N'097 848 01 87'  -- soDTNV - nvarchar(50)
		)

------------------------
INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Gà luộc', -- tenMOn - nvarchar(100)
          200000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Gà rán', -- tenMOn - nvarchar(100)
          200000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Gà rang muối', -- tenMOn - nvarchar(100)
          200000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Gà quay', -- tenMOn - nvarchar(100)
          200000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Vịt luộc', -- tenMOn - nvarchar(100)
          150000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Vịt rán', -- tenMOn - nvarchar(100)
          150000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Vịt rang muối', -- tenMOn - nvarchar(100)
          150000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Vịt quay', -- tenMOn - nvarchar(100)
          150000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Rau muống xào tỏi', -- tenMOn - nvarchar(100)
          50000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Rau muống luộc', -- tenMOn - nvarchar(100)
          30000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Rau cải xào', -- tenMOn - nvarchar(100)
          50000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Rau cải luộc', -- tenMOn - nvarchar(100)
          30000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Đậu lướt ván', -- tenMOn - nvarchar(100)
          30000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Canh cua', -- tenMOn - nvarchar(100)
          50000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Cà muối', -- tenMOn - nvarchar(100)
          15000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Dưa muối', -- tenMOn - nvarchar(100)
          15000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Thịt rang cháy cạnh', -- tenMOn - nvarchar(100)
          100000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Thịt kho tàu', -- tenMOn - nvarchar(100)
          100000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Thịt gà rang gừng', -- tenMOn - nvarchar(100)
          150000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Vịt om sấu', -- tenMOn - nvarchar(100)
          200000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Lẩu Thái', -- tenMOn - nvarchar(100)
          300000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Lẩu Hải sản', -- tenMOn - nvarchar(100)
          300000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Ếch om chuối đậu', -- tenMOn - nvarchar(100)
          150000  -- donGia - decimal
        )

INSERT INTO	dbo.MONAN
        ( tenMOn, donGia )
VALUES  ( N'Ốc bươu hấp', -- tenMOn - nvarchar(100)
          150000  -- donGia - decimal
        )
GO



-------------------------------------------------
INSERT INTO	dbo.ACOUNT
        ( maNV ,
          UserName ,
          Pasword ,
          typeAcount
        )
VALUES  ( 1 , -- maNV - int
          N'trieunv' , -- UserName - nvarchar(100)
          N'123456' , -- Pasword - nvarchar(1000)
          N'Admin'  -- typeAcount - int
        )

INSERT INTO	dbo.ACOUNT
        ( maNV ,
          UserName ,
          Pasword ,
          typeAcount
        )
VALUES  ( 2 , -- maNV - int
          N'hoangdd' , -- UserName - nvarchar(100)
          N'123456' , -- Pasword - nvarchar(1000)
          N'Tài khoản thường'  -- typeAcount - int
        )

INSERT INTO	dbo.ACOUNT
        ( maNV ,
          UserName ,
          Pasword ,
          typeAcount
        )
VALUES  ( 3 , -- maNV - int
          N'tienbv' , -- UserName - nvarchar(100)
          N'123456' , -- Pasword - nvarchar(1000)
          N'Tài khoản thường'  -- typeAcount - int
        )

INSERT INTO	dbo.ACOUNT
        ( maNV ,
          UserName ,
          Pasword ,
          typeAcount
        )
VALUES  ( 4 , -- maNV - int
          N'phuongus' , -- UserName - nvarchar(100)
          N'123456' , -- Pasword - nvarchar(1000)
          N'Tài khoản thường'  -- typeAcount - int
        )

INSERT INTO	dbo.ACOUNT
        ( maNV ,
          UserName ,
          Pasword ,
          typeAcount
        )
VALUES  ( 5 , -- maNV - int
          N'mannt' , -- UserName - nvarchar(100)
          N'123456' , -- Pasword - nvarchar(1000)
          N'Tài khoản thường'  -- typeAcount - int
        )

INSERT INTO	dbo.ACOUNT
        ( maNV ,
          UserName ,
          Pasword ,
          typeAcount
        )
VALUES  ( 6 , -- maNV - int
          N'bacnn' , -- UserName - nvarchar(100)
          N'123456' , -- Pasword - nvarchar(1000)
          N'Admin'  -- typeAcount - int
        )
GO