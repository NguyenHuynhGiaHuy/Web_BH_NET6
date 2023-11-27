using ShopHuyNhu.Repository.KtAnh;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopHuyNhu.Models
{
    public class SanPhamModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Hãy nhập tên sản phẩm")] // không đc null, MinLength(4) ít nhất 4 kí tự
        public string Ten { get; set; }
        [Required( ErrorMessage = "Hãy nhập Mô tả sản phẩm")] // không đc null, MinLength(4) ít nhất 4 kí tự
        public string MoTa { get; set; }


        
       
        public decimal Gia { get; set; }

        public int BrandId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Hãy chọn thương hiệu")]
        public int DanhMucId { get; set;}

        public DanhMucModel DanhMuc { get; set; }

        public BrandModel Brand { get; set; }
        public string Slug { get; set; }
        public string Hinh { get; set; } = "noimage.jpg";

		public int? DatHangId { get; set; }
		public DatHangModel DatHang { get; set; }
		[NotMapped]
        [FileExtension]
        public IFormFile TaiHinh { get; set; }

    }
}
