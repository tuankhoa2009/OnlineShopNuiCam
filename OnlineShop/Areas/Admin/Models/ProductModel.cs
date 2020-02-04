using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web;
namespace OnlineShop.Areas.Admin.Models
{
    public class ProductModel
    {
        [Key]
        public long ID { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống")]
        [StringLength(250)]
        public string Name { get; set; }
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được bỏ trống")]
        [StringLength(50)]
        public string Code { get; set; }
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề không được bỏ trống")]
        [StringLength(250)]
        public string MetaTitle { get; set; }
        [Display(Name = "Mô tả")]
        [StringLength(500, ErrorMessage = "Mô tả không thể vượt quá 500 ký tự")]
        public string Description { get; set; }
        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Display(Name = "Hình ảnh thêm")]
        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Giá")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Danh mục sản phẩm")]
        [Required(ErrorMessage = "Danh mục sản phẩm không được bỏ trống")]
        public long? CategoryID { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}