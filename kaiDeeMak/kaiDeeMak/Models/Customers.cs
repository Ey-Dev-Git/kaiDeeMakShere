using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace kaiDeeMak.Models
{
    public class Customers
    {
        [Key]
        public int CustomerID { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }
        
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        public bool IsVIP { get; set; }
        
        [JsonIgnore]
        [InverseProperty("Customer")]
        public ICollection<Orders>? Orders { get; set; }
    }
}
