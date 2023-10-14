using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace kaiDeeMak.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(255)]
        public string ProductDescription { get; set; }

        public float Price { get; set; }

        public bool InStock { get; set; }

        public DateTime ManufactureDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        [JsonIgnore]
        [InverseProperty("Product")]
        public ICollection<OrderDetils>? OrdersDetils { get; set; }


    }
}
