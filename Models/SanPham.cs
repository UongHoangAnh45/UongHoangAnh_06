using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UongHoangAnh_06.Models
{
    [Table("SanPhams")]
    public class SanPham : NhaCungCap
    {
        [Key]
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
    }
}