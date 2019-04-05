namespace thumucchung.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [Key]
        [StringLength(50)]
        public string username { get; set; }

        public string password { get; set; }

        [StringLength(50)]
        public string fullName { get; set; }

        [Required]
        [StringLength(50)]
        public string phongban { get; set; }

        public bool show { get; set; }

        public bool upload { get; set; }

        [Required]
        [StringLength(3)]
        public string chinhanh { get; set; }

        public bool doimk { get; set; }

        public bool trangthai { get; set; }

        [StringLength(50)]
        public string dirPathName { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public bool adminkl { get; set; }

        public bool adminkd { get; set; }

        public System.DateTime ngaydoimk { get; set; }
    }
}
