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

        public DateTime OrderDate { get; set; }
        
        [MaxLength(255)]
        public string ShippingAddress { get; set; }

        public float TotalAmount { get; set; }
        public bool IsDelivered { get; set; }

        public bool IsPaid { get; set; }

        [JsonIgnore]
        [InverseProperty("Order")]
        public ICollection<OrderDetils>? OrderDetils { get; set; }


    }
}
