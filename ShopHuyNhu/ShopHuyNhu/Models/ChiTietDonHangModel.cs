using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopHuyNhu.Models
{
	public class ChiTietDonHangModel
	{
		[Key]
		public int ID { get; set; }	
		public string UserName { get; set; }
        
        public string MaDatHang { get; set; }
		public long SanPhamId { get; set;}

        
        public decimal Gia { get; set; }

		public int SoLuong { get; set;}
		[ForeignKey("SanPhamId1")]
		//public SanPhamModel SanPham { get; set; }
		
		public SanPhamModel SanPham { get; set; }
        
    }
}
