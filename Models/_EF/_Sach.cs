using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models._EF
{
    public class _Sach
    {
        public int ID { get; set; }

        [DisplayName("Tên sách")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sách")]
        public string TenSach { get; set; }

        [DisplayName("Giá bán")]
        [Required(ErrorMessage = "Bạn chưa nhập giá bán")]
        [Range(0, int.MaxValue)]
        public int GiaBan { get; set; }

        [DisplayName("Số lượng tồn")]
        [Required(ErrorMessage = "Bạn chưa nhập số lượng tồn kho")]
        [Range(0, int.MaxValue)]
        public int SoLuongTon { get; set; }

        [DisplayName(" Ảnh bìa trước")]
        public string AnhBia { get; set; }

        [DisplayName("Ảnh bìa sau")]
        public string BiaSau { get; set; }

        [DisplayName("Tác giả")]
        [Required(ErrorMessage = "Bạn chưa nhập tên tác giả")]

        public string HoTenTG { get; set; }

        [DisplayName("Chủ đề")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sách")]
        public string TenCD { get; set; }

        [DisplayName("Nhà xuất bản")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sách")]
        public string TenNXB { get; set; }

        [StringLength(500)]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Xem chi tiết")]
        public string Detail { get; set; }
    }
}