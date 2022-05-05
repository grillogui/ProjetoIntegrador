using System.ComponentModel.DataAnnotations;

namespace ProjectEcommerce.src.dtos
{
    /// <summary>
    /// <para>Resume:Implementing Methods and Constructors for the Purchase Class </para>
    /// <para>Created by: Matheus Brazolin</para>
    /// <para>version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>
    public class NewPurchaseDTO
    {
        [Required]
        [StringLength(50)]
        public string EmailBuyer { get; set; }

        [Required]
        [StringLength(50)]
        public string NameItems { get; set; }

        public NewPurchaseDTO(string emailBuyer, string nameItems)
        {
            EmailBuyer = emailBuyer;
            NameItems = nameItems;
        }



    }
}
