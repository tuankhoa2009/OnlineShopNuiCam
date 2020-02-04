namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public long ID { get; set; }

        public long ProductID { get; set; }

        public long OrderID { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        
        
    }
}
