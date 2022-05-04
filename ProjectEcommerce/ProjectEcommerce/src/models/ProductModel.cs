using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectEcommerce.src.models
{
    /// <summary>
    /// <para>Resume:Implementing Methods and Constructors for the Products Class </para>
    /// <para>Created by: Matheus Brazolin</para>
    /// <para>version: 1.0</para>
    /// <para>Date: 04/05/2022</para>
    /// </summary>

    [Table("tb_products")]
    public class ProductModel
    {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            [StringLength(50)]
            public string Name { get; set; }

            [Required]
            [StringLength(30)]
            public float Price { get; set; }

            [Required]
            [StringLength(30)]
            public string Image { get; set; }

            [Required]
            [StringLength(255)]
            public string Description { get; set; }

            [Required]
            [StringLength(30)]
            public float Quantity { get; set; }

            [JsonIgnore]
            public List<PurchaseModel> ProductsSold { get; set; }
    }
}
