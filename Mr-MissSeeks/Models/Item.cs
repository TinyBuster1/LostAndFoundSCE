namespace Mr_MissSeeks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        [Column(Order = 4)]
        public string status { get; set; }

        [StringLength(50)]
        [Column(Order = 2)]
        public string type { get; set; }

        [StringLength(50)]
        [Column(Order = 3)]
        public string color { get; set; }

        [StringLength(50)]
        [Column(Order = 5)]
        public string location { get; set; }

        [StringLength(50)]
        public string userID { get; set; }

        [StringLength(50)]
        [Column(Order = 6)]
        public string imagePath { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        [StringLength(50)]
        [Column(Order = 1)]
        public string name { get; set; }
    }
}