using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VietMark.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        /* [Required]
         [Display(Name = "Email")]
         [EmailAddress]
         public string Email { get; set; }*/        
        [Display(Name = "Tên đăng nhập hoặc Email")]
        public string UserNameOrEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [Display(Name = "Họ và Tên")]
        public string TenKH { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, ErrorMessage = "Phải có từ {2} ký tự trở lên", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại Mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }
    public class RegisterOwnerViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Tên Cửa hàng không được để trống")]
        [Display(Name = "Tên Cửa Hàng")]
        public string TenCH { get; set; }


        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ cửa hàng")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại liên hệ")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]      
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, ErrorMessage = "Phải có từ {2} ký tự trở lên", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
