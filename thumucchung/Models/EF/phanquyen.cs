namespace thumucchung.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("phanquyen")]
    public partial class phanquyen
    {
        [Key]
        [StringLength(50)]
        public string role { get; set; }
    }
}
