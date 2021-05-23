namespace WebApplication.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaXuatBan")]
    public partial class NhaXuatBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaXuatBan()
        {
            Saches = new HashSet<Sach>();
        }

        public int ID { get; set; }

        [DisplayName("Tên nhà xuất bản")]
        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập tên nhà xuất bản")]
        public string TenNXB { get; set; }

        [DisplayName("Địa chỉ")]
        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Số điện thoại")]
        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string DienThoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
