using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    public class ThongKeUserController : Controller
    {
        // GET: ThongKe1

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ThongKe
        public ActionResult ThongKe()
        {
            var MaCH = User.TenCuaHang();

            var Get = from s in db.DonHangs
                      where s.CuaHangId.ToString() == MaCH
                      group s by DbFunctions.TruncateTime(s.NgayMua) into g
                      select new Test<DateTime, int, int, float> { Key = g.Key.Value, TongDH = g.Count(), TongSL = g.Sum(s => s.SoLuongBan), TongTien = g.Sum(s => s.TongTien) };

            return View(Get.ToList());
        }
    }
}
