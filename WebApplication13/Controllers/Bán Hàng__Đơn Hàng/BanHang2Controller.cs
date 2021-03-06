﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
namespace WebApplication13.Controllers.User
{
    public class BanHang2Controller : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: BanHang2


        //Lấy Giỏ Hàng
        //======================================================================//
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        //======================================================================//
        //======================================================================//


        //Thêm Giỏ Hàng 
        //========================================================================//
        public ActionResult ThemGioHang(int gSanPhamId, string strURL)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.Find(n => n.gSanPhamId == gSanPhamId);
            if (sanpham == null)
            {
                sanpham = new GioHang(gSanPhamId);
                listGioHang.Add(sanpham);
                ViewBag.message = "Them Thanh Cong";
                return Redirect(strURL);
            }
            else
            {
                sanpham.gSoLuong++;
                return Redirect(strURL);
            }
        }
        //========================================================================//
        //======================================================================//

        //Toán tử
        //======================================================================//
        private int TongSoLuong()
        {
            int gTongSoLuong = 0;

            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                gTongSoLuong = listGioHang.Sum(n => n.gSoLuong);
            }
            return gTongSoLuong;
        }
        private float ChietKhau()
        {
            float gChietKhau = 0;

            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                gChietKhau = listGioHang.Sum(n => n.gGiamGia);
            }
            return gChietKhau;
        }

        private float TongTien()
        {
            float gTongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                gTongTien = listGioHang.Sum(n => n.gThanhTien);
            }
            return gTongTien;
        }
        public ActionResult TenCuaHang(int gSanPhamId)
        {

            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.gSanPhamId == gSanPhamId);
            if (listGioHang != null)
            {
                db.cuaHangs.Where(p => p.CuaHangId.Equals(User.TenCuaHang())).Select(p => p.TenCuaHang).ToList();
            }
            return View(listGioHang);
        }



        //======================================================================//
        //======================================================================//


        //Giỏ Hàng
        //======================================================================//
        public ActionResult GioHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            if (listGioHang.Count == 0)
            {
                ViewBag.message = "";
                return RedirectToAction("DS", "BanHang2");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        //======================================================================//
        //======================================================================//




        // Xoá Giỏ Hàng
        //======================================================================//

        public ActionResult XoaGioHang(int gSanPhamId)
        {
            //Lấy giỏ hànng từ session
            List<GioHang> listGioHanng = LayGioHang();
            GioHang sanpham = listGioHanng.SingleOrDefault(n => n.gSanPhamId == gSanPhamId);
            if (sanpham != null)
            {
                listGioHanng.RemoveAll(n => n.gSanPhamId == gSanPhamId);
                return RedirectToAction("GioHang");
            }
            if (listGioHanng.Count == 0)
            {
                return RedirectToAction("TrangChu", "Admin");
            }
            return RedirectToAction("GioHang");
        }

        //======================================================================//
        //======================================================================//

        //Cập Nhật Giỏ Hàng
        //======================================================================//

        public ActionResult CapNhatGioHang(int gSanPhamId, FormCollection f)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.gSanPhamId == gSanPhamId);
            if (sanpham != null)
            {
                sanpham.gGiamGia = float.Parse(f["txtGiamGia"]);
                sanpham.gSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");


        }
        //======================================================================//
        //======================================================================//
        [HttpGet]
        public ActionResult MuaHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("DS", "BanHang2");
            }
            List<GioHang> ListGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(ListGioHang);
        }

        [HttpPost]
        public ActionResult MuaHang(FormCollection collection)
        {
            string s = collection["inputtext"];
            var MaKH = db.KhachHangs.SingleOrDefault(n => n.SoDT == s).KhachHangId;


            DonHang DH = new DonHang();
            List<GioHang> gh = LayGioHang();
            var MaCH = int.Parse(User.TenCuaHang());
            DH.KhachHangId = MaKH;
            DH.CuaHangId = MaCH;
            DH.NgayMua = DateTime.Now;
            DH.SoLuongBan = TongSoLuong();
            DH.TongTien = TongTien();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            db.DonHangs.Add(DH);

            foreach (var item in gh)
            {
                CTDonHang CTDH = new CTDonHang();
                CTDH.DonhangId = DH.DonHangId;
                CTDH.SanPhamId = item.gSanPhamId;
                CTDH.SoLuong = item.gSoLuong;
                CTDH.DonGia = item.gDonGia;
                CTDH.GiamGia = item.gGiamGia;
                db.CTDonHangs.Add(CTDH);

                var get = db.SanPhams.SingleOrDefault(n => n.SanPhamId == item.gSanPhamId);
                get.SoLuong = (get.SoLuong - item.gSoLuong);
                db.Entry(get).State = EntityState.Modified;
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("DS", "BanHang2");
        }
        //Danh Sách Sản Phẩm
        //======================================================================//
        [Authorize]
        public ActionResult DS()
        {
            var MaCH = int.Parse(User.TenCuaHang());
            var sanpham = db.SanPhams.Where(n => n.KhoHangId == MaCH).Include(s => s.LoaiSP);
            return View(sanpham.ToList());
        }
        //======================================================================//
        //======================================================================//

    }


}




