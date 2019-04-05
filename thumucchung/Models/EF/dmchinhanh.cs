namespace thumucchung.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dmchinhanh")]
    public partial class dmchinhanh
    {
        [Key]
        [StringLength(3)]
        public string chinhanh { get; set; }
    }
}
