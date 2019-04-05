namespace thumucchung.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dmphong")]
    public partial class dmphong
    {
        [Key]
        [StringLength(2)]
        public string phongban { get; set; }

        [StringLength(50)]
        public string tenphongban { get; set; }
    }
}
