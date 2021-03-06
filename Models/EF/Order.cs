namespace WebApplication.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Điền thông tin họ tên.")]
        public string ShipName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Cần nhập số điện thoại.")]
        public string ShipMobile { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Nhập địa chỉ giao hàng.")]
        public string ShipAddress { get; set; }

        [StringLength(100)]
        public string ShipEmail { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
