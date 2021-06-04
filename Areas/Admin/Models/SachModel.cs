using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication.Models._EF;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Models
{
    public class SachModel
    {
        private dbContext db = null;

        public SachModel()
        {
            db = new dbContext();
        }

        public List<_Sach> ListAll(int id)
        {
            //var list = db.Database.SqlQuery<_Sach>("SELECT * FROM Sach ORDER BY ID DESC").ToList();
            var list = db.Database.SqlQuery<_Sach>("USP_SelectDetailSach @ID = "+id+"").ToList();
            return list;
        }

        public List<TacGia> GetList_HoTenTG_IntoDropDownList()
        {
            return db.TacGias.ToList();
        }

        public List<ChuDe> GetList_TenCD_IntoDropDownList()
        {
            return db.ChuDes.ToList();
        }

        public List<NhaXuatBan> GetList_TenNXB_IntoDropDownList()
        {
            return db.NhaXuatBans.ToList();
        }

        public IEnumerable<_Sach> listAllPaging(int page, int pageSize, string searchString, string searchField, int priceMin, int priceMax)
        {
            //new   
            var model = db.Database.SqlQuery<_Sach>("USP_GetListSach").ToList();

            if (!String.IsNullOrEmpty(searchString) && searchField == "TenSach" || string.IsNullOrEmpty(searchString) && searchField == "TenSach")
            {
                model = model.Where(s => s.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "HoTenTG" || string.IsNullOrEmpty(searchString) && searchField == "HoTenTG")
            {
                model = model.Where(s => s.HoTenTG.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "TenCD" || string.IsNullOrEmpty(searchString) && searchField == "TenCD")
            {
                model = model.Where(s => s.TenCD.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "TenNXB" || string.IsNullOrEmpty(searchString) && searchField == "TenNXB")
            {
                model = model.Where(s => s.TenNXB.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "GiaTang" || string.IsNullOrEmpty(searchString) && searchField == "GiaTang")
            {
                model = model.Where(s => s.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).OrderBy(s => s.GiaBan).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "GiaGiam" || string.IsNullOrEmpty(searchString) && searchField == "GiaGiam")
            {
                model = model.Where(s => s.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).OrderByDescending(s => s.GiaBan).ToList();
            }

            if (priceMin > 0 && priceMax >= priceMin)
            {
                model = model.Where(x => x.GiaBan >= priceMin && x.GiaBan <= priceMax).OrderBy(x => x.GiaBan).ToList();
            }
            else if (priceMin >= 0 && priceMax == 0)
            {
                model = model.Where(x => x.GiaBan >= priceMin).OrderBy(x => x.GiaBan).ToList();
            }
            else if (priceMax >= 0 && priceMin == 0)
            {
                model = model.Where(x => x.GiaBan <= priceMax).OrderBy(x => x.GiaBan).ToList();
            }

            if (searchField == "GiaGiam")
            {
                model = model.OrderByDescending(x => x.GiaBan).ToList();
            }
            else if (searchField == "GiaTang")
            {
                model = model.OrderBy(x => x.GiaBan).ToList();
            }

            return model.ToPagedList(page, pageSize);
        }

        public string GetPathImageByID(int id)
        {
            var res = db.Saches.Where(x => x.ID == id).FirstOrDefault();
            return res.AnhBia;
        }

        public string GetPathImageBiaSauByID(int id)
        {
            var res = db.Saches.Where(x => x.ID == id).FirstOrDefault();
            return res.BiaSau;
        }

        public int GetIDSachByName(string name)
        {
            var res = db.Saches.ToList().Find(x => x.TenSach == name);
            if (res == null) return 0;
            else return res.ID;
        }

        public int GetIDTGByName(string name)
        {
            var res = db.TacGias.ToList().Find(x => x.HoTenTG == name);
            if (res == null) return 0;
            else return res.ID;
        }

        public int GetIDByTenCD(string tenCD)
        {
            var res = db.ChuDes.Where(x => x.TenCD == tenCD).FirstOrDefault();
            return res.ID;
        }

        public int GetIDByTenNXB(string tenNXB)
        {
            var res = db.NhaXuatBans.Where(x => x.TenNXB == tenNXB).FirstOrDefault();
            return res.ID;
        }

        public int GetIDTacGiaByIDSach(int idSach)
        {
            var res = db.ThamGias.Where(x => x.MaSach == idSach).FirstOrDefault();
            return res.MaTG;
        }

        public int Create(string TenSach, int GiaBan, string AnhBia, string BiaSau, int SoLuongTon, string HoTenTG,string TenCD, string TenNXB, string MoTa, string Detail)
        {   
            int chudeID = GetIDByTenCD(TenCD);
            int nxbID = GetIDByTenNXB(TenNXB);
            string MetaTitle = new MetaLink().nameToMeta(TenSach);
            object[] parameters =
            {
                new SqlParameter("@TenSach", TenSach),
                new SqlParameter("@GiaBan", GiaBan),
                new SqlParameter("@AnhBia", AnhBia),
                new SqlParameter("@BiaSau", BiaSau),
                new SqlParameter("@SoLuongTon", SoLuongTon),
                new SqlParameter("@MaCD", chudeID),
                new SqlParameter("@MaNXB", nxbID),
                new SqlParameter("@MoTa", MoTa),
                new SqlParameter("@Detail", Detail),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertSach @TenSach, @GiaBan,  @AnhBia, @BiaSau, @SoLuongTon, @MaCD, @MaNXB, @MoTa, @Detail, @MetaTitle", parameters);
            if (result > 0)
            {
                int MaSach = GetIDSachByName(TenSach);
                int MaTG = GetIDTGByName(HoTenTG);
                ThamGiaModel t = new ThamGiaModel();
                t.InsertThamGia(MaSach, MaTG);
            }
            return result;
        }

        public int Edit(int ID, string TenSach, int GiaBan, string AnhBia, string BiaSau, int SoLuongTon, string HoTenTG, string TenCD, string TenNXB, string MoTa, string Detail)
        {
            int chudeID = GetIDByTenCD(TenCD);
            int nxbID = GetIDByTenNXB(TenNXB);
            string MetaTitle = new MetaLink().nameToMeta(TenSach);
            var s = db.Saches.Where(x => x.ID == ID).FirstOrDefault();
            s.NgayCapNhat = DateTime.Now;
            var _date = s.NgayCapNhat;
            var MaTGCu = GetIDTacGiaByIDSach(ID);

            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@TenSach", TenSach),
                new SqlParameter("@GiaBan", GiaBan),
                new SqlParameter("@AnhBia", AnhBia),
                new SqlParameter("@BiaSau", BiaSau),
                new SqlParameter("@SoLuongTon", SoLuongTon),
                new SqlParameter("@MaCD", chudeID),
                new SqlParameter("@MaNXB", nxbID),
                new SqlParameter("@MoTa", MoTa),
                new SqlParameter("@Detail", Detail),
                new SqlParameter("@MetaTitle", MetaTitle),
                new SqlParameter("@NgayCapNhat", _date)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateSach @ID, @TenSach, @GiaBan,  @AnhBia, @BiaSau, @SoLuongTon, @MaCD, @MaNXB, @MoTa, @Detail, @MetaTitle, @NgayCapNhat", parameters);

            if (result > 0)
            {
                bool tacgiamoi = false;
                int MaSach = GetIDSachByName(TenSach);
                int MaTGMoi = GetIDTGByName(HoTenTG);
                ThamGiaModel t = new ThamGiaModel();

                if (t.AlreadyJoin(MaSach, MaTGMoi) == 0) tacgiamoi = true;
                if (tacgiamoi == true)
                {
                    t.DeleteThamGia(MaSach, MaTGCu);
                    t.InsertThamGia(MaSach, MaTGMoi);
                }
            }
            return result;
        }

        public int Delete(int id)
        {
            object[] parameters =
            {
                new SqlParameter("@ID", id)
            };
            int result = db.Database.ExecuteSqlCommand("USP_DeleteSach @ID", parameters);
            return result;
        }
    }
}