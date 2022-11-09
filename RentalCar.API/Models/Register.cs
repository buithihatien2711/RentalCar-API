using System.ComponentModel.DataAnnotations;

namespace RentalCar.API.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Bạn cần nhập tên.")]
        public string YourName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập username.")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập password.")]
        [MinLength(6, ErrorMessage = "Password phải có ít nhất 6 ký tự")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập password.")]
        [MinLength(6, ErrorMessage = "Password phải có ít nhất 6 ký tự")]
        public string ConfirmPassword { set; get; }

        [Phone]
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        public string PhoneNumber { set; get; }
    }
}