﻿using PagedList;
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
                if (priceMin >= 0 && priceMin < priceMax)
                {
                    model = model.Where(s => s.GiaBan >= priceMin && priceMax >= s.GiaBan).OrderBy(s => s.GiaBan).ToList();
                }
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "TenCD" || string.IsNullOrEmpty(searchString) && searchField == "TenCD")
            {
                model = model.Where(s => s.TenCD.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
                if (priceMin >= 0 && priceMin < priceMax)
                {
                    model = model.Where(s => s.GiaBan >= priceMin && priceMax >= s.GiaBan).OrderBy(s => s.GiaBan).ToList();
                }
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "TenNXB" || string.IsNullOrEmpty(searchString) && searchField == "TenNXB")
            {
                model = model.Where(s => s.TenNXB.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
                if (priceMin >= 0 && priceMin < priceMax)
                {
                    model = model.Where(s => s.GiaBan >= priceMin && priceMax >= s.GiaBan).OrderBy(s => s.GiaBan).ToList();
                }
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "GiaTang" || string.IsNullOrEmpty(searchString) && searchField == "GiaTang")
            {
                model = model.Where(s => s.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).OrderBy(s => s.GiaBan).ToList();
                if (priceMin >= 0 && priceMin < priceMax)
                {
                    model = model.Where(s => s.GiaBan >= priceMin && priceMax >= s.GiaBan).OrderBy(s => s.GiaBan).ToList();
                }
            }
            else if (!String.IsNullOrEmpty(searchString) && searchField == "GiaGiam" || string.IsNullOrEmpty(searchString) && searchField == "GiaGiam")
            {
                model = model.Where(s => s.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).OrderByDescending(s => s.GiaBan).ToList();
                if (priceMin >= 0 && priceMin < priceMax)
                {
                    model = model.Where(s => s.GiaBan >= priceMin && priceMax >= s.GiaBan).OrderBy(s => s.GiaBan).ToList();
                }
            }

            return model.ToPagedList(page, pageSize);
        }

        //public ChuDe GetChuDeByID(int id)
        //{
        //    return db.ChuDes.Find(id);
        //}

        //public NhaXuatBan GetNhaXuatBanByID(int id)
        //{
        //    return db.NhaXuatBans.Find(id);
        //}

        public string GetPathImageByID(int id)
        {
            var res = db.Saches.Where(x => x.ID == id).FirstOrDefault();
            return res.AnhBia;
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

        public int Create(string TenSach, int GiaBan, string AnhBia, int SoLuongTon, string TenCD, string TenNXB)
        {   
            int chudeID = GetIDByTenCD(TenCD);
            int nxbID = GetIDByTenNXB(TenNXB);
            object[] parameters =
            {
                new SqlParameter("@TenSach", TenSach),
                new SqlParameter("@GiaBan", GiaBan),
                new SqlParameter("@AnhBia", AnhBia),
                new SqlParameter("@SoLuongTon", SoLuongTon),
                new SqlParameter("@MaCD", chudeID),
                new SqlParameter("@MaNXB", nxbID)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertSach @TenSach, @GiaBan,  @AnhBia, @SoLuongTon, @MaCD, @MaNXB", parameters);

            return result;
        }

        public int Edit(int ID, string TenSach, int GiaBan, string AnhBia, int SoLuongTon, string TenCD, string TenNXB)
        {
            int chudeID = GetIDByTenCD(TenCD);
            int nxbID = GetIDByTenNXB(TenNXB);
            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@TenSach", TenSach),
                new SqlParameter("@GiaBan", GiaBan),
                new SqlParameter("@AnhBia", AnhBia),
                new SqlParameter("@SoLuongTon", SoLuongTon),
                new SqlParameter("@MaCD", chudeID),
                new SqlParameter("@MaNXB", nxbID)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateSach @ID, @TenSach, @GiaBan, @AnhBia, @SoLuongTon, @MaCD, @MaNXB", parameters);
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