using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace kaiDeeMak.Models
{
    public class OrderDetils
    {
        [Key]
        public int OrderDetilID { get; set; }

        [ForeignKey("OrderID")]
        public int? OrderID { get; set; }
        [JsonIgnore]
        public Orders? Order { get; set; }

        [ForeignKey("ProductID")]
        public int? ProductID { get; set; }
        [JsonIgnore]
        public Products? Product { get; set; }

        public float Total { get; set; }

        public int Quantity { get; set; }

        public int Discount { get; set; }

    }
}
