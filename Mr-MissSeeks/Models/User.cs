namespace Mr_MissSeeks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("User")]
    public partial class User
    {
        [Column(Order = 0)]
        [StringLength(50)]
        public string phone { get; set; }


        [Column(Order = 1)]
        [StringLength(50)]
        public string name { get; set; }


        [Column(Order = 2)]
        [StringLength(50)]
        public string familyName { get; set; }


        [Column(Order = 3)]
        [StringLength(50)]
        public string password { get; set; }

        [Column(Order = 4)]
        [StringLength(50)]
        public string email { get; set; }

        [Column(Order = 5)]
        [StringLength(50)]
        public string age { get; set; }


        [Column(Order = 6)]
        [StringLength(50)]
        public string gender { get; set; }


        [Column(Order = 7)]
        [StringLength(50)]
        public string status { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 8)]
        public int Id { get; set; }


    }
}