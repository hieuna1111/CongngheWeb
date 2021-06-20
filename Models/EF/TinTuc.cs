namespace WebApplication.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        public int ID { get; set; }

        [StringLength(500)]
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề cho tin tức.")]
        public string Name { get; set; }

        [StringLength(500)]
        public string Descriptions { get; set; }

        [StringLength(300)]
        [DisplayName("Ảnh")]
        public string Image { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? MaNXB { get; set; }

        public int? MaCD { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Nội dung")]
        //[AllowHtml]
        public string Detail { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool? Status { get; set; }

        public int? ViewCount { get; set; }
    }
}
