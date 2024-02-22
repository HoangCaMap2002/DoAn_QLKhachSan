using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoAn_QLKhachSan.Models;

public partial class QuanLyKhachSanContext : DbContext
{
    public QuanLyKhachSanContext()
    {
    }

    public QuanLyKhachSanContext(DbContextOptions<QuanLyKhachSanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DatPhong> DatPhongs { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<GiaoDich> GiaoDiches { get; set; }

    public virtual DbSet<HinhAnh> HinhAnhs { get; set; }

    public virtual DbSet<KhachSan> KhachSans { get; set; }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TienNghiKhachSan> TienNghiKhachSans { get; set; }

    public virtual DbSet<TienNghiPhong> TienNghiPhongs { get; set; }

    public virtual DbSet<TinhThanh> TinhThanhs { get; set; }

    public virtual DbSet<ViTri> ViTris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=QuanLyKhachSan;User ID=SA;Password=Password123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatPhong>(entity =>
        {
            entity.ToTable("DatPhong");

            entity.Property(e => e.BatDau).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoVaTen).HasMaxLength(100);
            entity.Property(e => e.KetThuc).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ThanhToan).HasMaxLength(100);

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.IdPhong)
                .HasConstraintName("FK_DatPhong_Phong");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_DatPhong_TaiKhoan");
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.ToTable("DichVu");

            entity.Property(e => e.GhiChu)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TenDichVu).HasMaxLength(255);
        });

        modelBuilder.Entity<GiaoDich>(entity =>
        {
            entity.ToTable("GiaoDich");

            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ThoiGianGiaoDich).HasColumnType("datetime");

            entity.HasOne(d => d.IdDichVuNavigation).WithMany(p => p.GiaoDiches)
                .HasForeignKey(d => d.IdDichVu)
                .HasConstraintName("FK_GiaoDich_DichVu");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.GiaoDiches)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_GiaoDich_TaiKhoan");
        });

        modelBuilder.Entity<HinhAnh>(entity =>
        {
            entity.ToTable("HinhAnh");

            entity.Property(e => e.UrlHinhAnh).HasMaxLength(255);

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.HinhAnhs)
                .HasForeignKey(d => d.IdPhong)
                .HasConstraintName("FK_HinhAnh_Phong");
        });

        modelBuilder.Entity<KhachSan>(entity =>
        {
            entity.ToTable("KhachSan");

            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.NguoiQuanLy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachSan).HasMaxLength(255);
            entity.Property(e => e.TieuDe).HasMaxLength(255);

            entity.HasOne(d => d.IdTinhThanhNavigation).WithMany(p => p.KhachSans)
                .HasForeignKey(d => d.IdTinhThanh)
                .HasConstraintName("FK_KhachSan_TinhThanh");

            entity.HasOne(d => d.NguoiQuanLyNavigation).WithMany(p => p.KhachSans)
                .HasForeignKey(d => d.NguoiQuanLy)
                .HasConstraintName("FK_KhachSan_TaiKhoan");
        });

        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.ToTable("LoaiPhong");

            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.TenLoai).HasMaxLength(255);
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.ToTable("PhanQuyen");

            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdQuyenNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.IdQuyen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanQuyen_Quyen");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.TenDangNhap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanQuyen_TaiKhoan");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.ToTable("Phong");

            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.TenPhong).HasMaxLength(255);

            entity.HasOne(d => d.IdKhachSanNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.IdKhachSan)
                .HasConstraintName("FK_Phong_KhachSan");

            entity.HasOne(d => d.IdLoaiPhongNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.IdLoaiPhong)
                .HasConstraintName("FK_Phong_LoaiPhong");

            entity.HasOne(d => d.IdTienNghiPhongNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.IdTienNghiPhong)
                .HasConstraintName("FK_Phong_TienNghiPhong");

            entity.HasOne(d => d.IdViTriNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.IdViTri)
                .HasConstraintName("FK_Phong_ViTri");
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.ToTable("Quyen");

            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.TenQuyen).HasMaxLength(255);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TenDangNhap);

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TienNghiKhachSan>(entity =>
        {
            entity.ToTable("TienNghiKhachSan");

            entity.Property(e => e.ClassIcon).HasMaxLength(255);
            entity.Property(e => e.TenTienNghi).HasMaxLength(100);
        });

        modelBuilder.Entity<TienNghiPhong>(entity =>
        {
            entity.ToTable("TienNghiPhong");

            entity.Property(e => e.ClassIcon).HasMaxLength(255);
            entity.Property(e => e.TenTienNghi).HasMaxLength(100);
        });

        modelBuilder.Entity<TinhThanh>(entity =>
        {
            entity.ToTable("TinhThanh");

            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.TenTinh).HasMaxLength(255);
        });

        modelBuilder.Entity<ViTri>(entity =>
        {
            entity.ToTable("ViTri");

            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.TenViTri).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
