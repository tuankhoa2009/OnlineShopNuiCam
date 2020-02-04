using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class UserModel
    {
        [Key]
        public long ID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không thể bỏ trống")]
        public string UserName { get; set; }

        [StringLength(32)]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không thể bỏ trống")]
        public string Password { get; set; }

        [StringLength(50)]
        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên không thể bỏ trống")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không thể bỏ trống")]
        public string Address { get; set; }

        [StringLength(50)]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Địa chỉ không thể bỏ trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [StringLength(50)]
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không thể bỏ trống")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        public string GroupID { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
    }
}