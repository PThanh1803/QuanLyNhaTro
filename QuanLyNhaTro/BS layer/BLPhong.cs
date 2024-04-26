﻿using QuanLyNhaTro;
using QuanLyNhaTro.DB_layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaTro.BS_layer
{
     class BLPhong
    {
        DBMain db = null;
        string err;

        public BLPhong()
        {
            db = new DBMain();
        }
        public DataTable LayPhongtrong(string makv)
        {
            string sql = "SELECT lp.MaLoaiPhong, lp.TenLoaiPhong, lp.DienTichPhong, lp.DonGia, p.TrangThai " +
                         "FROM LoaiPhong lp " +
                         "INNER JOIN Phong p ON p.MaLoaiPhong = lp.MaLoaiPhong " +
                         "WHERE p.MaKhuVuc = N'" + makv + "' AND p.TrangThai = N'Trống' " +
                         "GROUP BY lp.MaLoaiPhong, lp.TenLoaiPhong, lp.DienTichPhong, lp.DonGia, p.TrangThai";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
           
        }

        public DataTable LayLoaiPhong()
        {
            string sql = "Select * from LoaiPhong ";
            return db.ExecuteQueryDataSet(sql,CommandType.Text);
            
        }

        public DataTable LayPhongtrong(string makv, string loaiphong)
        {           
            string sql = "SELECT * FROM Phong " +
             "WHERE MaKhuVuc = N'" + makv + "' AND TrangThai = N'Trống' AND MaLoaiPhong = N'" + loaiphong + "'";
            return db.ExecuteQueryDataSet (sql,CommandType.Text);          
        }

       

        public DataTable LayPhongtrong_MaKV_TenP(string makv, string tenphong)
        {
            string sql = "SELECT * FROM Phong " +
             "WHERE MaKhuVuc = N'" + makv + "' AND TrangThai = N'Trống' AND TenPhong = N'" + tenphong + "'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
           
        }

        public DataTable LayPhong_MaP(string maphong)
        {
            string sql = "SELECT p.TenPhong, lp.TenLoaiPhong, lp.DienTichPhong, lp.DonGia " +
                         "FROM Phong p " +
                         "INNER JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong " +
                         "WHERE p.MaPhong = N'"+maphong +"'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
           
        }

        public DataTable Layphong_TT_MaKV(string trangthai,string makv)
        {
            string sql = "SELECT MaPhong, TenPhong " +
                 "FROM Phong " +
                 "WHERE TrangThai = N'"+trangthai +"' AND MaKhuVuc = N'"+makv+"'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
            
        }
        public bool UpdateTrangThai(string maphong,string trangthai ) 
        {
            string sqlString = "Update Phong  Set TrangThai=N'" +
                                trangthai + "' Where MaPhong =N'" + maphong + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);
           
        }

        public bool ThemPhong(string maphong,string maloaiphong,string khuvuc,string tenphong,string  day)
        {
            string sqlString = "Insert Into Phong Values(" + "'" +
                             maphong + "',N'" +
                             maloaiphong + "',N'" +
                             khuvuc + "',N'" +
                             tenphong + "',N'" +
                             day  + "',N'Trống',N'1')";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref err);           
        }
        public DataTable LayPhongChuaLapHD(string makv, int  thismonth , int  thisyear ) 
        {
            string sql = "SELECT * From Phong WHERE MaPhong not in (Select MaPhong From PhieuThu Where MONTH(NgayLap)="
                + thismonth + " AND YEAR(NgayLap)=" + thisyear + ") AND TrangThai=N'Đã thuê' AND MaKhuVuc='" + makv + "'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
            
            
        }
        public DataTable LayMaPhong(string tenphong, string Makv)
        {
            string sql = "select MaPhong from Phong where TenPhong =N'" + tenphong + "' and MaKhuVuc = N'" + Makv + "'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
           
        }

        public DataTable LayP_dathue_TTThue(string maphong)
        {
            string sql = "Select p.MaPhong,p.TenPhong,tp.NgayThue from Phong as p join ThongTinThuePhong as tp on tp.MaPhong = p.MaPhong "+
                           "where p.MaPhong = N'"+maphong+"' and p.TrangThai =N'Đã thuê'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
            
        }

        public DataTable LayPhongDaThue_MaKV_LP(string makv, string loaiphong)
        {
            string sql = "Select * from Phong " +
                "where MaKhuVuc = N'" + makv + "' and TrangThai = N'Đã thuê' and  MaLoaiPhong =N'"+loaiphong+"'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
            
        }

        public DataTable LayPhongDaThue_MaKV_TenP(string makv, string tenphong)
        {
            string sql = "Select * from Phong "+
                "where MaKhuVuc = N'" + makv + "' and TrangThai = N'Đã thuê' and  TenPhong =N'" + tenphong + "'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
           
        }

        public DataTable LayPhongDaThue_MaKV(string makv)
        {

            string sql = "select Maphong,TenPhong,MaLoaiPhong,Day from Phong " +
                "where MaKhuVuc = N'" + makv + "' and TrangThai = N'Đã thuê'";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
            
        }
    }
}
