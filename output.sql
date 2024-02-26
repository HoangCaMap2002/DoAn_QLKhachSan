IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [DichVu] (
        [Id] int NOT NULL IDENTITY,
        [TenDichVu] nvarchar(255) NULL,
        [GiaTien] float NULL,
        [GhiChu] nchar(10) NULL,
        CONSTRAINT [PK_DichVu] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [LoaiPhong] (
        [Id] int NOT NULL IDENTITY,
        [TenLoai] nvarchar(255) NULL,
        [GhiChu] nvarchar(255) NULL,
        CONSTRAINT [PK_LoaiPhong] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [Quyen] (
        [Id] int NOT NULL IDENTITY,
        [TenQuyen] nvarchar(255) NULL,
        [GhiChu] nvarchar(255) NULL,
        CONSTRAINT [PK_Quyen] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [TaiKhoan] (
        [TenDangNhap] varchar(100) NOT NULL,
        [MatKhau] varchar(255) NULL,
        [SoDienThoai] varchar(10) NULL,
        [NgaySinh] date NULL,
        [IsDelete] bit NULL,
        CONSTRAINT [PK_TaiKhoan] PRIMARY KEY ([TenDangNhap])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [TienNghiKhachSan] (
        [Id] int NOT NULL IDENTITY,
        [TenTienNghi] nvarchar(100) NULL,
        [ClassIcon] nvarchar(255) NULL,
        CONSTRAINT [PK_TienNghiKhachSan] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [TienNghiPhong] (
        [Id] int NOT NULL IDENTITY,
        [TenTienNghi] nvarchar(100) NULL,
        [ClassIcon] nvarchar(255) NULL,
        CONSTRAINT [PK_TienNghiPhong] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [TinhThanh] (
        [Id] int NOT NULL IDENTITY,
        [AnhDaiDien] nvarchar(255) NULL,
        [TenTinh] nvarchar(255) NULL,
        [GhiChu] nvarchar(255) NULL,
        [IsDelete] bit NULL,
        CONSTRAINT [PK_TinhThanh] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [ViTri] (
        [Id] int NOT NULL IDENTITY,
        [TenViTri] nvarchar(255) NULL,
        [GhiChu] nvarchar(255) NULL,
        CONSTRAINT [PK_ViTri] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [GiaoDich] (
        [Id] int NOT NULL IDENTITY,
        [IdDatPhong] int NULL,
        [TenDangNhap] varchar(100) NULL,
        [IdDichVu] int NULL,
        [SoLuong] int NULL,
        [ThoiGianGiaoDich] datetime NULL,
        CONSTRAINT [PK_GiaoDich] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GiaoDich_DichVu] FOREIGN KEY ([IdDichVu]) REFERENCES [DichVu] ([Id]),
        CONSTRAINT [FK_GiaoDich_TaiKhoan] FOREIGN KEY ([TenDangNhap]) REFERENCES [TaiKhoan] ([TenDangNhap])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [PhanQuyen] (
        [Id] int NOT NULL IDENTITY,
        [TenDangNhap] varchar(100) NOT NULL,
        [IdQuyen] int NOT NULL,
        CONSTRAINT [PK_PhanQuyen] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PhanQuyen_Quyen] FOREIGN KEY ([IdQuyen]) REFERENCES [Quyen] ([Id]),
        CONSTRAINT [FK_PhanQuyen_TaiKhoan] FOREIGN KEY ([TenDangNhap]) REFERENCES [TaiKhoan] ([TenDangNhap])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [KhachSan] (
        [Id] int NOT NULL IDENTITY,
        [IdTinhThanh] int NULL,
        [TenKhachSan] nvarchar(255) NULL,
        [DiaChi] nvarchar(255) NULL,
        [GioiThieu] nvarchar(max) NULL,
        [TieuDe] nvarchar(255) NULL,
        [GhiChu] nvarchar(255) NULL,
        [AnhDaiDien] nvarchar(255) NULL,
        [SoSao] int NULL,
        [NguoiQuanLy] varchar(100) NULL,
        [IsDelete] bit NULL,
        CONSTRAINT [PK_KhachSan] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_KhachSan_TaiKhoan] FOREIGN KEY ([NguoiQuanLy]) REFERENCES [TaiKhoan] ([TenDangNhap]),
        CONSTRAINT [FK_KhachSan_TinhThanh] FOREIGN KEY ([IdTinhThanh]) REFERENCES [TinhThanh] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [Phong] (
        [Id] int NOT NULL IDENTITY,
        [TenPhong] nvarchar(255) NULL,
        [IdViTri] int NULL,
        [TrangThai] bit NULL,
        [GiaPhong] float NULL,
        [IdLoaiPhong] int NULL,
        [IdKhachSan] int NULL,
        [IdTienNghiPhong] int NULL,
        [AnhDaiDien] nvarchar(255) NULL,
        [IsDelete] bit NULL,
        CONSTRAINT [PK_Phong] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Phong_KhachSan] FOREIGN KEY ([IdKhachSan]) REFERENCES [KhachSan] ([Id]),
        CONSTRAINT [FK_Phong_LoaiPhong] FOREIGN KEY ([IdLoaiPhong]) REFERENCES [LoaiPhong] ([Id]),
        CONSTRAINT [FK_Phong_TienNghiPhong] FOREIGN KEY ([IdTienNghiPhong]) REFERENCES [TienNghiPhong] ([Id]),
        CONSTRAINT [FK_Phong_ViTri] FOREIGN KEY ([IdViTri]) REFERENCES [ViTri] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [DatPhong] (
        [Id] int NOT NULL IDENTITY,
        [IdPhong] int NULL,
        [TenDangNhap] varchar(100) NULL,
        [BatDau] datetime NULL,
        [KetThuc] datetime NULL,
        [TongTien] float NULL,
        [ThanhToan] nvarchar(100) NULL,
        [Status] bit NULL,
        [HoVaTen] nvarchar(100) NULL,
        [SoDienThoai] varchar(10) NULL,
        [Email] nvarchar(100) NULL,
        [GhiChu] nvarchar(max) NULL,
        CONSTRAINT [PK_DatPhong] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DatPhong_Phong] FOREIGN KEY ([IdPhong]) REFERENCES [Phong] ([Id]),
        CONSTRAINT [FK_DatPhong_TaiKhoan] FOREIGN KEY ([TenDangNhap]) REFERENCES [TaiKhoan] ([TenDangNhap])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE TABLE [HinhAnh] (
        [Id] int NOT NULL IDENTITY,
        [UrlHinhAnh] nvarchar(255) NULL,
        [IdPhong] int NULL,
        [IsDelete] bit NULL,
        CONSTRAINT [PK_HinhAnh] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HinhAnh_Phong] FOREIGN KEY ([IdPhong]) REFERENCES [Phong] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_DatPhong_IdPhong] ON [DatPhong] ([IdPhong]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_DatPhong_TenDangNhap] ON [DatPhong] ([TenDangNhap]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_GiaoDich_IdDichVu] ON [GiaoDich] ([IdDichVu]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_GiaoDich_TenDangNhap] ON [GiaoDich] ([TenDangNhap]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_HinhAnh_IdPhong] ON [HinhAnh] ([IdPhong]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_KhachSan_IdTinhThanh] ON [KhachSan] ([IdTinhThanh]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_KhachSan_NguoiQuanLy] ON [KhachSan] ([NguoiQuanLy]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_PhanQuyen_IdQuyen] ON [PhanQuyen] ([IdQuyen]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_PhanQuyen_TenDangNhap] ON [PhanQuyen] ([TenDangNhap]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_Phong_IdKhachSan] ON [Phong] ([IdKhachSan]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_Phong_IdLoaiPhong] ON [Phong] ([IdLoaiPhong]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_Phong_IdTienNghiPhong] ON [Phong] ([IdTienNghiPhong]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    CREATE INDEX [IX_Phong_IdViTri] ON [Phong] ([IdViTri]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240226090848_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240226090848_InitialCreate', N'7.0.15');
END;
GO

COMMIT;
GO

