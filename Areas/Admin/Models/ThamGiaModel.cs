using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Models
{
    public class ThamGiaModel
    {
        private dbContext db = null;
        public ThamGiaModel()
        {
            db = new dbContext();
        }

        public void InsertThamGia(int MaSach, int MaTG, string VaiTro = "Tác giả chính", string ViTri = null)
        {
            ThamGia t = new ThamGia();
            t.MaSach = MaSach;
            t.MaTG = MaTG;
            t.VaiTro = VaiTro;
            t.ViTri = ViTri;
            db.ThamGias.Add(t);
            db.SaveChanges();
        }

        public int AlreadyJoin(int masach, int matacgia)
        {
            var res = db.ThamGias.Where(x => x.MaSach == masach && x.MaTG == matacgia).FirstOrDefault();
            if (res != null) return 1;
            return 0;
        }

        public void DeleteThamGia(int masach, int matacgia)
        {
            var res = db.ThamGias.Where(x => x.MaSach == masach && x.MaTG == matacgia).FirstOrDefault();
            db.ThamGias.Remove(res);
            db.SaveChanges();
        }
    }
}