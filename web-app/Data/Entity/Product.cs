using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web_app.Data.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDesc { get; set; }

        public decimal Price { get; set; }

        public string? Category { get; set; }

        public int Stock { get; set; }

        public bool IsLive { get; set; }

        public string? Image { get; set; }

    }
}
