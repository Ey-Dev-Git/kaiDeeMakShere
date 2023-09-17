using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace kaiDeeMak.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        
        [ForeignKey("CustomerID")]
        public int? CustomerID { get; set; }
        [JsonIgnore]
        public Customers? Customer { get; set; }

        [ForeignKey("ProductID")]
        public int? ProductID { get; set; }
        [JsonIgnore]
        public Products? Product { get; set; }

        public DateTime OrderDate { get; set; }
        
        [MaxLength(255)]
        public string ShippingAddress { get; set; }

        public float TotalAmount { get; set; }

        public int Quantity { get; set; }

        public int Discount { get; set; }

        public bool IsDelivered { get; set; }

        public bool IsPaid { get; set; }


    }
}
