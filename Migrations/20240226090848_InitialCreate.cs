using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_QLKhachSan.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DichVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GiaTien = table.Column<double>(type: "float", nullable: true),
                    GhiChu = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quyen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    TenDangNhap = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.TenDangNhap);
                });

            migrationBuilder.CreateTable(
                name: "TienNghiKhachSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTienNghi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClassIcon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TienNghiKhachSan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TienNghiPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTienNghi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClassIcon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TienNghiPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TinhThanh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TenTinh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhThanh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViTri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenViTri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViTri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDatPhong = table.Column<int>(type: "int", nullable: true),
                    TenDangNhap = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IdDichVu = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    ThoiGianGiaoDich = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiaoDich_DichVu",
                        column: x => x.IdDichVu,
                        principalTable: "DichVu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiaoDich_TaiKhoan",
                        column: x => x.TenDangNhap,
                        principalTable: "TaiKhoan",
                        principalColumn: "TenDangNhap");
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IdQuyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanQuyen_Quyen",
                        column: x => x.IdQuyen,
                        principalTable: "Quyen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhanQuyen_TaiKhoan",
                        column: x => x.TenDangNhap,
                        principalTable: "TaiKhoan",
                        principalColumn: "TenDangNhap");
                });

            migrationBuilder.CreateTable(
                name: "KhachSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTinhThanh = table.Column<int>(type: "int", nullable: true),
                    TenKhachSan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GioiThieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SoSao = table.Column<int>(type: "int", nullable: true),
                    NguoiQuanLy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachSan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachSan_TaiKhoan",
                        column: x => x.NguoiQuanLy,
                        principalTable: "TaiKhoan",
                        principalColumn: "TenDangNhap");
                    table.ForeignKey(
                        name: "FK_KhachSan_TinhThanh",
                        column: x => x.IdTinhThanh,
                        principalTable: "TinhThanh",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Phong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdViTri = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
                    GiaPhong = table.Column<double>(type: "float", nullable: true),
                    IdLoaiPhong = table.Column<int>(type: "int", nullable: true),
                    IdKhachSan = table.Column<int>(type: "int", nullable: true),
                    IdTienNghiPhong = table.Column<int>(type: "int", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phong_KhachSan",
                        column: x => x.IdKhachSan,
                        principalTable: "KhachSan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phong_LoaiPhong",
                        column: x => x.IdLoaiPhong,
                        principalTable: "LoaiPhong",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phong_TienNghiPhong",
                        column: x => x.IdTienNghiPhong,
                        principalTable: "TienNghiPhong",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phong_ViTri",
                        column: x => x.IdViTri,
                        principalTable: "ViTri",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DatPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPhong = table.Column<int>(type: "int", nullable: true),
                    TenDangNhap = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    KetThuc = table.Column<DateTime>(type: "datetime", nullable: true),
                    TongTien = table.Column<double>(type: "float", nullable: true),
                    ThanhToan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatPhong_Phong",
                        column: x => x.IdPhong,
                        principalTable: "Phong",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatPhong_TaiKhoan",
                        column: x => x.TenDangNhap,
                        principalTable: "TaiKhoan",
                        principalColumn: "TenDangNhap");
                });

            migrationBuilder.CreateTable(
                name: "HinhAnh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlHinhAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdPhong = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhAnh_Phong",
                        column: x => x.IdPhong,
                        principalTable: "Phong",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatPhong_IdPhong",
                table: "DatPhong",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhong_TenDangNhap",
                table: "DatPhong",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_IdDichVu",
                table: "GiaoDich",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_TenDangNhap",
                table: "GiaoDich",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_IdPhong",
                table: "HinhAnh",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_KhachSan_IdTinhThanh",
                table: "KhachSan",
                column: "IdTinhThanh");

            migrationBuilder.CreateIndex(
                name: "IX_KhachSan_NguoiQuanLy",
                table: "KhachSan",
                column: "NguoiQuanLy");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_IdQuyen",
                table: "PhanQuyen",
                column: "IdQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_TenDangNhap",
                table: "PhanQuyen",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_Phong_IdKhachSan",
                table: "Phong",
                column: "IdKhachSan");

            migrationBuilder.CreateIndex(
                name: "IX_Phong_IdLoaiPhong",
                table: "Phong",
                column: "IdLoaiPhong");

            migrationBuilder.CreateIndex(
                name: "IX_Phong_IdTienNghiPhong",
                table: "Phong",
                column: "IdTienNghiPhong");

            migrationBuilder.CreateIndex(
                name: "IX_Phong_IdViTri",
                table: "Phong",
                column: "IdViTri");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatPhong");

            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.DropTable(
                name: "HinhAnh");

            migrationBuilder.DropTable(
                name: "PhanQuyen");

            migrationBuilder.DropTable(
                name: "TienNghiKhachSan");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "Phong");

            migrationBuilder.DropTable(
                name: "Quyen");

            migrationBuilder.DropTable(
                name: "KhachSan");

            migrationBuilder.DropTable(
                name: "LoaiPhong");

            migrationBuilder.DropTable(
                name: "TienNghiPhong");

            migrationBuilder.DropTable(
                name: "ViTri");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "TinhThanh");
        }
    }
}
