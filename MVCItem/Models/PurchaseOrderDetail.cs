namespace MVCItem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("Purchasing.PurchaseOrderDetail")]
    public partial class PurchaseOrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseOrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PurchaseOrderDetailID { get; set; }

        public DateTime DueDate { get; set; }

        public short OrderQty { get; set; }

        public int ProductID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal LineTotal { get; set; }

        public decimal ReceivedQty { get; set; }

        public decimal RejectedQty { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal StockedQty { get; set; }

        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }
}
