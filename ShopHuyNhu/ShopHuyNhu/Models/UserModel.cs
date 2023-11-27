using System.ComponentModel.DataAnnotations;

namespace ShopHuyNhu.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="ĐM quên nhập tên tài khoản à thằng kia")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "ĐM quên nhập Email à thằng kia"),EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password),Required(ErrorMessage="Thằng ngu kia nhập pass kìa")] 
        public string Password { get; set; }

		public string Role { get; set; }

	}
}
