using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectEcommerce.src.models
{
    /// <summary>
    /// <para>Resume:Implementing Methods and Constructors for the Purchase Class </para>
    /// <para>Created by: Matheus Brazolin</para>
    /// <para>version: 1.0</para>
    /// <para>Date: 04/05/2022</para>
    /// </summary>
     
    [Table("tb_purchases")]
    public class PurchaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("fk_user")]
        public UserModel Buyer{ get; set; }

        [ForeignKey("fk_product")]
        public ProductModel Items { get; set; }

    }
}
