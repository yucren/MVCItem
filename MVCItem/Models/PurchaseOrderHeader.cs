namespace MVCItem.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Purchasing.PurchaseOrderHeader")]
    public partial class PurchaseOrderHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrderHeader()
        {
            PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
        }

        [Key]
        public int PurchaseOrderID { get; set; }

        public byte RevisionNumber { get; set; }

        public byte Status { get; set; }

        public int EmployeeID { get; set; }

        public int VendorID { get; set; }

        public int ShipMethodID { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShipDate { get; set; }

        [Column(TypeName = "money")]

        public decimal SubTotal { get; set; }
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TaxAmt { get; set; }

        [Column(TypeName = "money")]
        
        public decimal Freight { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalDue { get; set; }
        [DisplayFormat(DataFormatString = "{0:今天日期是yyyyMMdd}")]
        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        [JsonIgnore]
        public virtual ShipMethod ShipMethod { get; set; }
        [JsonIgnore]
        public virtual Vendor Vendor { get; set; }
    }
}
